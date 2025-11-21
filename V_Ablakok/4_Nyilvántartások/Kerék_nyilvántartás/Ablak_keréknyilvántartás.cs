using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Ablakok.Kerék_nyilvántartás;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{

    public partial class Ablak_keréknyilvántartás
    {
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Osztály_Adat KézCsat = new Kezelő_Osztály_Adat();
        readonly Kezelő_Kerék_Erő KézErőTám = new Kezelő_Kerék_Erő();
        readonly Kezelő_Kerék_Eszterga KézEszterga = new Kezelő_Kerék_Eszterga();
        readonly Kezelő_Kerék_Eszterga_Igény KézEsztIgény = new Kezelő_Kerék_Eszterga_Igény();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézKiegJelenlét = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_T5C5_Kmadatok KézT5C5Elő = new Kezelő_T5C5_Kmadatok("T5C5");
        readonly Kezelő_T5C5_Kmadatok KézICSElő = new Kezelő_T5C5_Kmadatok("ICS");
        readonly Kezelő_Kerék_Eszterga_Tengely KézTengely = new Kezelő_Kerék_Eszterga_Tengely();

        List<Adat_Kerék_Mérés> AdatokMérés = new List<Adat_Kerék_Mérés>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Kerék_Tábla> AdatokKerék = new List<Adat_Kerék_Tábla>();
        List<Adat_Osztály_Adat> AdatokCsat = new List<Adat_Osztály_Adat>();
        List<Adat_Kerék_Erő> AdatokErőTám = new List<Adat_Kerék_Erő>();
        List<Adat_Kerék_Eszterga> AdatokEszterga = new List<Adat_Kerék_Eszterga>();
        List<Adat_Kerék_Eszterga_Igény> AdatokIgény = new List<Adat_Kerék_Eszterga_Igény>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Kiegészítő_Jelenlétiív> AdatokKiegJelenlét = new List<Adat_Kiegészítő_Jelenlétiív>();

        #region alap
        public Ablak_keréknyilvántartás()
        {
            InitializeComponent();
            Start();
        }

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

                Pályaszámfeltöltés();
                Állapotfeltöltés();
                Dátumig.Value = DateTime.Today;
                Eszterga.Value = DateTime.Today;
                Dátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);

                Irányítófeltöltés();
                Jegyzettömb.Visible = false;
                Tábla1.Visible = true;
                LapFülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ablak_keréknyilvántartás_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_keréknyilvántartás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_segéd?.Close();
            Új_Ablak_Kerék_gyűjtő?.Close();
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

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


        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\berendezés_kerék.html";
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
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                BtnSAP.Enabled = false;
                Panel5.Enabled = false;

                Rögzítrögzít.Enabled = false;
                GyűjtőRögzítés.Enabled = false;
                Command3.Enabled = false;
                Command6.Enabled = false;

                Command10.Enabled = false;
                Command8.Enabled = false;
                Command7.Enabled = false;



                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Panel5.Visible = true;
                }
                else
                {
                    Panel5.Visible = false;
                }
                melyikelem = 186;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    BtnSAP.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Panel5.Enabled = true;
                }

                melyikelem = 187;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))

                {
                    Rögzítrögzít.Enabled = true;
                    GyűjtőRögzítés.Enabled = true;
                    Command3.Enabled = true;
                    Command6.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))

                {
                    Command10.Enabled = true;
                    Command8.Enabled = true;
                    Command7.Enabled = true;
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

        private void LAPFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {

            switch (LapFülek.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        break;
                    }


                case 3:
                    {
                        Típus_Feltöltés();
                        break;
                    }

                case 4:
                    {
                        break;
                    }

                case 5:
                    {
                        break;
                    }

            }
        }

        private void Pályaszámfeltöltés()
        {
            try
            {
                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                else
                    Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adatok = (from a in Adatok
                          where a.Törölt == false
                          orderby a.Azonosító
                          select a).ToList();


                SAPPályaszám.Items.Clear();
                PályaszámCombo2.Items.Clear();
                RögzítPályaszám.Items.Clear();
                foreach (Adat_Jármű Elem in Adatok)
                {
                    SAPPályaszám.Items.Add(Elem.Azonosító);
                    PályaszámCombo2.Items.Add(Elem.Azonosító);
                    RögzítPályaszám.Items.Add(Elem.Azonosító);
                }

                SAPPályaszám.Refresh();
                PályaszámCombo2.Refresh();
                RögzítPályaszám.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Állapotfeltöltés()
        {
            RögzítÁllapot.Items.Clear();
            foreach (MyEn.Kerék_Állapot elem in Enum.GetValues(typeof(MyEn.Kerék_Állapot)))
                RögzítÁllapot.Items.Add($"{(int)elem}-{elem.ToString().Replace('_', ' ')}");
        }

        private void Irányítófeltöltés()
        {
            try
            {
                Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Kilépésiidő.ToShortDateString() == new DateTime(1900, 1, 1).ToShortDateString()
                          && a.Főkönyvtitulus.Trim() != "" && a.Főkönyvtitulus.Trim() != "_"
                          orderby a.DolgozóNév
                          select a).ToList();

                Kiadta.Items.Clear();
                Kiadta.Items.Add("");
                foreach (Adat_Dolgozó_Alap Elem in Adatok)
                    Kiadta.Items.Add(Elem.DolgozóNév);
                Kiadta.Refresh();
            }
            catch (HibásBevittAdat ex)
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
            Pályaszámfeltöltés();
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
            StringFormat sf = new StringFormat()
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
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();

        }
        #endregion


        #region SAP Adatok lapfül

        #region Listázás
        private void BtnListáz_Click(object sender, EventArgs e)
        {
            if (Csakkerék.Checked == true)
                Berendezés_adatok("Kerék");
            else
                Berendezés_adatok("Minden");

            Berendezés_ellemőrzés();
        }

        private void Berendezés_adatok(string Választ)
        {
            try
            {
                if (SAPPályaszám.Text.Trim() == "") return;
                Erőtámvan.Visible = Erőtámkiolvasás(SAPPályaszám.Text.Trim());

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 8;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Psz";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Berendezésszám";
                Tábla.Columns[1].Width = 150;
                Tábla.Columns[2].HeaderText = "Gyári szám";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Pozíció";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Mérés Dátuma";
                Tábla.Columns[4].Width = 170;
                Tábla.Columns[5].HeaderText = "Állapot";
                Tábla.Columns[5].Width = 180;
                Tábla.Columns[6].HeaderText = "Méret";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Megnevezés";
                Tábla.Columns[7].Width = 300;

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
                switch (Választ)
                {
                    case "Kerék":
                        Adatok = (from a in Adatok
                                  where a.Azonosító == SAPPályaszám.Text.Trim()
                                  && a.Objektumfajta == "V.KERÉKPÁR"
                                  orderby a.Pozíció
                                  select a).ToList();
                        break;
                    case "Forgóváz":
                        Adatok = (from a in Adatok
                                  where a.Azonosító == SAPPályaszám.Text.Trim()
                                  && (a.Objektumfajta == "V.KERÉKPÁR"
                                  || a.Objektumfajta == "FORGVKERET")
                                  orderby a.Pozíció
                                  select a).ToList();
                        break;
                    case "Minden":
                        Adatok = (from a in Adatok
                                  where a.Azonosító == SAPPályaszám.Text.Trim()
                                  orderby a.Pozíció
                                  select a).ToList();
                        break;
                    default:
                        break;
                }


                AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.AddYears(-1).Year);
                List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(DateTime.Today.Year);
                AdatokMérés.AddRange(Ideig);
                AdatokMérés = (from a in AdatokMérés
                               orderby a.Kerékberendezés, a.Mikor descending
                               select a).ToList();

                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Tábla.Rows[i].Cells[4].Value = "1900.01.01";
                    Tábla.Rows[i].Cells[5].Value = "_";
                    Tábla.Rows[i].Cells[6].Value = "_";
                    Tábla.Rows[i].Cells[7].Value = rekord.Kerékmegnevezés.Trim();
                    if (AdatokMérés != null)
                    {
                        Adat_Kerék_Mérés Mérés = (from a in AdatokMérés
                                                  where a.Kerékberendezés == rekord.Kerékberendezés
                                                  orderby a.Mikor ascending
                                                  select a).LastOrDefault();
                        if (Mérés != null)
                        {
                            Tábla.Rows[i].Cells[4].Value = Mérés.Mikor.ToString("yyyy.MM.dd");
                            Tábla.Rows[i].Cells[5].Value = MilyenÁllapot(Mérés.Állapot);
                            Tábla.Rows[i].Cells[6].Value = Mérés.Méret;
                        }
                    }
                }
                Tábla.Visible = true;
                Tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SAPPályaszám_TextUpdate(object sender, EventArgs e)
        {
            Erőtámvan.Visible = Erőtámkiolvasás(SAPPályaszám.Text.Trim());
        }

        private void Berendezés_ellemőrzés()
        {
            try
            {
                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();

                string előző = "";
                List<Adat_Kerék_Tábla> AdatokGY = new List<Adat_Kerék_Tábla>();
                List<string> SzövegGY = new List<string>();
                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {
                    if (előző == rekord.Kerékberendezés)
                    {
                        // ha egyforma akkor töröljük
                        SzövegGY.Add(rekord.Kerékberendezés);

                        Adat_Kerék_Tábla ADAT = new Adat_Kerék_Tábla(
                                 rekord.Kerékberendezés,
                                 rekord.Kerékmegnevezés,
                                 rekord.Kerékgyártásiszám,
                                 rekord.Föléberendezés,
                                 rekord.Azonosító,
                                 rekord.Pozíció,
                                 rekord.Dátum,
                                 rekord.Objektumfajta);
                        AdatokGY.Add(ADAT);
                    }
                    else
                    {
                        előző = rekord.Kerékberendezés;
                    }
                }
                //Először törölni kell utána rögzíteni
                if (SzövegGY.Count > 0) KézKerék.Törlés(SzövegGY);
                if (AdatokGY.Count > 0) KézKerék.Rögzítés(AdatokGY);

            }
            catch (HibásBevittAdat ex)
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

        private void Command8_Click(object sender, EventArgs e)
        {
            try
            {
                // ha üres a tábla akkor kilép
                if (SAPPályaszám.Text.Trim() == "") return;

                AdatokKiegJelenlét = KézKiegJelenlét.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Kiegészítő_Jelenlétiív AdatKiegJelenlét = (from a in AdatokKiegJelenlét
                                                                where a.Id == 1
                                                                select a).FirstOrDefault();

                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                Adat_Jármű AdatJármű = (from a in AdatokJármű
                                        where a.Azonosító == SAPPályaszám.Text.Trim()
                                        select a).FirstOrDefault();
                Csakkerék.Checked = true;
                Berendezés_adatok("Forgóváz");

                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Kerék esztergálási adatlap készítés",
                    FileName = $"Kerék_esztergálási_tábla_{SAPPályaszám.Text.Trim()}_{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // megnyitjuk az excelt
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                Holtart.Be(3);

                int újlap = 0;

                MyX.Oszlopszélesség(munkalap, "b:i", 12);
                MyX.Oszlopszélesség(munkalap, "a:a", 10);
                MyX.Oszlopszélesség(munkalap, "e:e", 14);
                MyX.Oszlopszélesség(munkalap, "f:f", 13);
                MyX.Oszlopszélesség(munkalap, "g:g", 10);

                // betűméret
                MyX.Munkalap_betű(munkalap, "Arial", 12);

                int sor = 1;
                int eleje;
                for (int i = 1; i <= 2; i++)
                {
                    Holtart.Lép();
                    // Lap felsőrész
                    MyX.Egyesít(munkalap, $"a{sor}:H{sor}");
                    if (AdatKiegJelenlét != null) MyX.Kiir(AdatKiegJelenlét.Szervezet, $"a{sor}");

                    sor += 2;
                    MyX.Egyesít(munkalap, $"a{sor}:H{sor}");
                    MyX.Kiir("Munkafelvételilap kerék esztergáláshoz", $"a{sor}");

                    Beállítás_Betű BeállításBetű = new Beállítás_Betű
                    {
                        Méret = 14
                    };
                    MyX.Betű(munkalap, $"a{sor}:H{sor}", BeállításBetű);

                    sor += 2;
                    MyX.Egyesít(munkalap, $"A{sor}:B{sor}");
                    MyX.Kiir("Jármű pályaszáma:", $"a{sor}");
                    MyX.Kiir(SAPPályaszám.Text.Trim(), $"C{sor}");
                    MyX.Egyesít(munkalap, $"E{sor}:F{sor}");
                    MyX.Kiir("Jármű típusa:", $"E{sor}");

                    string Jármű_típus = "";

                    if (AdatJármű != null)
                    {
                        MyX.Kiir(Jármű_típus, $"g{sor}");
                        Jármű_típus = AdatJármű.Típus;
                    }


                    sor += 2;
                    MyX.Egyesít(munkalap, $"A{sor}:C{sor}");
                    MyX.Kiir("Utolsó felújítás óta futott:", $"A{sor}");
                    MyX.Kiir(Km_Adat(SAPPályaszám.Text.Trim(), Jármű_típus) + " km", $"D{sor}");



                    sor += 2;
                    eleje = sor;
                    // Fejléc táblázat
                    MyX.Egyesít(munkalap, $"a{sor}:a{sor + 1}");
                    MyX.Kiir("Pozíció", $"a{sor}");
                    MyX.Egyesít(munkalap, $"b{sor}:b{sor + 1}");
                    MyX.Kiir("Gyári szám", $"b{sor}");
                    MyX.Egyesít(munkalap, $"c{sor}:d{sor + 1}");
                    MyX.Kiir("SAP megnevezés", $"c{sor}");
                    MyX.Egyesít(munkalap, $"e{sor}:g{sor}");
                    MyX.Kiir("Előző mérési eredmények", $"e{sor}");
                    MyX.Kiir("Esztergált", $"h{sor}");
                    sor += 1;
                    MyX.Kiir("Dátum", $"e{sor}");
                    MyX.Kiir("Állapot", $"f{sor}");
                    MyX.Kiir("Méret", $"g{sor}");
                    MyX.Kiir("Méret", $"h{sor}");
                    MyX.Rácsoz($"a{eleje}:g{sor}");
                    MyX.Vastagkeret($"a{eleje}:h{sor}");

                    // Átmásoljuk a táblázatos értékeket
                    for (int j = 0; j <= Tábla.Rows.Count - 1; j++)
                    {
                        sor += 1;
                        MyX.Sormagasság(munkalap, $"{sor}:{sor}", 40);
                        MyX.Kiir(Tábla.Rows[j].Cells[3].Value.ToStrTrim(), $"a{sor}"); // pozíció
                        MyX.Kiir(Tábla.Rows[j].Cells[2].Value.ToStrTrim(), $"b{sor}"); // kerékgyártásiszám
                        MyX.Kiir(Tábla.Rows[j].Cells[7].Value.ToStrTrim(), $"c{sor}"); // kerékmegnevezés
                        if (Tábla.Rows[j].Cells[4].Value.ToStrTrim() != "")
                            MyX.Kiir(Tábla.Rows[j].Cells[4].Value.ToÉrt_DaTeTime().ToString("yyyy.MM.dd"), $"e{sor}"); // mikor
                        MyX.Kiir(Tábla.Rows[j].Cells[5].Value.ToStrTrim(), $"f{sor}"); // állapot
                        MyX.Sortörésseltöbbsorba(munkalap,$"f{sor}",true );
                        BeállításBetű = new Beállítás_Betű
                        {
                            Méret = 10
                        };
                        MyX.Betű(munkalap, $"f{sor}", BeállításBetű);
                        MyX.Kiir(Tábla.Rows[j].Cells[6].Value.ToStrTrim(), $"g{sor}"); // méret
                    }

                    MyX.Rácsoz($"a{eleje + 2}:h{sor}");
                    MyX.Vastagkeret($"a{eleje + 2}:h{sor}");
                    sor += 2;
                    MyX.Kiir("Kelt, Budapest " + DateTime.Today.ToString("yyyy.MM.dd"), $"a{sor}");
                    MyX.Kiir("Elkészült:", $"f{sor}");
                    MyX.Egyesít(munkalap, $"g{sor + 1}:h{sor + 1}");
                    MyX.Aláírásvonal($"g{sor + 1}:h{sor + 1}");
                    sor += 4;
                    MyX.Egyesít(munkalap, "b" + sor.ToString() + ":c" + sor.ToString());
                    MyX.Kiir("Esztergálást igénylő", "b" + sor.ToString());
                    MyX.Egyesít(munkalap, $"g{sor}:h{sor}");
                    MyX.Kiir("Esztergálást végző", $"g{sor}");
                    MyX.Aláírásvonal($"b{sor}:c{sor}");
                    MyX.Aláírásvonal($"g{sor}:h{sor}");
                    sor += 1;
                    MyX.Egyesít(munkalap, $"b{sor}:c{sor}" );
                    MyX.Kiir(Kiadta.Text.Trim(), $"b{sor}");
                    if (i == 1)
                    {
                        sor += 4;
                        újlap = sor;
                        
                    }

                }
                Holtart.Lép();
                Beállítás_Nyomtatás beállításnyomtatás = new Beállítás_Nyomtatás { 
                    Munkalap=munkalap ,
                    NyomtatásiTerület=$"a1:h{sor}",
                    Oldaltörés =30
                };
                MyX.NyomtatásiTerület_részletes(munkalap, beállításnyomtatás);
                // bezárjuk az Excel-t
            
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
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

        private void Command10_Click(object sender, EventArgs e)
        {
            try
            {
                // ha üres a tábla akkor kilép
                if (SAPPályaszám.Text.Trim() == "") return;

                Csakkerék.Checked = true;
                Berendezés_adatok("Kerék");
                Nyomtatvány_készítés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Command7_Click(object sender, EventArgs e)
        {
            try
            {      // ha üres a tábla akkor kilép
                if (SAPPályaszám.Text.Trim() == "") return;

                Csakkerék.Checked = false;
                Berendezés_adatok("Minden");

                Nyomtatvány_készítés1();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nyomtatvány_készítés()
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Kerékmérés adatlap készítés",
                    FileName = $"Kerékmérés_tábla_{SAPPályaszám.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                Holtart.Be(10);

                MyE.Oszlopszélesség("Munka1", "b:i", 12);
                MyE.Oszlopszélesség("Munka1", "a:a", 10);
                MyE.Oszlopszélesség("Munka1", "e:e", 14);
                MyE.Oszlopszélesség("Munka1", "f:f", 13);
                MyE.Oszlopszélesség("Munka1", "g:g", 10);
                // betűméret
                MyE.Munkalap_betű("Arial", 12);

                int sor = 1;
                int eleje;

                AdatokKiegJelenlét = KézKiegJelenlét.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Kiegészítő_Jelenlétiív AdatKiegJelenlét = (from a in AdatokKiegJelenlét
                                                                where a.Id == 1
                                                                select a).FirstOrDefault();

                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Jármű AdatJármű = (from a in AdatokJármű
                                        where a.Azonosító == SAPPályaszám.Text.Trim()
                                        select a).FirstOrDefault();


                for (int i = 1; i <= 2; i++)
                {
                    Holtart.Lép();
                    // Lap felsőrész
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":i" + sor.ToString());

                    if (AdatKiegJelenlét != null) MyE.Kiir(AdatKiegJelenlét.Szervezet, "a" + sor.ToString());

                    sor += 2;
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir("Kerék mérés", "A" + sor.ToString());
                    MyE.Egyesít("Munka1", "A" + sor.ToString() + ":b" + sor.ToString());
                    MyE.Betű("A" + sor.ToString() + ":b" + sor.ToString(), 14);
                    sor += 2;
                    MyE.Kiir("Jármű pályaszáma:", "a" + sor.ToString());
                    MyE.Kiir(SAPPályaszám.Text.Trim(), "C" + sor.ToString());
                    MyE.Egyesít("Munka1", "e" + sor.ToString() + ":f" + sor.ToString());
                    MyE.Kiir("Jármű típusa:", "e" + sor.ToString());

                    if (AdatJármű != null) MyE.Kiir(AdatJármű.Típus, "g" + sor.ToString());

                    sor += 2;
                    eleje = sor;
                    // Fejléc táblázat
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":" + "a" + (sor + 1).ToString());
                    MyE.Kiir("Pozíció", "a" + sor.ToString());
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":" + "b" + (sor + 1).ToString());
                    MyE.Kiir("Gyári szám", "b" + sor.ToString());
                    MyE.Egyesít("Munka1", "c" + sor.ToString() + ":" + "d" + (sor + 1).ToString());
                    MyE.Kiir("SAP megnevezés", "c" + sor.ToString());
                    MyE.Egyesít("Munka1", "e" + sor.ToString() + ":" + "g" + sor.ToString());
                    MyE.Kiir("Előző mérési eredmények", "e" + sor.ToString());
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":" + "i" + sor.ToString());
                    MyE.Kiir("Mért eredmények", "h" + sor.ToString());
                    sor += 1;
                    MyE.Kiir("Dátum", "e" + sor.ToString());
                    MyE.Kiir("Állapot", "f" + sor.ToString());
                    MyE.Kiir("Méret", "g" + sor.ToString());
                    MyE.Kiir("Állapot", "h" + sor.ToString());
                    MyE.Kiir("Méret", "i" + sor.ToString());
                    MyE.Rácsoz("a" + eleje.ToString() + ":i" + sor.ToString());
                    MyE.Vastagkeret("a" + eleje.ToString() + ":i" + sor.ToString());

                    // Átmásoljuk a táblázatos értékeket

                    for (int j = 0; j < Tábla.Rows.Count; j++)
                    {
                        sor += 1;
                        MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 40);
                        MyE.Kiir(Tábla.Rows[j].Cells[3].Value.ToStrTrim(), "a" + sor.ToString()); // pozíció
                        MyE.Kiir(Tábla.Rows[j].Cells[2].Value.ToStrTrim(), "b" + sor.ToString()); // kerékgyártásiszám
                        MyE.Kiir(Tábla.Rows[j].Cells[7].Value.ToStrTrim(), "c" + sor.ToString()); // kerékmegnevezés
                        if (Tábla.Rows[j].Cells[4].Value.ToStrTrim() != "")
                        {
                            MyE.Kiir(Tábla.Rows[j].Cells[4].Value.ToÉrt_DaTeTime().ToString("yyyy.MM.dd"), "e" + sor.ToString()); // mikor
                        }
                        MyE.Kiir(Tábla.Rows[j].Cells[5].Value.ToStrTrim(), "f" + sor.ToString()); // állapot
                        MyE.Kiir(Tábla.Rows[j].Cells[6].Value.ToStrTrim(), "g" + sor.ToString()); // méret
                    }
                    MyE.Rácsoz("a" + (eleje + 2).ToString() + ":i" + sor.ToString());
                    MyE.Vastagkeret("a" + (eleje + 2).ToString() + ":i" + sor.ToString());
                    sor += 2;
                    MyE.Kiir("Erőtám:     van   /   nincs", "a" + sor.ToString());
                    sor += 2;
                    MyE.Kiir("Kelt, Budapest " + DateTime.Today.ToString("yyyy.MM.dd").ToString(), "a" + sor.ToString());
                    sor += 2;
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":i" + sor.ToString());
                    sor += 1;
                    MyE.Aláírásvonal("b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Kiir("Mérést végezte", "b" + sor.ToString());
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir("Ellenőrizte", "H" + sor.ToString());
                    MyE.Aláírásvonal("b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Aláírásvonal("h" + sor.ToString() + ":i" + sor.ToString());
                    sor += 1;
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir(Kiadta.Text.Trim(), "H" + sor.ToString());
                    if (i == 1)
                        sor += 4;
                }
                Holtart.Lép();
                MyE.NyomtatásiTerület_részletes("Munka1", "a1:i" + sor.ToString(), "", "", true);


                // bezárjuk az Excel-t
                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();


                MyE.Megnyitás(fájlexc);
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

        private string Km_Adat(string Azonosító, string Típus)
        {
            long KMU = 0;
            List<Adat_T5C5_Kmadatok> AdatokT5CElő = KézT5C5Elő.Lista_Adatok();
            Adat_T5C5_Kmadatok AdatT5C5Elő = (from a in AdatokT5CElő
                                              where a.Törölt == false
                                              && a.Azonosító == Azonosító.Trim()
                                              orderby a.Vizsgdátumk descending
                                              select a).FirstOrDefault();

            List<Adat_T5C5_Kmadatok> AdatokICSelő = KézICSElő.Lista_Adatok();
            Adat_T5C5_Kmadatok AdatICSelő = (from a in AdatokICSelő
                                             where a.Törölt == false
                                             && a.Azonosító == Azonosító.Trim()
                                             orderby a.Vizsgdátumk descending
                                             select a).FirstOrDefault();

            switch (Típus)
            {
                case "T5C5K2":
                    KMU = AdatT5C5Elő.KMUkm;
                    break;

                case "T5C5":
                    KMU = AdatT5C5Elő.KMUkm;
                    break;

                case "ICS":
                    KMU = AdatICSelő.KMUkm;
                    break;

                case "KCSV-7":
                    KMU = AdatICSelő.KMUkm;
                    break;
                default:
                    break;
            }
            return KMU.ToString();
        }

        private void Nyomtatvány_készítés1()
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Berendezések adatlap készítés",
                    FileName = $"Berendezés_tábla_{SAPPályaszám.Text.Trim()}_{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                Holtart.Be(10);

                MyE.Oszlopszélesség("Munka1", "b:i", 12);
                MyE.Oszlopszélesség("Munka1", "h:i", 16);

                // betűméret
                MyE.Munkalap_betű("Arial", 12);

                int sor = 1;
                int eleje;

                //Új

                AdatokKiegJelenlét = KézKiegJelenlét.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Kiegészítő_Jelenlétiív AdatKiegJelenlét = (from a in AdatokKiegJelenlét
                                                                where a.Id == 1
                                                                select a).FirstOrDefault();

                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Jármű AdatJármű = (from a in AdatokJármű
                                        where a.Azonosító == SAPPályaszám.Text.Trim()
                                        select a).FirstOrDefault();

                Holtart.Lép();
                // Lap felsőrész
                MyE.Egyesít("Munka1", $"a{sor}" + ":i" + sor.ToString());

                if (AdatKiegJelenlét != null) MyE.Kiir(AdatKiegJelenlét.Szervezet, $"a{sor}");

                sor += 2;
                MyE.Egyesít("Munka1", $"a{sor}" + ":i" + sor.ToString());
                MyE.Kiir("Forgalombiztonsági kiemelt szerkezeti elemek azonosítószám ellenőrző lapja", $"a{sor}");
                MyE.Betű($"a{sor}", 14);
                sor += 2;
                MyE.Egyesít("Munka1", $"a{sor}" + ":b" + sor.ToString());

                MyE.Kiir("Jármű pályaszáma:", $"a{sor}");
                MyE.Kiir(SAPPályaszám.Text.Trim(), $"c{sor}");
                MyE.Egyesít("Munka1", "e" + sor.ToString() + ":f" + sor.ToString());
                MyE.Kiir("Jármű típusa:", "e" + sor.ToString());

                if (AdatJármű != null) MyE.Kiir(AdatJármű.Típus, $"g{sor}");

                sor += 2;
                eleje = sor;
                // Fejléc táblázat
                MyE.Egyesít("Munka1", $"a{sor}" + ":" + $"a{sor}");
                MyE.Kiir("Pozíció", $"a{sor}");
                MyE.Egyesít("Munka1", $"b{sor}" + ":" + $"c{sor}");
                MyE.Kiir("Gyári szám", $"b{sor}");
                MyE.Egyesít("Munka1", $"d{sor}" + ":" + $"g{sor}");
                MyE.Kiir("SAP megnevezés", $"d{sor}");
                MyE.Egyesít("Munka1", $"h{sor}" + ":" + $"h{sor}");
                MyE.Kiir("Megfelelő", $"h{sor}");
                MyE.Egyesít("Munka1", $"i{sor}" + ":" + $"i{sor}");
                MyE.Kiir("Nem megfelelő", $"i{sor}");

                MyE.Rácsoz($"a{sor}" + ":i" + sor.ToString());
                MyE.Vastagkeret($"a{sor}" + ":i" + sor.ToString());

                // Átmásoljuk a táblázatos értékeket

                for (int j = 0; j < Tábla.Rows.Count; j++)
                {
                    sor += 1;
                    MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 40);
                    MyE.Kiir(Tábla.Rows[j].Cells[3].Value.ToStrTrim(), $"a{sor}"); // pozíció
                    MyE.Egyesít("Munka1", $"b{sor}" + ":" + $"c{sor}");
                    MyE.Kiir(Tábla.Rows[j].Cells[2].Value.ToStrTrim(), $"b{sor}"); // kerékgyártásiszám
                    MyE.Egyesít("Munka1", $"d{sor}" + ":" + $"g{sor}");
                    MyE.Kiir(Tábla.Rows[j].Cells[7].Value.ToStrTrim(), $"d{sor}"); // kerékmegnevezés

                }
                MyE.Rácsoz("a" + (eleje + 1).ToString() + ":i" + sor.ToString());
                MyE.Vastagkeret("a" + (eleje + 1).ToString() + ":i" + sor.ToString());
                sor += 2;
                MyE.Kiir("Kelt, Budapest " + DateTime.Today.ToString("yyyy.MM.dd"), $"a{sor}");
                sor += 2;
                MyE.Egyesít("Munka1", $"h{sor}" + ":i" + sor.ToString());
                MyE.Kiir("Ellenőrizte", $"h{sor}");
                MyE.Aláírásvonal($"h{sor}" + ":i" + sor.ToString());
                sor += 1;
                MyE.Egyesít("Munka1", $"h{sor}" + ":i" + sor.ToString());
                MyE.Kiir(Kiadta.Text.Trim(), $"h{sor}");

                Holtart.Lép();
                MyE.NyomtatásiTerület_részletes("Munka1", "a1:i" + sor.ToString(), "", "", true);


                // bezárjuk az Excel-t
                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
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

        #region SAP gomb eseményei
        private void BtnSAP_Click(object sender, EventArgs e)
        {
            Beolvas_SAP();
        }

        private async void Beolvas_SAP()
        {
            string fájlexc = "";
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                DateTime Eleje = DateTime.Now;
                //Adattáblába tesszük
                Holtart.Be();
                timer1.Enabled = true;
                await Task.Run(() => SAP_Adatokbeolvasása.Kerék_beolvasó(fájlexc));
                DateTime Vége = DateTime.Now;
                timer1.Enabled = false;
                Holtart.Ki();
                //kitöröljük a betöltött fájlt
                Delete(fájlexc);

                MessageBox.Show($"Az adat konvertálás befejeződött!\nidő:{Vége - Eleje}", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.StackTrace.Contains("System.IO.File.InternalDelete"))
                    MessageBox.Show($"A programnak a beolvasott adatokat tartalmazó fájlt nem sikerült törölni.\n Valószínüleg a {fájlexc} nyitva van.\n\nAz adat konvertálás befejeződött!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }
        #endregion
        #endregion


        #region Eredmények listázása lapfül
        private void Command5_Click(object sender, EventArgs e)
        {
            try
            {
                Feltöltések();
                // oszlopok számának meghatározása
                int oszlop = 0;

                List<Adat_Jármű> Jármű = new List<Adat_Jármű>();
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Jármű = KézJármű.Lista_Adatok("Főmérnökség");
                else
                    Jármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                if (Típus_Szűrő.Text.Trim() == "")
                    Jármű = Jármű.Where(a => a.Típus.Trim() == Típus_Szűrő.Text.Trim()).ToList();

                Jármű = (from a in Jármű
                         where a.Törölt == false
                         orderby a.Azonosító
                         select a).ToList();

                Holtart.Be(Jármű.Count + 1);
                foreach (Adat_Jármű Elem in Jármű)
                {
                    Holtart.Lép();
                    List<Adat_Kerék_Tábla> poz = (from a in AdatokKerék
                                                  where a.Azonosító == Elem.Azonosító.Trim()
                                                  select a).ToList();
                    if (oszlop < poz.Count)
                        oszlop = poz.Count;
                }

                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 7 + oszlop * 3;
                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Psz";
                Tábla2.Columns[0].Width = 60;
                Tábla2.Columns[1].HeaderText = "Erőtám";
                Tábla2.Columns[1].Width = 100;
                Tábla2.Columns[2].HeaderText = "Esztergálás";
                Tábla2.Columns[2].Width = 100;
                Tábla2.Columns[3].HeaderText = "Típus";
                Tábla2.Columns[3].Width = 100;
                Tábla2.Columns[4].HeaderText = "Csatolhatóság";
                Tábla2.Columns[4].Width = 100;
                Tábla2.Columns[5].HeaderText = "Kerékszám";
                Tábla2.Columns[5].Width = 100;
                Tábla2.Columns[6].HeaderText = "Meghibásodás";
                Tábla2.Columns[6].Width = 200;
                if (oszlop > 1)
                {
                    for (int k = 1; k <= oszlop; k++)
                    {
                        Tábla2.Columns[7 + 3 * (k - 1)].HeaderText = "Poz.:";
                        Tábla2.Columns[7 + 3 * (k - 1)].Width = 60;
                        Tábla2.Columns[8 + 3 * (k - 1)].HeaderText = "Áll.:";
                        Tábla2.Columns[8 + 3 * (k - 1)].Width = 60;
                        Tábla2.Columns[9 + 3 * (k - 1)].HeaderText = "Átm.:";
                        Tábla2.Columns[9 + 3 * (k - 1)].Width = 60;
                    }
                }

                // Pályaszámok kiírása
                Feltöltések();

                Tábla2.RowCount = Jármű.Count;
                int j = 0;
                if (Jármű != null)
                {
                    foreach (Adat_Jármű Elem in Jármű)
                    {
                        Holtart.Lép();
                        Tábla2.Rows[j].Cells[0].Value = Elem.Azonosító.Trim();
                        if (oszlop > 1)
                        {
                            Tábla2.Rows[j].Cells[2].Value = "";
                            Tábla2.Rows[j].Cells[7].Value = "";
                            Tábla2_típus(Elem.Azonosító, j);
                            Tábla2_kerékszám(Elem.Azonosító, j);
                            Tábla2_Csatolhatóság(Elem.Azonosító, j);
                            Tábla2_erőtám(Elem.Azonosító, j);
                            Tábla2_esztergálás(Elem.Azonosító, j);
                            Tábla2_Eszt_igény(Elem.Azonosító, j);
                            Tábla2_méretek(Elem.Azonosító, j);
                            Hiba_listázása(Elem.Azonosító, j);
                            j++;
                            Holtart.Lép();
                        }
                    }
                }
                Tábla2.Visible = true;
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

        private void Feltöltések()
        {
            Holtart.Be();

            AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
            Holtart.Lép();

            AdatokKerék = KézKerék.Lista_Adatok().Where(a => a.Objektumfajta == "V.KERÉKPÁR").ToList();
            Holtart.Lép();

            AdatokCsat = KézCsat.Lista_Adat();
            Holtart.Lép();

            AdatokErőTámLista();
            Holtart.Lép();

            AdatokKerékEszterga();
            Holtart.Lép();

            AdatokEsztergaIgényLista();
            Holtart.Lép();

            AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.AddYears(-1).Year);
            List<Adat_Kerék_Mérés> IdeigM = KézMérés.Lista_Adatok(DateTime.Today.Year);
            AdatokMérés.AddRange(IdeigM);
            AdatokMérés = (from a in AdatokMérés
                           orderby a.Kerékberendezés, a.Mikor descending
                           select a).ToList();
            Holtart.Lép();

            HibaLista();
            Holtart.Ki();
        }

        private void Tábla2_típus(string azonosító, int sor)
        {
            try
            {
                if (AdatokJármű != null)
                {
                    Adat_Jármű rekordszer = (from a in AdatokJármű
                                             where a.Azonosító == azonosító
                                             select a).FirstOrDefault();

                    if (rekordszer != null)
                        Tábla2.Rows[sor].Cells[3].Value = rekordszer.Típus.Trim();
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

        private void Tábla2_kerékszám(string azonosító, int sor)
        {
            try
            {
                Tábla2.Rows[sor].Cells[5].Value = 0;
                if (AdatokKerék != null)
                {
                    List<Adat_Kerék_Tábla> Elem = (from a in AdatokKerék
                                                   where a.Azonosító == azonosító
                                                   select a).ToList();
                    if (Elem != null)
                        Tábla2.Rows[sor].Cells[5].Value = Elem.Count;
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

        private void Tábla2_Csatolhatóság(string azonosító, int sor)
        {
            try
            {
                if (AdatokCsat != null)
                {
                    Adat_Osztály_Adat rekordszer = (from a in AdatokCsat
                                                    where a.Azonosító == azonosító
                                                    select a).FirstOrDefault();
                    if (rekordszer != null) Tábla2.Rows[sor].Cells[4].Value = KézCsat.Érték(rekordszer, "Csatolhatóság");
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

        private void Tábla2_erőtám(string azonosító, int sor)
        {
            try
            {
                if (AdatokErőTám != null)
                {
                    Adat_Kerék_Erő rekordszer = (from a in AdatokErőTám
                                                 where a.Azonosító == azonosító
                                                 select a).LastOrDefault();
                    if (rekordszer != null)
                    {
                        if (rekordszer.Van.Trim() == "1")
                            Tábla2.Rows[sor].Cells[1].Value = "Igen";
                        else
                            Tábla2.Rows[sor].Cells[1].Value = "Nem";
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

        private void Tábla2_esztergálás(string azonosító, int sor)
        {
            try
            {
                if (AdatokEszterga != null)
                {
                    Adat_Kerék_Eszterga rekordszer = (from a in AdatokEszterga
                                                      where a.Azonosító == azonosító
                                                      select a).LastOrDefault();
                    if (rekordszer != null)
                        Tábla2.Rows[sor].Cells[2].Value = rekordszer.Eszterga.ToString("yyyy.MM.dd");
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

        private void Tábla2_Eszt_igény(string azonosító, int sor)
        {
            try
            {
                if (AdatokIgény != null)
                {
                    Adat_Kerék_Eszterga_Igény Keres = (from a in AdatokIgény
                                                       where a.Pályaszám.Contains(azonosító)
                                                       select a).LastOrDefault();
                    if (Keres != null)
                    {
                        if (Keres.Státus == 0) Tábla2.Rows[sor].Cells[2].Style.BackColor = Color.Yellow;
                        if (Keres.Státus == 2) Tábla2.Rows[sor].Cells[2].Style.BackColor = Color.Orange;
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

        private void Tábla2_méretek(string azonosító, int sor)
        {
            try
            {
                if (AdatokMérés != null)
                {
                    List<Adat_Kerék_Mérés> Mérések = (from a in AdatokMérés
                                                      where a.Azonosító == azonosító
                                                      orderby a.Mikor.ToString("yyyy.MM.dd") descending, a.Pozíció ascending
                                                      select a).ToList();

                    int oszlop = 7;
                    string előző = "";
                    int kerékszám = Tábla2.Rows[sor].Cells[5].Value.ToÉrt_Int();
                    foreach (Adat_Kerék_Mérés rekord in Mérések)
                    {
                        if (Tábla2.Columns.Count <= oszlop + 2) break;
                        if ((kerékszám * 3) + 5 < oszlop) break;
                        if (rekord.Pozíció.Trim() != előző)
                        {
                            Tábla2.Rows[sor].Cells[oszlop].Value = rekord.Pozíció;
                            előző = rekord.Pozíció;
                            Tábla2.Rows[sor].Cells[oszlop + 1].Value = MilyenÁllapot(rekord.Állapot);
                            switch (rekord.Állapot.Substring(0, 1))
                            {
                                case "2":
                                    {
                                        Tábla2.Rows[sor].Cells[oszlop + 1].Style.BackColor = Color.Yellow;
                                        break;
                                    }
                                case "3":
                                    {
                                        Tábla2.Rows[sor].Cells[oszlop + 1].Style.BackColor = Color.Orange;
                                        break;
                                    }
                                case "4":
                                    {
                                        Tábla2.Rows[sor].Cells[oszlop + 1].Style.BackColor = Color.Red;
                                        break;
                                    }
                            }
                            Tábla2.Rows[sor].Cells[oszlop + 2].Value = rekord.Méret;

                            if (rekord.Méret <= 630)
                            {
                                Tábla2.Rows[sor].Cells[oszlop + 2].Style.BackColor = Color.Red;
                            }
                            else if (rekord.Méret <= 634)
                            {
                                Tábla2.Rows[sor].Cells[oszlop + 2].Style.BackColor = Color.Yellow;
                            }
                            oszlop += 3;

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

        private void Hiba_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokHiba == null) return;
                Adat_Nap_Hiba rekordszer = (from a in AdatokHiba
                                            where a.Azonosító == azonosító
                                            select a).FirstOrDefault();
                if (rekordszer != null) Tábla2.Rows[sor].Cells[6].Value = rekordszer.Üzemképtelen + "-" + rekordszer.Beálló + "-" + rekordszer.Üzemképeshiba;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExcelKöltség_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Kerékméretek_export_{Program.PostásTelephely.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                MyX.DataGridViewToXML(fájlexc, Tábla2);
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

        private void Típus_Feltöltés()
        {
            try
            {
                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                else
                    Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adatok = (from a in Adatok
                          where a.Törölt == false
                          orderby a.Típus
                          select a).ToList();
                List<string> Típusok = Adatok.Select(a => a.Típus).Distinct().ToList();



                Típus_Szűrő.Items.Clear();
                Típus_Szűrő.Items.Add("");
                foreach (string Elem in Típusok)
                    Típus_Szűrő.Items.Add(Elem);

                Típus_Szűrő.Refresh();
            }
            catch (HibásBevittAdat ex)
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


        #region Rögzítések listázása
        private void Command4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dátumtól.Value == Dátumig.Value) throw new HibásBevittAdat("A kezdő és a vég dátumnak különbözőnek kell lennie.");
                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 9;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Psz";
                Tábla1.Columns[0].Width = 70;
                Tábla1.Columns[1].HeaderText = "Berendezésszám";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = "Gyári szám";
                Tábla1.Columns[2].Width = 90;
                Tábla1.Columns[3].HeaderText = "Pozíció";
                Tábla1.Columns[3].Width = 90;
                Tábla1.Columns[4].HeaderText = "Mérés Dátuma";
                Tábla1.Columns[4].Width = 170;
                Tábla1.Columns[5].HeaderText = "Állapot";
                Tábla1.Columns[5].Width = 180;
                Tábla1.Columns[6].HeaderText = "Méret";
                Tábla1.Columns[6].Width = 100;
                Tábla1.Columns[7].HeaderText = "Megnevezés";
                Tábla1.Columns[7].Width = 170;
                Tábla1.Columns[8].HeaderText = "Mérés Oka";
                Tábla1.Columns[8].Width = 170;

                List<Adat_Kerék_Mérés> Adatok = new List<Adat_Kerék_Mérés>();
                for (int Év = Dátumtól.Value.Year; Év <= Dátumig.Value.Year; Év++)
                {
                    List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(Év);
                    Adatok.AddRange(Ideig);

                }

                Adatok = (from a in Adatok
                          where a.Mikor >= Dátumtól.Value
                          && a.Mikor <= Dátumig.Value
                          orderby a.Azonosító, a.Pozíció
                          select a).ToList();

                if (PályaszámCombo2.Text.Trim() != "") Adatok = Adatok.Where(a => a.Azonosító == PályaszámCombo2.Text.Trim()).ToList();

                foreach (Adat_Kerék_Mérés rekord in Adatok)
                {
                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla1.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla1.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla1.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Tábla1.Rows[i].Cells[4].Value = rekord.Mikor.ToString();
                    Tábla1.Rows[i].Cells[5].Value = MilyenÁllapot(rekord.Állapot);
                    Tábla1.Rows[i].Cells[6].Value = rekord.Méret.ToString();
                    Tábla1.Rows[i].Cells[7].Value = rekord.Módosító.Trim();
                    Tábla1.Rows[i].Cells[8].Value = rekord.Oka.Trim();
                }
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

        private string MilyenÁllapot(string Állapot)
        {
            string MilyenÁllapot = "";
            try
            {
                int szám = int.Parse(Állapot);
                MilyenÁllapot = ((MyEn.Kerék_Állapot)szám).ToString().Replace('_', ' ');
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return MilyenÁllapot;
        }

        private void Command9_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Kerékméretek_export_{Program.PostásTelephely.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                MyX.DataGridViewToXML(fájlexc, Tábla1);
                MessageBox.Show("Elkészült az Excel tábla: \n" + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        #region Főmérnökség adatok feltöltése
        private void Command4Főm_Click(object sender, EventArgs e)
        {
            Főmérnökség_Frissítés();
        }

        private void Főmérnökség_Frissítés()
        {
            Tábla1írófőm();
            Jegyzettömbírófőm();
        }

        private void Command5Főm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Visible == false)
                {
                    Jegyzettömb.Visible = false;
                    Tábla1.Visible = true;
                }
                else
                {
                    Jegyzettömb.Visible = true;
                    Tábla1.Visible = false;
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

        private void Tábla1írófőm()
        {
            try
            {
                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 9;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Psz";
                Tábla1.Columns[0].Width = 70;
                Tábla1.Columns[1].HeaderText = "Berendezésszám";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = "Gyári szám";
                Tábla1.Columns[2].Width = 90;
                Tábla1.Columns[3].HeaderText = "Pozíció";
                Tábla1.Columns[3].Width = 90;
                Tábla1.Columns[4].HeaderText = "Mérés Dátuma";
                Tábla1.Columns[4].Width = 170;
                Tábla1.Columns[5].HeaderText = "Állapot";
                Tábla1.Columns[5].Width = 100;
                Tábla1.Columns[6].HeaderText = "Méret";
                Tábla1.Columns[6].Width = 100;
                Tábla1.Columns[7].HeaderText = "Megnevezés";
                Tábla1.Columns[7].Width = 170;
                Tábla1.Columns[8].HeaderText = "Mérés Oka";
                Tábla1.Columns[8].Width = 170;

                List<Adat_Kerék_Mérés> Adatok = KézMérés.Lista_Adatok(Dátumtól.Value.Year);
                AdatSzűrés(ref Adatok);

                foreach (Adat_Kerék_Mérés rekord in Adatok)
                {

                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla1.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla1.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla1.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Tábla1.Rows[i].Cells[4].Value = rekord.Mikor.ToString();
                    Tábla1.Rows[i].Cells[5].Value = MilyenÁllapot(rekord.Állapot);
                    Tábla1.Rows[i].Cells[6].Value = rekord.Méret;
                    Tábla1.Rows[i].Cells[7].Value = rekord.Módosító.Trim();
                    Tábla1.Rows[i].Cells[8].Value = rekord.Oka.Trim();
                }

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

        private void AdatSzűrés(ref List<Adat_Kerék_Mérés> Adatok)
        {
            try
            {
                if (PályaszámCombo2.Text.ToStrTrim() != "")
                    Adatok = Adatok.Where(a => a.Azonosító == PályaszámCombo2.Text.Trim()).ToList();

                if (SAPba.Checked)
                    Adatok = Adatok.Where(a => a.SAP == 1).ToList();
                else
                    Adatok = Adatok.Where(a => a.SAP != 1).ToList();

                if (Dátumtól.Value == Dátumig.Value)
                {
                    Adatok = Adatok.Where(a => a.Mikor >= MyF.Nap0000(Dátumtól.Value) && a.Mikor <= MyF.Nap2359(Dátumtól.Value)).ToList();
                }
                else
                {
                    Adatok = Adatok.Where(a => a.Mikor >= MyF.Nap0000(Dátumtól.Value) && a.Mikor <= MyF.Nap2359(Dátumig.Value)).ToList();
                }
                Adatok = Adatok.OrderBy(a => a.Azonosító).ThenBy(a => a.Pozíció).ToList();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Jegyzettömbírófőm()
        {
            try
            {
                Jegyzettömb.Text = "";
                List<Adat_Kerék_Mérés> Adatok = KézMérés.Lista_Adatok(Dátumtól.Value.Year);
                AdatSzűrés(ref Adatok);

                foreach (Adat_Kerék_Mérés rekord in Adatok)
                {
                    string szöveg = rekord.Kerékberendezés.Trim() + "\t";
                    szöveg += "ATM_J" + "\t" + "\t" + "\t" + "\t" + "\t";
                    szöveg += "0" + "\t" + "\t" + "\t";
                    szöveg += rekord.Mikor.ToString("yyyy.MM.dd") + "\t";
                    szöveg += rekord.Mikor.ToString("hh:mm:ss") + "\t";
                    if (rekord.Módosító.Trim().Length < 12)
                        szöveg += rekord.Módosító.Trim() + "\t";
                    else
                        szöveg += rekord.Módosító.Trim().Substring(0, 12) + "\t";


                    szöveg += rekord.Méret.ToString() + "\t" + "\t";

                    if (rekord.Oka.Trim().Length < 39)
                        szöveg += rekord.Oka.Trim();
                    else
                        szöveg += rekord.Oka.Trim().Substring(0, 40);

                    szöveg += "\r" + "\n";

                    Jegyzettömb.Text += szöveg;
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

        private void Command7Főm_Click(object sender, EventArgs e)
        {
            try
            {
                Jegyzettömb.Visible = false;

                Tábla1.Visible = true;
                Holtart.Be(Tábla1.Rows.Count + 2);

                AdatokMérés.Clear();
                AdatokMérés = KézMérés.Lista_Adatok(Dátumtól.Value.Year);

                List<Adat_Kerék_Mérés> AdatokGy = new List<Adat_Kerék_Mérés>();
                for (int i = 0; i < Tábla1.Rows.Count; i++)
                {
                    string kerékberendezés = Tábla1.Rows[i].Cells[1].Value.ToString();

                    DateTime rögzítésdátuma = DateTime.Parse(Tábla1.Rows[i].Cells[4].Value.ToString());
                    Adat_Kerék_Mérés AdatKerék = (from a in AdatokMérés
                                                  where a.Kerékberendezés == kerékberendezés.Trim()
                                                  && a.Mikor.ToShortDateString() == rögzítésdátuma.ToShortDateString()
                                                  select a).FirstOrDefault();
                    if (AdatKerék != null)
                    {
                        Adat_Kerék_Mérés Adat = new Adat_Kerék_Mérés(kerékberendezés.Trim(), rögzítésdátuma, 1);
                        AdatokGy.Add(Adat);
                    }
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézMérés.Módosítás(Dátumtól.Value.Year, AdatokGy);

                Holtart.Ki();
                MessageBox.Show("Az adatok státus állítása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Főmérnökség_Frissítés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Command3Főm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Jegyzettömb.Text.Trim() == "") return;
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Kerék esztergálási adatok előkészítése SAP-ba való feltöltéshez",
                    FileName = $"Kerékmérések_export_{Dátumtól.Value:yyyyMMdd}_{Dátumig.Value:yyyyMMdd}",
                    Filter = "Normal text file |*.txt"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                TextWriter Writer = new StreamWriter(fájlexc);
                Writer.Write(Jegyzettömb.Text);
                Writer.Close();
                //Vágólapra másoljuk az elérési utat
                Clipboard.SetText(fájlexc);

                MessageBox.Show("Az adatok mentése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
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


        #region Mérési adatok lapfül
        private void RögzítPályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Új_Gyári.Visible = false;
            Pályaszám_választás();
        }

        private void Pályaszám_választás()
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") return;
                Rögzítürít();

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
                List<Adat_Kerék_Tábla> EgyKocsi = (from a in Adatok
                                                   where a.Azonosító == RögzítPályaszám.Text.Trim()
                                                   orderby a.Pozíció
                                                   select a).ToList();
                Rögzítpozíció.Items.Clear();
                foreach (Adat_Kerék_Tábla Elem in EgyKocsi)
                    Rögzítpozíció.Items.Add(Elem.Pozíció);

                Rögzítpozíció.Refresh();

                ChkErőtám.Checked = Erőtámkiolvasás(SAPPályaszám.Text.Trim());

                EsztergaDátum.Text = "";
                KMU_old.Text = "";
                Adat_Kerék_Eszterga rekord = Esztergakiolvasás(SAPPályaszám.Text.Trim());
                if (rekord != null)
                {
                    EsztergaDátum.Text = rekord.Eszterga.ToString("yyyy.MM.dd");
                    KMU_old.Text = rekord.KMU.ToString();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Pályaszám_választás();
            Új_Gyári.Visible = false;
        }

        private void Rögzítürít()
        {
            ChkErőtám.Checked = false;
            Rögzítpozíció.Text = "";
            Gyártási.Text = "";
            Berendezés.Text = "";
            Megnevezés.Text = "";
            RögzítÁllapot.Text = "";
            Állapot.Text = "";
            Méret.Text = "";
            Oka.Text = "";
            EsztergaDátum.Text = "";
        }

        private void Rögzítpozíció_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Rögzítpozíció.Text.Trim() == "") return;
                if (RögzítPályaszám.Text.Trim() == "") return;

                // kiírjuk az utolsó értékeket
                Gyártási.Text = "";
                Megnevezés.Text = "";
                Berendezés.Text = "";
                Állapot.Text = "";
                Méret.Text = "";
                Oka.Text = "";

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
                Adat_Kerék_Tábla Elem = (from a in Adatok
                                         where a.Azonosító == RögzítPályaszám.Text.Trim()
                                         && a.Pozíció == Rögzítpozíció.Text.Trim()
                                         select a).FirstOrDefault();
                if (Elem != null)
                {
                    Gyártási.Text = Elem.Kerékgyártásiszám.Trim();
                    Megnevezés.Text = Elem.Kerékmegnevezés.Trim();
                    Berendezés.Text = Elem.Kerékberendezés.Trim();
                }

                AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.AddYears(-1).Year);
                List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(DateTime.Today.Year);
                AdatokMérés.AddRange(Ideig);

                Adat_Kerék_Mérés Mérés = (from a in AdatokMérés
                                          where a.Azonosító == RögzítPályaszám.Text.Trim()
                                          && a.Kerékberendezés == Berendezés.Text.Trim()
                                          orderby a.Mikor descending
                                          select a).FirstOrDefault();

                if (Mérés != null)
                {
                    Állapot.Text = MilyenÁllapot(Mérés.Állapot.Trim().Substring(0, 1));
                    Méret.Text = Mérés.Méret.ToString();
                    Oka.Text = Mérés.Oka.Trim();
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

        private void Rögzítrögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("A jármű pályaszámát meg kell adni.");
                if (Rögzítpozíció.Text.Trim() == "") throw new HibásBevittAdat("A poizíciót meg kell adni.");
                if (RögzítÁllapot.Text.Trim() == "") throw new HibásBevittAdat("Az állapotot meg kell adni.");
                if (RögzítOka.Text.Trim() == "") throw new HibásBevittAdat("A rögzítés okát meg kell adni.");
                if (!int.TryParse(RögzítMéret.Text, out int Méret)) throw new HibásBevittAdat("A méret mezőnek egész számnak kell lennie.");
                if (Méret > 1000) throw new HibásBevittAdat("Biztos, hogy a kerék mérete 1000 mm-nél nagyobb?");
                if (Új_Gyári.Visible == true)
                {
                    if (Új_Gyári.Text.Trim() == "") throw new HibásBevittAdat("A gyári szám mezót ki kell tölteni.");
                    ÚjGyáriKitöltése();
                }
                string[] Darabol = RögzítÁllapot.Text.Trim().Split('-');

                Adat_Kerék_Mérés ADAT = new Adat_Kerék_Mérés(
                                        RögzítPályaszám.Text.Trim(),
                                        Rögzítpozíció.Text.Trim(),
                                        Berendezés.Text.Trim(),
                                        Gyártási.Text.Trim(),
                                        Darabol[0],
                                        Méret,
                                        Program.PostásNév,
                                        DateTime.Now,
                                        RögzítOka.Text.Trim(),
                                        Új_Gyári.Visible ? 1 : 0);
                KézMérés.Rögzítés(DateTime.Today.Year, ADAT);

                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Rögzítpozíció.Text = "";
                if (Új_Gyári.Visible == true)
                {
                    Új_Gyári.Text = "";
                    Gyártási.Text = "";
                    Berendezés.Text = "";
                    Megnevezés.Text = "";
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

        private void ÚjGyáriKitöltése()
        {
            Gyártási.Text = Új_Gyári.Text.Trim();
            Berendezés.Text = "Ideiglenes";
            Megnevezés.Text = "Ideiglenes fődarab";
        }

        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszámot meg kell adni.");

                AdatokErőTám = KézErőTám.Lista_Adatok(DateTime.Today.Year);
                Adat_Kerék_Erő AdatKerékErő = (from a in AdatokErőTám
                                               where a.Azonosító == RögzítPályaszám.Text.Trim()
                                               select a).FirstOrDefault();

                Adat_Kerék_Erő ADAT = new Adat_Kerék_Erő(
                          RögzítPályaszám.Text.Trim(),
                          ChkErőtám.Checked ? "1" : "0",
                          Program.PostásNév.Trim(),
                          DateTime.Now);

                if (AdatKerékErő != null)
                    KézErőTám.Módosítás(DateTime.Today.Year, ADAT);
                else
                    KézErőTám.Rögzítés(DateTime.Today.Year, ADAT);

                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("A jármű pályaszámát meg kell adni.");
                if (!long.TryParse(KMU_új.Text.Trim(), out long KMU_érték))
                {
                    KMU_új.Text = "0";
                    KMU_érték = 0;
                }
                Adat_Kerék_Eszterga ADAT = new Adat_Kerék_Eszterga(
                          RögzítPályaszám.Text.Trim(),
                          Eszterga.Value,
                          Program.PostásNév.Trim(),
                          DateTime.Now,
                          KMU_érték);
                KézEszterga.Rögzítés(DateTime.Today.Year, ADAT);
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Új_Pozíció_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva pályaszám.");
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatokKerék = KézKerék.Lista_Adatok().Where(a => a.Objektumfajta == "V.KERÉKPÁR").ToList();

                string típus = (from a in AdatokJármű
                                where a.Azonosító == RögzítPályaszám.Text.Trim()
                                select a.Valóstípus).FirstOrDefault();
                if (típus != null)
                {
                    List<string> pozíciók = (from a in AdatokJármű
                                             join b in AdatokKerék on a.Azonosító equals b.Azonosító
                                             where a.Valóstípus == típus && b.Objektumfajta == "V.KERÉKPÁR"
                                             orderby b.Pozíció
                                             select b.Pozíció).Distinct().ToList();

                    Rögzítpozíció.Items.Clear();
                    Rögzítpozíció.BeginUpdate();

                    foreach (string Elem in pozíciók)
                        Rögzítpozíció.Items.Add(Elem);


                    Rögzítpozíció.EndUpdate();
                    Rögzítpozíció.Refresh();
                    Új_gyári_be();
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

        private void Új_gyári_be()
        {
            Új_Gyári.Left = 313;
            Új_Gyári.Top = 62;
            Új_Gyári.Visible = true;
        }
        #endregion


        #region Kerékesztergára ütemez
        Ablak_Kerék_segéd Új_Ablak_Kerék_segéd;

        private void Kerék_Ütemez_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.SelectedRows.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve egy jármű sem kerékesztergálásra.");

                List<string> Küld = new List<string>();
                int tengely = 0;
                int prioritás = 0;
                int[] prioritásdb = { 0, 0, 0, 0 };
                int norma = 0;
                string típusküld = Tábla2.SelectedRows[0].Cells[3].Value.ToStrTrim();

                List<Adat_Kerék_Eszterga_Tengely> Normaidő = KézTengely.Lista_Adatok();
                Normaidő = (from a in Normaidő
                            where a.Típus == típusküld
                            orderby a.Állapot
                            select a).ToList();

                foreach (DataGridViewRow Sor in Tábla2.SelectedRows)
                {
                    Küld.Add(Sor.Cells[0].Value.ToStrTrim());
                    for (int oszlop = 8; oszlop < Tábla2.ColumnCount - 1; oszlop += 3)
                    {
                        if (Sor.Cells[oszlop].Value != null)
                        {
                            string beolvasott = Sor.Cells[oszlop].Value.ToStrTrim();
                            if (beolvasott == "") beolvasott = "1";
                            switch (beolvasott.Substring(0, 1))
                            {
                                case "1":
                                    prioritásdb[0]++;
                                    if (prioritás < 1) prioritás = 1;
                                    break;
                                case "2":
                                    tengely++;
                                    prioritásdb[1]++;
                                    if (prioritás < 2) prioritás = 2;
                                    break;
                                case "3":
                                    prioritásdb[2]++;
                                    if (prioritás < 3) prioritás = 3;
                                    tengely++;
                                    break;
                                case "4":
                                    prioritásdb[3]++;
                                    tengely++;
                                    if (prioritás < 4) prioritás = 4;
                                    break;
                            }
                        }
                    }
                }

                foreach (Adat_Kerék_Eszterga_Tengely rekord in Normaidő)
                {
                    norma += prioritásdb[rekord.Állapot - 1] * rekord.Munkaidő;
                }

                Küld.Sort();
                string Szerelvény = "";
                foreach (string elem in Küld)
                    Szerelvény += elem + "-";

                Szerelvény = Szerelvény.Substring(0, Szerelvény.Length - 1);

                Új_Ablak_Kerék_segéd?.Close();

                Új_Ablak_Kerék_segéd = new Ablak_Kerék_segéd(Cmbtelephely.Text.Trim(), Szerelvény.Trim(), tengely, prioritás, típusküld, norma);
                Új_Ablak_Kerék_segéd.FormClosed += Ablak_Kerék_segéd_Closed;
                Új_Ablak_Kerék_segéd.Változás += Tábla2_Eszt_igényLista;
                Új_Ablak_Kerék_segéd.Show();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla2_Eszt_igényLista()
        {
            try
            {
                AdatokEsztergaIgényLista();
                if (AdatokIgény != null)
                {
                    for (int i = 0; i < Tábla2.Rows.Count; i++)
                    {
                        string PályaSzám = Tábla2.Rows[i].Cells[0].Value.ToStrTrim();
                        //Adat_Kerék_Eszterga_Igény Keres = Összes.Find(s => s.Pályaszám.Trim() == PályaSzám.Trim());
                        Adat_Kerék_Eszterga_Igény Keres = AdatokIgény.Find(x => x.Pályaszám.Contains(PályaSzám));
                        if (Keres != null)
                        {
                            if (Keres.Státus == 0)
                                Tábla2.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                            if (Keres.Státus == 2)
                                Tábla2.Rows[i].Cells[2].Style.BackColor = Color.Orange;
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

        private void Ablak_Kerék_segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_segéd = null;
        }
        #endregion


        #region Táblázatos rögzítés
        Ablak_Kerék_Gyűjtő Új_Ablak_Kerék_gyűjtő;

        private void GyűjtőRögzítés_Click(object sender, EventArgs e)
        {
            Új_Ablak_Kerék_gyűjtő?.Close();

            Új_Ablak_Kerék_gyűjtő = new Ablak_Kerék_Gyűjtő(RögzítPályaszám.Text.Trim());
            Új_Ablak_Kerék_gyűjtő.FormClosed += Ablak_Kerék_Gyűjtő_Closed;
            Új_Ablak_Kerék_gyűjtő.Show();
        }

        private void Ablak_Kerék_Gyűjtő_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_gyűjtő = null;
        }
        #endregion


        #region Listák
        private void AdatokErőTámLista()
        {
            try
            {
                AdatokErőTám.Clear();
                AdatokErőTám = KézErőTám.Lista_Adatok(DateTime.Today.Year - 1);
                List<Adat_Kerék_Erő> Ideig = KézErőTám.Lista_Adatok(DateTime.Today.Year);
                AdatokErőTám.AddRange(Ideig);
                AdatokErőTám = (from a in AdatokErőTám
                                orderby a.Azonosító, a.Mikor descending
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

        private void AdatokEsztergaIgényLista()
        {
            try
            {
                AdatokIgény.Clear();
                AdatokIgény = KézEsztIgény.Lista_Adatok(DateTime.Today.AddYears(-1).Year);
                List<Adat_Kerék_Eszterga_Igény> AdatokIgény1 = KézEsztIgény.Lista_Adatok(DateTime.Today.Year);
                AdatokIgény.AddRange(AdatokIgény1);
                AdatokIgény = AdatokIgény.Where(a => a.Státus < 8).ToList();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HibaLista()
        {
            try
            {
                if (Program.PostásTelephely != "Főmérnökség") Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                AdatokHiba.Clear();
                AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdatokKerékEszterga()
        {
            try
            {
                AdatokEszterga.Clear();
                AdatokEszterga = KézEszterga.Lista_Adatok(DateTime.Today.AddYears(-1).Year);
                List<Adat_Kerék_Eszterga> Ideig = KézEszterga.Lista_Adatok(DateTime.Today.Year);
                AdatokEszterga.AddRange(Ideig);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Erőtámkiolvasás(string pályaszám)
        {
            bool Válasz = false;
            try
            {
                // betöljük az utolsó erőtám adatot
                AdatokErőTámLista();
                Adat_Kerék_Erő Elem = (from a in AdatokErőTám
                                       where a.Azonosító == pályaszám
                                       orderby a.Mikor descending
                                       select a).FirstOrDefault();
                if (Elem != null)
                {
                    if (Elem.Van == "van") Válasz = true;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KerékNyilvántartás_funkciók", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

        private Adat_Kerék_Eszterga Esztergakiolvasás(string pályaszám)
        {
            Adat_Kerék_Eszterga Válasz = null;
            try
            {
                // betöljük az utolsó erőtám adatot
                AdatokKerékEszterga();
                Válasz = (from a in AdatokEszterga
                          where a.Azonosító == pályaszám
                          orderby a.Mikor descending
                          select a).FirstOrDefault();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KerékNyilvántartás_funkciók", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

        #endregion

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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