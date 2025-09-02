using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.MindenEgyéb;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Behajtási
    {
        #region Listák def


#pragma warning disable IDE0044
        List<string> TáblaTelephely = new List<string>();
        List<string> Szereplők = new List<string>();
        List<Adat_Behajtás_Behajtási> Adatok_Behajtás = new List<Adat_Behajtás_Behajtási>();
        List<Adat_Behajtás_Kérelemoka> Adatok_Behajtás_Kérelemoka = new List<Adat_Behajtás_Kérelemoka>();
        List<Adat_Behajtás_Engedélyezés> EmailAdatok = new List<Adat_Behajtás_Engedélyezés>();
        List<Adat_Behajtási_Engedélyek> EngedélyMátrix = new List<Adat_Behajtási_Engedélyek>();
#pragma warning restore IDE0044
        #endregion


        string Cellaelőzmény = "";


        #region Kezelők def
        readonly Kezelő_Behajtás_Alap Kéz_BehajtásAlap = new Kezelő_Behajtás_Alap();
        readonly Kezelő_Behajtás_Behajtási Kéz_Behajtás = new Kezelő_Behajtás_Behajtási();
        readonly Kezelő_Behajtás_Engedélyezés EmailKéz = new Kezelő_Behajtás_Engedélyezés();
        readonly Kezelő_Behajtás_Kérelemoka Kéz_Kérelemoka = new Kezelő_Behajtás_Kérelemoka();
        readonly Kezelő_Behajtás_Behajtási_Napló KézNapló = new Kezelő_Behajtás_Behajtási_Napló();
        readonly Kezelő_Behajtás_Telephelystátusz KézStátus = new Kezelő_Behajtás_Telephelystátusz();
        readonly Kezelő_Kiegészítő_Könyvtár KézKiegKönyvtár = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Behajtás_Dolgozótábla KézDolgozó = new Kezelő_Behajtás_Dolgozótábla();
        readonly Kezelő_Behajtás_Jogosultság KézBehajtJog = new Kezelő_Behajtás_Jogosultság();
        readonly Kezelő_Behajtás_Kérelemstátus KézKérelemStát = new Kezelő_Behajtás_Kérelemstátus();
        #endregion


        public Ablak_Behajtási()
        {
            InitializeComponent();
            Start();
        }

        #region Alap
        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                Szereplők_lista();
                Alapadatokfeltöltése();

                // GOMBOK láthstódágának korlátozása
                BtnOktatásÚj.Visible = false;
                BtnkérelemRögzítés.Visible = false;
                BtnKérelemPDF.Visible = false;
                BtnSzakszeng.Visible = false;
                Elutasít_gomb.Visible = false;
                BtnDolgozóilsta.Visible = false;
                PanelEngedély.Visible = false;
                BtnDolgozóilsta.Visible = false;
                BtnAdminOkrögzítés.Visible = false;
                BtnAdminOkfel.Visible = false;
                BtnAdminOkTöröl.Visible = false;
                BtnAdminRögz.Visible = false;
                BtnAdminÚjEngedély.Visible = false;

                if (Program.PostásTelephely == "Főmérnökség")
                {
                    // főmérnökségi funciók bekapcsolása
                    BtnOktatásÚj.Visible = true;
                    BtnkérelemRögzítés.Visible = true;
                    BtnKérelemPDF.Visible = true;
                    BtnSzakszeng.Visible = true;
                    Elutasít_gomb.Visible = true;
                    BtnDolgozóilsta.Visible = true;
                    PanelEngedély.Visible = true;
                    BtnDolgozóilsta.Visible = true;
                    BtnAdminOkrögzítés.Visible = true;
                    BtnAdminOkfel.Visible = true;
                    BtnAdminOkTöröl.Visible = true;
                    BtnAdminRögz.Visible = true;
                    BtnAdminÚjEngedély.Visible = true;
                }

                if (Program.PostásTelephely.ToUpper().Contains("VONTATÁSI TÖRZS"))
                {
                    BtnSzakszeng.Visible = true;
                    Elutasít_gomb.Visible = true;
                }

                // nem látszanak a gombok amik státus vezéreltek
                Gombok_kikapcs();

                // egyéb feltöltések
                Adminalapbeállítás();
                Adminokokfeltöltése();
                Jogosultságtípusfeltöltés();
                Kérelemalaptábla();
                CMBkérelemStátusfeltöltés();

                KérelemDátuma.Value = DateTime.Today;

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\{TxtAdminkönyvtár.Text.Trim()}\PDF".KönyvSzerk();

                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
                Engedélyek_Listázása();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AblakBehajtási_Load(object sender, EventArgs e)
        {
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
                        // Vezérlő lista
                        Szolgálatihelyfeltöltés();
                        Kérelemürítés();
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        // gondnok 
                        ListafeltöltésGondnok();
                        break;
                    }
                case 4:
                    {
                        // Szakszolgálat
                        Listafeltöltésszaksz();
                        if (Cmbtelephely.Text.ToUpper().Contains("VONTATÁSI TÖRZS")) Szakszlista();
                        LblSzakszGondnokiFelülbírálás.Text = "Gondoki engedélyezést felül lehet bírálni a szakszolgálat-vezetői engedélyezést megelőzően.\n Telephelyen beírt 2,3 státust át lehet írni a táblázatban, majd a felülbírálás gombbal kell rögzíteni.";
                        break;
                    }
                case 5:
                    {
                        // administrátor
                        Alapadatokfeltöltése();
                        Adminalapbeállítás();
                        Adminokokfeltöltése();
                        break;
                    }
            }
        }

        private void Telephelyekfeltöltése()
        {
            // COMBO amibe az adatokat feltöltjük
            Cmbtelephely.Items.Clear();
            Cmbtelephely.Enabled = false;

            List<Adat_Kiegészítő_Könyvtár> AdatokÖ = KézKiegKönyvtár.Lista_Adatok();
            List<Adat_Kiegészítő_Könyvtár> Adatok;
            if (Program.PostásTelephely.Trim() == "Főmérnökség")
            {
                Adatok = (from a in AdatokÖ
                          where a.Név != "Főmérnökség"
                          orderby a.Név
                          select a).ToList();
                // Ha főmérnökség akkor minden telephelyet feltölt
                Cmbtelephely.Enabled = true;
            }
            else
            {
                Adatok = (from a in AdatokÖ
                          where a.Csoport1 == Program.Postás_csoport
                          orderby a.Név
                          select a).ToList();
                Cmbtelephely.Enabled = Program.Postás_Vezér;
            }


            foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                Cmbtelephely.Items.Add(rekord.Név);


            // kiírjuk, hogy honnan lépett be
            if (Cmbtelephely.Text.Trim() == "")
                Cmbtelephely.Text = Program.PostásTelephely.Trim();
        }
        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                Cmbtelephely.Text = Program.PostásTelephely;
            }
            catch (HibásBevittAdat ex)
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

            // adminisztrátori gombok
            BtnAdminOkrögzítés.Enabled = false;
            BtnAdminOkfel.Enabled = false;
            BtnAdminOkTöröl.Enabled = false;
            BtnAdminRögz.Enabled = false;
            BtnDolgozóilsta.Enabled = false;
            BtnAdminÚjEngedély.Enabled = false;

            // Gondnoki/ Szakszolgálatvezetói engedély
            BtnGondnokSave.Enabled = false;
            BtnSzakszeng.Enabled = false;
            BtnEngedélySzakBírál.Enabled = false;
            Elutasít_gomb.Enabled = false;

            // Kérelem
            BtnOktatásÚj.Enabled = false;
            BtnKérelemPDF.Enabled = false;
            BtnkérelemRögzítés.Enabled = false;

            // lista
            BtnEngedélyListaGondnokEmail.Enabled = false;
            BtnEngedélyListaSzakEmail.Enabled = false;
            BtnEngedélyListaEngedélyNyomtat.Enabled = false;
            BtnEngedélyListaÁtvételNyomtat.Enabled = false;
            BtnEngedélyListaÁtvételKüld.Enabled = false;
            BtnEngedélyListaÁtvételMegtörtént.Enabled = false;
            BtnEngedélyListaTörlés.Enabled = false;


            melyikelem = 240;
            // módosítás 1 Kérelelem oka
            if (MyF.Vanjoga(melyikelem, 1))
            {
                // admin lapfül kérelemoka
                BtnAdminOkrögzítés.Enabled = true;
                BtnAdminOkfel.Enabled = true;
                BtnAdminOkTöröl.Enabled = true;
            }
            // módosítás 2 Értesítési e-mail
            if (MyF.Vanjoga(melyikelem, 2))
            {
            }
            // módosítás 3  Alapadatok módosítása
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BtnAdminRögz.Enabled = true;
                BtnAdminÚjEngedély.Enabled = true;
            }


            melyikelem = 241;
            // módosítás 1 Admin dolgozók frissítése
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnDolgozóilsta.Enabled = true;
            }
            // módosítás 2 Értesítési e-mail
            if (MyF.Vanjoga(melyikelem, 2))
            {
            }
            // módosítás 3  
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }


            melyikelem = 242;
            // módosítás 1 Gondnok/Szakszolgálati engedély
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnGondnokSave.Enabled = true;
            }
            // módosítás 2 Értesítési e-mail
            if (MyF.Vanjoga(melyikelem, 2))
            {
                BtnSzakszeng.Enabled = true;
                BtnEngedélySzakBírál.Enabled = true;
                Elutasít_gomb.Enabled = true;
            }
            // módosítás 3  
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }


            melyikelem = 243;
            // módosítás 1 új létrehozás
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnOktatásÚj.Enabled = true;
            }
            // módosítás 2 pdf feltöltés
            if (MyF.Vanjoga(melyikelem, 2))
            {
                BtnKérelemPDF.Enabled = true;
            }
            // módosítás 3  rögzítés
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BtnkérelemRögzítés.Enabled = true;
            }


            melyikelem = 244;
            // módosítás 1 e-mail értesítés küldés
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnEngedélyListaGondnokEmail.Enabled = true;
                BtnEngedélyListaSzakEmail.Enabled = true;
            }
            // módosítás 2 engedély nyomtatás
            if (MyF.Vanjoga(melyikelem, 2))
            {
                BtnEngedélyListaEngedélyNyomtat.Enabled = true;
            }
            // módosítás 3  átvételi elismervény nyomtatás
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BtnEngedélyListaÁtvételNyomtat.Enabled = true;
            }


            melyikelem = 245;
            // módosítás 1 Átvételre küldés
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnEngedélyListaÁtvételKüld.Enabled = true;
            }
            // módosítás 2 Készre jelentés
            if (MyF.Vanjoga(melyikelem, 2))
            {
                BtnEngedélyListaÁtvételMegtörtént.Enabled = true;
            }
            // módosítás 3  Törlés
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BtnEngedélyListaTörlés.Enabled = true;
            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\behajtási.html";
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
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);

            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }
        #endregion


        #region kérelem
        private void Szolgálatihelyfeltöltés()
        {
            try
            {
                List<Adat_Behajtás_Dolgozótábla> AdatokDolgÖ = KézDolgozó.Lista_Adatok();
                List<string> Adatok = AdatokDolgÖ.Select(a => a.Szervezetiegység).Distinct().ToList();
                CmbKérelemSzolgálati.Items.Clear();
                CmbKérelemSzolgálati.BeginUpdate();

                foreach (string rekord in Adatok)
                    CmbKérelemSzolgálati.Items.Add(rekord);

                CmbKérelemSzolgálati.EndUpdate();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Jogosultságtípusfeltöltés()
        {
            try
            {
                CmbKérelemTípus.Items.Clear();
                CmbKérelemTípus.BeginUpdate();
                List<Adat_Behajtás_Jogosultság> Adatok = KézBehajtJog.Lista_Adatok();
                foreach (Adat_Behajtás_Jogosultság rekord in Adatok)
                    CmbKérelemTípus.Items.Add(rekord.Státustípus);

                CmbKérelemTípus.EndUpdate();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CMBkérelemStátusfeltöltés()
        {
            try
            {
                CMBkérelemStátus.Items.Clear();
                CMBkérelemStátus.BeginUpdate();
                CmbEngedélylistaszűrő.Items.Clear();
                CmbEngedélylistaszűrő.BeginUpdate();
                CmbEngedélylistaszűrő.Items.Add("");

                List<Adat_Behajtás_Kérelemsátus> Adatok = KézKérelemStát.Lista_Adatok();
                foreach (Adat_Behajtás_Kérelemsátus rekord in Adatok)
                {
                    CMBkérelemStátus.Items.Add(rekord.Státus);
                    CmbEngedélylistaszűrő.Items.Add(rekord.Státus);
                }

                CMBkérelemStátus.EndUpdate();
                CmbEngedélylistaszűrő.EndUpdate();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnÖsszSzabiLista_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtkérelemHR.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a HR azonosító mező.");

                List<Adat_Behajtás_Dolgozótábla> Adatok = KézDolgozó.Lista_Adatok();
                Adat_Behajtás_Dolgozótábla rekord = (from a in Adatok
                                                     where a.Dolgozószám == TxtkérelemHR.Text.Trim()
                                                     select a).FirstOrDefault();

                if (rekord != null)
                {
                    // név és szervezet
                    Txtkérelemnév.Text = rekord.Dolgozónév.Trim();
                    CmbKérelemSzolgálati.Text = rekord.Szervezetiegység.Trim();
                }

                List<Adat_Behajtás_Behajtási> AdatokAlapÖ = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                List<Adat_Behajtás_Behajtási> AdatokAlap = (from a in AdatokAlapÖ
                                                            where a.HRazonosító == TxtkérelemHR.Text.Trim()
                                                            select a).ToList();

                if (AdatokAlap != null)
                    TxtKérelemautó.Text = (AdatokAlap.Count + 1).ToString();
                else
                    TxtKérelemautó.Text = "1";
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kérelemürítés()
        {
            TxtkérelemHR.Text = "";
            Txtkérelemnév.Text = "";
            CmbKérelemSzolgálati.Text = "";
            TxtKérelemFrsz.Text = "";
            CmbkérelemOka.Text = "";
            TxtKérrelemPDF.Text = "";
            TxtKérelemautó.Text = "1";
            TxtKérelemMegjegyzés.Text = "";
            CmbKérelemTípus.Text = CmbKérelemTípus.Items[0].ToString();
            DatÉrvényes.Value = DatadminÉrvényes.Value;
            Kérelemalaptábla();
        }

        private void Kérelemújraírás()
        {
            try
            {
                Kérelemürítés();

                Adatok_Behajtás = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                Adat_Behajtás_Behajtási rekord = (from a in Adatok_Behajtás
                                                  where a.Sorszám == TxtKérelemID.Text.Trim()
                                                  select a).FirstOrDefault();
                int státus = 0;
                if (rekord != null)
                {
                    TxtkérelemHR.Text = rekord.HRazonosító;
                    Txtkérelemnév.Text = rekord.Név;
                    CmbKérelemSzolgálati.Text = rekord.Szolgálatihely;
                    TxtKérelemFrsz.Text = rekord.Rendszám;
                    CmbkérelemOka.Text = rekord.OKA;

                    for (int i = 0; i < KérelemTábla.Rows.Count; i++)
                    {
                        KérelemTábla.Rows[i].Cells[2].Value = rekord.GetType().GetProperty($"{KérelemTábla.Rows[i].Cells[1].Value}_engedély").GetValue(rekord);
                        KérelemTábla.Rows[i].Cells[4].Value = rekord.GetType().GetProperty($"{KérelemTábla.Rows[i].Cells[1].Value}_megjegyzés").GetValue(rekord);
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        int cellaErtek = KérelemTábla.Rows[i].Cells[2].Value.ToÉrt_Int();
                        KérelemTábla.Rows[i].Cells[0].Value = cellaErtek != 0;
                    }

                    TxtKérrelemPDF.Text = rekord.PDF;
                    TxtKérelemautó.Text = rekord.Autók_száma.ToString();
                    TxtKérelemMegjegyzés.Text = rekord.Megjegyzés;
                    CmbKérelemTípus.Text = rekord.Korlátlan;
                    státus = rekord.Státus;
                    KérelemDátuma.Value = rekord.Dátum;
                    DatÉrvényes.Value = rekord.Érvényes;
                }

                // kiírjuk az állapotokat
                List<Adat_Behajtás_Telephelystátusz> AdatokTelep = KézStátus.Lista_Adatok();
                for (int i = 0; i <= 9; i++)
                {
                    int ideigId = int.Parse(KérelemTábla.Rows[i].Cells[2].Value.ToString());
                    string ideig = (from a in AdatokTelep
                                    where a.ID == ideigId
                                    select a.Státus).FirstOrDefault().Trim();

                    if (ideig != null) KérelemTábla.Rows[i].Cells[3].Value = ideig;
                }

                List<Adat_Behajtás_Kérelemsátus> AdatokStátus = KézKérelemStát.Lista_Adatok();
                Adat_Behajtás_Kérelemsátus rekord2 = (from a in AdatokStátus
                                                      where a.ID == státus
                                                      select a).FirstOrDefault();
                if (rekord2 != null) CMBkérelemStátus.Text = rekord2.Státus.Trim();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnkérelemRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtKérelemID.Text == "") throw new HibásBevittAdat("Töltse ki az Engedély száma mezőt!");
                if (TxtkérelemHR.Text == "") throw new HibásBevittAdat("Töltse ki a HR azonosító mezőt!");
                if (Txtkérelemnév.Text == "") throw new HibásBevittAdat("Töltse ki a Dolgozó neve mezőt!");
                if (CmbKérelemSzolgálati.Text == "") throw new HibásBevittAdat("Töltse ki a Szolgálati hely mezőt!");
                if (TxtKérelemFrsz.Text == "") throw new HibásBevittAdat("Töltse ki a Forgalmi rendszám mezőt!");
                if (CmbkérelemOka.Text == "") throw new HibásBevittAdat("Töltse ki a Kérelem oka mezőt!");
                if (KérelemDátuma.Value.Year > DatÉrvényes.Value.Year) throw new HibásBevittAdat("Nem lehet az igénylés dátuma nagyobb mint az érvényességi ideje!");
                if (!int.TryParse(TxtKérelemautó.Text.Trim(), out int autókszáma)) autókszáma = 1;

                // HA nincs még feltöltve kérelem, akkor 
                if (TxtKérrelemPDF.Text.Trim() == "") TxtKérrelemPDF.Text = "_";

                // ha nem a fájl neve a sorszám, akkor odamásoljuk
                else if (TxtKérrelemPDF.Text.Trim() != $"{TxtKérelemID.Text.Trim()}.pdf" & TxtKérrelemPDF.Text.Trim() != "_")
                {
                    string helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\{TxtAdminkönyvtár.Text.Trim()}\pdf\{TxtKérelemID.Text.Trim()}.pdf";
                    if (File.Exists(helyi))
                    {
                        if (MessageBox.Show("Ezen a néven már létezik fájl, felülírjuk?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            return;
                        else
                            File.Delete(helyi);
                    }

                    // ha nem létezik akkor odamásoljuk
                    File.Copy(TxtKérrelemPDF.Text, helyi);
                    TxtKérrelemPDF.Text = $"{TxtKérelemID.Text.Trim()}.pdf";
                }

                if (TxtKérelemMegjegyzés.Text.Trim() == "") TxtKérelemMegjegyzés.Text = "_";

                string ideigrendszám;

                // ha szóközzel van elválasztva akkor javítja és nagybetűsít
                TxtKérelemFrsz.Text = TxtKérelemFrsz.Text.Replace(" ", "").ToUpper();

                // ha nincs benne elválasztó jel akkor belerakja
                if (TxtKérelemFrsz.Text.IndexOf("-") == -1)
                {
                    if (TxtKérelemFrsz.Text.Trim().Length > 6 && TxtKérelemFrsz.Text.IndexOf("-") == -1)
                    {
                        ideigrendszám = $"{TxtKérelemFrsz.Text.Substring(0, 4)}-{TxtKérelemFrsz.Text.Substring(4)}";
                        TxtKérelemFrsz.Text = ideigrendszám;
                    }
                    else
                    {
                        ideigrendszám = $"{TxtKérelemFrsz.Text.Substring(0, 3)}-{TxtKérelemFrsz.Text.Substring(3)}";
                        TxtKérelemFrsz.Text = ideigrendszám;
                    }
                }
                List<Adat_Behajtás_Behajtási> Adatok_Behajtás_Alap = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                Adat_Behajtás_Behajtási rekord = (from a in Adatok_Behajtás_Alap
                                                  where a.Sorszám == TxtKérelemID.Text.Trim()
                                                  select a).FirstOrDefault();
                TelepiAdatokFeltöltése();

                Adat_Behajtás_Behajtási ADAT;

                if (rekord != null)
                {
                    // ha van ilyen akkor módosítunk
                    int státus = 2;
                    if (rekord.Státus <= 1) státus = 1;
                    if (EngedélyMátrix.Where(a => a.Engedély == 1).FirstOrDefault() != null) státus = 1;

                    ADAT = new Adat_Behajtás_Behajtási(TxtKérelemID.Text.Trim(),
                                            CmbKérelemSzolgálati.Text.Trim(),
                                            TxtkérelemHR.Text.Trim(),
                                            Txtkérelemnév.Text.Trim(),
                                            TxtKérelemFrsz.Text.Trim().ToUpper(),
                                            (from a in EngedélyMátrix where a.Telephely == "Angyalföld" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Angyalföld" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Baross" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Baross" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Budafok" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Budafok" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Ferencváros" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Ferencváros" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Fogaskerekű" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Fogaskerekű" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Hungária" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Hungária" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Kelenföld" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Kelenföld" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Száva" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Száva" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Szépilona" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Szépilona" select a.Megjegyzés).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Zugló" select a.Engedély).First(),
                                            (from a in EngedélyMátrix where a.Telephely == "Zugló" select a.Megjegyzés).First(),
                                            CmbKérelemTípus.Text.Trim(),
                                            autókszáma,
                                            státus,
                                            KérelemDátuma.Value,
                                            TxtKérelemMegjegyzés.Text.Trim(),
                                            TxtKérrelemPDF.Text.Trim(),
                                            CmbkérelemOka.Text.Trim(),
                                            DatÉrvényes.Value);
                    Kéz_Behajtás.Módosítás(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);
                    // ********************
                    // naplófájl rögzítés
                    // ********************
                    Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(
                                    TxtKérelemID.Text.Trim(),
                                    CmbKérelemSzolgálati.Text.Trim(),
                                    TxtkérelemHR.Text.Trim(),
                                    Txtkérelemnév.Text.Trim(),
                                    TxtKérelemFrsz.Text.Trim().ToUpper(),
                                    KérelemTábla.Rows[4].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[4].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[8].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[8].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[9].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[9].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[5].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[5].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[0].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[0].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[7].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[7].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[1].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[1].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[6].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[6].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[2].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[2].Cells[4].Value.ToStrTrim(),
                                    KérelemTábla.Rows[3].Cells[2].Value.ToÉrt_Int(),
                                    KérelemTábla.Rows[3].Cells[4].Value.ToStrTrim(),
                                    CmbKérelemTípus.Text.Trim(),
                                    autókszáma,
                                    0, 0, 0,
                                    státus,
                                    KérelemDátuma.Value,
                                    TxtKérelemMegjegyzés.Text.Trim(),
                                    TxtKérrelemPDF.Text.Trim(),
                                    CmbkérelemOka.Text.Trim(),
                                    0,
                                    Program.PostásNév.Trim(),
                                    DateTime.Now,
                                    DatÉrvényes.Value);
                    KézNapló.Rögzítés(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);
                }

                else
                {
                    ADAT = new Adat_Behajtás_Behajtási(TxtKérelemID.Text.Trim(),
                                                       CmbKérelemSzolgálati.Text.Trim(),
                                                       TxtkérelemHR.Text.Trim(),
                                                       Txtkérelemnév.Text.Trim(),
                                                       TxtKérelemFrsz.Text.Trim().ToUpper(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Angyalföld" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Angyalföld" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Baross" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Baross" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Budafok" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Budafok" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Ferencváros" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Ferencváros" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Fogaskerekű" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Fogaskerekű" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Hungária" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Hungária" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Kelenföld" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Kelenföld" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Száva" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Száva" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Szépilona" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Szépilona" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Zugló" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Zugló" select a.Megjegyzés).First(),
                                                       CmbKérelemTípus.Text.Trim(),
                                                       autókszáma,
                                                       0, 0, 0, 1,
                                                       KérelemDátuma.Value,
                                                       TxtKérelemMegjegyzés.Text.Trim(),
                                                       TxtKérrelemPDF.Text.Trim(),
                                                       CmbkérelemOka.Text.Trim(),
                                                       DatÉrvényes.Value);
                    Kéz_Behajtás.Rögzítés(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);

                    Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(
                                                       TxtKérelemID.Text.Trim(),
                                                       CmbKérelemSzolgálati.Text.Trim(),
                                                       TxtkérelemHR.Text.Trim(),
                                                       Txtkérelemnév.Text.Trim(),
                                                       TxtKérelemFrsz.Text.Trim().ToUpper(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Angyalföld" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Angyalföld" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Baross" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Baross" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Budafok" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Budafok" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Ferencváros" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Ferencváros" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Fogaskerekű" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Fogaskerekű" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Hungária" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Hungária" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Kelenföld" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Kelenföld" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Száva" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Száva" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Szépilona" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Szépilona" select a.Megjegyzés).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Zugló" select a.Engedély).First(),
                                                       (from a in EngedélyMátrix where a.Telephely == "Zugló" select a.Megjegyzés).First(),
                                                       CmbKérelemTípus.Text.Trim(),
                                                       autókszáma,
                                                       0, 0, 0, 1,
                                                       KérelemDátuma.Value,
                                                       TxtKérelemMegjegyzés.Text.Trim(),
                                                       TxtKérrelemPDF.Text.Trim(),
                                                       CmbkérelemOka.Text.Trim(),
                                                       0,
                                                       Program.PostásNév.Trim(),
                                                       DateTime.Now,
                                                       DatÉrvényes.Value);

                    // ********************
                    // naplófájl rögzítés
                    // ********************
                    KézNapló.Rögzítés(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);
                }
                Kérelemújraírás();
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

        private void TelepiAdatokFeltöltése()
        {
            EngedélyMátrix.Clear();
            for (int i = 0; i < KérelemTábla.Rows.Count; i++)
            {
                string telephely = KérelemTábla.Rows[i].Cells[1].Value.ToString();

                if (!int.TryParse(KérelemTábla.Rows[i].Cells[2].Value.ToString(), out int engedély)) engedély = 0;
                if (bool.Parse(KérelemTábla.Rows[i].Cells[0].Value.ToString()) && engedély == 0) engedély = bool.Parse(KérelemTábla.Rows[i].Cells[0].Value.ToString()) ? 1 : 0;
                string megjegyzés = KérelemTábla.Rows[i].Cells[4].Value.ToString();
                EngedélyMátrix.Add(new Adat_Behajtási_Engedélyek(telephely, engedély, megjegyzés));
            }
        }

        private void BtnOktatásÚj_Click(object sender, EventArgs e)
        {
            try
            {
                Kérelemürítés();

                long szám;
                string betű = TxtadminBetű.Text.Trim();

                List<Adat_Behajtás_Behajtási> Adatok = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                Adat_Behajtás_Behajtási rekord = (from a in Adatok
                                                  orderby a.Sorszám descending
                                                  select a).FirstOrDefault();
                if (rekord != null)
                    szám = long.Parse(rekord.Sorszám.Substring(betű.Length)) + 1;
                else
                    szám = 1;

                TxtKérelemID.Text = betű + Bővít("0", szám, 4);
                CMBkérelemStátus.Text = CMBkérelemStátus.Items[0].ToString();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string Bővít(string a, long b, int darab)
        {
            int HiányzóKarakterekSzáma = darab - b.ToString().Length;
            string c = "";
            if (HiányzóKarakterekSzáma > 1)
            {
                for (int i = 0; i < HiányzóKarakterekSzáma; i++)
                    c += a;
                c += b.ToString();
            }
            else
            {
                c = b.ToString();
            }
            return c;
        }

        private void Kérelemalaptábla()
        {
            KérelemTábla.Columns[0].HeaderText = "";
            KérelemTábla.Columns[0].Width = 45;
            KérelemTábla.Columns[1].HeaderText = "Telephely";
            KérelemTábla.Columns[1].Width = 200;
            KérelemTábla.Columns[2].HeaderText = "Állapota";
            KérelemTábla.Columns[2].Width = 150;
            KérelemTábla.Columns[3].HeaderText = "Állapota";
            KérelemTábla.Columns[3].Width = 150;
            KérelemTábla.Columns[4].HeaderText = "Megjegyzés";
            KérelemTábla.Columns[4].Width = 1000;

            KérelemTábla.ColumnCount = 5;
            KérelemTábla.RowCount = 10;

            List<Adat_Behajtás_Engedélyezés> AdatokÖ = EmailKéz.Lista_Adatok();
            List<Adat_Behajtás_Engedélyezés> Adatok = (from a in AdatokÖ
                                                       where a.Gondnok == true
                                                       && a.Szakszolgálat == false
                                                       orderby a.Szakszolgálatszöveg, a.Id
                                                       select a).ToList();
            for (int sor = 0; sor < Adatok.Count; sor++)
            {
                KérelemTábla.Rows[sor].Cells[1].Value = Adatok[sor].Telephely;
                KérelemTábla.Rows[sor].Cells[0].Value = false;
                KérelemTábla.Rows[sor].Cells[2].Value = "";
                KérelemTábla.Rows[sor].Cells[3].Value = "";
                KérelemTábla.Rows[sor].Cells[4].Value = "";
            }

            TáblaTelephely.Clear();
            //A táblázatból feltöltjük a telephelyeket egy listába
            foreach (DataGridViewRow row in KérelemTábla.Rows)
            {
                if (row.Cells[1].Value != null)
                    TáblaTelephely.Add(row.Cells[1].Value.ToString());
            }
        }

        private void Btn1szak_Click(object sender, EventArgs e)
        {
            KérelemTábla.Rows[0].Cells[0].Value = true;
            KérelemTábla.Rows[1].Cells[0].Value = true;
            KérelemTábla.Rows[2].Cells[0].Value = true;
        }

        private void Btn2szak_Click(object sender, EventArgs e)
        {
            KérelemTábla.Rows[3].Cells[0].Value = true;
            KérelemTábla.Rows[4].Cells[0].Value = true;
            KérelemTábla.Rows[5].Cells[0].Value = true;
            KérelemTábla.Rows[6].Cells[0].Value = true;
        }

        private void Btn3szak_Click(object sender, EventArgs e)
        {
            KérelemTábla.Rows[7].Cells[0].Value = true;
            KérelemTábla.Rows[8].Cells[0].Value = true;
            KérelemTábla.Rows[9].Cells[0].Value = true;
        }

        private void BtnKijelölcsop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 9; i++)
                KérelemTábla.Rows[i].Cells[0].Value = true;
        }

        private void Btnkilelöltörlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 9; i++)
                KérelemTábla.Rows[i].Cells[0].Value = false;
        }

        private void BtnKérelemPDF_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
                TxtKérrelemPDF.Text = "";
                OpenFileDialog1.Filter = "PDF Files |*.pdf";

                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Kezelő_Pdf.PdfMegnyitás(PDF_néző, OpenFileDialog1.FileName);

                    TxtKérrelemPDF.Text = OpenFileDialog1.FileName;
                    Fülek.SelectedIndex = 2;
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

        private void KérelemTábla_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                if (KérelemTábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Bool() == false)
                    KérelemTábla.Rows[e.RowIndex].Cells[2].Value = 0;
            }
        }
        #endregion


        #region Lista
        private void BtnEngedélyListaFrissít_Click(object sender, EventArgs e)
        {
            LISTAlista();
        }

        private void LISTAlista()
        {
            try
            {
                Engedélyek_Listázása();
                TáblaLista.Rows.Clear();
                TáblaLista.Columns.Clear();
                TáblaLista.Refresh();
                TáblaLista.ColumnCount = 22;
                TáblaLista.RowCount = 0;

                TáblaLista.Columns[0].HeaderText = "Engedély száma";
                TáblaLista.Columns[0].Width = 100;
                TáblaLista.Columns[1].HeaderText = "Név";
                TáblaLista.Columns[1].Width = 200;
                TáblaLista.Columns[2].HeaderText = "HR azonosító";
                TáblaLista.Columns[2].Width = 100;
                TáblaLista.Columns[3].HeaderText = "Besorolás";
                TáblaLista.Columns[3].Width = 150;
                TáblaLista.Columns[4].HeaderText = "Dátum";
                TáblaLista.Columns[4].Width = 100;
                TáblaLista.Columns[5].HeaderText = "Rendszám";
                TáblaLista.Columns[5].Width = 100;
                TáblaLista.Columns[6].HeaderText = "Hungária";
                TáblaLista.Columns[6].Width = 80;
                TáblaLista.Columns[7].HeaderText = "Száva";
                TáblaLista.Columns[7].Width = 80;
                TáblaLista.Columns[8].HeaderText = "Zugló";
                TáblaLista.Columns[8].Width = 80;
                TáblaLista.Columns[9].HeaderText = "I eng.";
                TáblaLista.Columns[9].Width = 60;
                TáblaLista.Columns[10].HeaderText = "Angyalföld";
                TáblaLista.Columns[10].Width = 100;
                TáblaLista.Columns[11].HeaderText = "Baross";
                TáblaLista.Columns[11].Width = 80;
                TáblaLista.Columns[12].HeaderText = "Fogaskerekű";
                TáblaLista.Columns[12].Width = 110;
                TáblaLista.Columns[13].HeaderText = "Szépilona";
                TáblaLista.Columns[13].Width = 80;
                TáblaLista.Columns[14].HeaderText = "II eng.";
                TáblaLista.Columns[14].Width = 60;
                TáblaLista.Columns[15].HeaderText = "Kelenföld";
                TáblaLista.Columns[15].Width = 80;
                TáblaLista.Columns[16].HeaderText = "Budafok";
                TáblaLista.Columns[16].Width = 80;
                TáblaLista.Columns[17].HeaderText = "Ferencváros";
                TáblaLista.Columns[17].Width = 100;
                TáblaLista.Columns[18].HeaderText = "III eng.";
                TáblaLista.Columns[18].Width = 60;
                TáblaLista.Columns[19].HeaderText = "Státus";
                TáblaLista.Columns[19].Width = 80;
                TáblaLista.Columns[20].HeaderText = "Dolgozó szolgálati helye";
                TáblaLista.Columns[20].Width = 400;
                TáblaLista.Columns[21].HeaderText = "Pdf";
                TáblaLista.Columns[21].Width = 400;

                if (Nézet_Egyszerű.Checked)
                {
                    TáblaLista.Columns[6].Visible = false;
                    TáblaLista.Columns[7].Visible = false;
                    TáblaLista.Columns[8].Visible = false;

                    TáblaLista.Columns[10].Visible = false;
                    TáblaLista.Columns[11].Visible = false;
                    TáblaLista.Columns[12].Visible = false;
                    TáblaLista.Columns[13].Visible = false;

                    TáblaLista.Columns[15].Visible = false;
                    TáblaLista.Columns[16].Visible = false;
                    TáblaLista.Columns[17].Visible = false;
                }
                else
                {
                    TáblaLista.Columns[6].Visible = true;
                    TáblaLista.Columns[7].Visible = true;
                    TáblaLista.Columns[8].Visible = true;

                    TáblaLista.Columns[10].Visible = true;
                    TáblaLista.Columns[11].Visible = true;
                    TáblaLista.Columns[12].Visible = true;
                    TáblaLista.Columns[13].Visible = true;

                    TáblaLista.Columns[15].Visible = true;
                    TáblaLista.Columns[16].Visible = true;
                    TáblaLista.Columns[17].Visible = true;
                }

                int státus = 0;
                List<Adat_Behajtás_Kérelemsátus> AdatokStát = KézKérelemStát.Lista_Adatok();
                Adat_Behajtás_Kérelemsátus rekord2 = (from a in AdatokStát
                                                      where a.Státus == CmbEngedélylistaszűrő.Text.Trim()
                                                      select a).FirstOrDefault();
                if (rekord2 != null) státus = rekord2.ID;


                List<Adat_Behajtás_Behajtási> Adatok = null;
                if (CmbEngedélylistaszűrő.SelectedIndex <= 0)
                {
                    if (Txtnévszűrő.Text.Trim() == "")
                    {
                        if (TxtRendszámszűrő.Text.Trim() == "")
                        {
                            Adatok = Adatok_Behajtás;
                        }
                        else
                        {
                            Adatok = (from a in Adatok_Behajtás
                                      where a.Rendszám.Contains(TxtRendszámszűrő.Text.Trim().ToUpper())
                                      orderby a.Sorszám
                                      select a).ToList();
                        }
                    }
                    else
                    {
                        if (TxtRendszámszűrő.Text.Trim() == "")
                        {
                            Adatok = (from a in Adatok_Behajtás
                                      where a.Név.Contains(Txtnévszűrő.Text.Trim())
                                      orderby a.Sorszám
                                      select a).ToList();
                        }
                        else
                        {
                            Adatok = (from a in Adatok_Behajtás
                                      where a.Név.Contains(Txtnévszűrő.Text.Trim()) && a.Rendszám.Contains(TxtRendszámszűrő.Text.Trim().ToUpper())
                                      orderby a.Sorszám
                                      select a).ToList();
                        }
                    }
                }
                else
                {
                    if (Txtnévszűrő.Text.Trim() == "")
                    {
                        if (TxtRendszámszűrő.Text.Trim() == "")
                        {
                            Adatok = (from a in Adatok_Behajtás
                                      where a.Státus == státus
                                      orderby a.Sorszám
                                      select a).ToList();
                        }
                        else
                        {
                            Adatok = (from a in Adatok_Behajtás
                                      where a.Státus == státus && a.Rendszám.Contains(TxtRendszámszűrő.Text.Trim().ToUpper())
                                      orderby a.Sorszám
                                      select a).ToList();
                        }
                    }
                    else
                    {
                        if (TxtRendszámszűrő.Text.Trim() == "")
                        {
                            Adatok = (from a in Adatok_Behajtás
                                      where a.Státus == státus && a.Név.Contains(Txtnévszűrő.Text.Trim())
                                      orderby a.Sorszám
                                      select a).ToList();
                        }
                        else
                        {
                            Adatok = (from a in Adatok_Behajtás
                                      where a.Státus == státus && a.Név.Contains(Txtnévszűrő.Text.Trim()) && a.Rendszám.Contains(TxtRendszámszűrő.Text.Trim().ToUpper())
                                      orderby a.Sorszám
                                      select a).ToList();
                        }
                    }
                }


                foreach (Adat_Behajtás_Behajtási rekord in Adatok)
                {
                    TáblaLista.RowCount++;
                    int i = TáblaLista.RowCount - 1;
                    TáblaLista.Rows[i].Cells[0].Value = rekord.Sorszám.Trim();
                    TáblaLista.Rows[i].Cells[1].Value = rekord.Név.Trim();
                    TáblaLista.Rows[i].Cells[2].Value = rekord.HRazonosító.Trim();
                    TáblaLista.Rows[i].Cells[3].Value = rekord.Korlátlan.Trim();
                    TáblaLista.Rows[i].Cells[4].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    TáblaLista.Rows[i].Cells[5].Value = rekord.Rendszám.Trim();
                    TáblaLista.Rows[i].Cells[6].Value = rekord.Hungária_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[7].Value = rekord.Száva_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[8].Value = rekord.Zugló_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[9].Value = rekord.I_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[10].Value = rekord.Angyalföld_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[11].Value = rekord.Baross_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[12].Value = rekord.Fogaskerekű_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[13].Value = rekord.Szépilona_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[14].Value = rekord.II_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[15].Value = rekord.Kelenföld_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[16].Value = rekord.Budafok_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[17].Value = rekord.Ferencváros_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[18].Value = rekord.III_engedély.ToString().Trim();
                    TáblaLista.Rows[i].Cells[19].Value = rekord.Státus.ToString().Trim();
                    TáblaLista.Rows[i].Cells[20].Value = rekord.Szolgálatihely.Trim();
                    TáblaLista.Rows[i].Cells[21].Value = rekord.PDF.Trim();
                }
                TáblaLista_Szinez();
                TáblaLista.Refresh();
                TáblaLista.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TáblaLista_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (TáblaLista.SelectedRows.Count != 0)
                {
                    TxtKérelemID.Text = TáblaLista.Rows[TáblaLista.SelectedRows[0].Index].Cells[0].Value.ToStrTrim();
                    TextNaplósorszám.Text = TáblaLista.Rows[TáblaLista.SelectedRows[0].Index].Cells[0].Value.ToStrTrim();
                    Kérelemújraírás();
                    if (TxtKérrelemPDF.Text.Trim() != "_")
                    {
                        string helypdf = TxtKérrelemPDF.Text.Trim();
                        PDF_Megjelenítés(helypdf);
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

        private void TáblaLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (Táblaszaksz.Rows.Count <= 1)
                {
                    TxtKérelemID.Text = TáblaLista.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    TextNaplósorszám.Text = TáblaLista.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    Kérelemújraírás();

                    if (TxtKérrelemPDF.Text.Trim() != "_")
                    {
                        string helypdf = TxtKérrelemPDF.Text.Trim();

                        PDF_Megjelenítés(helypdf);
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

        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaLista.Rows.Count <= 0) throw new HibásBevittAdat("Nincsenek sorok a táblázatban!");

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Behajtási_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                // JAVÍTANDÓ: Mivel nem látszik a teljes táblázat, hogy hozzon hibát minden adatot listázunk.
                Nézet_Egyszerű.Checked = false;
                LISTAlista();

                MyE.DataGridViewToExcel(fájlexc, TáblaLista);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Gombok_kikapcs()
        {
            BtnEngedélyListaEngedélyNyomtat.Visible = false;
            BtnEngedélyListaGondnokEmail.Visible = false;
            BtnEngedélyListaSzakEmail.Visible = false;
            BtnEngedélyListaÁtvételNyomtat.Visible = false;
            BtnEngedélyListaÁtvételKüld.Visible = false;
            BtnEngedélyListaÁtvételMegtörtént.Visible = false;
            BtnEngedélyListaTörlés.Visible = false;

        }

        private void CmbEngedélylistaszűrő_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gombok_kikapcs();

            switch (CmbEngedélylistaszűrő.SelectedIndex)
            {
                case 0:

                    break;
                case 1:
                    // engedélyezésre vár
                    BtnEngedélyListaGondnokEmail.Visible = true;
                    BtnEngedélyListaSzakEmail.Visible = true;
                    BtnEngedélyListaTörlés.Visible = true;
                    break;

                case 2:
                    // nyomtatás engedélyezve
                    BtnEngedélyListaEngedélyNyomtat.Visible = true;
                    BtnEngedélyListaTörlés.Visible = true;
                    break;

                case 3:
                    // feldolgozás alatt
                    BtnEngedélyListaEngedélyNyomtat.Visible = true;
                    BtnEngedélyListaÁtvételNyomtat.Visible = true;
                    BtnEngedélyListaÁtvételKüld.Visible = true;
                    BtnEngedélyListaTörlés.Visible = true;
                    break;

                case 4:
                    // elküldött
                    BtnEngedélyListaEngedélyNyomtat.Visible = true;
                    BtnEngedélyListaÁtvételMegtörtént.Visible = true;
                    BtnEngedélyListaTörlés.Visible = true;
                    break;

                case 5:
                    // kész
                    BtnEngedélyListaEngedélyNyomtat.Visible = true;
                    BtnEngedélyListaTörlés.Visible = true;
                    break;
            }

            LISTAlista();
        }

        private void BtnEngedélyListaEngedélyNyomtat_Click(object sender, EventArgs e)
        {
            Holtart.Be();
            try
            {
                Engedélyek_Listázása();
                // nyomtatjuk az engedélyeket
                string helyexcel = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Behajtási_engedély.xlsx";

                // ha nincs meg a fájl akkor kilép
                if (!File.Exists(helyexcel)) throw new HibásBevittAdat("Hiányzik az kitöltendő táblázat!");
                if (TáblaLista.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincsenek sorok a táblázatban!");

                int j = 1;
                // megnyitjuk az excel táblát
                MyE.ExcelMegnyitás(helyexcel);

                int k = 0;
                int l = 0;
                string eredmény;

                for (int i = 0; i < TáblaLista.SelectedRows.Count; i++)
                {
                    Holtart.Lép();
                    MyE.Munkalap_aktív("Adatok");

                    j++;
                    k = 0;

                    Adat_Behajtás_Behajtási Elem = (from a in Adatok_Behajtás
                                                    where a.Sorszám == TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim()
                                                    select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        MyE.Kiir($"{TáblaLista.SelectedRows[i].Cells[0].Value}".Trim(), $"b{j}");
                        if (Elem.Korlátlan == "Vezetői")
                        {
                            MyE.Kiir("Vezetői behajtási engedély".ToUpper(), $"c{j}");
                            MyE.Kiir("    ", $"e{j}");
                        }
                        else if (Elem.Korlátlan == "Normál")
                        {
                            MyE.Kiir("behajtási engedély".ToUpper(), $"c{j}");
                            MyE.Kiir("Szabad parkoló esetén", $"e{j}");
                        }
                        else
                        {
                            MyE.Kiir("Parkolási engedély".ToUpper(), $"c{j}");
                            MyE.Kiir(Elem.Korlátlan, $"e{j}");
                        }
                        MyE.Kiir(Elem.Név, $"f{j}");
                        MyE.Kiir(Elem.Rendszám, $"g{j}");
                        MyE.Kiir($"Érvényes: {Elem.Érvényes:yyyy.MM.dd}", $"h{j}");
                        eredmény = "";

                        for (l = 6; l <= 17; l++)
                        {
                            if (l == 9 | l == 14)
                            {
                            }
                            else if (TáblaLista.SelectedRows[i].Cells[l].Value?.ToString()?.Trim() == "2")
                            {
                                if (k >= 1)
                                    eredmény += ", ";
                                eredmény += TáblaLista.Columns[l].HeaderText;
                                k++;
                            }
                        }

                        if (k == 10)
                        {
                            k = 0;
                            eredmény = "Összes";
                        }

                        MyE.Kiir(eredmény, $"d{j}");

                        if (k == 0)
                            MyE.Kiir("üzem területére", $"i{j}");

                        else if (k == 1)
                            MyE.Kiir("üzem területére", $"i{j}");

                        else
                            MyE.Kiir("üzemek területére", $"i{j}");


                        // Módosítjuk a kérelem státusát
                        Adat_Behajtás_Behajtási ADAT = new Adat_Behajtás_Behajtási(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 3);
                        Kéz_Behajtás.Módosítás_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);

                        Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 3, 0, Program.PostásNév.Trim(), DateTime.Now);
                        KézNapló.Rögzítés_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);

                        // ha a negyedikhez érünk akkor nyomtatunk egyet.
                        if (j == 5)
                        {
                            MyE.Munkalap_aktív("Engedély");
                            MyE.Nyomtatás("Engedély", 1, 1);

                            j = 1;
                            MyE.Munkalap_aktív("Adatok");
                        }
                    }
                }
                if (j != 1)
                {
                    MyE.Munkalap_aktív("Engedély");
                    MyE.Nyomtatás("Engedély", 1, 1);

                    MyE.Munkalap_aktív("Adatok");
                }
                // az excel tábla bezárása
                MyE.ExcelBezárás();
                LISTAlista();
                MessageBox.Show("Az engedélyek nyomtatása megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Holtart.Ki();
        }

        private void BtnEngedélyListaGondnokEmail_Click(object sender, EventArgs e)
        {
            Gondnoki();
        }

        private void BtnEngedélyListaSzakEmail_Click(object sender, EventArgs e)
        {
            Szakszolgálati();
        }

        private void Gondnoki()
        {
            try
            {
                Holtart.Be();
                string címzett;
                string tárgy;
                string tartalom;
                int ii;
                Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail;
                string Tábla_html;

                for (ii = 0; ii < Cmbtelephely.Items.Count; ii++)
                {
                    Holtart.Lép();
                    mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                    mail.HTMLBody = "";

                    Cmbtelephely.Text = Cmbtelephely.Items[ii].ToString();
                    Gondnoklista();
                    // ha a gondnoki tábla eredménye hogy van eleme, akkor küldünk e-mailt
                    címzett = "";
                    tárgy = "";
                    tartalom = "";

                    EmailAdatok_Feltöltése();

                    címzett = (from a in EmailAdatok
                               where a.Telephely == Cmbtelephely.Text
                               select a.Emailcím).FirstOrDefault();
                    if (címzett == null)
                        címzett = "";

                    tárgy = $"Behajtási engedély engedélyezése {DateTime.Now:yyyyMMdd}";
                    mail.HTMLBody = "<html><body> <p> ";
                    tartalom = $"{Cmbtelephely.Text} telephely vonatkozásában {Táblagondnok.Rows.Count} darab engedélyezési feladata van a Villamos programban.</p><br><br>";

                    // Table start.
                    // Adding fejléc.
                    Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";
                    foreach (DataGridViewColumn column in Táblagondnok.Columns)
                        Tábla_html += $"<th style='background-color: #B8DBFD;border: 1px solid #ccc'>{column.HeaderText}</th>";
                    Tábla_html += "</tr>";
                    // Adding adatsorok.
                    foreach (DataGridViewRow row in Táblagondnok.Rows)
                    {
                        Tábla_html += "<tr>";

                        foreach (DataGridViewCell cell in row.Cells)
                            Tábla_html += $"<td style='width:120px;border: 1px solid #ccc'>{cell.Value}</td>";

                        Tábla_html += "</tr>";
                    }
                    Tábla_html += "</table> <br>";
                    // Table end.
                    tartalom += Tábla_html;

                    mail.HTMLBody += tartalom;
                    mail.HTMLBody += "<p> Ezt az e-mailt a Villamos program generálta. </p>";
                    mail.HTMLBody += "</body></html>  ";

                    if (Táblagondnok.Rows.Count > 0 & címzett.Trim() != "")
                    {
                        // üzenet címzettje
                        mail.To = címzett;
                        // üzent szövege

                        // üzenet tárgya
                        mail.Subject = tárgy;
                        mail.Send();
                        MessageBox.Show($"Üzenet el lett küldve a {Cmbtelephely.Text} gondnokának.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Holtart.Ki();
        }

        private void Szakszolgálati()
        {
            Holtart.Be();
            try
            {
                string címzett;
                string tárgy;
                string tartalom;

                Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail;
                string Tábla_html;

                for (int ii = 0; ii < Cmbtelephely.Items.Count; ii++)
                {
                    Holtart.Lép();
                    mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                    mail.HTMLBody = "";

                    Cmbtelephely.Text = Cmbtelephely.Items[ii].ToString();
                    Szakszlista();
                    // ha a szakszolgálati tábla eredménye hogy van eleme, akkor küldünk e-mailt
                    címzett = "";
                    tárgy = "";
                    tartalom = "";

                    EmailAdatok_Feltöltése();

                    címzett = (from a in EmailAdatok
                               where a.Telephely == Cmbtelephely.Text
                               select a.Emailcím).FirstOrDefault();
                    if (címzett == null)
                        címzett = "";

                    tárgy = $"Behajtási engedély engedélyezése {DateTime.Now:yyyyMMdd}";
                    mail.HTMLBody = "<html><body> <p> ";
                    tartalom = $"{Cmbtelephely.Text} telephely vonatkozásában {Táblaszaksz.Rows.Count} darab engedélyezési feladata vannak a Villamos programban.</p><br><br>";
                    // Table start.
                    // Adding fejléc.
                    Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";
                    foreach (DataGridViewColumn column in Táblaszaksz.Columns)
                        Tábla_html += $"<th style='background-color: #B8DBFD;border: 1px solid #ccc'>{column.HeaderText}</th>";
                    Tábla_html += "</tr>";
                    // Adding adatsorok.
                    foreach (DataGridViewRow row in Táblaszaksz.Rows)
                    {
                        Tábla_html += "<tr>";

                        foreach (DataGridViewCell cell in row.Cells)
                            Tábla_html += $"<td style='width:120px;border: 1px solid #ccc'>{cell.Value}</td>";

                        Tábla_html += "</tr>";
                    }
                    Tábla_html += "</table> <br>";
                    // Table end.
                    tartalom += Tábla_html;


                    mail.HTMLBody += tartalom;
                    mail.HTMLBody += "<p> Ezt az e-mailt a Villamos program generálta. </p>";
                    mail.HTMLBody += "</body></html>  ";

                    if (Táblaszaksz.Rows.Count > 0 && címzett.Trim() != "")
                    {
                        // üzenet címzettje
                        mail.To = címzett;
                        // üzent szövege

                        // üzenet tárgya
                        mail.Subject = tárgy;
                        mail.Send();
                        MessageBox.Show($"Üzenet el lett küldve a {Cmbtelephely.Text} vezetőjének.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Holtart.Ki();
        }

        private void BtnEngedélyListaÁtvételNyomtat_Click(object sender, EventArgs e)
        {
            Holtart.Be();
            try
            {
                // kinyomtatjuk az átvételi elismervényeket
                string helyexcel = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Behajtási_engedély.xlsx";

                // ha nincs meg a fájl akkor kilép
                if (!File.Exists(helyexcel)) throw new HibásBevittAdat("Hiányzik az kitöltendő táblázat!");
                if (TáblaLista.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincsen kijelölve sor!");

                int j = 1;

                // megnyitjuk az excel táblát
                MyE.ExcelMegnyitás(helyexcel);

                int k = 0;
                int l = 0;
                string eredmény;
                Adatok_Behajtás = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());

                for (int i = 0; i < TáblaLista.SelectedRows.Count; i++)
                {
                    Holtart.Lép();
                    MyE.Munkalap_aktív("Adatok");

                    j++;
                    k = 0;

                    Adat_Behajtás_Behajtási rekord = (from a in Adatok_Behajtás
                                                      where a.Sorszám == TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim()
                                                      select a).FirstOrDefault();
                    MyE.Kiir(TáblaLista.SelectedRows[i].Cells[0].Value.ToString().Trim(), $"b{j}");


                    MyE.Kiir(TáblaLista.SelectedRows[i].Cells[0].Value.ToString().Trim(), $"b{j}");
                    if (rekord.Korlátlan == "Vezetői")
                    {
                        MyE.Kiir("Vezetői behajtási engedély".ToUpper(), $"c{j}");
                        MyE.Kiir("    ", $"e{j}");
                    }
                    else if (rekord.Korlátlan == "Normál")
                    {
                        MyE.Kiir("behajtási engedély".ToUpper(), $"c{j}");
                        MyE.Kiir("Szabad parkoló esetén", $"e{j}");
                    }
                    else
                    {
                        MyE.Kiir("Parkolási engedély".ToUpper(), $"c{j}");
                        MyE.Kiir(rekord.Korlátlan, $"e{j}");
                    }
                    MyE.Kiir(rekord.Név, $"f{j}");
                    MyE.Kiir(rekord.Rendszám, $"g{j}");
                    MyE.Kiir($"Érvényes: {rekord.Érvényes:yyyy.MM.dd}", $"h{j}");

                    eredmény = "";
                    for (l = 6; l <= 17; l++)
                    {
                        if (l == 9 | l == 14)
                        {
                        }
                        else if (TáblaLista.SelectedRows[i].Cells[l].Value.ToString().Trim() == "2")
                        {
                            if (k >= 1)
                                eredmény += ", ";
                            eredmény += TáblaLista.Columns[l].HeaderText;
                            k++;
                        }
                    }
                    if (k == 10)
                    {
                        k = 0;
                        eredmény = "Összes";
                    }
                    MyE.Kiir(eredmény, $"d{j}");

                    if (k == 0)
                        MyE.Kiir("üzem területére", $"i{j}");

                    else if (k == 1)
                        MyE.Kiir("üzem területére", $"i{j}");

                    else
                        MyE.Kiir("üzemek területére", $"i{j}");

                    // ha a negyedikhez érünk akkor nyomtatunk egyet.
                    if (j == 5)
                    {
                        MyE.Munkalap_aktív("átvételi_lap");
                        MyE.Nyomtatás("átvételi_lap", 1, 1);
                        j = 1;
                        MyE.Munkalap_aktív("Adatok");
                    }
                }
                if (j != 1)
                {
                    MyE.Munkalap_aktív("átvételi_lap");
                    MyE.Nyomtatás("átvételi_lap", 1, 1);

                    MyE.Munkalap_aktív("Adatok");
                }
                // az excel tábla bezárása
                MyE.ExcelBezárás();

                LISTAlista();
                MessageBox.Show("Az átvételi lapok nyomtatása megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Holtart.Ki();
        }

        private void BtnEngedélyListaÁtvételKüld_Click(object sender, EventArgs e)
        {
            Holtart.Be();
            try
            {
                // elküldjük átvételre
                if (TáblaLista.SelectedRows.Count < 1) MessageBox.Show("Nincsen kijelölve sor!");

                for (int i = 0; i < TáblaLista.SelectedRows.Count; i++)
                {
                    Holtart.Lép();
                    // Módosítjuk a kérelem státusát
                    Adat_Behajtás_Behajtási ADAT = new Adat_Behajtás_Behajtási(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 4);
                    Kéz_Behajtás.Módosítás_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);

                    Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 4, 0, Program.PostásNév.Trim(), DateTime.Now);
                    KézNapló.Rögzítés_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);
                }
                LISTAlista();

                MessageBox.Show("Az engedélyek státus állítása sikeresen megtörtént Átvételre küldvére.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Holtart.Ki();
        }

        private void BtnEngedélyListaÁtvételMegtörtént_Click(object sender, EventArgs e)
        {
            Holtart.Be();
            try
            {
                // kész
                if (TáblaLista.SelectedRows.Count < 1) MessageBox.Show("Nincsen kijelölve sor!");

                for (int i = 0; i < TáblaLista.SelectedRows.Count; i++)
                {
                    Holtart.Lép();
                    // Módosítjuk a kérelem státusát
                    Adat_Behajtás_Behajtási ADAT = new Adat_Behajtás_Behajtási(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 5);
                    Kéz_Behajtás.Módosítás_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);

                    Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 5, 0, Program.PostásNév.Trim(), DateTime.Now);
                    KézNapló.Rögzítés_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);
                }
                LISTAlista();

                MessageBox.Show("Az engedélyek státus állítása sikeresen megtörtént Készre.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Holtart.Ki();
        }

        private void BtnEngedélyListaTörlés_Click(object sender, EventArgs e)
        {
            Holtart.Be();

            try
            {
                // törölt
                if (TáblaLista.SelectedRows.Count < 1) MessageBox.Show("Nincsen kijelölve sor!");
                for (int i = 0; i < TáblaLista.SelectedRows.Count; i++)
                {
                    Holtart.Lép();
                    // Módosítjuk a kérelem státusát
                    Adat_Behajtás_Behajtási ADAT = new Adat_Behajtás_Behajtási(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 8);
                    Kéz_Behajtás.Módosítás_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);

                    Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(TáblaLista.SelectedRows[i].Cells[0].Value.ToStrTrim(), 8, 0, Program.PostásNév.Trim(), DateTime.Now);
                    KézNapló.Rögzítés_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);
                }
                LISTAlista();

                MessageBox.Show("Az engedélyek státus állítása sikeresen megtörtént Törlésre.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Holtart.Ki();
        }
        #endregion


        #region Gondnok
        private void BtnGondnokFrissít_Click(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i < KérelemTábla.Rows.Count; i++)
            {
                if (KérelemTábla.Rows[i].Cells[1].Value.ToString().Trim() == Cmbtelephely.Text.Trim())
                {
                    j = 1;
                    break;
                }
            }

            if (j == 0)
            {
                Táblagondnok.Rows.Clear();
                Táblagondnok.Columns.Clear();
                Táblagondnok.Refresh();
                MessageBox.Show("Az adott szervezeti egységnek nincs parkolási lehetősége!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
                Gondnoklista();
        }

        private void Gondnoklista()
        {
            try
            {
                // leellenőrizzük, hogy telephely-e
                Táblagondnok.Rows.Clear();
                Táblagondnok.Columns.Clear();
                Táblagondnok.Refresh();

                EmailAdatok_Feltöltése();

                Adat_Behajtás_Engedélyezés volt = (from a in EmailAdatok
                                                   where a.Telephely == Cmbtelephely.Text.Trim() && a.Gondnok
                                                   select a).FirstOrDefault();
                if (volt == null) return;

                // Kilistázza a képernyőre a rögzített adatokat

                List<Adat_Behajtás_Behajtási> AdatokÖ = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                List<Adat_Behajtás_Behajtási> Adatok = (from a in AdatokÖ
                                                        where (int)a.GetType().GetProperty($"{Cmbtelephely.Text.Trim()}_engedély").GetValue(a) == 1
                                                        select a).ToList();
                Táblagondnok.Rows.Clear();
                Táblagondnok.Columns.Clear();
                Táblagondnok.Refresh();
                Táblagondnok.Visible = false;
                Táblagondnok.ColumnCount = 10;
                Táblagondnok.RowCount = 0;

                Táblagondnok.Columns[0].HeaderText = "Engedély száma";
                Táblagondnok.Columns[1].HeaderText = "Név";
                Táblagondnok.Columns[2].HeaderText = "HR azonosító";
                Táblagondnok.Columns[3].HeaderText = "Besorolás";
                Táblagondnok.Columns[4].HeaderText = "Szolgálati hely";
                Táblagondnok.Columns[5].HeaderText = "Dátum";
                Táblagondnok.Columns[6].HeaderText = "Rendszám";
                Táblagondnok.Columns[7].HeaderText = "Oka";
                Táblagondnok.Columns[8].HeaderText = "PDF";
                Táblagondnok.Columns[9].HeaderText = "Megjegyzés";

                Táblagondnok.Columns[0].Width = 80;
                Táblagondnok.Columns[1].Width = 180;
                Táblagondnok.Columns[2].Width = 90;
                Táblagondnok.Columns[3].Width = 80;
                Táblagondnok.Columns[4].Width = 280;
                Táblagondnok.Columns[5].Width = 100;
                Táblagondnok.Columns[6].Width = 100;
                Táblagondnok.Columns[7].Width = 150;
                Táblagondnok.Columns[8].Width = 100;
                Táblagondnok.Columns[9].Width = 100;

                foreach (Adat_Behajtás_Behajtási rekord in Adatok)
                {
                    Táblagondnok.RowCount++;
                    int ii = Táblagondnok.RowCount - 1;
                    Táblagondnok.Rows[ii].Cells[0].Value = rekord.Sorszám;
                    Táblagondnok.Rows[ii].Cells[1].Value = rekord.Név;
                    Táblagondnok.Rows[ii].Cells[2].Value = rekord.HRazonosító;
                    Táblagondnok.Rows[ii].Cells[3].Value = rekord.Korlátlan;
                    Táblagondnok.Rows[ii].Cells[4].Value = rekord.Szolgálatihely;
                    Táblagondnok.Rows[ii].Cells[5].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Táblagondnok.Rows[ii].Cells[6].Value = rekord.Rendszám;
                    Táblagondnok.Rows[ii].Cells[7].Value = rekord.OKA;
                    Táblagondnok.Rows[ii].Cells[8].Value = rekord.PDF;
                    Táblagondnok.Rows[ii].Cells[9].Value = rekord.Megjegyzés;
                }

                Táblagondnok_Szinez();
                Táblagondnok.Refresh();
                Táblagondnok.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Táblagondnok_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Táblagondnok.SelectedRows.Count != 0)
                {
                    TxtKérelemID.Text = Táblagondnok.Rows[Táblagondnok.SelectedRows[0].Index].Cells[0].Value.ToString();
                    Kérelemújraírás();
                    if (TxtKérrelemPDF.Text.Trim() != "_")
                    {
                        string helypdf = TxtKérrelemPDF.Text.Trim();

                        PDF_Megjelenítés(helypdf);
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

        private void ListafeltöltésGondnok()
        {
            try
            {
                CmbGondnokEngedély.Items.Clear();
                CmbGondnokEngedély.BeginUpdate();

                List<Adat_Behajtás_Telephelystátusz> AdatokÖ = KézStátus.Lista_Adatok();
                List<Adat_Behajtás_Telephelystátusz> Adatok = (from a in AdatokÖ
                                                               where a.Gondnok == 1
                                                               orderby a.ID
                                                               select a).ToList();

                foreach (Adat_Behajtás_Telephelystátusz rekord in Adatok)
                    CmbGondnokEngedély.Items.Add($"{rekord.ID} - {rekord.Státus}");

                CmbGondnokEngedély.EndUpdate();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGondnokSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbGondnokEngedély.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                if (Táblagondnok.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs a táblázatban kijelölve érvényes sor.");
                List<Adat_Behajtás_Telephelystátusz> AdatokS = KézStátus.Lista_Adatok();
                Adat_Behajtás_Telephelystátusz Státus = (from a in AdatokS
                                                         where a.ID == int.Parse(CmbGondnokEngedély.Text.Substring(0, 1))
                                                         select a).FirstOrDefault();

                if (Státus != null && Státus.Indoklás == 1 && TxtGondnokMegjegyzés.Text.Trim() == "") throw new HibásBevittAdat("A Indoklás/Megjegyzés mezőt ki kell tölteni.");

                int HUE = 0;
                int SZÁE = 0;
                int ZUE = 0;
                int AFE = 0;
                int BAE = 0;
                int FOE = 0;
                int SZIE = 0;
                int KEE = 0;
                int BUE = 0;
                int FEE = 0;
                int IE = 0;
                int IIE = 0;
                int IIIE = 0;

                foreach (DataGridViewRow row in Táblagondnok.SelectedRows)
                {
                    int i = row.Index;

                    string SorSzám = Táblagondnok.Rows[i].Cells[0].Value.ToStrTrim();

                    Kéz_Behajtás.Módosítás_Gondnok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), Cmbtelephely.Text.Trim(), int.Parse(CmbGondnokEngedély.Text.Substring(0, 1)), TxtGondnokMegjegyzés.Text.Trim(), SorSzám);
                    KézNapló.Rögzítés_Gondnok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), Cmbtelephely.Text.Trim(), int.Parse(CmbGondnokEngedély.Text.Substring(0, 1)), TxtGondnokMegjegyzés.Text.Trim(), SorSzám);

                    List<Adat_Behajtás_Behajtási> AdatokÖ = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                    Adat_Behajtás_Behajtási rekord = (from a in AdatokÖ
                                                      where a.Sorszám == SorSzám
                                                      select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        HUE = rekord.Hungária_engedély;
                        SZÁE = rekord.Száva_engedély;
                        ZUE = rekord.Zugló_engedély;

                        AFE = rekord.Angyalföld_engedély;
                        BAE = rekord.Baross_engedély;
                        FOE = rekord.Fogaskerekű_engedély;
                        SZIE = rekord.Szépilona_engedély;

                        KEE = rekord.Kelenföld_engedély;
                        BUE = rekord.Budafok_engedély;
                        FEE = rekord.Ferencváros_engedély;

                        IE = rekord.I_engedély;
                        IIE = rekord.II_engedély;
                        IIIE = rekord.III_engedély;
                    }

                    // I szolgálat engedély 
                    if (HUE == 0 & SZÁE == 0 & ZUE == 0 & IE == 0)
                    {
                        // ha nem volt jelölve semmi
                    }
                    // ha valami volt jelölve
                    else if (HUE != 1 & SZÁE != 1 & ZUE != 1)
                    {
                        // ha volt egy engedély, vagy elutasítás
                        if ((HUE == 2 | HUE == 0) & (SZÁE == 2 | SZÁE == 0) & (ZUE == 2 | ZUE == 0))
                        {
                            // ha engedély volt
                            IE = 1;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "I_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "I_engedély", 1, SorSzám);
                        }
                        else if ((HUE == 3 | HUE == 0) & (SZÁE == 3 | SZÁE == 0) & (ZUE == 3 | ZUE == 0))
                        {
                            // ha elutasították mindenhol
                            IE = 3;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "I_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "I_engedély", 1, SorSzám);
                        }
                        else
                        {
                            // ha valahol engedélyezték
                            IE = 1;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "I_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "I_engedély", 1, SorSzám);
                        }
                    }

                    // II szolgálat engedély 
                    if (AFE == 0 & BAE == 0 & FOE == 0 & SZIE == 0 & IIE == 0)
                    {
                        // ha nem volt jelölve semmi
                    }
                    // ha valami volt jelölve
                    else if (AFE != 1 & BAE != 1 & FOE != 1 & SZIE != 1)
                    {
                        // ha volt egy engedély, vagy elutasítás
                        if ((AFE == 2 | AFE == 0) & (BAE == 2 | BAE == 0) & (FOE == 2 | FOE == 0) & (SZIE == 2 | SZIE == 0))
                        {
                            // ha engedély volt
                            IIE = 1;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "II_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "II_engedély", 1, SorSzám);
                        }
                        else if ((AFE == 3 | AFE == 0) & (BAE == 3 | BAE == 0) & (FOE == 3 | FOE == 0) & (SZIE == 3 | SZIE == 0))
                        {
                            // ha elutasították mindenhol
                            IIE = 3;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "II_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "II_engedély", 1, SorSzám);
                        }
                        else
                        {
                            // ha valahol engedélyezték
                            IIE = 1;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "II_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "II_engedély", 1, SorSzám);
                        }
                    }
                    //
                    // III szolgálat engedély 

                    if (KEE == 0 & BUE == 0 & FEE == 0 & IIIE == 0)
                    {
                        // ha nem volt jelölve semmi
                    }
                    // ha valami volt jelölve
                    else if (KEE != 1 & BUE != 1 & FEE != 1)
                    {
                        // ha volt egy engedély, vagy elutasítás
                        if ((KEE == 2 | KEE == 0) & (BUE == 2 | BUE == 0) & (FEE == 2 | FEE == 0))
                        {
                            // ha engedély volt
                            IIIE = 1;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "III_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "III_engedély", 1, SorSzám);
                        }
                        else if ((KEE == 3 | KEE == 0) & (BUE == 3 | BUE == 0) & (FEE == 3 | FEE == 0))
                        {
                            // ha elutasították mindenhol
                            IIIE = 3;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "III_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "III_engedély", 1, SorSzám);
                        }
                        else
                        {
                            // ha valahol engedélyezték
                            IIIE = 1;
                            Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "III_engedély", 1, SorSzám);
                            KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), "III_engedély", 1, SorSzám);
                        }
                    }
                }
                Gondnoklista();
                TxtGondnokMegjegyzés.Text = "";
            }
            catch (HibásBevittAdat ex)
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


        #region Szakszolgálat
        private void BtnEngedélySzakFrissít_Click(object sender, EventArgs e)
        {
            if (Cmbtelephely.Text.ToUpper().Contains("VONTATÁSI TÖRZS"))
                Szakszlista();
            else
                MessageBox.Show("Csak Szakszolgálati törzsből lehet engedélyezn!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Szakszlista()
        {
            if (Táblaszaksz.Rows.Count != 0)
            {
                Táblaszaksz.Rows.Clear();
                Táblaszaksz.Columns.Clear();
                Táblaszaksz.Refresh();
            }

            if (!Cmbtelephely.Text.ToUpper().Contains("VONTATÁSI TÖRZS")) return;

            string eredmény = Cmbtelephely.Text;
            // Kilistázza a képernyőre a rögzített adatokat

            Táblaszaksz.Rows.Clear();
            Táblaszaksz.Columns.Clear();
            Táblaszaksz.Refresh();
            Táblaszaksz.Visible = false;
            Táblaszaksz.ColumnCount = 10;
            Táblaszaksz.RowCount = 0;


            Táblaszaksz.Columns[0].HeaderText = "Engedély száma";
            Táblaszaksz.Columns[0].Width = 80;

            Táblaszaksz.Columns[1].HeaderText = "Név";
            Táblaszaksz.Columns[1].Width = 180;

            Táblaszaksz.Columns[2].HeaderText = "HR azonosító";
            Táblaszaksz.Columns[2].Width = 90;

            Táblaszaksz.Columns[3].HeaderText = "Besorolás";
            Táblaszaksz.Columns[3].Width = 80;

            Táblaszaksz.Columns[4].HeaderText = "Szolgálati hely";
            Táblaszaksz.Columns[4].Width = 280;

            Táblaszaksz.Columns[5].HeaderText = "Dátum";
            Táblaszaksz.Columns[5].Width = 100;

            Táblaszaksz.Columns[6].HeaderText = "Rendszám";
            Táblaszaksz.Columns[6].Width = 90;

            Táblaszaksz.Columns[7].HeaderText = "Oka";
            Táblaszaksz.Columns[7].Width = 150;

            Táblaszaksz.Columns[8].HeaderText = "PDF";
            Táblaszaksz.Columns[8].Width = 80;

            Táblaszaksz.Columns[9].HeaderText = "Megjegyzés";
            Táblaszaksz.Columns[9].Width = 150;

            foreach (string Elem in Szereplők)
            {
                Táblaszaksz.ColumnCount++;
                Táblaszaksz.Columns[Táblaszaksz.ColumnCount - 1].HeaderText = Elem;
                Táblaszaksz.Columns[Táblaszaksz.ColumnCount - 1].Width = 100;
            }

            // módosítás figyelő oszlop
            Táblaszaksz.ColumnCount++;
            Táblaszaksz.Columns[Táblaszaksz.ColumnCount - 1].HeaderText = "Módosítás";
            Táblaszaksz.Columns[Táblaszaksz.ColumnCount - 1].Width = 0;

            Cmbtelephely.Text = eredmény;

            List<Adat_Behajtás_Behajtási> AdatokÖ = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
            List<Adat_Behajtás_Behajtási> Adatok = (from a in AdatokÖ
                                                    where (int)a.GetType().GetProperty($"{Cmbtelephely.Text.Trim().Split(' ')[0]}_engedély").GetValue(a) == 1
                                                    orderby a.Sorszám
                                                    select a).ToList();

            foreach (Adat_Behajtás_Behajtási rekord in Adatok)
            {
                Táblaszaksz.RowCount++;
                int ii = Táblaszaksz.RowCount - 1;
                Táblaszaksz.Rows[ii].Cells[0].Value = rekord.Sorszám;
                Táblaszaksz.Rows[ii].Cells[1].Value = rekord.Név;
                Táblaszaksz.Rows[ii].Cells[2].Value = rekord.HRazonosító;
                Táblaszaksz.Rows[ii].Cells[3].Value = rekord.Korlátlan;
                Táblaszaksz.Rows[ii].Cells[4].Value = rekord.Szolgálatihely;
                Táblaszaksz.Rows[ii].Cells[5].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                Táblaszaksz.Rows[ii].Cells[6].Value = rekord.Rendszám;
                Táblaszaksz.Rows[ii].Cells[7].Value = rekord.OKA;
                Táblaszaksz.Rows[ii].Cells[8].Value = rekord.PDF;
                Táblaszaksz.Rows[ii].Cells[9].Value = rekord.Megjegyzés;

                for (int i = 10; i < Táblaszaksz.ColumnCount - 1; i++)
                    Táblaszaksz.Rows[ii].Cells[i].Value = rekord.GetType().GetProperty($"{Táblaszaksz.Columns[i].HeaderText}_engedély").GetValue(rekord);

                Táblaszaksz.Rows[ii].Cells[Táblaszaksz.ColumnCount - 1].Value = 0;
            }
            Táblaszaksz_Szinez();
            Táblaszaksz.Refresh();
            Táblaszaksz.Visible = true;
        }

        private void Táblaszaksz_SelectionChanged(object sender, EventArgs e)
        {
            if (Táblaszaksz.SelectedRows.Count != 0)
            {
                TxtKérelemID.Text = Táblaszaksz.Rows[Táblaszaksz.SelectedRows[0].Index].Cells[0].Value.ToString();
                Kérelemújraírás();
                if (TxtKérrelemPDF.Text.Trim() != "_")
                {
                    string helypdf = TxtKérrelemPDF.Text.Trim();

                    PDF_Megjelenítés(helypdf);
                }
            }
        }

        private void PDF_Megjelenítés(string pdffájlnév)
        {
            try
            {
                string helypdf = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\{TxtAdminkönyvtár.Text.Trim()}\pdf\{pdffájlnév}";
                if (!File.Exists(helypdf)) return;

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

        private void Táblaszaksz_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // cella tartalma a szerkesztés előtt
            Cellaelőzmény = Táblaszaksz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void Táblaszaksz_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Táblaszaksz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "2" | Táblaszaksz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "3")
            {
                if ((Cellaelőzmény != null) && (!Cellaelőzmény.Equals(Táblaszaksz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)))
                    Táblaszaksz.Rows[e.RowIndex].Cells[Táblaszaksz.ColumnCount - 1].Value = 1;
            }
            else
            {
                Táblaszaksz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Cellaelőzmény;
                throw new HibásBevittAdat("Hiba a bevitt adatokban! 2,3 a lehetséges bevitel.");
            }
        }

        private void Listafeltöltésszaksz()
        {
            try
            {
                CmbSzakszlista.Items.Clear();
                CmbSzakszlista.BeginUpdate();

                List<Adat_Behajtás_Telephelystátusz> AdatokÖ = KézStátus.Lista_Adatok();
                List<Adat_Behajtás_Telephelystátusz> Adatok = (from a in AdatokÖ
                                                               where a.Gondnok == 1
                                                               orderby a.ID
                                                               select a).ToList();

                foreach (Adat_Behajtás_Telephelystátusz rekord in Adatok)
                    CmbSzakszlista.Items.Add($"{rekord.ID} - {rekord.Státus}");

                CmbSzakszlista.EndUpdate();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSzakszeng_Click(object sender, EventArgs e)
        {
            CmbSzakszlista.Text = CmbSzakszlista.Items[0].ToString();
            SzakengEljárás();
        }

        private void Elutasít_gomb_Click(object sender, EventArgs e)
        {
            CmbSzakszlista.Text = CmbSzakszlista.Items[1].ToString();
            SzakengEljárás();
        }

        private void SzakengEljárás()
        {
            try
            {
                if (CmbSzakszlista.Text.Trim() == "") return;
                if (Táblaszaksz.SelectedRows.Count < 1) return;

                int HUE = 0;
                int SZÁE = 0;
                int ZUE = 0;
                int AFE = 0;
                int BAE = 0;
                int FOE = 0;
                int SZIE = 0;
                int KEE = 0;
                int BUE = 0;
                int FEE = 0;
                int IE = 0;
                int IIE = 0;
                int IIIE = 0;

                foreach (DataGridViewRow row in Táblaszaksz.SelectedRows)
                {
                    int i = row.Index;

                    // módosítjuk a szakszolgálat státuszát
                    string[] darabol = Cmbtelephely.Text.Trim().Split(' ');
                    string sorszám = Táblaszaksz.Rows[i].Cells[0].Value.ToStrTrim();

                    Kéz_Behajtás.Módosítás_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), $"{darabol[0].Trim()}_engedély", int.Parse(CmbSzakszlista.Text.Substring(0, 1)), sorszám);
                    KézNapló.Rögzítés_Szakszolgálat(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), $"{darabol[0].Trim()}_engedély", int.Parse(CmbSzakszlista.Text.Substring(0, 1)), sorszám);

                    List<Adat_Behajtás_Behajtási> AdatokÖ = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                    Adat_Behajtás_Behajtási rekord = (from a in AdatokÖ
                                                      where a.Sorszám == sorszám
                                                      select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        HUE = rekord.Hungária_engedély;
                        SZÁE = rekord.Száva_engedély;
                        ZUE = rekord.Zugló_engedély;

                        AFE = rekord.Angyalföld_engedély;
                        BAE = rekord.Baross_engedély;
                        FOE = rekord.Fogaskerekű_engedély;
                        SZIE = rekord.Szépilona_engedély;

                        KEE = rekord.Kelenföld_engedély;
                        BUE = rekord.Budafok_engedély;
                        FEE = rekord.Ferencváros_engedély;

                        IE = rekord.I_engedély;
                        IIE = rekord.II_engedély;
                        IIIE = rekord.III_engedély;



                        if (HUE == 1 || SZÁE == 1 || ZUE == 1 || AFE == 1 || BAE == 1 || FOE == 1 || SZIE == 1 || KEE == 1 || BUE == 1 || FEE == 1)
                        {
                            // ha van mér valahol engedélyezendő akkor nem csinál semmit
                        }
                        // ha már minden szakszolgálatnál nincs mit csinálni
                        else
                        {
                            if (IE != 1 && IIE != 1 && IIIE != 1)
                            {
                                // ha valahol engedélyezték
                                if (IE == 2 || IIE == 2 || IIIE == 2)
                                {
                                    Adat_Behajtás_Behajtási ADAT = new Adat_Behajtás_Behajtási(sorszám, 2);
                                    Kéz_Behajtás.Módosítás_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);
                                    Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(sorszám, 2, 0, Program.PostásNév.Trim(), DateTime.Now);
                                    KézNapló.Rögzítés_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);
                                }

                                // ha mindenütt elutasították
                                if (IE == 0 && IIE == 0 && IIIE == 3 || IE == 0 && IIE == 3 && IIIE == 0 || IE == 3 && IIE == 0 && IIIE == 0)
                                {
                                    Adat_Behajtás_Behajtási ADAT = new Adat_Behajtás_Behajtási(sorszám, 9);
                                    Kéz_Behajtás.Módosítás_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADAT);
                                    Adat_Behajtás_Behajtási_Napló ADATNapló = new Adat_Behajtás_Behajtási_Napló(sorszám, 9, 0, Program.PostásNév.Trim(), DateTime.Now);
                                    KézNapló.Rögzítés_Státus(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), ADATNapló);
                                }
                            }
                        }
                    }
                }
                Szakszlista();
            }
            catch (HibásBevittAdat ex)
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
            Szereplők_lista();
        }

        private void Szereplők_lista()
        {
            try
            {
                // ha vontatási törzs, akkor feltöltjük a telephelyeit
                Szereplők.Clear();
                if (Cmbtelephely.Text.ToUpper().Contains("VONTATÁSI TÖRZS"))
                {
                    List<Adat_Kiegészítő_Könyvtár> TeljAdatok = KézKiegKönyvtár.Lista_Adatok();
                    Adat_Kiegészítő_Könyvtár Ideig = (from a in TeljAdatok
                                                      where a.Név == Cmbtelephely.Text.Trim()
                                                      select a).FirstOrDefault();
                    if (Ideig != null)
                    {
                        List<Adat_Kiegészítő_Könyvtár> Adatok = (from a in TeljAdatok
                                                                 where a.Csoport1 == Ideig.Csoport1 && a.Vezér1 == false
                                                                 select a).ToList();

                        foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                        {
                            Szereplők.Add(rekord.Név.Trim());
                        }

                    }
                }
                Cmbtelephely.Enabled = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEngedélySzakBírál_Click(object sender, EventArgs e)
        {
            try
            {
                // kijelöljük a módosított sorokat
                for (int i = 0; i < Táblaszaksz.Rows.Count; i++)
                {
                    if (Táblaszaksz.Rows[i].Cells[Táblaszaksz.ColumnCount - 1].Value != null && int.Parse(Táblaszaksz.Rows[i].Cells[Táblaszaksz.ColumnCount - 1].Value.ToString()) == 1)
                    {
                        Táblaszaksz.Rows[i].Selected = true;
                    }
                }

                // ha van kijelölt sor
                if (Táblaszaksz.SelectedRows.Count > 0)
                {

                    for (int j = 0; j < Táblaszaksz.SelectedRows.Count; j++)
                    {
                        if (Táblaszaksz.SelectedRows[j].Cells[Táblaszaksz.ColumnCount - 1].Value != null && int.Parse(Táblaszaksz.SelectedRows[j].Cells[Táblaszaksz.ColumnCount - 1].Value.ToString()) == 1)
                        {
                            string SorSzám = Táblaszaksz.SelectedRows[j].Cells[0].Value.ToStrTrim();
                            for (int i = 10; i < Táblaszaksz.ColumnCount - 1; i++)
                            {
                                string telephely = Táblaszaksz.Columns[i].HeaderText.Trim();
                                int engedély = int.Parse(Táblaszaksz.SelectedRows[j].Cells[i].Value.ToString());
                                Kéz_Behajtás.Módosítás_Gondnok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), telephely, engedély, "Szakszolgálat-vezető felülbírálta", SorSzám);
                                KézNapló.Rögzítés_Gondnok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim(), telephely, engedély, "Szakszolgálat-vezető felülbírálta", SorSzám);
                            }
                        }
                    }
                    Szakszlista();
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


        #region Szinezes
        private void TáblaLista_Szinez()
        {
            foreach (DataGridViewRow row in TáblaLista.Rows)
            {
                if (row.Cells[19].Value != null)
                {
                    switch (int.Parse(row.Cells[19].Value.ToString()))
                    {
                        case 8:
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.IndianRed;
                            row.DefaultCellStyle.Font = new Font("Arial Narrow", 12, FontStyle.Strikeout);
                            break;
                        case 5:
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.Green;
                            row.DefaultCellStyle.Font = new Font("Arial Narrow", 12, FontStyle.Bold);
                            break;
                        case 9:
                            row.DefaultCellStyle.ForeColor = Color.Black;
                            row.DefaultCellStyle.BackColor = Color.Aqua;
                            row.DefaultCellStyle.Font = new Font("Arial Narrow", 12, FontStyle.Strikeout);
                            break;
                    }
                }
            }
        }

        private void Táblagondnok_Szinez()
        {
            foreach (DataGridViewRow row in Táblagondnok.Rows)
            {
                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() == "Vezetői")
                {
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                }
            }
        }

        private void Táblaszaksz_Szinez()
        {
            for (int rowIndex = 0; rowIndex < Táblaszaksz.Rows.Count; rowIndex++)
            {
                DataGridViewRow row = Táblaszaksz.Rows[rowIndex];
                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() == "Vezetői")
                {
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                }

                for (int columnIndex = 10; columnIndex < Táblaszaksz.Columns.Count - 1; columnIndex++)
                {
                    DataGridViewCell cell = row.Cells[columnIndex];

                    switch (cell.Value.ToString())
                    {
                        case "0":
                            break;

                        case "1":
                            break;

                        case "2":
                            cell.Style.BackColor = Color.LightGreen;
                            cell.Style.ForeColor = Color.Black;
                            cell.Style.Font = new Font("Arial Narrow", 12f, FontStyle.Italic);
                            break;

                        case "3":
                            cell.Style.BackColor = Color.LightPink;
                            cell.Style.ForeColor = Color.Black;
                            cell.Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                            break;
                    }
                }
            }
        }
        #endregion


        #region Kérelem
        private void BtnTíputlétOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtAdminOk.Text.Trim() == "") throw new HibásBevittAdat("A kérelem oka mezőt ki kell tölteni.");

                Adatok_Behajtás_Kérelemoka = Kéz_Kérelemoka.Lista_Adatok();

                Adat_Behajtás_Kérelemoka vane = (from a in Adatok_Behajtás_Kérelemoka
                                                 where a.Ok == TxtAdminOk.Text.Trim()
                                                 select a).FirstOrDefault();
                if (vane == null)
                {
                    Adat_Behajtás_Kérelemoka ADAT = new Adat_Behajtás_Kérelemoka(0, TxtAdminOk.Text.Trim());
                    Kéz_Kérelemoka.Rögzítés(ADAT);
                }
                Adminokokfeltöltése();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Adminokokfeltöltése()
        {
            try
            {
                LstAdminokok.Items.Clear();
                CmbkérelemOka.Items.Clear();
                foreach (Adat_Behajtás_Kérelemoka rekord in Kéz_Kérelemoka.Lista_Adatok())
                {
                    LstAdminokok.Items.Add(rekord.Ok);
                    CmbkérelemOka.Items.Add(rekord.Ok);
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

        private void BtnAdminOkTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (LstAdminokok.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva törlendő elem.");

                Adatok_Behajtás_Kérelemoka = Kéz_Kérelemoka.Lista_Adatok();
                Adat_Behajtás_Kérelemoka vane = (from a in Adatok_Behajtás_Kérelemoka
                                                 where a.Ok == LstAdminokok.Text.Trim()
                                                 select a).FirstOrDefault();
                if (vane == null)
                {
                    Adat_Behajtás_Kérelemoka ADAT = new Adat_Behajtás_Kérelemoka(0, TxtAdminOk.Text.Trim());
                    Kéz_Kérelemoka.Törlés(ADAT);
                    Adminokokfeltöltése();
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

        private void BtnAdminOkfel_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtAdminOk.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva előrébb sorolandó elem.");
                if (LstAdminokok.FindString(TxtAdminOk.Text.Trim()) == 0) throw new HibásBevittAdat("Az első elemet nem lehet előrébb sorolni.");


                List<Adat_Behajtás_Kérelemoka> Adatok = Kéz_Kérelemoka.Lista_Adatok();

                Adat_Behajtás_Kérelemoka Elem = (from a in Adatok
                                                 where a.Ok == TxtAdminOk.Text.Trim()
                                                 select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen rögzített elem.");
                Kéz_Kérelemoka.Csere(TxtAdminOk.Text.Trim());
                Adminokokfeltöltése();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LstAdminokok_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstAdminokok.SelectedIndex < 0)
                return;

            TxtAdminOk.Text = LstAdminokok.SelectedItem.ToString().Trim();
        }
        #endregion


        #region Alapadatok
        private void Alapadatokfeltöltése()
        {
            try
            {
                List<Adat_Behajtás_Alap> Adatok = Kéz_BehajtásAlap.Lista_Adatok();

                // fejléc elkészítése
                DataAdminAlap.Rows.Clear();
                DataAdminAlap.Columns.Clear();
                DataAdminAlap.Refresh();
                DataAdminAlap.Visible = false;
                DataAdminAlap.ColumnCount = 7;

                DataAdminAlap.Columns[0].HeaderText = "Ssz.";
                DataAdminAlap.Columns[0].Width = 50;
                DataAdminAlap.Columns[1].HeaderText = "Adatbázisnév";
                DataAdminAlap.Columns[1].Width = 150;
                DataAdminAlap.Columns[2].HeaderText = "Sorszám Betűjele";
                DataAdminAlap.Columns[2].Width = 100;
                DataAdminAlap.Columns[3].HeaderText = "Sorszám kezdete";
                DataAdminAlap.Columns[3].Width = 100;
                DataAdminAlap.Columns[4].HeaderText = "Érvényességi idő";
                DataAdminAlap.Columns[4].Width = 120;
                DataAdminAlap.Columns[5].HeaderText = "Aktuális";
                DataAdminAlap.Columns[5].Width = 100;
                DataAdminAlap.Columns[6].HeaderText = "Adatbázis könyvtár";
                DataAdminAlap.Columns[6].Width = 200;

                foreach (Adat_Behajtás_Alap rekord in Adatok)
                {
                    DataAdminAlap.RowCount++;
                    int i = DataAdminAlap.Rows.Count - 1;
                    DataAdminAlap.Rows[i].Cells[0].Value = rekord.Id;
                    DataAdminAlap.Rows[i].Cells[1].Value = rekord.Adatbázisnév;
                    DataAdminAlap.Rows[i].Cells[2].Value = rekord.Sorszámbetűjele;
                    DataAdminAlap.Rows[i].Cells[3].Value = rekord.Sorszámkezdete;
                    DataAdminAlap.Rows[i].Cells[4].Value = rekord.Engedélyérvényes.ToString("yyyy.MM.dd");
                    DataAdminAlap.Rows[i].Cells[5].Value = rekord.Státus;
                    DataAdminAlap.Rows[i].Cells[6].Value = rekord.Adatbáziskönyvtár;
                }

                DataAdminAlap.Refresh();
                DataAdminAlap.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataAdminAlap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            TxtAdminaktuális.Text = DataAdminAlap.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAdminkönyvtár.Text = DataAdminAlap.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtAmindFájl.Text = DataAdminAlap.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtAdminSorszám.Text = DataAdminAlap.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtadminBetű.Text = DataAdminAlap.Rows[e.RowIndex].Cells[2].Value.ToString();
            DatadminÉrvényes.Value = DateTime.Parse(DataAdminAlap.Rows[e.RowIndex].Cells[4].Value.ToString());
            TxtadminAktuálissor.Text = DataAdminAlap.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void Adminalapbeállítás()
        {
            Adat_Behajtás_Alap rekord = (from a in Kéz_BehajtásAlap.Lista_Adatok()
                                         where a.Státus == 1
                                         orderby a.Id
                                         select a).FirstOrDefault();
            if (rekord != null)
            {
                TxtAdminaktuális.Text = rekord.Id.ToString().Trim();
                TxtAmindFájl.Text = rekord.Adatbázisnév.Trim();
                TxtAdminkönyvtár.Text = rekord.Adatbáziskönyvtár.Trim();
                TxtadminBetű.Text = rekord.Sorszámbetűjele.Trim();
                TxtAdminSorszám.Text = rekord.Sorszámkezdete.ToString().Trim();
                DatadminÉrvényes.Value = DateTime.Parse(rekord.Engedélyérvényes.ToString().Trim());
                TxtadminAktuálissor.Text = rekord.Státus.ToString().Trim();
            }
        }

        private void BtnAdminRögz_Click(object sender, EventArgs e)
        {
            try
            {
                // Módosítjuk a tábla beállításait
                if (!int.TryParse(TxtAdminaktuális.Text, out int ID)) throw new HibásBevittAdat("Javítsa ki az aktuális adatbázis mezőt!");
                if (!int.TryParse(TxtAdminSorszám.Text, out int Sorszám)) throw new HibásBevittAdat("A sorszám kezdetének egész számnak kell lennie.");
                if (!int.TryParse(TxtadminAktuálissor.Text, out int Státus)) throw new HibásBevittAdat("Státus mező csak 0 és 1 tartalmhazhat.");
                if (Státus < 0 || Státus > 1) throw new HibásBevittAdat("Státus mező csak 0 és 1 tartalmhazhat.");
                if (TxtAdminkönyvtár.Text.Trim() == "") TxtAdminkönyvtár.Text = "_";
                if (TxtAmindFájl.Text.Trim() == "") TxtAmindFájl.Text = "_";
                if (TxtadminBetű.Text.Trim() == "") TxtadminBetű.Text = "_";

                Adat_Behajtás_Alap ADAT = new Adat_Behajtás_Alap(ID,
                                                                 TxtAmindFájl.Text.Trim(),
                                                                 TxtadminBetű.Text.Trim(),
                                                                 Sorszám,
                                                                 DatadminÉrvényes.Value,
                                                                 Státus,
                                                                 TxtAdminkönyvtár.Text.Trim());
                Kéz_BehajtásAlap.Módosítás(ADAT);
                Alapadatokfeltöltése();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdminÚjEngedély_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Behajtás_Alap ADAT = new Adat_Behajtás_Alap(0, "_", "_", 1, DateTime.Parse("1900.01.01"), 0, "_");
                Kéz_BehajtásAlap.Rögzítés(ADAT);
                Alapadatokfeltöltése();
            }
            catch (HibásBevittAdat ex)
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


        #region Dolgozóbetöltés
        private void BtnDolgozóilsta_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "IDM-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                IDM_beolvasás.Behajtási_beolvasás(fájlexc);

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


        #endregion


        #region Naplózás
        private void BtnNaplóLista_Click(object sender, EventArgs e)
        {
            Naplózás_listázása();
        }

        private void Naplózás_listázása()
        {
            try
            {
                List<Adat_Behajtás_Behajtási_Napló> AdatokÖ = KézNapló.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
                List<Adat_Behajtás_Behajtási_Napló> Adatok;
                if (TextNaplósorszám.Text.Trim() != "")
                    Adatok = (from a in AdatokÖ
                              where a.Sorszám == TextNaplósorszám.Text.Trim()
                              orderby a.Rögzítésdátuma
                              select a).ToList();
                else
                    Adatok = AdatokÖ.OrderBy(a => a.Rögzítésdátuma).ToList();

                DataNapló.Rows.Clear();
                DataNapló.Columns.Clear();
                DataNapló.Refresh();
                DataNapló.Visible = false;
                DataNapló.ColumnCount = 38;

                string[] fejléc = { "Engedély száma", "Szolgálti hely", "HR azonosító", "Név", "Rendszám", "AF eng", "AF megj",
                    "BA eng", "BA megj", "BU eng", "BU megj", "FE eng", "FE megj", "FO eng", "FO megj", "HU eng", "HU megj", "KE eng",
                    "KE megj", "SZA eng", "SZA megj", "SZE eng", "SZE megj", "ZU eng", "ZU megj", "Besorolás", "Autók száma", "I eng",
                    "II eng", "III eng", "Státusz", "Dátum", "Megjegyzés", "PDF", "OKA", "Napló Sorszám", "Rögzítette", "Rögzítés ideje" };
                int[] szélesség = { 90, 200, 100, 200, 100, 50, 70, 50, 70, 50, 70, 50, 70, 50, 70, 50, 70,
                    50, 70, 50, 70, 50, 70, 50, 70, 100, 70, 70, 70, 70, 70, 70, 70, 70, 70, 70, 70, 160, 160 };

                for (int i = 0; i < fejléc.Length; i++)
                {
                    DataNapló.Columns[i].HeaderText = fejléc[i];
                    DataNapló.Columns[i].Width = szélesség[i];
                }

                foreach (Adat_Behajtás_Behajtási_Napló rekord in Adatok)
                {
                    DataNapló.Rows.Add(rekord.Sorszám.Trim(), rekord.Szolgálatihely.Trim(), rekord.HRazonosító.ToString().Trim(), rekord.Név.Trim(), rekord.Rendszám.Trim(),
                    rekord.Angyalföld_engedély.ToString().Trim(), rekord.Angyalföld_megjegyzés.Trim(), rekord.Baross_engedély.ToString().Trim(), rekord.Baross_megjegyzés.Trim(),
                    rekord.Budafok_engedély.ToString().Trim(), rekord.Budafok_megjegyzés.Trim(), rekord.Ferencváros_engedély.ToString().Trim(), rekord.Ferencváros_megjegyzés.Trim(),
                    rekord.Fogaskerekű_engedély.ToString().Trim(), rekord.Fogaskerekű_megjegyzés.Trim(), rekord.Hungária_engedély.ToString().Trim(), rekord.Hungária_megjegyzés.Trim(),
                    rekord.Kelenföld_engedély.ToString().Trim(), rekord.Kelenföld_megjegyzés.Trim(), rekord.Száva_engedély.ToString().Trim(), rekord.Száva_megjegyzés.Trim(),
                    rekord.Szépilona_engedély.ToString().Trim(), rekord.Szépilona_megjegyzés.Trim(), rekord.Zugló_engedély.ToString().Trim(), rekord.Zugló_megjegyzés.Trim(),
                    rekord.Korlátlan.Trim(), rekord.Autók_száma.ToString().Trim(), rekord.I_engedély.ToString().Trim(), rekord.II_engedély.ToString().Trim(), rekord.III_engedély.ToString().Trim(),
                    rekord.Státus.ToString().Trim(), rekord.Dátum.ToString().Trim(), rekord.Megjegyzés.ToString().Trim(), rekord.PDF.ToString().Trim(), rekord.OKA.ToString().Trim(),
                    rekord.ID.ToString().Trim(), rekord.Rögzítette.Trim(), rekord.Rögzítésdátuma.ToString().Trim());
                }

                DataNapló.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNaplóExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataNapló.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Behajtási_napló_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;

                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, DataNapló);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
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
        private void Engedélyek_Listázása()
        {
            try
            {
                Adatok_Behajtás.Clear();
                Adatok_Behajtás = Kéz_Behajtás.Lista_Adatok(TxtAdminkönyvtár.Text.Trim(), TxtAmindFájl.Text.Trim());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmailAdatok_Feltöltése()
        {
            try
            {
                EmailAdatok = EmailKéz.Lista_Adatok();
            }
            catch (HibásBevittAdat ex)
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