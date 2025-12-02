using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;


namespace Villamos
{

    public partial class Ablak_SAP_osztály
    {
        #region Kezelő és lista és változók
        readonly Kezelő_Osztály_Adat KézOsztály = new Kezelő_Osztály_Adat();
        readonly Kezelő_Osztály_Név KézNév = new Kezelő_Osztály_Név();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();


        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Osztály_Adat> AdatokOsztály = new List<Adat_Osztály_Adat>();
        List<Adat_Osztály_Név> AdatokNév = new List<Adat_Osztály_Név>();
        List<Adat_Osztály_Adat> AdatokLekérdezés = new List<Adat_Osztály_Adat>();
        #endregion


        #region Alap
        public Ablak_SAP_osztály()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség").Where(a => a.Törölt == false).ToList();
            AdatokOsztály = KézOsztály.Lista_Adat();
            AdatokNév = KézNév.Lista_Adat();
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this, "Főmérnökség");
            else
                Jogosultságkiosztás();

            Fülekkitöltése();
            Pályaszámfeltöltés();

            Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            Osztályfeltöltés();
        }

        private void Ablak_SAP_osztály_Load(object sender, EventArgs e)
        {

        }

        private void Jogosultságkiosztás()
        {
            try
            {
                SAP_Betölt.Visible = false;

                int melyikelem = 189;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))

                {
                    SAP_Betölt.Visible = true;
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
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Osztály.html";
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            switch (Lapfülek.SelectedIndex)
            {
                case 0:
                    {
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
                        break;
                    }
            }
        }

        private void Lapfülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Lapfülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Lapfülek.GetTabRect(e.Index);

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
                Font BoldFont = new Font(Lapfülek.Font.Name, Lapfülek.Font.Size, FontStyle.Bold);
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
        #endregion


        #region SAP osztály adatok
        private void Pályaszámfeltöltés()
        {
            try
            {
                PályaszámCombo1.Items.Clear();
                PályaszámCombo1.Items.Add("");

                foreach (Adat_Jármű Elem in AdatokJármű)
                    PályaszámCombo1.Items.Add(Elem.Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frissít_Click(object sender, EventArgs e)
        {
            Tábla_író();
        }

        private void Tábla_író()
        {
            try
            {
                if (PályaszámCombo1.Text.Trim() == "") return;

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();

                Tábla.ColumnCount = 2;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Osztály név";
                Tábla.Columns[0].Width = 400;
                Tábla.Columns[1].HeaderText = "Osztály érték";
                Tábla.Columns[1].Width = 400;

                Adat_Osztály_Adat Elem = (from a in AdatokOsztály
                                          where a.Azonosító == PályaszámCombo1.Text.Trim()
                                          select a).FirstOrDefault();

                if (Elem == null) return;

                Tábla.Visible = false;
                Tábla.RowCount = Elem.Adatok.Count;
                for (int i = 0; i < Elem.Adatok.Count; i++)
                {
                    string Név = (from a in AdatokNév
                                  where a.Osztálymező.Trim() == Elem.Mezőnév[i]
                                  select a.Osztálynév.Trim()).FirstOrDefault();
                    if (Név != null)
                        Tábla.Rows[i].Cells[0].Value = Név;
                    else
                        Tábla.Rows[i].Cells[0].Value = Elem.Mezőnév[i];

                    Tábla.Rows[i].Cells[1].Value = Elem.Adatok[i];

                }
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

        private void SAP_Betölt_Click(object sender, EventArgs e)
        {
            SAP_Betöltés();
        }

        private void SAP_Betöltés()
        {
            try
            {
                string pályaszám = "";
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Szövegfájlok |*.txt"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                List<Adat_Osztály_Adat> AdatokAdat = KézOsztály.Lista_Adat();

                Holtart.Be();
                // beolvassuk a szövegfájlt
                string[] lines = ReadAllLines(fájlexc);
                // soronként elemezzük

                //Fejléc adatok
                string[] Soradatok = lines[3].ToString().Split('\t');
                int fejléchossz = Soradatok.Length + 1;
                string[] Fejléc = new string[fejléchossz];
                int[] Sorszám = new int[fejléchossz];
                Fejléc[0] = "azonosító"; //Első elem

                for (int i = 0; i < Soradatok.Length; i++)
                {
                    string szó = Soradatok[i].Trim();
                    if (szó == "Berendez.") Sorszám[0] = i;

                    Adat_Osztály_Név Elem = (from a in AdatokNév
                                             where a.Osztálynév.Trim() == szó
                                             select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        Fejléc[i] = Elem.Osztálymező;
                        Sorszám[i] = Elem.Id; //megadja , hogy az elemet hova rakjuk
                    }
                }

                List<Adat_Osztály_Adat> AdatokR = new List<Adat_Osztály_Adat>();
                List<Adat_Osztály_Adat> AdatokM = new List<Adat_Osztály_Adat>();
                for (int i = 5; i < lines.Length; i++)  // lines
                {
                    Soradatok = lines[i].ToString().Split('\t');

                    List<string> Értékek = new List<string>();
                    List<string> Mezők = new List<string>();
                    // Feldaraboljuk a sort elemekre és beletesszük a megfelelő helyre
                    for (int j = 1; j < Soradatok.Length; j++)
                    {
                        if (Sorszám[j] != 0)
                        {
                            if (Soradatok[j].ToStrTrim() != "")
                                Értékek.Add(Soradatok[j].ToStrTrim());
                            else
                                Értékek.Add("?");
                            Mezők.Add(Fejléc[j].ToStrTrim());
                        }
                    }
                    pályaszám = MyF.Szöveg_Tisztítás(Soradatok[Sorszám[0]], 1, -1).Trim();

                    // az új azonosító
                    Adat_Osztály_Adat Elem = (from a in AdatokAdat
                                              where a.Azonosító == pályaszám.ToStrTrim()
                                              select a).FirstOrDefault();

                    Adat_Osztály_Adat Adat = new Adat_Osztály_Adat(
                                        pályaszám,
                                        Értékek,
                                        Mezők);
                    if (Elem == null)
                        AdatokR.Add(Adat);
                    else
                        AdatokM.Add(Adat);
                    Holtart.Lép();
                }

                if (AdatokR.Count > 0) KézOsztály.Rögzítés(AdatokR);
                if (AdatokM.Count > 0) KézOsztály.Módosítás(AdatokM);
                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                Delete(fájlexc);

                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség").Where(a => a.Törölt == false).ToList();
                AdatokOsztály = KézOsztály.Lista_Adat();
                AdatokNév = KézNév.Lista_Adat();
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

        private void PályaszámCombo1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if ((int)e.KeyCode == 13)
                {
                    this.AcceptButton = Frissít;
                    Tábla_író();
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


        #region Lekérdezések
        private void Osztályfeltöltés()
        {
            try
            {
                Osztálylista.Items.Clear();
                Osztálylista.BeginUpdate();
                List<Adat_Osztály_Név> Adatok = (from a in AdatokNév
                                                 where a.Használatban == true
                                                 select a).ToList();

                foreach (Adat_Osztály_Név rekord in Adatok)
                    Osztálylista.Items.Add(rekord.Osztálynév);

                Osztálylista.EndUpdate();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LekérdezésAdatokFeltöltése()
        {
            try
            {
                Holtart.Be(AdatokOsztály.Count + 1);
                //Tartalom
                AdatokLekérdezés.Clear();
                foreach (Adat_Osztály_Adat rekord in AdatokOsztály)
                {
                    string telephely = "";
                    string típus = "";
                    Adat_Jármű Jármű = (from a in AdatokJármű
                                        where a.Azonosító == rekord.Azonosító
                                        select a).FirstOrDefault();
                    if (Jármű != null)
                    {
                        telephely = Jármű.Üzem;
                        típus = Jármű.Valóstípus;
                    }

                    Adat_Osztály_Adat Lekérdezés = new Adat_Osztály_Adat(
                                             rekord.Azonosító,
                                             rekord.Adatok,
                                             rekord.Mezőnév,
                                             telephely,
                                             típus);
                    AdatokLekérdezés.Add(Lekérdezés);
                    Holtart.Lép();
                }
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

        private void LekérdezTelep_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálylista.SelectedItems.Count < 1) return;
                if (Osztálylista.Items.Count < 0) return;
                LekérdezésAdatokFeltöltése();


                string honnan = Osztálylista.SelectedItem.ToStrTrim();
                string helyhiba = (from a in AdatokNév
                                   where a.Osztálynév == honnan
                                   select a.Osztálymező).FirstOrDefault() ?? "";
                if (helyhiba.Trim() == "") return;

                int sorszám = 0;
                for (int i = 0; i < AdatokOsztály[0].Mezőnév.Count; i++)
                {
                    if (AdatokOsztály[0].Mezőnév[i] == helyhiba)
                    {
                        sorszám = i;
                        break;
                    }
                }

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 4;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Telephely";
                Tábla1.Columns[0].Width = 140;
                Tábla1.Columns[1].HeaderText = "Típus";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = honnan;
                Tábla1.Columns[2].Width = 400;
                Tábla1.Columns[3].HeaderText = "Darabszám";
                Tábla1.Columns[3].Width = 100;

                List<string> Elemek = AdatokLekérdezés.Select(a => a.Adatok[sorszám]).Distinct().ToList();

                foreach (string elem in Elemek)
                {
                    if (elem != "?" && elem != "")
                    {

                        List<Adat_Osztály_Adat> Szűrtlista = (from a in AdatokLekérdezés
                                                              where a.Adatok[sorszám] == elem
                                                              orderby a.Telephely, a.Típus
                                                              select a).ToList();
                        List<string> SzűrtTelep = Szűrtlista.Select(a => a.Telephely).Distinct().ToList();
                        List<string> SzűrtTípus = Szűrtlista.Select(a => a.Típus).Distinct().ToList();
                        for (int j = 0; j < SzűrtTelep.Count; j++)
                        {
                            for (int k = 0; k < SzűrtTípus.Count; k++)
                            {
                                List<Adat_Osztály_Adat> Eredmény = (from a in AdatokLekérdezés
                                                                    where a.Adatok[sorszám] == elem
                                                                    && a.Telephely == SzűrtTelep[j]
                                                                    && a.Típus == SzűrtTípus[k]
                                                                    orderby a.Telephely, a.Típus
                                                                    select a).ToList();
                                int darab = 0;
                                if (Eredmény != null) darab = Eredmény.Count;
                                if (darab != 0)
                                {
                                    Tábla1.RowCount++;
                                    int i = Tábla1.RowCount - 1;
                                    Tábla1.Rows[i].Cells[0].Value = SzűrtTelep[j];
                                    Tábla1.Rows[i].Cells[1].Value = SzűrtTípus[k];
                                    Tábla1.Rows[i].Cells[2].Value = elem;
                                    Tábla1.Rows[i].Cells[3].Value = darab;
                                }
                            }
                        }
                    }
                }

                Tábla1.Refresh();
                Tábla1.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LekérdezFajta_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálylista.SelectedItems.Count < 1) return;
                LekérdezésAdatokFeltöltése();

                string honnan = Osztálylista.SelectedItem.ToStrTrim();
                string helyhiba = (from a in AdatokNév
                                   where a.Osztálynév == honnan
                                   select a.Osztálymező).FirstOrDefault() ?? "";
                if (helyhiba.Trim() == "") return;

                int sorszám = 0;
                for (int i = 0; i < AdatokOsztály[0].Mezőnév.Count; i++)
                {
                    if (AdatokOsztály[0].Mezőnév[i] == helyhiba)
                    {
                        sorszám = i;
                        break;
                    }
                }

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 2;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = honnan;
                Tábla1.Columns[0].Width = 400;
                Tábla1.Columns[1].HeaderText = "Darabszám";
                Tábla1.Columns[1].Width = 140;

                List<string> Elemek = AdatokLekérdezés.Select(a => a.Adatok[sorszám]).Distinct().ToList();

                foreach (string elem in Elemek)
                {
                    if (elem != "?" && elem != "")
                    {
                        Tábla1.RowCount++;
                        int i = Tábla1.RowCount - 1;
                        Tábla1.Rows[i].Cells[0].Value = elem;
                        int darab = 0;
                        List<Adat_Osztály_Adat> Szűrtlista = (from a in AdatokLekérdezés
                                                              where a.Adatok[sorszám] == elem
                                                              select a).ToList();
                        if (Szűrtlista != null) darab = Szűrtlista.Count;
                        Tábla1.Rows[i].Cells[1].Value = darab;
                    }
                }
                Tábla1.Refresh();
                Tábla1.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LekérdezRészletes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálylista.SelectedItems.Count < 1) return;
                LekérdezésAdatokFeltöltése();

                string honnan = Osztálylista.SelectedItem.ToStrTrim();
                string helyhiba = (from a in AdatokNév
                                   where a.Osztálynév == honnan
                                   select a.Osztálymező).FirstOrDefault() ?? "";
                if (helyhiba.Trim() == "") return;

                int sorszám = 0;
                for (int i = 0; i < AdatokOsztály[0].Mezőnév.Count; i++)
                {
                    if (AdatokOsztály[0].Mezőnév[i] == helyhiba)
                    {
                        sorszám = i;
                        break;
                    }
                }

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 4;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Pályaszám";
                Tábla1.Columns[0].Width = 140;
                Tábla1.Columns[1].HeaderText = "Telephely";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = "Típus";
                Tábla1.Columns[2].Width = 240;
                Tábla1.Columns[3].HeaderText = honnan;
                Tábla1.Columns[3].Width = 400;
                foreach (Adat_Osztály_Adat rekord in AdatokLekérdezés)
                {
                    if (Értelmes.Checked)
                    {
                        //Minden adat kiírása
                        Tábla1.RowCount++;
                        int j = Tábla1.RowCount - 1;
                        Tábla1.Rows[j].Cells[0].Value = rekord.Azonosító;
                        Tábla1.Rows[j].Cells[1].Value = rekord.Telephely;
                        Tábla1.Rows[j].Cells[2].Value = rekord.Típus;
                        Tábla1.Rows[j].Cells[3].Value = rekord.Adatok[sorszám];
                    }
                    else
                    {
                        // Nem írjuk ki aminek nincs telephelye és értelmes adata
                        if (rekord.Telephely != "" && rekord.Adatok[sorszám] != "?" && rekord.Adatok[sorszám] != "")
                        {
                            Tábla1.RowCount++;
                            int j = Tábla1.RowCount - 1;
                            Tábla1.Rows[j].Cells[0].Value = rekord.Azonosító;
                            Tábla1.Rows[j].Cells[1].Value = rekord.Telephely;
                            Tábla1.Rows[j].Cells[2].Value = rekord.Típus;
                            Tábla1.Rows[j].Cells[3].Value = rekord.Adatok[sorszám];
                        }
                    }
                }

                Tábla1.Refresh();
                Tábla1.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Osztály_" + Program.PostásNév.ToString().Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla1);

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
    }
}