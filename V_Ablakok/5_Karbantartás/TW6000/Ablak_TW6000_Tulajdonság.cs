using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.TW6000;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public partial class Ablak_TW6000_Tulajdonság
    {

        readonly string TW6000_Villamos = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos4TW.mdb";
        readonly string TW6000_Napló = $@"{Application.StartupPath}\Főmérnökség\napló\naplóTW6000_{DateTime.Today:yyyy}.mdb";
        readonly string TW6000_Napló_Ütem = $@"{Application.StartupPath}\Főmérnökség\napló\naplóTW6000Ütem_{DateTime.Today:yyyy}.mdb";

        Ablak_Kereső Új_Ablak_Kereső;

        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_TW6000_Ütemezés KézÜtem = new Kezelő_TW6000_Ütemezés();
        readonly Kezelő_TW6000_Alap KézAlap = new Kezelő_TW6000_Alap();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_TW600_Telephely KézTelep = new Kezelő_TW600_Telephely();
        readonly Kezelő_TW600_Színezés KézSzín = new Kezelő_TW600_Színezés();
        readonly Kezelő_Váltós_Naptár KézVNaptár = new Kezelő_Váltós_Naptár();
        readonly Kezelő_kiegészítő_telephely KézTelephely = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_TW600_AlapNapló KézAlapNapló = new Kezelő_TW600_AlapNapló();
        readonly Kezelő_TW600_ÜtemNapló KézÜtemNapló = new Kezelő_TW600_ÜtemNapló();
        readonly Kezelő_TW6000_Előterv KézElőterv = new Kezelő_TW6000_Előterv();

        List<Adat_TW6000_Ütemezés> AdatokÜtem = new List<Adat_TW6000_Ütemezés>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_TW6000_Alap> AdatokAlap = new List<Adat_TW6000_Alap>();

        #region Alap
        public Ablak_TW6000_Tulajdonság()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// Ablak betöltésekor végrehajtandó műveletek
        /// </summary>
        private void Start()
        {
            Telephelyekfeltöltése();

            GombLathatosagKezelo.Beallit(this);
            Jogosultságkiosztás();
            Pályaszám_feltöltés();
            CiklusListaFeltöltés();
            AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");

            Ütemkezdete.Value = DateTime.Today;
            Ütemvége.Value = DateTime.Today.AddDays(30);
            Vizsgdátum.Value = DateTime.Today;
            ÜtemNaplóKezdet.Value = DateTime.Today.AddDays(-30);
            ÜtemNaplóVége.Value = DateTime.Today;
            NaplóKezdete.Value = DateTime.Today.AddDays(-30);
            NaplóVége.Value = DateTime.Today;
            Előkezdődátum.Value = DateTime.Today;
            ElőbefejezőDátum.Value = DateTime.Today.AddDays(30);

            LapFülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Tulajdonság_TW6000_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Ablak bezárásakor a megnyitott ablakokat is zárja be
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ablak_TW6000_Tulajdonság_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TW6000_Telephely?.Close();
            Új_Ablak_TW6000_Színkezelő?.Close();
            Új_Ablak_Kereső?.Close();
        }

        /// <summary>
        /// Telephelyek feltöltése a legördülő listába
        /// </summary>
        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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

        /// <summary>
        /// Jogosultságok kiosztása a felhasználónak
        /// </summary>
        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Járműadatok_rögzít.Visible = false;
                Ütemfrissít.Visible = false;
                BtnÜtemÜtemezés.Visible = false;

                BtnÜtemTörlés.Visible = false;
                Telephely_lap.Visible = false;
                BtnSzínező.Visible = false;

                BtnÜtemRészTerv.Visible = false;
                BtnÜtemRészRögz.Visible = false;

                melyikelem = 110;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                    Járműadatok_rögzít.Visible = true;

                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                    Ütemfrissít.Visible = true;

                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                    BtnÜtemÜtemezés.Visible = true;

                melyikelem = 111;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                    BtnÜtemTörlés.Visible = true;

                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                    Telephely_lap.Visible = true;

                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                    BtnSzínező.Visible = true;

                melyikelem = 112;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                    BtnÜtemRészTerv.Visible = true;

                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                    BtnÜtemRészRögz.Visible = true;

                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {
                }

                // ha nem főmérnökségbe lépett be akkor csak néz
                if (Program.PostásTelephely != "Főmérnökség")
                {
                    Járműadatok_rögzít.Enabled = false;
                    Ütemfrissít.Enabled = false;
                    BtnÜtemÜtemezés.Enabled = false;

                    BtnÜtemTörlés.Enabled = false;
                    Telephely_lap.Enabled = false;
                    BtnSzínező.Enabled = false;

                    BtnÜtemRészTerv.Enabled = false;
                    BtnÜtemRészRögz.Enabled = false;
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

        /// <summary>
        /// Megnyitja a súgó fájlt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\TW6000_ütem.html";
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

        /// <summary>
        /// Lapfülek kiválasztásakor a megfelelő lapot tölti be
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        /// <summary>
        /// A kiválasztott lapfül tartalmának feltöltése
        /// </summary>
        private void Fülekkitöltése()
        {
            switch (LapFülek.SelectedIndex)
            {
                case 0:
                    {
                        // Ütemezés
                        Státus_feltöltés();
                        CiklusTípusfeltöltés();
                        ÜCiklusrend.Text = "TW6000";
                        break;
                    }
                case 1:
                    {
                        // ütemezés részletes
                        CiklusTípusfeltöltés();
                        ÜCiklusrend.Text = "TW6000";
                        Ciklussorszámfeltöltés();
                        Státus_feltöltés();
                        UV_Telephely_feltöltés();
                        break;
                    }
                case 2:
                    {
                        // járműadatok
                        CiklusTípusfeltöltés();
                        Ciklussorszámfeltöltés_Jármű();
                        break;
                    }
                case 3:
                    {
                        // karbantartás előzmények
                        NaplóPályaszám_feltöltés();
                        break;
                    }
                case 4:
                    {
                        // ütemezés napló
                        ÜtemPályaszám_feltöltés();
                        Státus_feltöltés();
                        break;
                    }
                case 5:
                    {
                        // előtervező
                        CiklusTípusfeltöltés();
                        Telephelylista_feltöltés();
                        Pszlista_feltöltés();
                        Vizsgálatfeltöltés();
                        break;
                    }
            }
        }

        /// <summary>
        /// Lapfülek megjelenítése az aktív kiemelt szinezést kap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lapfülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = LapFülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = LapFülek.GetTabRect(e.Index);

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
                Font BoldFont = new Font(LapFülek.Font.Name, LapFülek.Font.Size, FontStyle.Bold);
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


        #region Ütemezés lapfül
        /// <summary>
        /// Ütemezés lapfül kiválasztásakor a megfelelő lapot tölti be
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Terv_lista_Click(object sender, EventArgs e)
        {
            Újkiíró();
        }

        /// <summary>
        /// Az időintervallumnak megfelelően listázza az ütemezés adatokat.
        /// </summary>
        private void Újkiíró()
        {
            try
            {
                // Ellenőrizze, hogy az intervallum helyes-e
                if (Ütemkezdete.Value > Ütemvége.Value) throw new HibásBevittAdat("A kezdő dátum nem lehet későbbi, mint a vége dátum. Kérlek ellenőrizd és adj meg érvényes időintervallumot.");
                if (Ütemkezdete.Value.Year != Ütemvége.Value.Year) throw new HibásBevittAdat("A két dátum évének egyeznie kell!");

                Holtart.Be();

                // Tábla inicializálása és beállítása
                Táblaütemezés.Rows.Clear();
                Táblaütemezés.Columns.Clear();
                Táblaütemezés.Refresh();

                // Az oszlopok inicializálása
                Táblaütemezés.ColumnCount = 2;
                Táblaütemezés.Columns[0].Name = "Dátum";
                Táblaütemezés.Columns[0].Width = 100;
                Táblaütemezés.Columns[1].HeaderText = "";
                Táblaütemezés.Columns[1].Width = 50;

                // Dátumok kiírása // Hétvégék és munkaidő naptár színezése
                KiírDátumok();
                int oszlop = 2;
                int oszlope = 2;

                List<Adat_TW6000_Telephely> AdatokTel = KézTelep.Lista_Adatok();
                List<Adat_TW6000_Színezés> SzínAdatok = KézSzín.Lista_Adatok();       //Színlista betöltése

                //listázzuk a járműveket, amik TW6000 típusúak
                List<Adat_Jármű> AdatokJ = KézJármű.Lista_Adatok("Főmérnökség");
                AdatokJ = (from a in AdatokJ
                           where a.Valóstípus == "TW6000"
                           && a.Törölt == false
                           select a).ToList();

                AdatokÜtem = KézÜtem.Lista_Adatok();
                AdatokÜtem = (from a in AdatokÜtem
                              where a.Vütemezés >= Ütemkezdete.Value
                              && a.Vütemezés <= Ütemvége.Value
                              orderby a.Azonosító
                              select a).ToList();

                //Két listát egyesítjük 
                List<Adat_TW6000_Ütemezés_Plusz> Egyesítettlista = Összesítvalami(AdatokJ, AdatokÜtem);

                bool páros = false;
                TimeSpan napokszáma = Ütemvége.Value - Ütemkezdete.Value;
                int[] összes = new int[napokszáma.Days + 1];
                int[] kiemelt = new int[napokszáma.Days + 1];

                foreach (Adat_TW6000_Telephely Elem in AdatokTel)
                {
                    //leszűrjük telephelyre
                    List<Adat_TW6000_Ütemezés_Plusz> TelephelyiLista = (from a in Egyesítettlista
                                                                        where a.Telephely.Trim() == Elem.Telephely.Trim()
                                                                        select a).ToList();

                    if (TelephelyiLista != null && TelephelyiLista.Count != 0)
                    {
                        Táblaütemezés.ColumnCount++;
                        for (int i = 0; i < Táblaütemezés.Rows.Count; i++)
                        {
                            DateTime ideig = DateTime.Parse(Táblaütemezés.Rows[i].Cells[0].Value.ToString());
                            List<Adat_TW6000_Ütemezés_Plusz> valami = (from a in TelephelyiLista
                                                                       where a.Vütemezés == ideig
                                                                       select a).ToList();

                            Táblaütemezés.Columns[oszlope].HeaderText = Elem.Telephely.Trim();
                            foreach (Adat_TW6000_Ütemezés_Plusz Elemm in valami)
                            {
                                // Írd ki a kocsikat a telephely alá
                                if (oszlop >= Táblaütemezés.Columns.Count)
                                {
                                    Táblaütemezés.ColumnCount++;
                                }
                                Táblaütemezés.Rows[i].Cells[oszlop].Value = $" {Elemm.Azonosító.Trim()}-{Elemm.Vizsgfoka.Trim()}";
                                switch (Elemm.Státus)
                                {
                                    case 2:
                                        Táblaütemezés.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Italic);
                                        break;
                                    case 4:
                                        Táblaütemezés.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Underline);
                                        break;
                                    case 6:
                                        Táblaütemezés.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                                        break;
                                    case 9:
                                        Táblaütemezés.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Strikeout);
                                        Táblaütemezés.Rows[i].Cells[oszlop].Value += "X";
                                        break;
                                }

                                Adat_TW6000_Színezés Egyszín = (from a in SzínAdatok
                                                                where a.Vizsgálatnév.Trim() == Elemm.Vizsgfoka.Trim()
                                                                select a).FirstOrDefault();
                                if (Egyszín != null)
                                {
                                    Szín_kódolás Színek = Kezelő_Szín.Szín_váltó(Egyszín.Szín);
                                    Táblaütemezés.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(Színek.Piros, Színek.Zöld, Színek.Kék);
                                }

                                if (Elemm.Státus != 9) összes[i]++;
                                if (!Elemm.Vizsgfoka.Contains("21N")) kiemelt[i]++;
                                oszlop++;
                            }
                            // ********telephelyek eltérő színezése***********
                            if (!páros)
                            {
                                for (int ii = oszlope; ii < Táblaütemezés.Columns.Count; ii++)
                                {
                                    Táblaütemezés.Columns[ii].DefaultCellStyle.BackColor = Color.FromArgb(207, 207, 207);
                                }
                            }
                            oszlop = oszlope;
                            Holtart.Lép();
                        }
                        oszlope = Táblaütemezés.Columns.Count;
                        oszlop = oszlope;
                        if (páros) páros = false; else páros = true;
                    }
                }
                ÖsszesítőOszlop(összes, kiemelt);
                Hétvége_Színezése();


                Táblaütemezés.Visible = true;
                Táblaütemezés.Refresh();
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

        /// <summary>
        /// Ha van feltöltve munkaidő naptár akkor a hétvégék az alapján kerülnek színezésben kiemelésre.
        /// Alapértemezés szerint a hétvégék színezését végzi el.
        /// </summary>
        private void Hétvége_Színezése()
        {
            List<Adat_Váltós_Naptár> Adatok = KézVNaptár.Lista_Adatok(Ütemkezdete.Value.Year, "");
            if (Adatok == null || Adatok.Count == 0)
                SzínezHétvégét();
            else
                SzínezMunkaidőNaptárt();
        }

        /// <summary>
        /// Összesíti a TW6000 ütemezés adatait és a járművek adatait egy új listába.
        /// </summary>
        /// <param name="Alapadat"></param>
        /// <param name="Ütemezés"></param>
        /// <returns></returns>
        List<Adat_TW6000_Ütemezés_Plusz> Összesítvalami(List<Adat_Jármű> Alapadat, List<Adat_TW6000_Ütemezés> Ütemezés)
        {
            List<Adat_TW6000_Ütemezés_Plusz> Valami = new List<Adat_TW6000_Ütemezés_Plusz>(); // Inicializáld a listát

            foreach (Adat_TW6000_Ütemezés rekord in Ütemezés)
            {
                string Telephely = (from a in Alapadat
                                    where rekord.Azonosító.Trim() == a.Azonosító.Trim()
                                    select a.Üzem.Trim()).FirstOrDefault();

                Adat_TW6000_Ütemezés_Plusz Elem = new Adat_TW6000_Ütemezés_Plusz(
                    rekord.Azonosító,
                    rekord.Ciklusrend,
                    rekord.Elkészült,
                    rekord.Megjegyzés,
                    rekord.Státus,
                    rekord.Velkészülés,
                    rekord.Vesedékesség,
                    rekord.Vizsgfoka,
                    rekord.Vsorszám,
                    rekord.Vütemezés,
                    rekord.Vvégezte,
                    Telephely ?? "_"
                    );

                Valami.Add(Elem);
            }
            return Valami;
        }

        /// <summary>
        /// Színezze a hétvégéket a táblázatban.
        /// </summary>
        private void SzínezHétvégét()
        {
            try
            {
                for (int sor = 0; sor < Táblaütemezés.RowCount; sor++)
                {
                    string napNeve = Táblaütemezés.Rows[sor].Cells[1].Value?.ToString();

                    if (napNeve == "V")
                    {
                        Táblaütemezés.Rows[sor].DefaultCellStyle.BackColor = Color.FromArgb(228, 189, 141);
                    }
                    else if (napNeve == "Szo")
                    {
                        Táblaütemezés.Rows[sor].DefaultCellStyle.BackColor = Color.FromArgb(186, 176, 165);
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

        /// <summary>
        /// Színezze a munkaidő naptárnak megfeleleően az adatokat a táblázatban.
        /// </summary>
        private void SzínezMunkaidőNaptárt()
        {
            try
            {
                List<Adat_Váltós_Naptár> Adatok = KézVNaptár.Lista_Adatok(Ütemkezdete.Value.Year, "");
                Adatok = (from a in Adatok
                          where a.Dátum >= Ütemkezdete.Value
                          && a.Dátum <= Ütemvége.Value
                          select a).ToList();

                for (int sor = 0; sor < Táblaütemezés.RowCount; sor++)
                {
                    if (DateTime.TryParse(Táblaütemezés.Rows[sor].Cells[0].Value.ToString(), out DateTime hétnapja))
                    {
                        //LINQ lekérdezés
                        Adat_Váltós_Naptár rekord = (from ab in Adatok
                                                     where ab.Dátum == hétnapja
                                                     select ab).FirstOrDefault();
                        //Napok színezése
                        if (rekord != null)
                        {
                            switch (rekord.Nap)
                            {
                                case "P":
                                    Táblaütemezés.Rows[sor].DefaultCellStyle.BackColor = Color.FromArgb(186, 176, 165);
                                    break;
                                case "V":
                                    Táblaütemezés.Rows[sor].DefaultCellStyle.BackColor = Color.FromArgb(228, 189, 141);
                                    break;
                                case "Ü":
                                    Táblaütemezés.Rows[sor].DefaultCellStyle.BackColor = Color.FromArgb(244, 95, 95);
                                    break;
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

        /// <summary>
        /// Kiírja a dátumokat a táblázatba, hogy hozzá lehessen rendelni az adatokat
        /// </summary>
        private void KiírDátumok()
        {
            TimeSpan napokszáma = Ütemvége.Value - Ütemkezdete.Value;
            int napok = napokszáma.Days + 1;
            Táblaütemezés.RowCount = napok;

            for (int i = 0; i < napok; i++)
            {
                Táblaütemezés.Rows[i].Cells[0].Value = Ütemkezdete.Value.AddDays(i).ToString("yyyy.MM.dd");
                Táblaütemezés.Rows[i].Cells[1].Value = Ütemkezdete.Value.AddDays(i).ToString("ddd");
            }
        }

        /// <summary>
        /// Összesítő oszlopok kiírása a táblázat végére
        /// Melyik kategóriába eső karbantartásból mennyi van naponta
        /// </summary>
        /// <param name="összes"></param>
        /// <param name="kiemelt"></param>
        private void ÖsszesítőOszlop(int[] összes, int[] kiemelt)
        {
            Táblaütemezés.ColumnCount++;
            Táblaütemezés.Columns[Táblaütemezés.ColumnCount - 1].HeaderText = "Összes";
            Táblaütemezés.ColumnCount++;
            Táblaütemezés.Columns[Táblaütemezés.ColumnCount - 1].HeaderText = "Kiemelt";

            for (int sor = 0; sor < Táblaütemezés.Rows.Count; sor++)
            {
                Táblaütemezés.Rows[sor].Cells[Táblaütemezés.ColumnCount - 2].Value = összes[sor];
                Táblaütemezés.Rows[sor].Cells[Táblaütemezés.ColumnCount - 1].Value = kiemelt[sor];
                Táblaütemezés.Rows[sor].Cells[Táblaütemezés.ColumnCount - 2].Style.BackColor = Color.LightSkyBlue;
                Táblaütemezés.Rows[sor].Cells[Táblaütemezés.ColumnCount - 1].Style.BackColor = Color.LightSkyBlue;
            }
        }

        /// <summary>
        /// Excel kimenet készítése a listázott adatok alapján
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Excelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "TW6000 ütemezés",
                    FileName = $"TW6000_Ütemterv_{Program.PostásNév}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                MyE.ExcelLétrehozás();
                // megnyitjuk és kitöltjük az excel táblát
                string munkalap = "Munka1";
                MyE.Munkalap_betű("arial", 12);

                // fejléc kiírása
                for (int oszlop = 0; oszlop < Táblaütemezés.ColumnCount; oszlop++)
                {
                    MyE.Kiir(Táblaütemezés.Columns[oszlop].HeaderText, MyE.Oszlopnév(oszlop + 1) + "1");
                    MyE.Háttérszín(MyE.Oszlopnév(oszlop + 1) + "1", Color.Yellow);
                    Holtart.Lép();
                }

                // tartalom kiírása
                for (int sor = 0; sor < Táblaütemezés.RowCount; sor++)
                {
                    Color ideigsor = Táblaütemezés.Rows[sor].DefaultCellStyle.BackColor;
                    if (ideigsor.Name == "0") ideigsor = Color.White;
                    MyE.Háttérszín($"A{sor + 2}:{MyE.Oszlopnév(Táblaütemezés.ColumnCount - 2)}{sor + 2}", ideigsor);

                    for (int oszlop = 0; oszlop < Táblaütemezés.ColumnCount; oszlop++)
                    {
                        if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null)
                        {
                            MyE.Kiir(Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToStrTrim(), MyE.Oszlopnév(oszlop + 1) + (sor + 2).ToString());

                            Color ideig = Táblaütemezés.Rows[sor].Cells[oszlop].Style.BackColor;
                            if (ideig.Name != "0")
                                MyE.Háttérszín(MyE.Oszlopnév(oszlop + 1) + (sor + 2).ToString(), ideig);
                        }
                    }
                    Holtart.Lép();
                }
                // megformázzuk
                int utolsóSor = Táblaütemezés.RowCount + 1;
                string utolsóOszlop = MyE.Oszlopnév(Táblaütemezés.ColumnCount);
                MyE.Rácsoz("A1:" + utolsóOszlop + utolsóSor);
                MyE.Vastagkeret("A1:" + utolsóOszlop + "1");


                MyE.Oszlopszélesség(munkalap, $"A:{utolsóOszlop}");
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + utolsóOszlop + utolsóSor, 0.590551181102362d, 0.590551181102362d,
                    0.78740157480315d, 0.590551181102362d, 0.511811023622047d, 0.511811023622047d, "1", "1", true, "A4", true, true);

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                Holtart.Ki();
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

        /// <summary>
        /// A táblázatba kattintást követően ha van tartalma a cellának, akkor az adatok módosítását lehetővé teszi az ütemezés 
        /// részletes lapon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Táblaütemezés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Üríti_a_mezőket();

                if (e.RowIndex < 0) return;
                if (Táblaütemezés.Columns.Count <= 2) return;
                if (e.ColumnIndex < 1) return;
                if (Táblaütemezés.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

                // kiirjuk a másik fülre a kiválasztott adatokat.
                CiklusTípusfeltöltés();
                string[] darabol = Táblaütemezés.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Split('-');
                string pszám = darabol[0];
                DateTime dátum = Táblaütemezés.Rows[e.RowIndex].Cells[0].Value.ToÉrt_DaTeTime();

                Ürítütemező();

                AdatokÜtem = KézÜtem.Lista_Adatok();
                Adat_TW6000_Ütemezés rekordütem = (from a in AdatokÜtem
                                                   where a.Azonosító == pszám.Trim()
                                                   && a.Vütemezés == dátum
                                                   select a).FirstOrDefault();
                if (rekordütem != null)
                {
                    Üazonosító.Text = rekordütem.Azonosító.Trim();
                    ÜCiklusrend.Text = rekordütem.Ciklusrend.Trim();
                    Ciklussorszámfeltöltés();
                    ÜVizsgfoka.Text = rekordütem.Vizsgfoka.Trim();
                    ÜVSorszám.Text = rekordütem.Vsorszám.ToString();
                    ÜMegjegyzés.Text = rekordütem.Megjegyzés.Trim();
                    ÜVEsedékesség.Value = rekordütem.Vesedékesség;
                    ÜVÜtemezés.Value = rekordütem.Vütemezés;
                    ÜVVégezte.Text = rekordütem.Vvégezte.Trim();
                    ÜVElkészülés.Value = rekordütem.Velkészülés;
                    Üstátus.Text = rekordütem.Státus + " - " + ((MyEn.TW6000_Státusz)rekordütem.Státus).ToString();
                    if (!rekordütem.Elkészült)
                        Üelkészült.Checked = false;
                    else
                        Üelkészült.Checked = true;
                }

                LapFülek.SelectedIndex = 1;
            }
            catch (HibásBevittAdat ex)
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
        /// Előzetes tervet készít a megadott intervallumra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ütemfrissít_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                if (Ütemkezdete.Value > Ütemvége.Value) throw new HibásBevittAdat("A kezdő dátum nem lehet későbbi, mint a vége dátum. Kérlek ellenőrizd és adj meg érvényes időintervallumot.");

                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatokJármű = (from a in AdatokJármű
                               where a.Valóstípus.Contains("TW6000")
                               orderby a.Üzem, a.Azonosító
                               select a).ToList();

                List<Adat_TW6000_Alap> AdatokAlap = KézAlap.Lista_Adatok();

                AdatokÜtem = KézÜtem.Lista_Adatok();

                List<Adat_TW6000_Ütemezés> AdatokGy = new List<Adat_TW6000_Ütemezés>();
                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    Holtart.Lép();
                    // megkeressük, hogy az adott napon mi az ütemezett feladat
                    Adat_TW6000_Alap Elem = (from a in AdatokAlap
                                             where a.Azonosító == rekord.Azonosító && a.Megállítás == false
                                             select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        string ciklusrend = Elem.Ciklusrend.Trim();
                        long ciklusmax = Ciklus_Max(Elem.Ciklusrend.Trim());

                        DateTime start = Elem.Start;

                        int Napokszáma = (int)(Ütemvége.Value - Ütemkezdete.Value).TotalDays + 2;


                        for (int i = 0; i < Napokszáma; i++)
                        {
                            string ciklusküld = ciklusrend.Trim();
                            double napokküld = (double)(Ütemkezdete.Value.AddDays(i - 1) - start).TotalDays;
                            if (ciklusmax <= napokküld)
                            {
                                int darab = (int)(napokküld / ciklusmax);
                                if (darab == napokküld / ciklusmax)
                                    napokküld = ciklusmax;
                                else
                                    napokküld += -ciklusmax * darab;
                            }

                            Adat_Ciklus cikluseredmény = (from a in AdatokCiklus
                                                          where a.Típus.Trim() == Elem.Ciklusrend.Trim() && a.Törölt == "0" && a.Névleges == napokküld
                                                          select a).FirstOrDefault();

                            if (cikluseredmény != null)
                            {
                                // rögzítjük az adatokat az előtervben

                                Adat_TW6000_Ütemezés ÜtemElem = (from a in AdatokÜtem
                                                                 where a.Azonosító == rekord.Azonosító &&
                                                                 a.Vesedékesség.ToShortDateString() == Ütemkezdete.Value.AddDays(i - 1).ToShortDateString()
                                                                 select a).FirstOrDefault();

                                if (ÜtemElem == null)
                                {
                                    Adat_TW6000_Ütemezés ADAT = new Adat_TW6000_Ütemezés(
                                                rekord.Azonosító.ToStrTrim(),
                                                ciklusrend.Trim(),
                                                false,
                                                "_",
                                                0,
                                                new DateTime(1900, 1, 1),
                                                Ütemkezdete.Value.AddDays(i - 1),
                                                cikluseredmény.Vizsgálatfok.Trim(),
                                                cikluseredmény.Sorszám,
                                                Ütemkezdete.Value.AddDays(i - 1),
                                                "_");
                                    AdatokGy.Add(ADAT);
                                }
                            }
                            Holtart.Lép();
                        }
                    }
                }
                KézÜtem.Rögzítés(AdatokGy);
                Holtart.Ki();
                Újkiíró();
            }
            catch (HibásBevittAdat ex)
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
        /// Ütemezés gomb megnyomásakor a táblázatban lévő adatokat ütemezi.
        /// Véglegesíti a tervet innetől kezdve fogja lehívni a program amikor esedékes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnÜtemÜtemezés_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                List<Adat_TW6000_Ütemezés> AdatokGY = new List<Adat_TW6000_Ütemezés>();
                List<Adat_TW6000_Ütemezés> AdatokTárolt = KézÜtem.Lista_Adatok();

                for (int sor = 0; sor < Táblaütemezés.Rows.Count; sor++)
                {
                    DateTime dátum = Táblaütemezés.Rows[sor].Cells[0].Value.ToÉrt_DaTeTime();

                    for (int oszlop = 1; oszlop < Táblaütemezés.ColumnCount; oszlop++)
                    {
                        if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null && Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToStrTrim() != "")
                        {
                            string[] darabol = Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToString().Split('-');
                            string pszám = darabol[0].Trim();
                            Adat_TW6000_Ütemezés Elem = (from a in AdatokTárolt
                                                         where a.Azonosító == pszám && a.Vütemezés == dátum
                                                         select a).FirstOrDefault();
                            if (Elem != null)
                            {
                                // ha tervezési a státusa akkor átállítjuk ütemezettnek
                                if (Elem.Státus == 0)
                                {
                                    Adat_TW6000_Ütemezés ADAT = new Adat_TW6000_Ütemezés(
                                                 pszám.Trim(),
                                                 "Csoportos ütemezés",
                                                 2,
                                                 dátum);
                                    AdatokGY.Add(ADAT);
                                }
                            }
                        }
                        Holtart.Lép();
                    }
                }
                KézÜtem.Módosítás(AdatokGY);
                Holtart.Ki();
                Újkiíró();
            }
            catch (HibásBevittAdat ex)
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
        /// Törli a táblázatban lévő adatokat, ha azok tervezési állapotban vannak.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnÜtemTörlésClick(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("A táblázat adatainak törlésére készül. A program csak akkor törli az adatokat, ha azok tervezési állapotban vannak. Biztos, hogy törli?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
                Holtart.Be();

                List<Adat_TW6000_Ütemezés> AdatokGY = new List<Adat_TW6000_Ütemezés>();
                AdatokÜtem = KézÜtem.Lista_Adatok();

                for (int sor = 0; sor < Táblaütemezés.Rows.Count; sor++)
                {
                    DateTime dátum = Táblaütemezés.Rows[sor].Cells[0].Value.ToÉrt_DaTeTime();
                    for (int oszlop = 1; oszlop < Táblaütemezés.ColumnCount; oszlop++)
                    {
                        if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null && Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToStrTrim() != "")
                        {
                            string pszám = MyF.Szöveg_Tisztítás(Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToStrTrim(), 0, 4).Trim();

                            Adat_TW6000_Ütemezés ÜtemElem = (from a in AdatokÜtem
                                                             where a.Azonosító == pszám.Trim() &&
                                                             a.Vütemezés.ToShortDateString() == dátum.ToÉrt_DaTeTime().ToShortDateString() &&
                                                             a.Státus == 0
                                                             select a).FirstOrDefault();
                            if (ÜtemElem != null)
                            {
                                Adat_TW6000_Ütemezés ADAT = new Adat_TW6000_Ütemezés(pszám, 0, dátum);
                                AdatokGY.Add(ADAT);
                            }
                        }
                    }
                    Holtart.Lép();
                }
                KézÜtem.Törlés(AdatokGY);
                Holtart.Ki();
                Újkiíró();
            }
            catch (HibásBevittAdat ex)
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


        #region Ütemezés részletes lapfül
        /// <summary>
        /// Ciklus típusok feltöltése a comboboxokba
        /// </summary>
        private void CiklusTípusfeltöltés()
        {
            try
            {
                Ciklusrend.Items.Clear();
                ÜCiklusrend.Items.Clear();
                ElőCiklusrend.Items.Clear();

                List<string> Adatok = AdatokCiklus.Select(a => a.Típus).Distinct().ToList();

                foreach (string rekord in Adatok)
                {
                    Ciklusrend.Items.Add(rekord.Trim());
                    ÜCiklusrend.Items.Add(rekord.Trim());
                    ElőCiklusrend.Items.Add(rekord.Trim());
                }
                ElőCiklusrend.Text = "TW6000";
            }
            catch (HibásBevittAdat ex)
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
        /// Ciklus sorszámok feltöltése a comboboxba
        /// </summary>
        private void Ciklussorszámfeltöltés()
        {
            try
            {
                ÜVSorszám.Items.Clear();

                List<Adat_Ciklus> Adatok = (from a in AdatokCiklus
                                            where a.Típus == ÜCiklusrend.Text.Trim()
                                            select a).ToList();

                foreach (Adat_Ciklus rekord in Adatok)
                    ÜVSorszám.Items.Add($"{rekord.Sorszám}-{rekord.Vizsgálatfok}-{rekord.Névleges}");
            }
            catch (HibásBevittAdat ex)
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
        /// Státus feltöltése a comboboxba
        /// </summary>
        private void Státus_feltöltés()
        {
            Üstátus.Items.Clear();
            foreach (MyEn.TW6000_Státusz elem in Enum.GetValues(typeof(MyEn.TW6000_Státusz)))
            {
                Üstátus.Items.Add((int)elem + " - " + elem);
            }
        }

        /// <summary>
        /// A mezők ürítése itt minden mezű alaphelyzetbe áll
        /// </summary>
        private void Üríti_a_mezőket()
        {
            Üazonosító.Text = "";
            ÜCiklusrend.Text = "";
            ÜVSorszám.Text = "";
            ÜVizsgfoka.Text = "";
            ÜVSorszám.Text = "";
            ÜMegjegyzés.Text = "";
            ÜVEsedékesség.Value = new DateTime(1900, 01, 01);
            ÜVÜtemezés.Value = new DateTime(1900, 01, 01);
            ÜVVégezte.Text = "";
            ÜVElkészülés.Value = new DateTime(1900, 01, 01);
            Üstátus.Text = "";
            Üelkészült.Checked = false;
            ÜVEsedékesség.Enabled = false;
        }

        /// <summary>
        /// A mezők ürítése itt nem minden mezű áll alaphelyzetbe
        /// </summary>
        private void Ürítütemező()
        {
            ÜVEsedékesség.Enabled = false;
            Üazonosító.Text = "";
            ÜVizsgfoka.Text = "";
            ÜMegjegyzés.Text = "";
            ÜVEsedékesség.Value = new DateTime(1900, 1, 1);
            ÜVÜtemezés.Value = new DateTime(1900, 1, 1);
            ÜVVégezte.Text = "";
            ÜVElkészülés.Value = new DateTime(1900, 1, 1);
            Üelkészült.Checked = false;
        }

        /// <summary>
        /// Ütemezés részletes lapfülön a rögzítés gomb megnyomásakor a mezőket alapján módosítja, vagy rögzíti az adatok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnÜtemRészRögz_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÜMegjegyzés.Text.Trim() == "") throw new HibásBevittAdat("A megjegyzés mezőt ki kell tölteni!");
                if (Üstátus.Text.Trim() == "" || !Üstátus.Text.Contains("-")) throw new HibásBevittAdat("A státus nem lehet üres mező és '-'-et kell tartalmaznia.");

                string[] darabol = Üstátus.Text.Split('-');
                int sorszám;
                if (ÜVSorszám.Text.Contains("-"))
                {
                    string[] darabolSorszám = ÜVSorszám.Text.Split('-');
                    sorszám = int.Parse(darabolSorszám[0].Trim());
                }
                else
                          if (!int.TryParse(ÜVSorszám.Text, out sorszám)) sorszám = 0;

                AdatokÜtem = KézÜtem.Lista_Adatok();
                Adat_TW6000_Ütemezés Elem = (from a in AdatokÜtem
                                             where a.Azonosító == Üazonosító.Text.Trim() &&
                                             a.Vesedékesség.ToShortDateString() == ÜVEsedékesség.Value.ToShortDateString()
                                             select a).FirstOrDefault();
                Adat_TW6000_Ütemezés ADAT = new Adat_TW6000_Ütemezés(
                             Üazonosító.Text.Trim(),
                             Üazonosító.Text.Trim(),
                             Üelkészült.Checked,
                             ÜMegjegyzés.Text.Trim(),
                             darabol[0].ToÉrt_Long(),
                             ÜVElkészülés.Value,
                             ÜVEsedékesség.Value,
                             ÜVizsgfoka.Text.Trim(),
                             sorszám,
                             ÜVÜtemezés.Value,
                             ÜVVégezte.Text.Trim());
                if (Elem != null)
                    KézÜtem.Módosítás(ADAT);
                else
                    KézÜtem.Rögzítés(ADAT);

                MessageBox.Show("Az adatok rögzítése megtörtént !", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ÜVEsedékesség.Enabled = false;
                LapFülek.SelectedIndex = 2;

                Újkiíró();
            }
            catch (HibásBevittAdat ex)
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
        /// Ütemezés részletes lapfülön a terv gomb megnyomásakor a mezőket kiüríti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnÜtemRészTerv_Click(object sender, EventArgs e)
        {
            Üríti_a_mezőket();
        }

        /// <summary>
        /// Feltölti a telephelyeket a comboba
        /// </summary>
        private void UV_Telephely_feltöltés()
        {
            try
            {
                ÜVVégezte.Items.Clear();
                List<Adat_kiegészítő_telephely> Adatok = KézTelephely.Lista_Adatok().OrderBy(a => a.Sorszám).ToList();

                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    ÜVVégezte.Items.Add(Elem.Telephelykönyvtár);
                ÜVVégezte.Refresh();
            }
            catch (HibásBevittAdat ex)
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
        /// Ciklus sorszámok feltöltése a comboboxba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ÜCiklusrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklussorszámfeltöltés();
        }

        /// <summary>
        /// A kiválasztott ciklus sorszám alapján kiírja a vizsgálatfokát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ÜVSorszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] darabol = ÜVSorszám.Text.Split('-');
            List<Adat_Ciklus> Adatok = KézCiklus.Lista_Adatok();
            string Ideig = (from a in Adatok
                            where a.Típus == ÜCiklusrend.Text.Trim() && a.Törölt == "0" && a.Sorszám == darabol[0].ToÉrt_Long()
                            select a.Vizsgálatfok).FirstOrDefault() ?? "_";
            ÜVizsgfoka.Text = Ideig;
        }
        #endregion


        #region Járműadatok lapfül
        /// <summary>
        /// Feltölti a pályaszámokat a comboboxokba
        /// </summary>
        private void Pályaszám_feltöltés()
        {
            try
            {
                Pályaszám.Items.Clear();
                ÜtemPályaszám.Items.Clear();
                PszJelölő.Items.Clear();

                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Valóstípus.Contains("TW6000")
                          orderby a.Azonosító
                          select a).ToList();
                foreach (Adat_Jármű Elem in Adatok)
                {
                    Pályaszám.Items.Add(Elem.Azonosító);
                    ÜtemPályaszám.Items.Add(Elem.Azonosító);
                    PszJelölő.Items.Add(Elem.Azonosító);
                }

                Pályaszám.Refresh();
                ÜtemPályaszám.Refresh();
                PszJelölő.Refresh();
            }
            catch (HibásBevittAdat ex)
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
        /// A pályaszám kereső gomb megnyomásakor a kiválasztott pályaszám alapján kiírja az alapadatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pályaszámkereső_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a pályaszám.");
                Adat_Jármű Elem = AdatokJármű.Where(a => a.Azonosító == Pályaszám.Text.Trim() && a.Valóstípus == "TW6000").FirstOrDefault();

                if (Elem == null)
                    throw new HibásBevittAdat($"Nincs {Pályaszám.Text.Trim()} pályaszámú jármű!");
                else
                    Alapadatokkiírása();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Pályaszám.Text = "";
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kiírja a kiválasztott pályaszámhoz tartozó alapadatokat a mezőkbe
        /// </summary>
        private void Alapadatokkiírása()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a pályaszám.");

                List<Adat_TW6000_Alap> Adatok = KézAlap.Lista_Adatok();
                Adat_TW6000_Alap rekord = Adatok.Where(a => a.Azonosító == Pályaszám.Text.Trim()).FirstOrDefault();
                if (rekord != null)
                {
                    StartDátum.Value = rekord.Start;
                    Ciklusrend.Text = rekord.Ciklusrend.Trim();
                    Megállítás.Checked = rekord.Megállítás;
                    KötöttStart.Checked = rekord.Kötöttstart;
                    Oka.Text = "";
                    Vizsgdátum.Value = rekord.Vizsgdátum;
                    Vizsgsorszám.Text = rekord.Vizsgsorszám.ToString();
                    VizsgNév.Text = rekord.Vizsgnév.Trim();

                    Ciklussorszámfeltöltés();
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

        /// <summary>
        /// A pályaszám comboboxban a kiválasztott pályaszám alapján kiírja az alapadatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alapadatokkiírása();
        }

        /// <summary>
        /// A járműadatok rögzítése gomb megnyomásakor a mezők alapján módosítja, vagy rögzíti az adatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Járműadatok_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Oka.Text.Trim() == "") throw new HibásBevittAdat("A módosítás oka mezőt ki kell tölteni !");
                if (!int.TryParse(Vizsgsorszám.Text, out int Sorszámvizsg)) throw new HibásBevittAdat("Az utolsó sorszám mezőt ki kell tölteni és egész számnak kell lennie.");
                if (VizsgNév.Text.Trim() == "") throw new HibásBevittAdat("A módosítás név mezőt ki kell tölteni !");

                AdatokAlap = KézAlap.Lista_Adatok();
                Adat_TW6000_Alap Elem = (from a in AdatokAlap
                                         where a.Azonosító == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();

                Adat_TW6000_Alap ADAT = new Adat_TW6000_Alap(
                           Pályaszám.Text.Trim(),
                           Ciklusrend.Text.Trim(),
                           KötöttStart.Checked,
                           Megállítás.Checked,
                           StartDátum.Value,
                           Vizsgdátum.Value,
                           Vizsgsorszám.Text.Trim(),
                           Sorszámvizsg);

                if (Elem == null)
                {
                    KézAlap.Rögzítés(ADAT);
                    KézAlapNapló.Rögzítés(DateTime.Today.Year, ADAT, MyF.Szöveg_Tisztítás(Oka.Text));
                }
                else
                {
                    KézAlap.Módosítás(ADAT);
                    KézAlapNapló.Rögzítés(DateTime.Today.Year, ADAT, MyF.Szöveg_Tisztítás(Oka.Text));
                }

                MessageBox.Show("Az adatok rögzítése megtörtént !", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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
        /// A vizsgálat sorszám comboboxban a kiválasztott sorszám alapján kiírja a vizsgálatfokát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vizsgsorszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Vizsgsorszám.Text, out int SorszámVizsg)) throw new HibásBevittAdat("A vizsgálat sorszámának egész számnak kell lenni.");
                if (Ciklusrend.Text.Trim() == "") throw new HibásBevittAdat("A ciklus rend nem lehet üres mező.");
                CiklusListaFeltöltés();
                Adat_Ciklus Elem = (from a in AdatokCiklus
                                    where a.Típus == Ciklusrend.Text.Trim() &&
                                    a.Sorszám == SorszámVizsg
                                    select a).FirstOrDefault();
                if (Elem != null) VizsgNév.Text = Elem.Vizsgálatfok;
            }
            catch (HibásBevittAdat ex)
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
        /// A kiválasztott ciklus rend alapján kiírja a vizsgálat sorszámokat a comboboxba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ciklusrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklussorszámfeltöltés_Jármű();
        }

        /// <summary>
        /// A kiválasztott ciklus rend alapján kiírja a vizsgálat sorszámokat a comboboxba
        /// </summary>
        private void Ciklussorszámfeltöltés_Jármű()
        {
            try
            {
                Vizsgsorszám.Items.Clear();
                List<Adat_Ciklus> Adatok = KézCiklus.Lista_Adatok().Where(a => a.Típus == Ciklusrend.Text.Trim()).OrderBy(a => a.Sorszám).ToList();
                foreach (Adat_Ciklus rekord in Adatok)
                    Vizsgsorszám.Items.Add($"{rekord.Sorszám}");
                Vizsgsorszám.Refresh();
            }
            catch (HibásBevittAdat ex)
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


        #region Karbantartási előzmények lapfül
        /// <summary>
        /// A karbantartási előzmények táblázatban a kiválasztott pályaszám alapján kiírja az adatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnKarbantartFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                if (NaplóKezdete.Value > NaplóVége.Value) throw new HibásBevittAdat("A kezdő dátum nem lehet későbbi, mint a vége dátum. Kérlek ellenőrizd és adj meg érvényes időintervallumot.");
                if (NaplóKezdete.Value.Year != NaplóVége.Value.Year) throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");

                Napló_Tábla.Rows.Clear();
                Napló_Tábla.Columns.Clear();
                Napló_Tábla.Refresh();
                Napló_Tábla.Visible = false;
                Napló_Tábla.ColumnCount = 11;

                Napló_Tábla.Columns[0].HeaderText = "Rögzítésideje";
                Napló_Tábla.Columns[0].Width = 180;
                Napló_Tábla.Columns[1].HeaderText = "Rögzítő";
                Napló_Tábla.Columns[1].Width = 100;
                Napló_Tábla.Columns[2].HeaderText = "Megjegyzés";
                Napló_Tábla.Columns[2].Width = 280;
                Napló_Tábla.Columns[3].HeaderText = "Azonosító";
                Napló_Tábla.Columns[3].Width = 100;
                Napló_Tábla.Columns[4].HeaderText = "Vizsg. Dátum";
                Napló_Tábla.Columns[4].Width = 110;
                Napló_Tábla.Columns[5].HeaderText = "Sorsz.";
                Napló_Tábla.Columns[5].Width = 100;
                Napló_Tábla.Columns[6].HeaderText = "vizsgfoka";
                Napló_Tábla.Columns[6].Width = 100;
                Napló_Tábla.Columns[7].HeaderText = "Ciklusrend";
                Napló_Tábla.Columns[7].Width = 100;
                Napló_Tábla.Columns[8].HeaderText = "Ciklus start";
                Napló_Tábla.Columns[8].Width = 110;
                Napló_Tábla.Columns[9].HeaderText = "Ciklus állj";
                Napló_Tábla.Columns[9].Width = 100;
                Napló_Tábla.Columns[10].HeaderText = "Kötött start";
                Napló_Tábla.Columns[10].Width = 150;

                List<Adat_TW6000_AlapNapló> Adatok = KézAlapNapló.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Rögzítésiidő >= MyF.Nap0000(NaplóKezdete.Value)
                          && a.Rögzítésiidő <= MyF.Nap2359(NaplóVége.Value)
                          orderby a.Rögzítésiidő descending
                          select a).ToList();
                if (!(NaplóPályaszám.Text.Trim() == "")) Adatok = Adatok.Where(a => a.Azonosító == NaplóPályaszám.Text.Trim()).ToList();
                foreach (Adat_TW6000_AlapNapló rekord in Adatok)
                {
                    Napló_Tábla.RowCount++;
                    int i = Napló_Tábla.RowCount - 1;
                    Napló_Tábla.Rows[i].Cells[0].Value = rekord.Rögzítésiidő.ToStrTrim();
                    Napló_Tábla.Rows[i].Cells[1].Value = rekord.Rögzítő.Trim();
                    Napló_Tábla.Rows[i].Cells[2].Value = rekord.Oka.Trim();
                    Napló_Tábla.Rows[i].Cells[3].Value = rekord.Azonosító.Trim();
                    Napló_Tábla.Rows[i].Cells[4].Value = rekord.Vizsgdátum.ToString("yyyy.MM.dd");
                    Napló_Tábla.Rows[i].Cells[5].Value = rekord.Vizsgsorszám.ToString();
                    Napló_Tábla.Rows[i].Cells[6].Value = rekord.Vizsgnév.Trim();
                    Napló_Tábla.Rows[i].Cells[7].Value = rekord.Ciklusrend.Trim();
                    Napló_Tábla.Rows[i].Cells[8].Value = rekord.Start.ToString("yyyy.MM.dd");
                    Napló_Tábla.Rows[i].Cells[9].Value = rekord.Megállítás ? "Igen" : "Nem";
                    Napló_Tábla.Rows[i].Cells[10].Value = rekord.Kötöttstart ? "Igen" : "Nem";
                }
                Napló_Tábla.Visible = true;
                Napló_Tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
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
        /// A pályaszám comboboxban a kiválasztott Típus alapján kiírja az alapadatokat
        /// </summary>
        private void NaplóPályaszám_feltöltés()
        {
            NaplóPályaszám.Items.Clear();
            List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("főmérnökség");
            Adatok = (from a in Adatok
                      where a.Valóstípus.Contains("TW6000")
                      orderby a.Azonosító
                      select a).ToList();
            foreach (Adat_Jármű rekord in Adatok)
                NaplóPályaszám.Items.Add(rekord.Azonosító.ToStrTrim());
        }

        /// <summary>
        /// A táblázat elemeit kimenti Excel táblába
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnKarbantartExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Napló_Tábla.Rows.Count < 1) throw new HibásBevittAdat("Nincsenek sorok a táblázatban!");

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"TW6000_Karbantartási_előzmények-{Program.PostásTelephely}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép

                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Napló_Tábla);
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


        #region Ütemezés napló lapfül
        /// <summary>
        /// A pályaszám comboboxban a kiválasztott Típus alapján kiírja az alapadatokat
        /// </summary>
        private void ÜtemPályaszám_feltöltés()
        {
            ÜtemPályaszám.Items.Clear();
            List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("főmérnökség");
            Adatok = (from a in Adatok
                      where a.Valóstípus.Contains("TW6000")
                      orderby a.Azonosító
                      select a).ToList();
            foreach (Adat_Jármű rekord in Adatok)
                ÜtemPályaszám.Items.Add(rekord.Azonosító.ToStrTrim());
        }

        /// <summary>
        /// Ütemezés naplózásának kiírása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnÜtemNaplóFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÜtemNaplóKezdet.Value > ÜtemNaplóVége.Value) throw new HibásBevittAdat("A kezdő dátumnnak kisebbnek kell lennie, mint a befejező dátum!");
                if (ÜtemNaplóKezdet.Value.Year != ÜtemNaplóVége.Value.Year) throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");

                ÜtemNapló.Rows.Clear();
                ÜtemNapló.Columns.Clear();
                ÜtemNapló.Refresh();
                ÜtemNapló.Visible = false;
                ÜtemNapló.ColumnCount = 12;

                // fejléc elkészítése
                ÜtemNapló.Columns[0].HeaderText = "Rögzítésideje";
                ÜtemNapló.Columns[0].Width = 160;
                ÜtemNapló.Columns[1].HeaderText = "Azonosító";
                ÜtemNapló.Columns[1].Width = 100;
                ÜtemNapló.Columns[2].HeaderText = "Ciklusrend";
                ÜtemNapló.Columns[2].Width = 100;
                ÜtemNapló.Columns[3].HeaderText = "vizsgfoka";
                ÜtemNapló.Columns[3].Width = 100;
                ÜtemNapló.Columns[4].HeaderText = "Sorsz.";
                ÜtemNapló.Columns[4].Width = 100;
                ÜtemNapló.Columns[5].HeaderText = "megjegyzés";
                ÜtemNapló.Columns[5].Width = 270;
                ÜtemNapló.Columns[6].HeaderText = "vesedékesség";
                ÜtemNapló.Columns[6].Width = 120;
                ÜtemNapló.Columns[7].HeaderText = "vütemezés";
                ÜtemNapló.Columns[7].Width = 100;
                ÜtemNapló.Columns[8].HeaderText = "vvégezte";
                ÜtemNapló.Columns[8].Width = 150;
                ÜtemNapló.Columns[9].HeaderText = "velkészülés";
                ÜtemNapló.Columns[9].Width = 120;
                ÜtemNapló.Columns[10].HeaderText = "státus";
                ÜtemNapló.Columns[10].Width = 160;
                ÜtemNapló.Columns[11].HeaderText = "rögzítő";
                ÜtemNapló.Columns[11].Width = 100;

                List<Adat_TW6000_ÜtemNapló> Adatok = KézÜtemNapló.Lista_Adatok(ÜtemNaplóKezdet.Value.Year);
                Adatok = (from a in Adatok
                          where a.Rögzítésideje >= MyF.Nap0000(ÜtemNaplóKezdet.Value)
                          && a.Rögzítésideje <= MyF.Nap2359(ÜtemNaplóVége.Value)
                          orderby a.Rögzítésideje descending
                          select a).ToList();
                if (ÜtemPályaszám.Text.Trim() != "") Adatok = Adatok.Where(a => a.Azonosító == ÜtemPályaszám.Text.Trim()).ToList();

                foreach (Adat_TW6000_ÜtemNapló rekord in Adatok)
                {
                    ÜtemNapló.RowCount++;
                    int i = ÜtemNapló.RowCount - 1;
                    ÜtemNapló.Rows[i].Cells[0].Value = rekord.Rögzítésideje.ToString();
                    ÜtemNapló.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                    ÜtemNapló.Rows[i].Cells[2].Value = rekord.Ciklusrend.Trim();
                    ÜtemNapló.Rows[i].Cells[3].Value = rekord.Vizsgfoka.Trim();
                    ÜtemNapló.Rows[i].Cells[4].Value = rekord.Vsorszám;
                    ÜtemNapló.Rows[i].Cells[5].Value = rekord.Megjegyzés.Trim();
                    ÜtemNapló.Rows[i].Cells[6].Value = rekord.Vesedékesség.ToString("yyyy.MM.dd");
                    ÜtemNapló.Rows[i].Cells[7].Value = rekord.Vütemezés.ToString("yyyy.MM.dd");
                    ÜtemNapló.Rows[i].Cells[8].Value = rekord.Vvégezte.Trim();
                    ÜtemNapló.Rows[i].Cells[9].Value = rekord.Velkészülés.ToString("yyyy.MM.dd");
                    ÜtemNapló.Rows[i].Cells[10].Value = (MyEn.TW6000_Státusz)rekord.Státus;
                    ÜtemNapló.Rows[i].Cells[11].Value = rekord.Rögzítő.Trim();
                }
                ÜtemNapló.Visible = true;
                ÜtemNapló.Refresh();
            }
            catch (HibásBevittAdat ex)
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
        /// A táblázat elemeit kimenti Excel táblába
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnÜtemNaplóExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÜtemNapló.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"TW6000_Ütemezés_előzmények {Program.PostásTelephely}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, ÜtemNapló);
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


        #region Keresés
        /// <summary>
        /// A kereső gomb megnyomásakor megnyitja a kereső ablakot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keresés_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }

        /// <summary>
        /// A kereső ablakban a keresendő szöveg alapján megkeresi a táblázatban
        /// </summary>
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

        /// <summary>
        /// A kereső ablakban a keresendő szöveg alapján megkeresi a táblázatban
        /// </summary>
        private void Szövegkeresés()
        {
            // megkeressük a szöveget a táblázatban
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;

            if (Táblaütemezés.Rows.Count < 0)
                return;

            for (int sor = 0; sor < Táblaütemezés.Rows.Count; sor++)
            {
                for (int oszlop = 0; oszlop < Táblaütemezés.Columns.Count; oszlop++)
                {
                    if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null && Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToStrTrim().Contains(Új_Ablak_Kereső.Keresendő.Trim()))
                    {
                        Táblaütemezés.Rows[sor].Cells[oszlop].Style.BackColor = Color.Orange;
                        Táblaütemezés.FirstDisplayedScrollingRowIndex = sor;
                        Táblaütemezés.CurrentCell = Táblaütemezés.Rows[sor].Cells[oszlop];
                    }
                }
            }
        }

        /// <summary>
        /// A kereső ablak bezárásakor törli a kereső ablakot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }
        #endregion


        #region telephely sorrend
        Ablak_TW6000_Telephely Új_Ablak_TW6000_Telephely;

        /// <summary>
        /// A telephely lapfül megnyomásakor megnyitja a telephely ablakot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Telephely_lap_Click(object sender, EventArgs e)
        {
            Új_Ablak_TW6000_Telephely?.Close();

            Új_Ablak_TW6000_Telephely = new Ablak_TW6000_Telephely();
            Új_Ablak_TW6000_Telephely.FormClosed += Ablak_TW6000_Telephely_Closed;
            Új_Ablak_TW6000_Telephely.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_TW6000_Telephely.Show();

        }

        /// <summary>
        /// A telephely ablak bezárásakor törli a telephely ablakot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ablak_TW6000_Telephely_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TW6000_Telephely = null;
        }
        #endregion


        #region Színezés
        Ablak_TW6000_Színkezelő Új_Ablak_TW6000_Színkezelő;

        /// <summary>
        /// A színező lapfül megnyomásakor megnyitja a színező ablakot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSzínező_Click(object sender, EventArgs e)
        {
            Új_Ablak_TW6000_Színkezelő?.Close();

            Új_Ablak_TW6000_Színkezelő = new Ablak_TW6000_Színkezelő();
            Új_Ablak_TW6000_Színkezelő.FormClosed += Ablak_TW6000_Színkezelő_Closed;
            Új_Ablak_TW6000_Színkezelő.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_TW6000_Színkezelő.Show();
        }

        /// <summary>
        /// A színező ablak bezárásakor törli a színező ablakot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ablak_TW6000_Színkezelő_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TW6000_Színkezelő = null;
        }
        #endregion


        #region Előtervező lapfül
        /// <summary>
        /// Minden elemet kijelöl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mindentkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, true);
        }

        /// <summary>
        /// Minden kijelölést töröl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kijelöléstörlése_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);
        }

        /// <summary>
        /// A telephely comboboxban a kiválasztott Típus alapján kiírja az üzemeket
        /// </summary>
        private void Telephelylista_feltöltés()
        {
            Telephely.Items.Clear();
            List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
            Adatok = (from a in Adatok
                      where a.Valóstípus.Contains("TW6000")
                      orderby a.Üzem
                      select a).ToList();
            List<string> Üzemek = Adatok.Select(a => a.Üzem).Distinct().ToList();
            foreach (string rekord in Üzemek)
                Telephely.Items.Add(rekord);
        }

        /// <summary>
        /// A pályaszám comboboxban a kiválasztott Típus alapján kiírja az pályaszámokat
        /// </summary>
        private void Pszlista_feltöltés()
        {
            try
            {
                PszJelölő.Items.Clear();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Valóstípus.Contains("TW6000")
                          orderby a.Azonosító
                          select a).Distinct().ToList();
                foreach (Adat_Jármű rekord in Adatok)
                    PszJelölő.Items.Add(rekord.Azonosító.ToStrTrim());
            }
            catch (HibásBevittAdat ex)
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
        /// A vizsgálat comboboxban a kiválasztott Típus alapján kiírja az alapadatokat
        /// </summary>
        private void Vizsgálatfeltöltés()
        {
            try
            {
                VizsgálatLista.Items.Clear();
                int volt;
                List<Adat_Ciklus> Adatok = KézCiklus.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Típus == ElőCiklusrend.Text.Trim()
                          && a.Törölt == "0"
                          orderby a.Sorszám
                          select a).ToList();

                foreach (Adat_Ciklus rekord in Adatok)
                {
                    volt = 0;
                    for (int i = 0; i < VizsgálatLista.Items.Count; i++)
                    {
                        if (VizsgálatLista.Items[i].ToStrTrim() == rekord.Vizsgálatfok.ToStrTrim()) volt = 1;
                    }
                    if (volt == 0) VizsgálatLista.Items.Add(rekord.Vizsgálatfok.ToStrTrim());
                }

                for (int i = 0; i < VizsgálatLista.Items.Count; i++)
                    VizsgálatLista.SetItemChecked(i, true);
            }
            catch (HibásBevittAdat ex)
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
        /// A telephely comboboxban a kiválasztott Típus alapján kiírja az pályaszámokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnElőtervezőFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Telephely.Text.Trim() == "") throw new HibásBevittAdat("Jelölj ki egy telephelyet!");
                if (Előkezdődátum.Value > ElőbefejezőDátum.Value) throw new HibásBevittAdat("A kezdő dátumnnak kisebbnek kell lennie, mint a befejező dátum!");
                if (Előkezdődátum.Value.Year != ElőbefejezőDátum.Value.Year) throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");

                PszJelölő.Items.Clear();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Valóstípus.Contains("TW6000")
                          && a.Törölt == false
                          && a.Üzem == Telephely.Text.Trim()
                          orderby a.Azonosító
                          select a).ToList();

                foreach (Adat_Jármű rekord in Adatok)
                    PszJelölő.Items.Add(rekord.Azonosító.ToStrTrim());
            }
            catch (HibásBevittAdat ex)
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElőCiklusrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgálatfeltöltés();
        }

        /// <summary>
        /// Előterv készítéséhez elkészít egy adatbázist, hogy ne okozzon zavart
        /// Valamint előtervet készít a megfelelő intervallumban és kocsikon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnElőtervezőKeres_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                if (PszJelölő.CheckedItems.Count < 1) throw new HibásBevittAdat("Legalább egy kocsit ki kell jelölni.");
                if (Előkezdődátum.Value >= ElőbefejezőDátum.Value) throw new HibásBevittAdat("A kezdő dátumnnak kisebbnek kell lennie, mint a befejező dátum!");
                if (VizsgálatLista.CheckedItems.Count < 1) throw new HibásBevittAdat("Ki kell választani legalább egy karbantartási ciklust!");

                Alaptábla();
                Egyhónaprögzítése();
                Exceltábla_Kimutatás();
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

        /// <summary>
        /// Előterv készítéséhez elkészít egy adatbázist, hogy ne okozzon zavart
        /// </summary>
        private void Alaptábla()
        {
            try
            {
                if (Check1.Checked) return;
                string hova = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\TW6000adatok.mdb";

                if (File.Exists(hova) && !Check1.Checked) File.Delete(hova);
                if (!File.Exists(hova)) Adatbázis_Létrehozás.TW6000tábla(hova);

                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatokAlap = KézAlap.Lista_Adatok();

                List<Adat_TW6000_Alap> AdatokGy = new List<Adat_TW6000_Alap>();
                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    Adat_Jármű Elem = (from a in AdatokJármű
                                       where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim() &&
                                       a.Törölt == false
                                       select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        // ha nincs törölve a pályaszám
                        Adat_TW6000_Alap rekord = (from a in AdatokAlap
                                                   where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                                   select a).FirstOrDefault();
                        if (rekord != null) AdatokGy.Add(rekord);
                        Holtart.Lép();
                    }
                }
                KézElőterv.Rögzítés(hova, AdatokGy);
            }
            catch (HibásBevittAdat ex)
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
        /// Elkészíti a tervet
        /// </summary>
        private void Egyhónaprögzítése()
        {
            try
            {
                string hova = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\TW6000adatok.mdb";
                if (!File.Exists(hova)) return;

                List<Adat_TW6000_Alap> Adatok = KézElőterv.Lista_Adatok(hova);

                List<Adat_TW6000_Ütemezés> AdatokGy = new List<Adat_TW6000_Ütemezés>();
                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    // pörgetjük a pályaszámokat
                    Adat_TW6000_Alap rekord = (from a in Adatok
                                               where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                               select a).FirstOrDefault();

                    if (rekord != null)
                    {
                        long Ciklusmax = Ciklus_Max(rekord.Ciklusrend); //a maximális napszám
                        long Ciklussormax = Ciklus_Sorszám(Ciklusmax, rekord.Ciklusrend); // a max vizsgálat száma
                        DateTime startdátum = rekord.Start; // az a dátum ahonnan a kocsi ciklusát kezdjük. 
                        double Nap = (Előkezdődátum.Value - startdátum).TotalDays;   // megkeressük, hogy mi a két dátum között az első vizsgálat dátuma.
                        double NapE = (Előkezdődátum.Value - startdátum).TotalDays;   //Ezzel később számolunk 
                        int HányszorFordult = (int)Math.Round(Nap / Ciklusmax, 1);   //Hányszor végezték el rajta a teljes ciklust

                        if (Ciklusmax <= Nap)
                        {
                            if (HányszorFordult == Nap / Ciklusmax)
                                Nap = 0;
                            else
                                Nap = (Ciklusmax * HányszorFordult);
                        }

                        startdátum = rekord.Start.AddDays(Nap);   // Beállítjuk az utolsó ciklus elejére
                        Adat_Ciklus ElőzőKarb = (from a in AdatokCiklus
                                                 where a.Névleges < NapE - Nap
                                                 && a.Típus == rekord.Ciklusrend
                                                 orderby a.Névleges descending
                                                 select a).FirstOrDefault();
                        long sorszám = 0;
                        DateTime futódátum = startdátum;  //Ezt futatjuk, amíg el nem éri a maximumot
                        if (ElőzőKarb != null)
                        {
                            sorszám = ElőzőKarb.Sorszám; // az utolsó vizsgálat sorszáma
                            futódátum = rekord.Start.AddDays(ElőzőKarb.Névleges + Nap);     //utolsó vizsgálat dátuma
                        }


                        while (ElőbefejezőDátum.Value >= futódátum)
                        {
                            sorszám++;
                            if (sorszám > Ciklussormax)
                            {
                                sorszám = 1;//A nagy Ciklus végén visszaállítjuk a sorszámot
                                startdátum = futódátum; //beállítjuk az új nagy ciklus elejét
                                Nap = 0;
                            }
                            Adat_Ciklus KövetkezőKarb = (from a in AdatokCiklus
                                                         where a.Sorszám == sorszám
                                                         && a.Típus == rekord.Ciklusrend
                                                         select a).FirstOrDefault();
                            futódátum = startdátum.AddDays(KövetkezőKarb.Névleges); // a következő vizsgálat dátuma

                            Adat_TW6000_Ütemezés ADATSor = new Adat_TW6000_Ütemezés(
                                   PszJelölő.CheckedItems[j].ToStrTrim(),
                                   rekord.Ciklusrend,
                                   false, "_", 0, new DateTime(1900, 1, 1),
                                   futódátum,
                                   KövetkezőKarb.Vizsgálatfok,
                                   sorszám,
                                   futódátum,
                                   Telephelykereső(PszJelölő.Items[j].ToStrTrim()));
                            AdatokGy.Add(ADATSor);
                        }
                    }
                    Holtart.Lép();
                }
                KézElőterv.Rögzítés(hova, AdatokGy);
            }
            catch (HibásBevittAdat ex)
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
        /// Megkeresi melyik telephelyen van a kocsi
        /// </summary>
        /// <param name="azonosító"></param>
        /// <returns></returns>
        private string Telephelykereső(string azonosító)
        {
            string TelephelykeresőRet = "_";
            List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
            Adat_Jármű rekord = (from a in Adatok
                                 where a.Azonosító == azonosító.Trim()
                                 select a).FirstOrDefault();
            if (rekord != null) TelephelykeresőRet = rekord.Üzem.Trim();

            return TelephelykeresőRet;
        }

        /// <summary>
        /// Kimutatás készítése
        /// </summary>
        private void Exceltábla_Kimutatás()
        {
            try
            {
                string[] cím = new string[4];
                string[] leírás = new string[4];

                // paraméter tábla feltöltése
                cím[1] = "Adatok";
                leírás[1] = "Előtervezett adatok";
                cím[2] = "Vizsgálatok";
                leírás[2] = "Vizsgálati adatok havonta";
                cím[3] = "Éves_terv";
                leírás[3] = "Vizsgálati adatok éves";
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Javítások előtervezése",
                    FileName = $"TW6000_javítások_előtervezése_{Program.PostásNév}_{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };

                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                string munkalap = "Tartalom";
                MyE.ExcelLétrehozás();
                MyE.Munkalap_átnevezés("Munka1", munkalap);

                for (int i = 1; i < 4; i++)
                    MyE.Új_munkalap(cím[i]);

                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.Kiir("Munkalapfül", "a1");
                MyE.Kiir("Leírás", "b1");

                for (int i = 1; i <= 3; i++)
                {

                    MyE.Link_beillesztés(munkalap, "A" + (i + 1).ToString(), cím[i].Trim());
                    MyE.Kiir(leírás[i], "b" + (i + 1).ToString());
                }
                MyE.Oszlopszélesség(munkalap, "A:B");


                //// ****************************************************
                //// Elkészítjük a munkalapokat
                //// ****************************************************

                long sor = Adatoklistázása();
                if (sor > 2)        //Azért kell mert nem tud csak 2 soros táblából kimutatást készíteni
                {
                    Kimutatás();
                    Kimutatás1();
                }

                MyE.Munkalap_aktív("Tartalom");
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

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

        /// <summary>
        /// Kiírja a vizsgálati adatokat
        /// </summary>
        private long Adatoklistázása()
        {
            long válasz = 0;
            try
            {
                string munkalap = "Adatok";
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                // fejlécet kiírjuk
                MyE.Kiir("Pályaszám", "a3");
                MyE.Kiir("ciklusrend", "b3");
                MyE.Kiir("elkészült", "c3");
                MyE.Kiir("Megjegyzés", "d3");
                MyE.Kiir("státus", "e3");
                MyE.Kiir("elkészülés", "f3");
                MyE.Kiir("esedékesség", "g3");
                MyE.Kiir("vizsgálat", "h3");
                MyE.Kiir("v. sorszám", "i3");
                MyE.Kiir("ütemezés", "j3");
                MyE.Kiir("végezte", "k3");
                MyE.Kiir("Év", "l3");
                MyE.Kiir("Hónap", "m3");

                string hely = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\TW6000adatok.mdb";
                if (VizsgálatLista.CheckedItems.Count < 1) return válasz;

                AdatokÜtem = KézElőterv.Lista_AdatokÜtem(hely);

                List<Adat_TW6000_Ütemezés> AdatokGy = new List<Adat_TW6000_Ütemezés>();
                for (int i = 0; i < VizsgálatLista.CheckedItems.Count; i++)
                {
                    List<Adat_TW6000_Ütemezés> Ideig = (from a in AdatokÜtem
                                                        where a.Vizsgfoka == VizsgálatLista.CheckedItems[i].ToStrTrim()
                                                        select a).ToList();
                    AdatokGy.AddRange(Ideig);
                }

                AdatokGy = (from a in AdatokGy
                            orderby a.Azonosító, a.Vütemezés
                            select a).ToList();
                int sor = 4;
                if (AdatokGy.Count > 0) válasz = AdatokGy.Count;
                foreach (Adat_TW6000_Ütemezés rekord in AdatokGy)
                {
                    MyE.Kiir(rekord.Azonosító.Trim(), "a" + sor);
                    MyE.Kiir(rekord.Ciklusrend.Trim(), "b" + sor);
                    MyE.Kiir(rekord.Elkészült.ToString(), "c" + sor);
                    MyE.Kiir(rekord.Megjegyzés.Trim(), "d" + sor);
                    MyE.Kiir(rekord.Státus.ToString(), "e" + sor);
                    MyE.Kiir(rekord.Velkészülés.ToString("yyyy.MM.dd"), "f" + sor);
                    MyE.Kiir(rekord.Vesedékesség.ToString("yyyy.MM.dd"), "g" + sor);
                    MyE.Kiir(rekord.Vizsgfoka.Trim(), "h" + sor);
                    MyE.Kiir(rekord.Vsorszám.ToString(), "i" + sor);
                    MyE.Kiir(rekord.Vütemezés.ToString("yyyy.MM.dd"), "j" + sor);
                    MyE.Kiir(rekord.Vvégezte.Trim(), "k" + sor);
                    MyE.Kiir(rekord.Vütemezés.Year.ToString(), "l" + sor);
                    MyE.Kiir(rekord.Vütemezés.Month.ToString(), "m" + sor);
                    sor++;
                    Holtart.Lép();
                }

                // megformázzuk
                MyE.Aktív_Cella(munkalap, "A:m");
                MyE.Aktív_Cella(munkalap, "m1");
                MyE.Oszlopszélesség(munkalap, "A:m");
                MyE.Vastagkeret("a3:m3");
                MyE.Rácsoz("a3:m" + (sor - 1).ToString());
                MyE.Vastagkeret("a3:m" + (sor - 1).ToString());
                MyE.Vastagkeret("a3:m3");

                // szűrő
                MyE.Szűrés(munkalap, $"a3:m{sor}", 3);

                // ablaktábla rögzítése
                MyE.Tábla_Rögzítés(3);

                // kiírjuk a tábla méretét
                MyE.Munkalap_aktív("Vizsgálatok");
                MyE.Kiir((sor - 1).ToString(), "aa1");
                MyE.Munkalap_aktív("Éves_terv");
                MyE.Kiir((sor - 1).ToString(), "aa1");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

        private void Kimutatás()
        {
            try
            {
                string munkalap = "Vizsgálatok";

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");
                // beolvassuk a sor végét
                int sor = int.Parse(MyE.Beolvas("aa1"));


                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "M" + sor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Hónap");


                SzűrőNév.Add("végezte");
                SzűrőNév.Add("év");

                oszlopNév.Add("vizsgálat");

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kimutatás1()
        {
            try
            {
                string munkalap = "Éves_terv";

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");
                // beolvassuk a sor végét
                int sor = int.Parse(MyE.Beolvas("aa1"));


                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "M" + sor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("év");

                SzűrőNév.Add("végezte");

                oszlopNév.Add("vizsgálat");

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            catch (HibásBevittAdat ex)
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


        #region Ciklus

        private long Ciklus_Sorszám(double napokszáma, string ciklusrend)
        {
            long válasz = (from a in AdatokCiklus
                           where a.Típus.Trim() == ciklusrend && a.Névleges == napokszáma
                           select a.Sorszám).FirstOrDefault();
            return válasz;
        }

        private long Ciklus_Max(string ciklusrend)
        {
            long válasz = (from a in AdatokCiklus
                           where a.Típus.Trim() == ciklusrend.Trim()
                           select a).Max(x => x.Névleges);
            return válasz;
        }
        #endregion


        #region ListaFeltöltések
        private void CiklusListaFeltöltés()
        {
            try
            {
                AdatokCiklus.Clear();
                AdatokCiklus = KézCiklus.Lista_Adatok();
                AdatokCiklus = (from a in AdatokCiklus
                                orderby a.Típus, a.Sorszám
                                select a).ToList();
            }
            catch (HibásBevittAdat ex)
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