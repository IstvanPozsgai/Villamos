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
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public partial class Ablak_TW6000_Tulajdonság
    {

        readonly string TW6000_Villamos = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos4TW.mdb";
        readonly string Ciklus_Hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
        readonly string Villamos_Hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
        readonly string TW6000_Napló = $@"{Application.StartupPath}\Főmérnökség\napló\naplóTW6000_{DateTime.Today:yyyy}.mdb";
        readonly string TW6000_Napló_Ütem = $@"{Application.StartupPath}\Főmérnökség\napló\naplóTW6000Ütem_{DateTime.Today:yyyy}.mdb";

        Ablak_Kereső Új_Ablak_Kereső;

        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_TW6000_Ütemezés KézÜtem = new Kezelő_TW6000_Ütemezés();
        readonly Kezelő_TW6000_Alap KézAlap = new Kezelő_TW6000_Alap();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        List<Adat_TW6000_Ütemezés> AdatokÜtem = new List<Adat_TW6000_Ütemezés>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_TW6000_Alap> AdatokAlap = new List<Adat_TW6000_Alap>();

        public Ablak_TW6000_Tulajdonság()
        {
            InitializeComponent();

        }

        private void Tulajdonság_TW6000_Load(object sender, EventArgs e)
        {
            // ellenőrizzük az alap adatok táblát
            if (!File.Exists(TW6000_Villamos)) Adatbázis_Létrehozás.TW6000tábla(TW6000_Villamos.Trim());
            if (!File.Exists(TW6000_Napló)) Adatbázis_Létrehozás.TW6000táblanapló(TW6000_Napló.Trim());
            if (!File.Exists(TW6000_Napló_Ütem)) Adatbázis_Létrehozás.TW6000ütemnapló(TW6000_Napló_Ütem.Trim());

            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Pályaszám_feltöltés();
            CiklusListaFeltöltés();
            JárműListaFeltöltés();

            Ütemkezdete.Value = DateTime.Today;
            Ütemvége.Value = DateTime.Today;
            Vizsgdátum.Value = DateTime.Today;
            ÜtemNaplóKezdet.Value = DateTime.Today;
            ÜtemNaplóVége.Value = DateTime.Today;
            NaplóKezdete.Value = DateTime.Today;
            NaplóVége.Value = DateTime.Today;
            Előkezdődátum.Value = DateTime.Today;
            ElőbefejezőDátum.Value = DateTime.Today;

            LapFülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Ablak_TW6000_Tulajdonság_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TW6000_Telephely?.Close();
            Új_Ablak_TW6000_Színkezelő?.Close();
            Új_Ablak_Kereső?.Close();
        }


        #region Alap

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());

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


        private void Btn_súgó_Click(object sender, EventArgs e)
        {
            string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\TW6000_ütem.html";
            MyE.Megnyitás(hely);
        }


        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


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


        private void Terv_lista_Click(object sender, EventArgs e)
        {
            Újkiíró();
        }


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

                //telephelyek sorrendje
                string hely = TW6000_Villamos;
                string jelszó = "czapmiklós";
                string szöveg = "SELECT * FROM telephely order by sorrend";
                int oszlop = 2;
                int oszlope = 2;

                Kezelő_TW600_Telephely kézTel = new Kezelő_TW600_Telephely();
                List<Adat_TW6000_Telephely> AdatokTel = kézTel.Lista_Adatok(hely, jelszó, szöveg);

                //Színlista betöltése
                szöveg = "SELECT * FROM szinezés";
                Kezelő_TW600_Színezés KézSzín = new Kezelő_TW600_Színezés();
                List<Adat_TW6000_Színezés> SzínAdatok = KézSzín.Lista_Adatok(hely, jelszó, szöveg);


                //listázzuk a járműveket, amik TW6000 típusúak
                szöveg = $"SELECT * FROM állománytábla WHERE valóstípus='TW6000' AND törölt=0";
                Kezelő_Jármű KézJ = new Kezelő_Jármű();
                string jelszóm = "pozsgaii";
                List<Adat_Jármű> AdatokJ = KézJ.Lista_Adatok(Villamos_Hely, jelszóm, szöveg);

                szöveg = "SELECT * FROM ütemezés WHERE ";
                szöveg += $" (vütemezés>=#{Ütemkezdete.Value:M-d-yy}";
                szöveg += $"# AND vütemezés<=#{Ütemvége.Value:M-d-yy}#) ORDER BY azonosító";

                AdatokÜtem = KézÜtem.Lista_Adatok(hely, jelszó, szöveg);

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
                            if (páros == false)
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


        private void Hétvége_Színezése()
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Ütemkezdete.Value:yyyy}\munkaidőnaptár.mdb";
            if (!File.Exists(hely))
                SzínezHétvégét();
            else
                SzínezMunkaidőNaptárt();
        }


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


        private void SzínezMunkaidőNaptárt()
        {
            try
            {

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Ütemkezdete.Value:yyyy}\munkaidőnaptár.mdb";
                string jelszó = "katalin";
                string szöveg = $"SELECT * FROM naptár ";
                szöveg += $"WHERE (dátum>=#{Ütemkezdete.Value:M-d-yy}";
                szöveg += $"# AND dátum<=#{Ütemvége.Value:M-d-yy}#)";

                Kezelő_Váltós_Naptár Kéz = new Kezelő_Váltós_Naptár();
                List<Adat_Váltós_Naptár> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);


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
                            MyE.Kiir(Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToString().Trim(), MyE.Oszlopnév(oszlop + 1) + (sor + 2).ToString());

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


        private void Táblaütemezés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Üríti_a_mezőket();

                if (e.RowIndex < 0)
                    return;
                if (Táblaütemezés.Columns.Count <= 2)
                    return;
                if (e.ColumnIndex < 1)
                    return;
                if (Táblaütemezés.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    return;

                // kiirjuk a másik fülre a kiválasztott adatokat.
                CiklusTípusfeltöltés();
                string[] darabol = Táblaütemezés.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Split('-');
                string pszám = darabol[0];
                DateTime dátum = Táblaütemezés.Rows[e.RowIndex].Cells[0].Value.ToÉrt_DaTeTime();

                Ürítütemező();

                string hely = TW6000_Villamos.Trim();
                string jelszó = "czapmiklós";
                string szöveg = $"SELECT * FROM ütemezés WHERE azonosító='{pszám.Trim()}' AND vütemezés=#{dátum:MM-dd-yyyy}#";


                Adat_TW6000_Ütemezés rekordütem = KézÜtem.Egy_Adat(hely, jelszó, szöveg);
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



        private void Ütemfrissít_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                if (Ütemkezdete.Value > Ütemvége.Value) throw new HibásBevittAdat("A kezdő dátum nem lehet későbbi, mint a vége dátum. Kérlek ellenőrizd és adj meg érvényes időintervallumot.");

                string hely = Villamos_Hely.Trim();
                string helyalap = TW6000_Villamos.Trim();
                string helynapló = TW6000_Napló_Ütem.Trim();
                string jelszó = "pozsgaii";
                string jelszóalap = "czapmiklós";

                string szöveg = "SELECT * FROM állománytábla WHERE valóstípus='TW6000' ORDER BY üzem, azonosító";
                Kezelő_Jármű kéz = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJármű = kéz.Lista_Adatok(hely, jelszó, szöveg);

                Kezelő_TW6000_Alap KézAlap = new Kezelő_TW6000_Alap();
                Adat_TW6000_Alap Elem;

                double napokküld;
                DateTime start;
                int darab;

                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    // megkeressük, hogy az adott napon mi az ütemezett feladat
                    szöveg = $"SELECT * FROM alap WHERE azonosító='{rekord.Azonosító.Trim()}' and megállítás=0";
                    Elem = KézAlap.Egy_Adat(helyalap, jelszóalap, szöveg);
                    if (Elem != null)
                    {
                        string ciklusrend = Elem.Ciklusrend.Trim();
                        long ciklusmax = Ciklus_Max(Elem.Ciklusrend.Trim());

                        start = Elem.Start;

                        int Napokszáma = (int)(Ütemvége.Value - Ütemkezdete.Value).TotalDays + 2;

                        for (int i = 0; i < Napokszáma; i++)
                        {
                            string ciklusküld = ciklusrend.Trim();
                            napokküld = (double)(Ütemkezdete.Value.AddDays(i - 1) - start).TotalDays;
                            if (ciklusmax <= napokküld)
                            {
                                darab = (int)(napokküld / ciklusmax);
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
                                ÜtemListaFeltöltés();
                                Adat_TW6000_Ütemezés ÜtemElem = (from a in AdatokÜtem
                                                                 where a.Azonosító == rekord.Azonosító &&
                                                                 a.Vesedékesség.ToShortDateString() == Ütemkezdete.Value.AddDays(i - 1).ToShortDateString()
                                                                 select a).FirstOrDefault();

                                if (ÜtemElem == null)
                                {
                                    szöveg = "INSERT INTO ütemezés (azonosító, ciklusrend, elkészült, megjegyzés, ";
                                    szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                                    szöveg += " vsorszám, vütemezés, vvégezte) VALUES (";
                                    szöveg += $"'{rekord.Azonosító.ToString().Trim()}', ";
                                    szöveg += $"'{ciklusrend.Trim()}', ";
                                    szöveg += "false, '_', 0, '1900.01.01', ";
                                    szöveg += $"'{Ütemkezdete.Value.AddDays(i - 1):yyyy.MM.dd}', ";
                                    szöveg += $"'{cikluseredmény.Vizsgálatfok.Trim()}', ";
                                    szöveg += $"{cikluseredmény.Sorszám}, ";
                                    szöveg += $"'{Ütemkezdete.Value.AddDays(i - 1):yyyy.MM.dd}', ";
                                    szöveg += "'_' )";
                                    MyA.ABMódosítás(helyalap, jelszóalap, szöveg);

                                    // naplózás
                                    szöveg = "INSERT INTO ütemezésnapló (azonosító, ciklusrend, elkészült, megjegyzés, ";
                                    szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                                    szöveg += " vsorszám, vütemezés, vvégezte, rögzítő, rögzítésideje) VALUES (";
                                    szöveg += $"'{rekord.Azonosító.ToString().Trim()}', ";
                                    szöveg += $"'{ciklusküld.Trim()}', ";
                                    szöveg += "false, '_', 0, '1900.01.01', ";
                                    szöveg += $"'{Ütemkezdete.Value.AddDays(i - 1):yyyy.MM.dd}', ";
                                    szöveg += $"'{cikluseredmény.Vizsgálatfok.Trim()}', ";
                                    szöveg += $"{cikluseredmény.Sorszám}, ";
                                    szöveg += $"'{Ütemkezdete.Value.AddDays(i - 1):yyyy.MM.dd}', ";
                                    szöveg += $"'_', '{Program.PostásNév.Trim()}', '{DateTime.Now}' )";
                                    MyA.ABMódosítás(helynapló, jelszóalap, szöveg);

                                }
                            }
                            Holtart.Lép();
                        }
                    }
                }
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



        private void BtnÜtemÜtemezés_Click(object sender, EventArgs e)
        {
            try
            {
                string helyalap = TW6000_Villamos.Trim();
                string helynapló = TW6000_Napló_Ütem.Trim();
                string jelszó = "czapmiklós";
                DateTime dátum;
                string pszám;
                Holtart.Be();
                for (int sor = 0; sor < Táblaütemezés.Rows.Count; sor++)
                {
                    dátum = DateTime.Parse(Táblaütemezés.Rows[sor].Cells[0].Value.ToString());

                    for (int oszlop = 1; oszlop < Táblaütemezés.ColumnCount; oszlop++)
                    {
                        if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null && Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToString().Trim() != "")
                        {
                            string[] darabol = Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToString().Split('-');
                            pszám = darabol[0].Trim();
                            // beolvassuk az adatokat a naplózáshoz
                            string szöveg = $"SELECT * FROM ütemezés WHERE azonosító='{pszám.Trim()}' AND vütemezés=#{dátum:yyyy-MM-dd}#";


                            Adat_TW6000_Ütemezés Adatok = KézÜtem.Egy_Adat(helyalap, jelszó, szöveg);

                            jelszó = "czapmiklós";

                            if (Adatok != null)
                            {
                                // ha tervezési a státusa akkor átállítjuk ütemezettnek
                                if (Adatok.Státus == 0)
                                {
                                    szöveg = "UPDATE ütemezés SET ";
                                    szöveg += " státus=2, megjegyzés='Csoportos ütemezés' ";
                                    szöveg += $" WHERE azonosító='{pszám.Trim()}'";
                                    szöveg += $" AND vütemezés=#{dátum:MM-dd-yyyy}#";
                                    ÜMegjegyzés.Text = "Csoportos ütemezés";

                                    jelszó = "czapmiklós";
                                    MyA.ABMódosítás(helyalap, jelszó, szöveg);

                                    // naplózás
                                    szöveg = "INSERT INTO ütemezésnapló (azonosító, ciklusrend, elkészült, megjegyzés, ";
                                    szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                                    szöveg += " vsorszám, vütemezés, vvégezte, rögzítő, rögzítésideje) VALUES (";
                                    szöveg += $"'{Adatok.Azonosító.Trim()}', "; // azonosító
                                    szöveg += $"'{Adatok.Ciklusrend.Trim()}', "; // ciklusrend
                                    if (Adatok.Elkészült)
                                        szöveg += " true, ";
                                    else
                                        szöveg += " false, "; // elkészült

                                    szöveg += $" '{Adatok.Megjegyzés.Trim()}', "; // megjegyzés
                                    szöveg += " 2, "; // státus 
                                    szöveg += $" '{Adatok.Velkészülés:yyyy.MM.dd}', "; // velkészülés
                                    szöveg += $"'{Adatok.Vesedékesség:yyyy.MM.dd}', "; // vesedékesség
                                    szöveg += $" '{Adatok.Vizsgfoka.Trim()}', "; // vizsgfoka
                                    szöveg += $"{Adatok.Vsorszám}, "; // vsorszám
                                    szöveg += $" '{Adatok.Vütemezés:yyyy.MM.dd}', ";  // vütemezés
                                    szöveg += $" '{Adatok.Vvégezte.Trim()}', "; // vvégezte
                                    szöveg += $" '{Program.PostásNév.Trim()}', "; // rögzítő
                                    szöveg += $" '{DateTime.Now}' )";

                                    jelszó = "czapmiklós";
                                    MyA.ABMódosítás(helynapló, jelszó, szöveg);
                                }
                            }
                        }
                    }
                    Holtart.Lép();
                }
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


        private void BtnÜtemTörlésClick(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("A táblázat adatainak törlésére készül. A program csak akkor törli az adatokat, ha azok tervezési állapotban vannak. Biztos, hogy törli?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
                Holtart.Be();
                string helyalap = TW6000_Villamos.Trim();
                string jelszó = "czapmiklós";
                string szöveg;
                string dátum = DateTime.Now.ToString("MM-dd-yyyy");
                string pszám;

                for (int sor = 0; sor < Táblaütemezés.Rows.Count; sor++)
                {
                    dátum = Táblaütemezés.Rows[sor].Cells[0].Value.ToString();
                    for (int oszlop = 1; oszlop < Táblaütemezés.ColumnCount; oszlop++)
                    {
                        if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null && Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToString().Trim() != "")
                        {
                            pszám = MyF.Szöveg_Tisztítás(Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToString().Trim(), 0, 4);
                            ÜtemListaFeltöltés();
                            Adat_TW6000_Ütemezés ÜtemElem = (from a in AdatokÜtem
                                                             where a.Azonosító == pszám.Trim() &&
                                                             a.Vütemezés.ToShortDateString() == dátum.ToÉrt_DaTeTime().ToShortDateString() &&
                                                             a.Státus == 0
                                                             select a).FirstOrDefault();
                            if (ÜtemElem != null)
                            {
                                szöveg = $"DELETE FROM  ütemezés WHERE azonosító='{pszám.Trim()}'";
                                szöveg += $" AND vütemezés=#{dátum.ToÉrt_DaTeTime():MM-dd-yyyy}#";
                                szöveg += " AND státus=0";
                                MyA.ABtörlés(helyalap, jelszó, szöveg);
                            }

                        }
                    }
                    Holtart.Lép();
                }
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


        #region Járműadatok lapfül
        private void Pályaszám_feltöltés()
        {
            Pályaszám.Items.Clear();
            ÜtemPályaszám.Items.Clear();
            PszJelölő.Items.Clear();

            string hely = Villamos_Hely;
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM állománytábla where valóstípus='TW6000' ORDER BY azonosító";

            Pályaszám.BeginUpdate();
            Pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
            Pályaszám.EndUpdate();
            Pályaszám.Refresh();

            ÜtemPályaszám.BeginUpdate();
            ÜtemPályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
            ÜtemPályaszám.EndUpdate();
            ÜtemPályaszám.Refresh();

            PszJelölő.BeginUpdate();
            PszJelölő.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
            PszJelölő.EndUpdate();
            PszJelölő.Refresh();
        }


        private void Pályaszámkereső_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a pályaszám.");
                Adat_Jármű Elem = AdatokJármű.Where(a => a.Azonosító == Pályaszám.Text.Trim() && a.Valóstípus == "TW6000").FirstOrDefault();

                if (Elem == null)
                {
                    throw new HibásBevittAdat($"Nincs {Pályaszám.Text.Trim()} pályaszámú jármű!");
                }
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


        private void Alapadatokkiírása()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a pályaszám.");

                // km adatok feltöltése
                string hely = TW6000_Villamos.Trim();
                string jelszó = "czapmiklós";
                string szöveg = $"SELECT * FROM Alap where azonosító='{Pályaszám.Text.Trim()}'";

                Kezelő_TW6000_Alap kéz = new Kezelő_TW6000_Alap();
                Adat_TW6000_Alap rekord = kéz.Egy_Adat(hely, jelszó, szöveg);
                if (rekord != null)
                {
                    Start.Value = rekord.Start;
                    Ciklusrend.Text = rekord.Ciklusrend.Trim();
                    if (rekord.Megállítás)
                        Megállítás.Checked = true;
                    else
                        Megállítás.Checked = false;
                    if (rekord.Kötöttstart)
                        KötöttStart.Checked = true;
                    else
                        KötöttStart.Checked = false;
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


        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alapadatokkiírása();
        }


        private void Járműadatok_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Oka.Text.Trim() == "") throw new HibásBevittAdat("A módosítás oka mezőt ki kell tölteni !");
                if (!int.TryParse(Vizsgsorszám.Text, out int Sorszámvizsg)) throw new HibásBevittAdat("Az utolsó sorszám mezőt ki kell tölteni és egész számnak kell lennie.");
                if (VizsgNév.Text.Trim() == "") throw new HibásBevittAdat("A módosítás név mezőt ki kell tölteni !");

                AlapListaFeltöltés();
                Adat_TW6000_Alap Elem = (from a in AdatokAlap
                                         where a.Azonosító == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();
                string szöveg;
                string hely = TW6000_Villamos;
                string jelszó = "czapmiklós";
                if (Elem == null)
                {
                    // új adat
                    szöveg = "INSERT INTO alap (azonosító, start, ciklusrend, megállítás, kötöttstart, vizsgsorszám, vizsgnév, vizsgdátum) VALUES (";
                    szöveg += $"'{Pályaszám.Text.Trim()}', ";
                    szöveg += $"'{Start.Value:yyyy.MM.dd}', ";
                    szöveg += $"'{Ciklusrend.Text.Trim()}', ";
                    if (Megállítás.Checked)
                        szöveg += "true, ";
                    else
                        szöveg += "false, ";
                    if (KötöttStart.Checked)
                        szöveg += "true, ";
                    else
                        szöveg += "false, ";
                    szöveg += $"{Sorszámvizsg}, ";
                    szöveg += $"'{Vizsgsorszám.Text.Trim()}', ";
                    szöveg += $"'{Vizsgdátum.Value:yyyy.MM.dd}') ";
                }
                else
                {
                    // adatmódosítás
                    szöveg = "UPDATE alap SET ";
                    szöveg += $" Start='{Start.Value:yyyy.MM.dd}', ";
                    szöveg += $" ciklusrend='{Ciklusrend.Text.Trim()}', ";
                    szöveg += " megállítás=";
                    if (Megállítás.Checked)
                        szöveg += "true, ";
                    else
                        szöveg += "false, ";
                    szöveg += " kötöttstart=";
                    if (KötöttStart.Checked)
                        szöveg += "true, ";
                    else
                        szöveg += "false, ";
                    szöveg += $" vizsgsorszám={Sorszámvizsg}, ";
                    szöveg += $" vizsgnév='{Vizsgsorszám.Text.Trim()}', ";
                    szöveg += $" vizsgdátum='{Vizsgdátum.Value:yyyy.MM.dd}' ";
                    szöveg += $" WHERE azonosító='{Pályaszám.Text.Trim()} '";
                }

                MyA.ABMódosítás(hely, jelszó, szöveg);

                // naplózás
                hely = TW6000_Napló.Trim();

                szöveg = "INSERT INTO alapnapló (azonosító, start, ciklusrend, megállítás, kötöttstart, vizsgsorszám, vizsgnév, vizsgdátum, oka, rögzítő, rögzítésiidő) VALUES (";
                szöveg += $"'{Pályaszám.Text.Trim()}', ";
                szöveg += $"'{Start.Value:yyyy.MM.dd}', ";
                szöveg += $"'{Ciklusrend.Text.Trim()}', ";
                if (Megállítás.Checked)
                    szöveg += "true, ";

                else
                    szöveg += "false, ";

                if (KötöttStart.Checked)
                    szöveg += "true, ";

                else
                    szöveg += "false, ";
                szöveg += $"{Sorszámvizsg}, ";
                szöveg += $"'{Vizsgsorszám.Text.Trim()}', ";
                szöveg += $"'{Vizsgdátum.Value:yyyy.MM.dd}', ";
                szöveg += $"'{Oka.Text.Trim()}', ";
                szöveg += $"'{Program.PostásTelephely.Trim()}', ";
                szöveg += $"'{DateTime.Now}') ";

                MyA.ABMódosítás(hely, jelszó, szöveg);
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


        private void Ciklusrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklussorszámfeltöltés_Jármű();
        }


        private void Ciklussorszámfeltöltés_Jármű()
        {
            try
            {
                Vizsgsorszám.Items.Clear();

                string hely = Ciklus_Hely.Trim();
                string jelszó = "pocsaierzsi";

                string szöveg = $"SELECT * FROM ciklusrendtábla where [típus]='{Ciklusrend.Text.Trim()}' AND törölt=false ORDER BY sorszám";
                Kezelő_Ciklus kéz = new Kezelő_Ciklus();
                List<Adat_Ciklus> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Ciklus rekord in Adatok)
                {
                    Vizsgsorszám.Items.Add($"{rekord.Sorszám}");
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



        #region Karbantartási előzmények lapfül
        private void BtnKarbantartFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                if (NaplóKezdete.Value > NaplóVége.Value)
                    throw new HibásBevittAdat("A kezdő dátum nem lehet későbbi, mint a vége dátum. Kérlek ellenőrizd és adj meg érvényes időintervallumot.");

                if (NaplóKezdete.Value.Year != NaplóVége.Value.Year)
                    throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");

                string hely = TW6000_Napló.Trim();
                string jelszó = "czapmiklós";
                string szöveg = $"SELECT * FROM alapnapló WHERE rögzítésiidő>=#{NaplóKezdete.Value:MM-dd-yyyy} 00:00:0#";
                szöveg += $" AND rögzítésiidő<=#{NaplóVége.Value:MM-dd-yyyy} 23:59:0#";

                if (!(NaplóPályaszám.Text.Trim() == ""))
                    szöveg += $" AND azonosító='{NaplóPályaszám.Text.Trim()}'";

                szöveg += " ORDER BY rögzítésiidő DESC";

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

                Kezelő_TW600_AlapNapló kéz = new Kezelő_TW600_AlapNapló();
                List<Adat_TW6000_AlapNapló> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_TW6000_AlapNapló rekord in Adatok)
                {
                    Napló_Tábla.RowCount++;
                    int i = Napló_Tábla.RowCount - 1;
                    Napló_Tábla.Rows[i].Cells[0].Value = rekord.Rögzítésiidő.ToString().Trim();
                    Napló_Tábla.Rows[i].Cells[1].Value = rekord.Rögzítő.Trim();
                    Napló_Tábla.Rows[i].Cells[2].Value = rekord.Oka.Trim();
                    Napló_Tábla.Rows[i].Cells[3].Value = rekord.Azonosító.Trim();
                    Napló_Tábla.Rows[i].Cells[4].Value = rekord.Vizsgdátum.ToString("yyyy.MM.dd");
                    Napló_Tábla.Rows[i].Cells[5].Value = rekord.Vizsgsorszám.ToString();
                    Napló_Tábla.Rows[i].Cells[6].Value = rekord.Vizsgnév.Trim();
                    Napló_Tábla.Rows[i].Cells[7].Value = rekord.Ciklusrend.Trim();
                    Napló_Tábla.Rows[i].Cells[8].Value = rekord.Start.ToString("yyyy.MM.dd");

                    if (!rekord.Megállítás)
                        Napló_Tábla.Rows[i].Cells[9].Value = "Nem";
                    else
                        Napló_Tábla.Rows[i].Cells[9].Value = "Igen";

                    if (!rekord.Kötöttstart)
                        Napló_Tábla.Rows[i].Cells[10].Value = "Nem";
                    else
                        Napló_Tábla.Rows[i].Cells[10].Value = "Igen";
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


        private void NaplóPályaszám_feltöltés()
        {
            NaplóPályaszám.Items.Clear();
            string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM Állománytábla WHERE [törölt]= false AND valóstípus='TW6000' ORDER BY azonosító ";

            Kezelő_Jármű kéz = new Kezelő_Jármű();
            List<Adat_Jármű> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_Jármű rekord in Adatok)
                NaplóPályaszám.Items.Add(rekord.Azonosító.ToString().Trim());
        }


        private void BtnKarbantartExcel_Click(object sender, EventArgs e)
        {
            if (Napló_Tábla.Rows.Count < 1)
                throw new HibásBevittAdat("Nincsenek sorok a táblázatban!");

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

            fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
            MyE.EXCELtábla(fájlexc, Napló_Tábla, false);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyE.Megnyitás(fájlexc + ".xlsx");
        }
        #endregion


        #region Ütemezés részletes lapfül
        private void CiklusTípusfeltöltés()
        {
            try
            {
                Ciklusrend.Items.Clear();
                ÜCiklusrend.Items.Clear();
                ElőCiklusrend.Items.Clear();

                List<string> Adatok = AdatokCiklus.Distinct(new ÖHasonlít_Adat_Ciklus_Típus()).Select(x => x.Típus).ToList();

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


        private void Státus_feltöltés()
        {
            Üstátus.Items.Clear();
            foreach (MyEn.TW6000_Státusz elem in Enum.GetValues(typeof(MyEn.TW6000_Státusz)))
            {
                Üstátus.Items.Add((int)elem + " - " + elem);
            }
        }


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


        private void BtnÜtemRészRögz_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÜMegjegyzés.Text.Trim() == "") throw new HibásBevittAdat("A megjegyzés mezőt ki kell tölteni!");
                if (Üstátus.Text.Trim() == "" || !Üstátus.Text.Contains("-")) throw new HibásBevittAdat("A státus nem lehet üres mező és '-'-et kell tartalmaznia.");

                string helynapló = TW6000_Napló_Ütem.Trim();
                string helyalap = TW6000_Villamos.Trim();
                string jelszó = "czapmiklós";
                string[] darabol = Üstátus.Text.Split('-');
                int sorszám;
                if (ÜVSorszám.Text.Contains("-"))
                {
                    string[] darabolSorszám = ÜVSorszám.Text.Split('-');
                    sorszám = int.Parse(darabolSorszám[0].Trim());
                }
                else
                          if (!int.TryParse(ÜVSorszám.Text, out sorszám)) sorszám = 0;

                ÜtemListaFeltöltés();
                Adat_TW6000_Ütemezés Elem = (from a in AdatokÜtem
                                             where a.Azonosító == Üazonosító.Text.Trim() &&
                                             a.Vesedékesség.ToShortDateString() == ÜVEsedékesség.Value.ToShortDateString()
                                             select a).FirstOrDefault();
                string szöveg;
                if (Elem != null)
                {
                    szöveg = $"UPDATE ütemezés SET ciklusrend='{ÜCiklusrend.Text.Trim()}', ";

                    if (!Üelkészült.Checked)
                        szöveg += "elkészült=false, ";
                    else
                        szöveg += "elkészült=true, ";

                    szöveg += $"megjegyzés='{ÜMegjegyzés.Text.Trim()}', ";
                    szöveg += $"státus={darabol[0]}, ";
                    szöveg += $"velkészülés='{ÜVElkészülés.Value:yyyy.MM.dd}', ";
                    szöveg += $"vizsgfoka='{ÜVizsgfoka.Text.Trim()}', ";
                    szöveg += $"vsorszám={sorszám}, ";
                    szöveg += $"vütemezés='{ÜVÜtemezés.Value:yyyy.MM.dd}', ";
                    szöveg += $"vvégezte='{ÜVVégezte.Text.Trim()}'";
                    szöveg += $"WHERE azonosító='{Üazonosító.Text.Trim()}'";
                    szöveg += $" and vesedékesség=#{ÜVEsedékesség.Value:MM-dd-yyyy}#";
                }
                else
                {
                    szöveg = "INSERT INTO ütemezés (azonosító, ciklusrend, elkészült, megjegyzés, ";
                    szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                    szöveg += " vsorszám, vütemezés, vvégezte) VALUES (";
                    szöveg += $"'{Üazonosító.Text.Trim()}', "; // azonosító
                    szöveg += $"'{ÜCiklusrend.Text.Trim()}', "; // ciklusrend

                    if (!Üelkészült.Checked)
                        szöveg += "false, ";
                    else
                        szöveg += "true, "; // elkészült

                    szöveg += $" '{ÜMegjegyzés.Text.Trim()}', "; // megjegyzés
                    szöveg += $"{darabol[0]}, "; // státus 
                    szöveg += $" '{ÜVElkészülés.Value:yyyy.MM.dd}', "; // velkészülés
                    szöveg += $"'{ÜVEsedékesség.Value:yyyy.MM.dd}', "; // vesedékesség
                    szöveg += $"'{ÜVizsgfoka.Text.Trim()}', "; // vizsgfoka
                    szöveg += $"{sorszám}, "; // vsorszám
                    szöveg += $"'{ÜVÜtemezés.Value:yyyy.MM.dd}', ";  // vütemezés
                    szöveg += $"'{ÜVVégezte.Text.Trim()}') "; // vvégezte

                }
                jelszó = "czapmiklós";
                MyA.ABMódosítás(helyalap, jelszó, szöveg);

                // naplózás
                szöveg = "INSERT INTO ütemezésnapló (azonosító, ciklusrend, elkészült, megjegyzés, ";
                szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                szöveg += " vsorszám, vütemezés, vvégezte, rögzítő, rögzítésideje) VALUES (";
                szöveg += $"'{Üazonosító.Text.Trim()}', "; // azonosító
                szöveg += $"'{ÜCiklusrend.Text.Trim()}', "; // ciklusrend

                if (!Üelkészült.Checked)
                    szöveg += "false, ";
                else
                    szöveg += "true, "; // elkészült

                szöveg += $" '{ÜMegjegyzés.Text.Trim()}', "; // megjegyzés
                szöveg += $"{darabol[0]}, "; // státus 
                szöveg += $" '{ÜVElkészülés.Value:yyyy.MM.dd}', "; // velkészülés
                szöveg += $"'{ÜVEsedékesség.Value:yyyy.MM.dd}', "; // vesedékesség
                szöveg += $"'{ÜVizsgfoka.Text.Trim()}', "; // vizsgfoka
                szöveg += $"{sorszám}, "; // vsorszám
                szöveg += $"'{ÜVÜtemezés.Value:yyyy.MM.dd}', ";  // vütemezés
                szöveg += $"'{ÜVVégezte.Text.Trim()}', "; // vvégezte
                szöveg += $"'{Program.PostásNév.Trim()}',"; // rögzítő
                szöveg += $"'{DateTime.Now}' )";

                jelszó = "czapmiklós";
                MyA.ABMódosítás(helynapló, jelszó, szöveg);

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


        private void BtnÜtemRészTerv_Click(object sender, EventArgs e)
        {
            ÜVEsedékesség.Enabled = true;
        }


        private void UV_Telephely_feltöltés()
        {
            ÜVVégezte.Items.Clear();

            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM telephelytábla order by sorszám";

            ÜVVégezte.BeginUpdate();
            ÜVVégezte.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelykönyvtár"));
            ÜVVégezte.EndUpdate();
            ÜVVégezte.Refresh();
        }


        private void ÜCiklusrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklussorszámfeltöltés();
        }


        private void ÜVSorszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hely = Ciklus_Hely.ToLowerInvariant();

            string[] darabol = ÜVSorszám.Text.Split('-');
            string jelszó = "pocsaierzsi";
            string szöveg = $"SELECT * FROM ciklusrendtábla";

            Kezelő_Ciklus KéZCiklus = new Kezelő_Ciklus();
            List<Adat_Ciklus> Adatok = KéZCiklus.Lista_Adatok(hely, jelszó, szöveg);


            string Ideig = (from a in Adatok
                            where a.Típus == ÜCiklusrend.Text.Trim() && a.Törölt == "0" && a.Sorszám == darabol[0].ToÉrt_Long()
                            select a.Vizsgálatfok).FirstOrDefault() ?? "_";
            ÜVizsgfoka.Text = Ideig;
        }


        #endregion


        #region Ütemezés napló lapfül

        private void ÜtemPályaszám_feltöltés()
        {
            ÜtemPályaszám.Items.Clear();
            string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM Állománytábla WHERE [törölt]= false AND valóstípus='TW6000' ORDER BY azonosító ";

            Kezelő_Jármű kéz = new Kezelő_Jármű();
            List<Adat_Jármű> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_Jármű rekord in Adatok)
                ÜtemPályaszám.Items.Add(rekord.Azonosító.ToString().Trim());
        }


        private void BtnÜtemNaplóFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÜtemNaplóKezdet.Value > ÜtemNaplóVége.Value)
                    throw new HibásBevittAdat("A kezdő dátumnnak kisebbnek kell lennie, mint a befejező dátum!");

                if (ÜtemNaplóKezdet.Value.Year != ÜtemNaplóVége.Value.Year)
                    throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");

                string hely = TW6000_Napló_Ütem.Trim();
                string jelszó = "czapmiklós";
                string szöveg = $"SELECT * FROM ütemezésnapló WHERE rögzítésideje>=#{ÜtemNaplóKezdet.Value:MM-dd-yyyy} 00:00:0#";
                szöveg += $" AND rögzítésideje<=#{ÜtemNaplóVége.Value:MM-dd-yyyy} 23:59:0#";

                if (ÜtemPályaszám.Text.Trim() != "")
                    szöveg += $" AND azonosító='{ÜtemPályaszám.Text.Trim()}'";

                szöveg += " ORDER BY rögzítésideje DESC";
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

                Kezelő_TW600_ÜtemNapló kéz = new Kezelő_TW600_ÜtemNapló();
                List<Adat_TW6000_ÜtemNapló> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

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


        private void BtnÜtemNaplóExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÜtemNapló.Rows.Count <= 0)
                    return;

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

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, ÜtemNapló, false);
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


        #region Keresés
        private void Keresés_Click(object sender, EventArgs e)
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

        private void Szövegkeresés()
        {
            // megkeressük a szöveget a táblázatban
            if (Új_Ablak_Kereső.Keresendő == null)
                return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "")
                return;

            if (Táblaütemezés.Rows.Count < 0)
                return;

            for (int sor = 0; sor < Táblaütemezés.Rows.Count; sor++)
            {
                for (int oszlop = 0; oszlop < Táblaütemezés.Columns.Count; oszlop++)
                {
                    if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null && Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToString().Trim().Contains(Új_Ablak_Kereső.Keresendő.Trim()))
                    {
                        Táblaütemezés.Rows[sor].Cells[oszlop].Style.BackColor = Color.Orange;
                        Táblaütemezés.FirstDisplayedScrollingRowIndex = sor;
                        Táblaütemezés.CurrentCell = Táblaütemezés.Rows[sor].Cells[oszlop];
                    }
                }
            }
        }


        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }

        #endregion


        #region telephely sorrend
        Ablak_TW6000_Telephely Új_Ablak_TW6000_Telephely;
        private void Telephely_lap_Click(object sender, EventArgs e)
        {
            Új_Ablak_TW6000_Telephely?.Close();

            Új_Ablak_TW6000_Telephely = new Ablak_TW6000_Telephely();
            Új_Ablak_TW6000_Telephely.FormClosed += Ablak_TW6000_Telephely_Closed;
            Új_Ablak_TW6000_Telephely.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_TW6000_Telephely.Show();

        }

        private void Ablak_TW6000_Telephely_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TW6000_Telephely = null;
        }

        #endregion


        #region Színezés
        Ablak_TW6000_Színkezelő Új_Ablak_TW6000_Színkezelő;
        private void BtnSzínező_Click(object sender, EventArgs e)
        {
            Új_Ablak_TW6000_Színkezelő?.Close();

            Új_Ablak_TW6000_Színkezelő = new Ablak_TW6000_Színkezelő();
            Új_Ablak_TW6000_Színkezelő.FormClosed += Ablak_TW6000_Színkezelő_Closed;
            Új_Ablak_TW6000_Színkezelő.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_TW6000_Színkezelő.Show();
        }

        private void Ablak_TW6000_Színkezelő_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TW6000_Színkezelő = null;
        }

        #endregion


        #region Előtervező lapfül

        private void Mindentkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, true);
        }


        private void Kijelöléstörlése_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);
        }


        private void Telephelylista_feltöltés()
        {
            Telephely.Items.Clear();

            string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM Állománytábla WHERE [törölt]= false AND valóstípus='TW6000' ORDER BY üzem";
            string szöveg0 = "";

            Kezelő_Jármű kéz = new Kezelő_Jármű();
            List<Adat_Jármű> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_Jármű rekord in Adatok)
            {
                if (rekord.Üzem != null && szöveg0.Trim() != rekord.Üzem.Trim())
                {
                    Telephely.Items.Add(rekord.Üzem.Trim());
                    szöveg0 = rekord.Üzem.Trim();
                }
            }
        }


        private void Pszlista_feltöltés()
        {
            PszJelölő.Items.Clear();
            string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM Állománytábla WHERE [törölt]= false AND valóstípus='TW6000' ORDER BY azonosító ";

            Kezelő_Jármű kéz = new Kezelő_Jármű();
            List<Adat_Jármű> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_Jármű rekord in Adatok)
                PszJelölő.Items.Add(rekord.Azonosító.ToString().Trim());
        }


        private void Vizsgálatfeltöltés()
        {
            try
            {
                VizsgálatLista.Items.Clear();

                string hely = Ciklus_Hely.Trim();
                string jelszó = "pocsaierzsi";
                string szöveg = $"SELECT * FROM ciklusrendtábla WHERE [típus]='{ElőCiklusrend.Text.Trim()}' AND törölt='0' ORDER BY sorszám";
                int volt;

                Kezelő_Ciklus kéz = new Kezelő_Ciklus();
                List<Adat_Ciklus> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Ciklus rekord in Adatok)
                {
                    volt = 0;
                    for (int i = 0; i < VizsgálatLista.Items.Count; i++)
                    {
                        if (VizsgálatLista.Items[i].ToString().Trim() == rekord.Vizsgálatfok.ToString().Trim())
                            volt = 1;
                    }
                    if (volt == 0)
                        VizsgálatLista.Items.Add(rekord.Vizsgálatfok.ToString().Trim());
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


        private void BtnElőtervezőFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Telephely.Text.Trim() == "")
                    throw new HibásBevittAdat("Jelölj ki egy telephelyet!");

                if (Előkezdődátum.Value > ElőbefejezőDátum.Value)
                    throw new HibásBevittAdat("A kezdő dátumnnak kisebbnek kell lennie, mint a befejező dátum!");

                if (Előkezdődátum.Value.Year != ElőbefejezőDátum.Value.Year)
                    throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");

                PszJelölő.Items.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM Állománytábla WHERE [törölt]= false AND  valóstípus='TW6000' AND üzem='{Telephely.Text.Trim()}' ORDER BY azonosító";

                Kezelő_Jármű kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = kéz.Lista_Jármű_állomány(hely, jelszó, szöveg);
                foreach (Adat_Jármű rekord in Adatok)
                    PszJelölő.Items.Add(rekord.Azonosító.ToString().Trim());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ElőCiklusrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgálatfeltöltés();
        }


        private void BtnElőtervezőKeres_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                if (PszJelölő.CheckedItems.Count < 1)
                    throw new HibásBevittAdat("Legalább egy kocsit ki kell jelölni.");

                if (Előkezdődátum.Value >= ElőbefejezőDátum.Value)
                    throw new HibásBevittAdat("A kezdő dátumnnak kisebbnek kell lennie, mint a befejező dátum!");

                if (VizsgálatLista.CheckedItems.Count < 1)
                    throw new HibásBevittAdat("Ki kell választani legalább egy karbantartási ciklust!");

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


        private void Alaptábla()
        {
            try
            {
                if (Check1.Checked) return;
                string hova = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\TW6000adatok.mdb";

                if (File.Exists(hova) && !Check1.Checked) File.Delete(hova);
                if (!File.Exists(hova)) Adatbázis_Létrehozás.TW6000tábla(hova);

                // kilistázzuk az Adatbázis adatait
                string jelszóhonnanhova = "czapmiklós";

                JárműListaFeltöltés();
                AlapListaFeltöltés();

                List<string> SzövegGy = new List<string>();
                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    Adat_Jármű Elem = (from a in AdatokJármű
                                       where a.Azonosító == PszJelölő.CheckedItems[j].ToString().Trim() &&
                                       a.Törölt == false
                                       select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        // ha nincs törölve a pályaszám
                        Adat_TW6000_Alap rekord = (from a in AdatokAlap
                                                   where a.Azonosító == PszJelölő.CheckedItems[j].ToString().Trim()
                                                   select a).FirstOrDefault();
                        if (rekord != null)
                        {
                            string szöveg = "INSERT INTO alap (azonosító, start, ciklusrend, megállítás, kötöttstart, vizsgsorszám, vizsgnév, vizsgdátum) VALUES (";
                            szöveg += $"'{rekord.Azonosító.ToString().Trim()}', ";
                            szöveg += $"'{rekord.Start}', ";
                            szöveg += $"'{rekord.Ciklusrend.ToString().Trim()}', ";
                            szöveg += $"{rekord.Megállítás}, ";
                            szöveg += $"{rekord.Kötöttstart}, ";
                            szöveg += $"'{rekord.Vizsgsorszám.ToString().Trim()}', ";
                            szöveg += $"'{rekord.Vizsgnév.ToString().Trim()}', ";
                            szöveg += $"'{rekord.Vizsgdátum}') ";

                            SzövegGy.Add(szöveg);
                        }
                        Holtart.Lép();
                    }
                }
                MyA.ABMódosítás(hova, jelszóhonnanhova, SzövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Egyhónaprögzítése()
        {
            try
            {
                string hova = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\TW6000adatok.mdb";
                string jelszóhova = "czapmiklós";
                string helyciklus = $@"{Application.StartupPath}\főmérnökség\adatok\ciklus.mdb";

                if (!File.Exists(hova))
                    return;


                string Ciklusküld;
                double Ciklussormax;
                int darab;
                string cikluseredmény = "";

                DateTime előkezdődátumValue = Előkezdődátum.Value;
                DateTime előbefejezőDátumValue = ElőbefejezőDátum.Value;
                TimeSpan időtartam = előbefejezőDátumValue - előkezdődátumValue;
                double idő = időtartam.TotalDays;

                DateTime futódátum;
                double Napokküld = default;
                double Előzőnap;
                DateTime Start;
                double napokszáma;
                DateTime startdátum = default;
                double előzősor;
                double sorszám;
                double sorosnap;

                string szöveg = $"SELECT * FROM alap";
                List<Adat_TW6000_Alap> Adatok = KézAlap.Lista_Adatok(hova, jelszóhova, szöveg);

                TimeSpan időtartam2 = előbefejezőDátumValue - előkezdődátumValue;
                napokszáma = (int)időtartam2.TotalDays;

                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    // pörgetjük a pályaszámokat
                    cikluseredmény = "";
                    szöveg = $"SELECT * FROM alap WHERE [azonosító]='{PszJelölő.Items[j].ToString().Trim()}'";
                    Adat_TW6000_Alap rekord = (from a in Adatok
                                               where a.Azonosító == PszJelölő.Items[j].ToString().Trim()
                                               select a).FirstOrDefault();

                    if (rekord != null)
                    {
                        Ciklusküld = rekord.Ciklusrend; // melyik ciklusrend szerint 
                        long Ciklusmax = Ciklus_Max(Ciklusküld.Trim());// hogy a soron következő vizsgálat hány nap múlva esedékes

                        Ciklussormax = Ciklus_Sorszám(Ciklusmax, Ciklusküld); // a soron következő vizsgálat száma

                        Start = rekord.Start; // az a dátum ahonnan a kocsi ciklusát kezdjük.

                        // megkeressük, hogy mi a két dátum között az első vizsgálat dátuma.
                        for (int i = 0; i < (int)Math.Round(napokszáma); i++)
                        {
                            futódátum = Előkezdődátum.Value.AddDays(i);
                            Napokküld = (int)(futódátum - Start).TotalDays;

                            if (Ciklusmax <= Napokküld)
                            {
                                darab = (int)Math.Round(Napokküld / Ciklusmax);
                                if (darab == Napokküld / Ciklusmax)
                                    Napokküld = Ciklusmax;
                                else
                                    Napokküld += -Ciklusmax * darab;
                            }
                            cikluseredmény = Ciklus_Vizsgálat(Napokküld, Ciklusküld).Trim();
                            if (!(cikluseredmény.Trim() == ""))
                            {
                                // első dátum amitől kezdjük a pörgést
                                startdátum = Előkezdődátum.Value.AddDays(i);
                                break;
                            }
                        }
                        // ha meg van a első elem akkor rögzítjük
                        if (!(cikluseredmény.Trim() == ""))
                        {
                            szöveg = "INSERT INTO ütemezés (azonosító, ciklusrend, elkészült, megjegyzés, ";
                            szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                            szöveg += " vsorszám, vütemezés, vvégezte) VALUES (";
                            szöveg += $"'{PszJelölő.Items[j].ToString().Trim()}', "; // azonosító
                            szöveg += $"'{Ciklusküld.Trim()}', "; // ciklusrend
                            szöveg += "false, '_', 0, '1900.01.01', "; // elkészül, Megjegyzés,státus, velkészülés
                            szöveg += $"'{startdátum:yyyy.MM.dd}', "; // esedékesség
                            szöveg += $"'{cikluseredmény.Trim()}', "; // vizsgálatfoka
                            előzősor = Ciklus_Sorszám(Napokküld, Ciklusküld);
                            szöveg += $"{előzősor}, "; // sorszám
                            szöveg += $"'{startdátum:yyyy.MM.dd}', "; // ütemezés dátuma
                            szöveg += $"'{Telephelykereső(PszJelölő.Items[j].ToString().Trim())}' )"; // telephely
                            MyA.ABMódosítás(hova, jelszóhova, szöveg);

                            Előzőnap = Napokküld;

                            // a következők
                            if (előzősor == Ciklussormax)
                                sorszám = 1d;
                            else
                                sorszám = előzősor + 1d;

                            while (ElőbefejezőDátum.Value >= startdátum)
                            {
                                sorosnap = Ciklus_Napok(sorszám, Ciklusküld);

                                startdátum = startdátum.AddDays(sorosnap - Előzőnap);

                                cikluseredmény = Ciklus_Vizsgálat(sorosnap, Ciklusküld).Trim();

                                szöveg = "INSERT INTO ütemezés (azonosító, ciklusrend, elkészült, megjegyzés, ";
                                szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                                szöveg += " vsorszám, vütemezés, vvégezte) VALUES (";
                                szöveg += $"'{PszJelölő.Items[j].ToString().Trim()}', "; // azonosító
                                szöveg += $"'{Ciklusküld.Trim()}', "; // ciklusrend
                                szöveg += "false, '_', 0, '1900.01.01', "; // elkészül, Megjegyzés,státus, velkészülés
                                szöveg += $"'{startdátum:yyyy.MM.dd}', "; // esedékesség
                                szöveg += $"'{cikluseredmény.Trim()}', "; // vizsgálatfoka
                                szöveg += $"{sorszám}, "; // sorszám
                                szöveg += $"'{startdátum:yyyy.MM.dd}', "; // ütemezés dátuma
                                szöveg += $"'{Telephelykereső(PszJelölő.Items[j].ToString().Trim())}' )"; // telephely
                                MyA.ABMódosítás(hova, jelszóhova, szöveg);
                                előzősor = sorszám;
                                Előzőnap = sorosnap;

                                if (sorszám == Ciklussormax)
                                {
                                    sorszám = 1;
                                    Előzőnap = 0;
                                }
                                else
                                    sorszám += 1;
                            }
                        }
                    }

                    Holtart.Lép();
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


        private string Telephelykereső(string azonosító)
        {
            string TelephelykeresőRet = "_";
            string helyciklus = Villamos_Hely.Trim();
            string jelszóciklus = "pozsgaii";
            string szövegciklus = $"SELECT * FROM állománytábla WHERE azonosító='{azonosító.Trim()}'";

            Kezelő_Jármű kéz = new Kezelő_Jármű();
            Adat_Jármű rekord = kéz.Egy_Adat(helyciklus, jelszóciklus, szövegciklus);
            if (rekord != null)
                TelephelykeresőRet = rekord.Üzem.Trim();

            return TelephelykeresőRet;
        }





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

                Adatoklistázása();
                Kimutatás();
                Kimutatás1();

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


        private void Adatoklistázása()
        {
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

                // megnyitjuk az adatbázist
                string hely = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\TW6000adatok.mdb";
                string jelszó = "czapmiklós";
                int darab = 0;
                int volt;

                for (int i = 0; i < VizsgálatLista.Items.Count; i++)
                {
                    if (VizsgálatLista.GetItemChecked(i))
                        darab++;
                }
                string szöveg = "SELECT * FROM ütemezés ";

                // ha nincs mind jelölve
                if (VizsgálatLista.Items.Count != darab)
                {
                    volt = 0;
                    for (int i = 0; i < VizsgálatLista.Items.Count; i++)
                    {
                        if (VizsgálatLista.GetItemChecked(i) & volt == 0)
                        {
                            // első jelölt
                            szöveg += $" WHERE ( vizsgfoka='{VizsgálatLista.Items[i].ToString().Trim()}'";
                            volt = 1;
                        }
                        else if (VizsgálatLista.GetItemChecked(i))
                            // az összes többi
                            szöveg += $" OR vizsgfoka='{VizsgálatLista.Items[i].ToString().Trim()}'";
                    }
                    szöveg += " )";
                }
                szöveg += " ORDER BY azonosító,vütemezés ";

                int sor = 4;


                AdatokÜtem = KézÜtem.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_TW6000_Ütemezés rekord in AdatokÜtem)
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
                MyE.Tábla_Rögzítés($"a3:m{sor}", 3);

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

        private string Ciklus_Vizsgálat(double napokszáma, string ciklusrend)
        {
            string válasz = "";
            string valami = (from a in AdatokCiklus
                             where a.Típus.Trim() == ciklusrend && a.Névleges == napokszáma
                             select a.Vizsgálatfok.Trim()).FirstOrDefault();
            if (valami != null) válasz = valami;
            return válasz;
        }


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


        private double Ciklus_Napok(double Sorszám, string ciklusrend)
        {
            double válasz = (from a in AdatokCiklus
                             where a.Típus.Trim() == ciklusrend.Trim() && a.Sorszám == Sorszám
                             select a).Max(x => x.Névleges);
            return válasz;
        }
        #endregion

        #region ListaFeltöltések

        private void ÜtemListaFeltöltés()
        {
            try
            {
                AdatokÜtem.Clear();
                string helyalap = TW6000_Villamos.Trim();
                string jelszóalap = "czapmiklós";
                string szöveg = $"SELECT * FROM ütemezés";
                AdatokÜtem = KézÜtem.Lista_Adatok(helyalap, jelszóalap, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CiklusListaFeltöltés()
        {
            try
            {
                AdatokCiklus.Clear();
                string hely = Ciklus_Hely.Trim();
                string jelszó = "pocsaierzsi";
                string szöveg = $"SELECT * FROM ciklusrendtábla  WHERE [törölt]='0' ORDER BY Típus, sorszám";
                AdatokCiklus = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void JárműListaFeltöltés()
        {
            try
            {
                string hely = Villamos_Hely.Trim();
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM állománytábla";
                AdatokJármű.Clear();
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlapListaFeltöltés()
        {
            try
            {
                string hely = TW6000_Villamos;
                string jelszó = "czapmiklós";
                string szöveg = $"SELECT * FROM alap";
                AdatokAlap.Clear();
                AdatokAlap = KézAlap.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
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