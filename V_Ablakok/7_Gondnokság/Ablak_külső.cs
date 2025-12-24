using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{

    public partial class Ablak_külső
    {
        int Email_id = 0;
        bool Rádió_főmérnök = false;
        string Telephely_választott = "";

        readonly Kezelő_Külső_Cégek Kéz_Külső_Cégek = new Kezelő_Külső_Cégek();
        readonly Kezelő_Behajtás_Engedélyezés Kéz_Behajtás_Engedély = new Kezelő_Behajtás_Engedélyezés();
        readonly Kezelő_Külső_Telephelyek Kéz_Külső_Telephelyek = new Kezelő_Külső_Telephelyek();
        readonly Kezelő_Kiegészítő_Jelenlétiív Kéz_Kieg_Jelenlétiív = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Külső_Email Kéz_Külső_Email = new Kezelő_Külső_Email();
        readonly Kezelő_Külső_Gépjárművek Kéz_Járművek = new Kezelő_Külső_Gépjárművek();
        readonly Kezelő_Külső_Dolgozók Kéz_Dolgozó = new Kezelő_Külső_Dolgozók();

        List<Adat_Külső_Gépjárművek> Adatok_autó = new List<Adat_Külső_Gépjárművek>();
        List<Adat_Külső_Dolgozók> Adatok_Dolg = new List<Adat_Külső_Dolgozók>();
        List<Adat_Külső_Cégek> Adatok_Külső_Cégek = new List<Adat_Külső_Cégek>();
        List<Adat_Behajtás_Engedélyezés> Adatok_Behajtás_Engedély = new List<Adat_Behajtás_Engedélyezés>();
        List<Adat_Külső_Telephelyek> Adatok_Külső_Telephelyek = new List<Adat_Külső_Telephelyek>();
        List<Adat_Kiegészítő_Jelenlétiív> Adatok_Kieg_Jelenlétiív = new List<Adat_Kiegészítő_Jelenlétiív>();
        List<Adat_Külső_Email> Adatok_Külső_Email = new List<Adat_Külső_Email>();


        readonly Beállítás_Betű BeBetű = new Beállítás_Betű();
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Vastag = true };

        #region alap
        public Ablak_külső()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_külső_Load(object sender, EventArgs e)
        {

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
                string helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\";
                if (!Directory.Exists(helyi)) Directory.CreateDirectory(helyi);

                helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_PDF";
                if (!Directory.Exists(helyi)) Directory.CreateDirectory(helyi);

                LapFülek.SelectedIndex = 0;
                Fülekkitöltése();

                LapFülek.DrawMode = TabDrawMode.OwnerDrawFixed;

                // autó lap
                Autó_cégnév.Text = "";
                Autó_munka.Text = "";
                Autó_Cégid.Text = "";

                // Dolgozólap
                Dolg_cégneve.Text = "";
                Dolg_munka.Text = "";
                Dolg_cégid.Text = "";

                // Telephely
                Telephely_Cégnév.Text = "";
                Telephely_Munka.Text = "";
                Telephely_Cégid.Text = "";

                CÉG_ürít();
                Engedély_lejárat();

                Adatok_Behajtás_Engedély = Kéz_Behajtás_Engedély.Lista_Adatok();

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Rádió_főmérnök = true;
                    Email_id = (from a in Adatok_Behajtás_Engedély
                                where a.Szakszolgálat == true && a.Gondnok == true
                                select a.Id).FirstOrDefault();
                }
                else
                {
                    Rádió_főmérnök = false;
                    Telephely_választott = Program.PostásTelephely.Trim();
                    // megkeressük, hogy a telephely melyik szakszolgálatba tartozik
                    string szakszolgálatszöveg = (from a in Adatok_Behajtás_Engedély
                                                  where a.Telephely.Trim() == Program.PostásTelephely.Trim()
                                                  select a.Szakszolgálatszöveg).FirstOrDefault();

                    Email_id = (from a in Adatok_Behajtás_Engedély
                                where a.Szakszolgálat == true && a.Szakszolgálatszöveg.Trim() == szakszolgálatszöveg.Trim()
                                select a.Id).FirstOrDefault();
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                if (Cmbtelephely.Enabled == true)
                {
                    BtnSzakszeng.Visible = true;
                    Engedély_elutasítás.Visible = true;
                    Engedély_visszavonás.Visible = true;
                }
                else
                {
                    BtnSzakszeng.Visible = false;
                    Engedély_elutasítás.Visible = false;
                    Engedély_visszavonás.Visible = false;
                }

                BtnSzakszeng.Enabled = false;
                Engedély_elutasítás.Enabled = false;
                Engedély_visszavonás.Enabled = false;
                Telephely_rögzít.Enabled = false;
                Alap_Rögzít.Enabled = false;
                Cégek_engedélyezésre.Enabled = false;

                Dolg_Rögzít.Enabled = false;
                Dolgozó_beolvas.Enabled = false;
                Dolgozó_töröl.Enabled = false;
                Email_rögzít.Enabled = false;

                Autó_ok.Enabled = false;
                Autó_töröl.Enabled = false;
                Autó_beolvas.Enabled = false;

                melyikelem = 247;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Autó_ok.Enabled = true;
                    Autó_töröl.Enabled = true;
                    Autó_beolvas.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Dolg_Rögzít.Enabled = true;
                    Dolgozó_töröl.Enabled = true;
                    Dolgozó_beolvas.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {

                }
                melyikelem = 248;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Telephely_rögzít.Enabled = true;

                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Alap_Rögzít.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Cégek_engedélyezésre.Enabled = true;
                }

                melyikelem = 249;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    BtnSzakszeng.Enabled = true;
                    Email_rögzít.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Engedély_elutasítás.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Engedély_visszavonás.Enabled = true;
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

        private void Fülekkitöltése()
        {
            try
            {
                Gombok_váltása();

                switch (LapFülek.SelectedIndex)
                {
                    case 0:
                        {
                            // Cég alapadatok

                            Cég_Tábla_író();
                            break;
                        }
                    case 1:
                        {
                            // dolgozók
                            Dolg_új_tiszta();
                            Dolg_tábla_író();
                            break;
                        }
                    case 2:
                        {
                            // autók
                            Autó_Ürítés();
                            Autó_tábla_lista();
                            break;
                        }
                    case 3:
                        {
                            // telephelyek
                            Telephely_tábla_alap_kiírás();
                            Telephely_tábla_jog_kiírás();
                            break;
                        }
                    case 4:
                        {
                            // Engedélyezés
                            Engedély_Tábla_író(1);
                            break;
                        }
                    case 6:
                        {
                            // Email
                            Email_kiírás();
                            break;
                        }
                    case 7:
                        {
                            // PDF
                            Pdflistázása();
                            break;
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            string helyi = Application.StartupPath + @"\Súgó\VillamosLapok\Külső_dolgozók.html";
            MyF.Megnyitás(helyi);
        }

        private void LapFülek_DrawItem(object sender, DrawItemEventArgs e)
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
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
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
        #endregion


        #region Cégek
        private void Alap_Új_adat_Click(object sender, EventArgs e)
        {
            CÉG_ürít();
            Gombok_váltása();
        }

        private void CÉG_ürít()
        {
            Cég_Aktív.Checked = false;
            Cég_cég.Text = "";
            Cég_címe.Text = "";
            Cég_email.Text = "";
            Cég_felelős_személy.Text = "";
            Cég_felelős_telefon.Text = "";
            Cég_Munkaleírás.Text = "";
            Cég_sorszám.Text = "";
            Cég_Érv_kezdet.Value = DateTime.Today;
            Cég_Érv_vég.Value = DateTime.Today;

            Cég_mikor.Items.Clear();
            Cég_mikor.Items.Add("üzemidőben");
            Cég_mikor.Items.Add("üzemszünetben");
            Cég_mikor.Items.Add("üzemidőben és üzemszünetben");

            Cég_engedély_státus.Items.Clear();
            Cég_engedély_státus.Items.Add("0 - Feltöltés alatt");
            Cég_engedély_státus.Items.Add("1 - Engedélyezhető");
            Cég_engedély_státus.Items.Add("5 - Engedélyezett");
            Cég_engedély_státus.Items.Add("7 - Visszavont");
            Cég_engedély_státus.Items.Add("8 - Lejárt");
            Cég_engedély_státus.Items.Add("9 - Törölt");
            Cég_engedély_státus.Text = "0 - Feltöltés alatt";

            if (!Cmbtelephely.Enabled)
            {
                Rádió_főmérnök = false;
                Telephely_választott = Cmbtelephely.Text.Trim();
            }
            else
            {
                Rádió_főmérnök = true;
            }
        }

        private void Alap_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cég_cég.Text.Trim() == "") throw new HibásBevittAdat("A cég neve mezőt ki kell tölteni.");
                if (Cég_címe.Text.Trim() == "") throw new HibásBevittAdat("A cég címe mezőt ki kell tölteni.");
                if (Cég_email.Text.Trim() == "") throw new HibásBevittAdat("A cég e-mail mezőt ki kell tölteni.");
                if (Cég_felelős_személy.Text.Trim() == "") throw new HibásBevittAdat("A felelős személyneve mezőt ki kell tölteni.");
                if (Cég_felelős_telefon.Text.Trim() == "") throw new HibásBevittAdat("A felelős telefonszáma mezőt ki kell tölteni.");
                if (Cég_Munkaleírás.Text.Trim() == "") throw new HibásBevittAdat("A munkaleírása mezőt ki kell tölteni.");
                if (Cég_engedély_státus.Text.Trim() == "") throw new HibásBevittAdat("Az engedély státusa mező nem lehet üres.");
                if (Cég_email.Text.Trim().IndexOf("@") < 0) throw new HibásBevittAdat("Az e-mail cím nem felel meg az előírásnak");
                if (Cég_email.Text.Contains(',')) Cég_email.Text = Cég_email.Text.Replace(',', ';');
                if (Cég_email.Text.Contains(' ')) Cég_email.Text = Cég_email.Text.Replace(' ', ';');


                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok();

                // Megkeressük a soron következőt
                if (Cég_sorszám.Text.Trim() == "")
                {
                    double rekord = Adatok_Külső_Cégek.Any() ? Adatok_Külső_Cégek.Max(a => a.Cégid) + 1 : 1;
                    Cég_sorszám.Text = rekord.ToString();
                    Adat_Külső_Cégek ADAT = new Adat_Külső_Cégek(
                        rekord,
                        Cég_cég.Text.Trim(),
                        Cég_címe.Text.Trim().Replace(",", ";"),
                        Cég_email.Text.Trim(),
                        Cég_felelős_személy.Text.Trim(),
                        Cég_felelős_telefon.Text.Trim(),
                        Cég_Munkaleírás.Text.Trim(),
                        Cég_mikor.Text.Trim(),
                        Cég_Érv_kezdet.Value,
                        Cég_Érv_vég.Value,
                        new DateTime(1900, 1, 1),
                        "_",
                        0,
                        false,
                        Rádió_főmérnök ? "Főmérnökség" : Cmbtelephely.Text.Trim());
                    Kéz_Külső_Cégek.Rögzítés(ADAT);
                }
                else
                {
                    Adat_Külső_Cégek ADAT = new Adat_Külső_Cégek(
                        Cég_sorszám.Text.Trim().ToÉrt_Double(),
                        Cég_cég.Text.Trim(),
                        Cég_címe.Text.Trim().Replace(",", ";"),
                        Cég_email.Text.Trim(),
                        Cég_felelős_személy.Text.Trim(),
                        Cég_felelős_telefon.Text.Trim(),
                        Cég_Munkaleírás.Text.Trim(),
                        Cég_mikor.Text.Trim(),
                        Cég_Érv_kezdet.Value,
                        Cég_Érv_vég.Value,
                        new DateTime(1900, 1, 1),
                        "_",
                        Cég_Aktív.Checked ? 9 : 0,
                        Cég_Aktív.Checked,
                        Rádió_főmérnök ? "Főmérnökség" : Cmbtelephely.Text.Trim());
                    Kéz_Külső_Cégek.Módosítás(ADAT);
                }

                // frissítjük a táblázatban
                Cég_Tábla_író();


                // megkeressük a táblázatban és újra kiírjuk        
                for (int i = 0; i < Cég_tábla.Rows.Count; i++)
                {
                    if (Cég_tábla.Rows[i].Cells[0].Value.ToString() == Cég_sorszám.Text.Trim())
                    {
                        Cégtábal_katt(i);
                        break;
                    }
                }
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cég_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cég_tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Külső_" + Program.PostásTelephely + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                //  bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Cég_tábla);
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

        private void Alap_Frissít_Click(object sender, EventArgs e)
        {
            try
            {
                Cég_sorszám.Text = "";
                Cég_Tábla_író();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cég_Tábla_író()
        {
            try
            {
                List<Adat_Külső_Cégek> Adatok = Kéz_Külső_Cégek.Lista_Adatok().OrderBy(y => y.Cégid).ToList();

                Cég_tábla.Rows.Clear();
                Cég_tábla.Columns.Clear();
                Cég_tábla.Refresh();
                Cég_tábla.Visible = false;
                Cég_tábla.ColumnCount = 15;

                // fejléc elkészítése
                Cég_tábla.Columns[0].HeaderText = "S.sz";
                Cég_tábla.Columns[0].Width = 50;
                Cég_tábla.Columns[1].HeaderText = "Cég";
                Cég_tábla.Columns[1].Width = 300;
                Cég_tábla.Columns[2].HeaderText = "Munkaleírása";
                Cég_tábla.Columns[2].Width = 550;
                Cég_tábla.Columns[3].HeaderText = "Cég címe";
                Cég_tábla.Columns[3].Width = 200;
                Cég_tábla.Columns[4].HeaderText = "Cég e-mail";
                Cég_tábla.Columns[4].Width = 200;
                Cég_tábla.Columns[5].HeaderText = "Felelős személy";
                Cég_tábla.Columns[5].Width = 200;
                Cég_tábla.Columns[6].HeaderText = "Felelős telefonszáma";
                Cég_tábla.Columns[6].Width = 120;
                Cég_tábla.Columns[7].HeaderText = "Munka ideje";
                Cég_tábla.Columns[7].Width = 200;
                Cég_tábla.Columns[8].HeaderText = "Kezdő dátum";
                Cég_tábla.Columns[8].Width = 100;
                Cég_tábla.Columns[9].HeaderText = "Befejező dátum";
                Cég_tábla.Columns[9].Width = 100;
                Cég_tábla.Columns[10].HeaderText = "Eng. dátuma";
                Cég_tábla.Columns[10].Width = 100;
                Cég_tábla.Columns[11].HeaderText = "Engedélyező";
                Cég_tábla.Columns[11].Width = 130;
                Cég_tábla.Columns[12].HeaderText = "Engedélyezve";
                Cég_tábla.Columns[12].Width = 150;
                Cég_tábla.Columns[13].HeaderText = "Státus";
                Cég_tábla.Columns[13].Width = 100;
                Cég_tábla.Columns[14].HeaderText = "Terület";
                Cég_tábla.Columns[14].Width = 120;

                foreach (Adat_Külső_Cégek rekord in Adatok)
                {

                    Cég_tábla.RowCount++;
                    int i = Cég_tábla.RowCount - 1;
                    Cég_tábla.Rows[i].Cells[0].Value = rekord.Cégid;
                    Cég_tábla.Rows[i].Cells[1].Value = rekord.Cég.Trim();
                    Cég_tábla.Rows[i].Cells[2].Value = rekord.Munkaleírás.Trim();
                    Cég_tábla.Rows[i].Cells[3].Value = rekord.Címe.Trim();
                    Cég_tábla.Rows[i].Cells[4].Value = rekord.Cég_email.Trim();
                    Cég_tábla.Rows[i].Cells[5].Value = rekord.Felelős_személy.Trim();
                    Cég_tábla.Rows[i].Cells[6].Value = rekord.Felelős_telefonszám.Trim();
                    Cég_tábla.Rows[i].Cells[7].Value = rekord.Mikor.Trim();
                    Cég_tábla.Rows[i].Cells[8].Value = rekord.Érv_kezdet.ToString("yyyy.MM.dd");
                    Cég_tábla.Rows[i].Cells[9].Value = rekord.Érv_vég.ToString("yyyy.MM.dd");
                    Cég_tábla.Rows[i].Cells[10].Value = rekord.Engedélyezés_dátuma.ToString("yyyy.MM.dd");
                    Cég_tábla.Rows[i].Cells[11].Value = rekord.Engedélyező.Trim();
                    switch (rekord.Engedély)
                    {
                        case 0:
                            {
                                Cég_tábla.Rows[i].Cells[12].Value = "0 - Feltöltés alatt";
                                break;
                            }
                        case 1:
                            {
                                Cég_tábla.Rows[i].Cells[12].Value = "1 - Engedélyezhető";
                                break;
                            }
                        case 5:
                            {
                                Cég_tábla.Rows[i].Cells[12].Value = "5 - Engedélyezett";
                                break;
                            }
                        case 7:
                            {
                                Cég_tábla.Rows[i].Cells[12].Value = "7 - Elutasított/Visszavont";
                                break;
                            }
                        case 8:
                            {
                                Cég_tábla.Rows[i].Cells[12].Value = "8 - Lejárt";
                                break;
                            }
                        case 9:
                            {
                                Cég_tábla.Rows[i].Cells[12].Value = "9 - Törölt";
                                break;
                            }

                        default:
                            {
                                Cég_tábla.Rows[i].Cells[12].Value = "0 - Feltöltés alatt";
                                break;
                            }
                    }
                    if (rekord.Státus)
                        Cég_tábla.Rows[i].Cells[13].Value = "Törölt";
                    else
                        Cég_tábla.Rows[i].Cells[13].Value = "Aktív";

                    Cég_tábla.Rows[i].Cells[14].Value = rekord.Terület.Trim();
                }

                Cég_tábla_Formázás();
                Cég_tábla.Visible = true;
                Cég_tábla.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cég_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Cégtábal_katt(e.RowIndex);
            Gombok_váltása();
        }

        private void Gombok_váltása()
        {
            Telephely_rögzít.Visible = false;
            Alap_Rögzít.Visible = false;
            Cégek_engedélyezésre.Visible = false;
            Autó_ok.Visible = false;
            Dolg_Rögzít.Visible = false;
            Dolgozó_töröl.Visible = false;
            Autó_töröl.Visible = false;
            Dolgozó_beolvas.Visible = false;
            Autó_beolvas.Visible = false;

            if (Cmbtelephely.Enabled == true || Cmbtelephely.Enabled == false && Rádió_főmérnök == false && (Telephely_választott.Trim() == Cmbtelephely.Text.Trim()))
            {
                Telephely_rögzít.Visible = true;
                Alap_Rögzít.Visible = true;
                Cégek_engedélyezésre.Visible = true;
                Autó_ok.Visible = true;
                Dolg_Rögzít.Visible = true;
                Dolgozó_töröl.Visible = true;
                Autó_töröl.Visible = true;
                Dolgozó_beolvas.Visible = true;
                Autó_beolvas.Visible = true;
            }
        }

        private void Cégtábal_katt(int sor)
        {
            try
            {
                if (Cég_tábla.Rows.Count < 1)
                    return;
                {
                    Cég_sorszám.Text = Cég_tábla.Rows[sor].Cells[0].Value.ToString();
                    Cég_cég.Text = Cég_tábla.Rows[sor].Cells[1].Value.ToString();
                    Cég_Munkaleírás.Text = Cég_tábla.Rows[sor].Cells[2].Value.ToString();
                    Cég_címe.Text = Cég_tábla.Rows[sor].Cells[3].Value.ToString();
                    Cég_email.Text = Cég_tábla.Rows[sor].Cells[4].Value.ToString();
                    Cég_felelős_személy.Text = Cég_tábla.Rows[sor].Cells[5].Value.ToString();
                    Cég_felelős_telefon.Text = Cég_tábla.Rows[sor].Cells[6].Value.ToString();
                    Cég_Érv_kezdet.Value = DateTime.Parse(Cég_tábla.Rows[sor].Cells[8].Value.ToString());
                    Cég_Érv_vég.Value = DateTime.Parse(Cég_tábla.Rows[sor].Cells[9].Value.ToString());
                    Cég_mikor.Text = Cég_tábla.Rows[sor].Cells[7].Value.ToString();
                    Cég_engedély_státus.Text = Cég_tábla.Rows[sor].Cells[12].Value.ToString();
                    if (Cég_tábla.Rows[sor].Cells[13].Value.ToString().Trim() == "Törölt")
                        Cég_Aktív.Checked = true;
                    else
                        Cég_Aktív.Checked = false;

                    Telephely_választott = Cég_tábla.Rows[sor].Cells[14].Value.ToString().Trim();
                    if (Cég_tábla.Rows[sor].Cells[14].Value.ToString().Trim() == "Főmérnökség")
                        Rádió_főmérnök = true;
                    else
                        Rádió_főmérnök = false;


                    // autó lap
                    Autó_cégnév.Text = Cég_tábla.Rows[sor].Cells[1].Value.ToString().Trim();
                    Autó_munka.Text = Cég_tábla.Rows[sor].Cells[2].Value.ToString().Trim();
                    Autó_Cégid.Text = Cég_tábla.Rows[sor].Cells[0].Value.ToString().Trim();

                    // Dolgozólap
                    Dolg_cégneve.Text = Cég_tábla.Rows[sor].Cells[1].Value.ToString().Trim();
                    Dolg_munka.Text = Cég_tábla.Rows[sor].Cells[2].Value.ToString().Trim();
                    Dolg_cégid.Text = Cég_tábla.Rows[sor].Cells[0].Value.ToString().Trim();

                    // Telephely
                    Telephely_Cégnév.Text = Cég_tábla.Rows[sor].Cells[1].Value.ToString().Trim();
                    Telephely_Munka.Text = Cég_tábla.Rows[sor].Cells[2].Value.ToString().Trim();
                    Telephely_Cégid.Text = Cég_tábla.Rows[sor].Cells[0].Value.ToString().Trim();

                    // Pdf lap
                    PDF_cégneve.Text = Cég_tábla.Rows[sor].Cells[1].Value.ToString().Trim();
                    PDF_munka.Text = Cég_tábla.Rows[sor].Cells[2].Value.ToString().Trim();
                    PDF_cégid.Text = Cég_tábla.Rows[sor].Cells[0].Value.ToString().Trim();

                    // ha nincs feltöltve az elem akkor nem látszódik a rögzítő gomb

                    string helyi = Application.StartupPath + @"\Főmérnökség\Adatok\Behajtási\Külső_PDF\";
                    helyi += PDF_cégid.Text.Trim() + "_" + Cég_Érv_kezdet.Value.ToString("yyyyMMdd") + "_" + Cég_Érv_vég.Value.ToString("yyyyMMdd") + ".pdf";
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

        private void Cégek_engedélyezésre_Click(object sender, EventArgs e)
        {
            try
            {
                int volt = 0;
                int hiba = 0;
                //a kijelöléseken végig megyünk és csak a 0 státust hagyjuk bejelölve

                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok();

                for (int i = 0; i < Cég_tábla.Rows.Count; i++)
                {
                    if (Cég_tábla.Rows[i].Cells[12].Value.ToString().Trim().Substring(0, 1) != "0" || Cég_tábla.Rows[i].Cells[12].Value.ToString().Trim() == "Törölt")
                        Cég_tábla.Rows[i].Selected = false;
                }

                List<Adat_Külső_Cégek> ADATOK = new List<Adat_Külső_Cégek>();
                for (int iii = 0; iii < Cég_tábla.Rows.Count; iii++)
                {
                    if (Cég_tábla.Rows[iii].Selected)
                    {
                        // ha ki volt jelölve, akkor megvizsgáljuk, hogy minden rendben van 
                        Cégtábal_katt(iii);
                        // nincs dolgozó
                        Dolg_tábla_író();
                        if (Dolg_tábla.Rows.Count < 1)
                            hiba = 3;
                        // nincs telephely
                        Telephely_tábla_alap_kiírás();
                        Telephely_tábla_jog_kiírás();
                        int Valami = 0;
                        for (int sor = 0; sor < Telephely_Tábla.Rows.Count; sor++)
                        {
                            if (bool.Parse(Telephely_Tábla.Rows[sor].Cells[0].Value.ToString()))
                            {
                                Valami = 1;
                                break;
                            }
                        }
                        if (Valami == 0)
                            hiba += 2;
                        // ha minden rendben van a feltöltöt adatokkal 
                        if (hiba == 0)
                        {
                            // csak a sajátját engedi engedélyezésre küldeni
                            if (Cmbtelephely.Enabled == true && Cég_tábla.Rows[iii].Cells[14].Value.ToString().Trim() == "Főmérnökség" || Cmbtelephely.Enabled == false && Cég_tábla.Rows[iii].Cells[14].Value.ToString().Trim() == Cmbtelephely.Text.Trim())
                            {
                                // csak a feltöltés alattiakat lehet elküldeni engedélyezésre
                                if (!double.TryParse(Cég_tábla.Rows[iii].Cells[0].Value.ToStrTrim(), out double CegId)) CegId = 0;
                                bool vane = Adatok_Külső_Cégek.Any(a => a.Cégid == CegId && a.Engedély == 0);
                                if (vane)
                                {
                                    Adat_Külső_Cégek ADAT = new Adat_Külső_Cégek(Cég_tábla.Rows[iii].Cells[0].Value.ToString().ToÉrt_Double(), 1);
                                    ADATOK.Add(ADAT);
                                    volt = 1;
                                }
                            }
                        }
                        else if (hiba == 2)
                        {
                            MessageBox.Show("Nincs kijelölve egy telephely sem a " + Cég_tábla.Rows[iii].Cells[0].Value.ToString().Trim() + " sorszámú cégnek.", "Engedélyezésre nem lett elküldve", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (hiba == 3)
                        {
                            MessageBox.Show("Nincs egy dolgozója sem a " + Cég_tábla.Rows[iii].Cells[0].Value.ToString().Trim() + " sorszámú cégnek.", "Engedélyezésre nem lett elküldve", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (hiba == 5)
                        {
                            MessageBox.Show("Nincs kijelölve egy telephely sem és nincs egy dolgózója sem a " + Cég_tábla.Rows[iii].Cells[0].Value.ToString().Trim() + " sorszámú cégnek.", "Engedélyezésre nem lett elküldve", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        hiba = 0;
                    }
                }
                Kéz_Külső_Cégek.Engedélyezésre(ADATOK);

                if (volt == 1)
                {
                    if (Rádió_főmérnök)
                        Főmérnöki_engedély_email();
                    else
                        Szakszolg_engedély_email();
                }
                Cég_sorszám.Text = "";
                Cég_Tábla_író();
                CÉG_ürít();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Főmérnöki_engedély_email()
        {
            try
            {
                Engedély_Tábla_író(1);
                if (Engedély_tábla.Rows.Count < 1) return;

                int ii = 0;
                Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail;

                mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                Cmbtelephely.Text = Cmbtelephely.Items[ii].ToString();

                string címzett = "";

                // Főmérnöki email cím

                Adatok_Behajtás_Engedély = Kéz_Behajtás_Engedély.Lista_Adatok();
                címzett = Adatok_Behajtás_Engedély
                    .Where(a => a.Gondnok == true && a.Szakszolgálat == true)
                    .Select(a => a.Emailcím)
                    .FirstOrDefault();


                string tárgy = $"Belépési és munkavégzési engedély engedélyezése {DateTime.Now:yyyyMMdd}";
                string tartalom = $"{Engedély_tábla.Rows.Count} darab engedélyezési feladata vannak a Villamos programban.\n\r\n\r Ezt az e-mailt a Villamos program generálta.";
                if (!(címzett.Trim() == ""))
                {
                    // üzenet címzettje
                    mail.To = címzett;
                    // üzent szövege
                    mail.Body = tartalom;
                    // üzenet tárgya
                    mail.Subject = tárgy;
                    mail.Send();
                    MessageBox.Show("Üzenet el lett küldve az engedélyező személynek.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Szakszolg_engedély_email()
        {
            try
            {
                Engedély_Tábla_író(1);
                if (Engedély_tábla.Rows.Count < 1) return;

                int ii = 0;
                Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail;

                mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                Cmbtelephely.Text = Cmbtelephely.Items[ii].ToString();

                string címzett = "";

                // Főmérnöki email cím

                Adatok_Behajtás_Engedély = Kéz_Behajtás_Engedély.Lista_Adatok();
                string szakszolgálatszöveg = (from a in Adatok_Behajtás_Engedély
                                              where a.Telephely.Trim() == Telephely_választott.Trim()
                                              select a.Szakszolgálatszöveg).FirstOrDefault();

                címzett = (from a in Adatok_Behajtás_Engedély
                           where a.Gondnok == false
                           && a.Szakszolgálat == true
                           && a.Szakszolgálatszöveg.Trim() == szakszolgálatszöveg.Trim()
                           select a.Emailcím).FirstOrDefault();

                string tárgy = "Belépési és munkavégzési engedély engedélyezése " + DateTime.Now.ToString("yyyyMMdd");
                string tartalom = Engedély_tábla.Rows.Count + " darab engedélyezési feladata vannak a Villamos programban.\n\r\n\r Ezt az e-mailt a Villamos program generálta.";
                if (!(címzett.Trim() == ""))
                {
                    // üzenet címzettje
                    mail.To = címzett;
                    // üzent szövege
                    mail.Body = tartalom;
                    // üzenet tárgya
                    mail.Subject = tárgy;
                    ((Microsoft.Office.Interop.Outlook._MailItem)mail).Send();
                    MessageBox.Show("Üzenet el lett küldve az engedélyező személynek.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Cég_tábla_Formázás()
        {
            try
            {
                // egész sor színezése ha törölt
                foreach (DataGridViewRow row in Cég_tábla.Rows)
                {
                    switch (row.Cells[12].Value.ToString().Substring(0, 1))
                    {
                        case "0":
                            {
                                break;
                            }
                        // nem színezzük
                        case "1":
                            {
                                row.DefaultCellStyle.ForeColor = Color.Black;
                                row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Italic);
                                break;
                            }
                        case "5":
                            {
                                row.DefaultCellStyle.ForeColor = Color.Black;
                                row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                                break;
                            }

                        case "8":
                            {
                                row.DefaultCellStyle.ForeColor = Color.White;
                                row.DefaultCellStyle.BackColor = Color.Red;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f);
                                break;
                            }
                        case "9":
                            {
                                row.DefaultCellStyle.ForeColor = Color.White;
                                row.DefaultCellStyle.BackColor = Color.Red;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                                break;
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


        #region autó
        private void Autó_Ürítés()
        {
            Autó_FRSZ.Text = "";

            Autó_státus.Items.Clear();
            Autó_státus.Items.Add("Érvényes");
            Autó_státus.Items.Add("Törölt");
            Autó_státus.Text = "Érvényes";

            Autó_FRSZ.Focus();
            AcceptButton = Autó_ok;
        }


        private void Autó_tábla_lista()
        {
            try
            {
                if (Autó_Cégid.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva cég.");

                Adatok_autó = Kéz_Járművek.Lista_Adatok();
                Adatok_autó = (from a in Adatok_autó
                               where a.Státus == false
                          && a.Cégid == Autó_Cégid.Text.Trim().ToÉrt_Double()
                               orderby a.Id
                               select a).ToList();
                Autó_fejléc();

                string válasz = "Érvényes";
                foreach (Adat_Külső_Gépjárművek rekord in Adatok_autó)
                {
                    Tábla_autó.RowCount++;
                    int i = Tábla_autó.RowCount - 1;

                    Tábla_autó.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla_autó.Rows[i].Cells[1].Value = rekord.Frsz.Trim();
                    Tábla_autó.Rows[i].Cells[2].Value = rekord.Cégid;
                    if (!rekord.Státus)
                        válasz = "Érvényes";
                    else
                        válasz = "Törölt";

                    Tábla_autó.Rows[i].Cells[3].Value = válasz;
                }
                Tábla_autó_Formázás();
                Tábla_autó.Visible = true;
                Tábla_autó.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Autó_fejléc()
        {
            Tábla_autó.Rows.Clear();
            Tábla_autó.Columns.Clear();
            Tábla_autó.Refresh();
            Tábla_autó.Visible = false;
            Tábla_autó.ColumnCount = 4;

            // fejléc elkészítése
            Tábla_autó.Columns[0].HeaderText = "Sorszám";
            Tábla_autó.Columns[0].Width = 100;
            Tábla_autó.Columns[1].HeaderText = "Frsz";
            Tábla_autó.Columns[1].Width = 100;
            Tábla_autó.Columns[2].HeaderText = "Cég kód";
            Tábla_autó.Columns[2].Width = 100;
            Tábla_autó.Columns[3].HeaderText = "Státus";
            Tábla_autó.Columns[3].Width = 100;
        }


        private void Autó_Frissít_Click(object sender, EventArgs e)
        {
            Autó_tábla_lista();
        }


        private void Tábla_autó_Formázás()
        {
            // egész sor színezése ha törölt
            foreach (DataGridViewRow row in Tábla_autó.Rows)
            {
                if (row.Cells[3].Value.ToString().Trim() == "Törölt")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                }
            }
        }


        private void Autó_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (Autó_Cégid.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes cég.");
                if (Autó_FRSZ.Text.Trim() == "") throw new HibásBevittAdat("Az autó rendszáma mező nem lehet üres.");
                if (Autó_státus.Text.Trim() == "") Autó_státus.Text = "Érvényes";

                // ha szóközzel van elválasztva akkor javítja és nagybetűsít
                Autó_FRSZ.Text = Autó_FRSZ.Text.ToUpper().Replace(" ", "").Replace("-", "");

                List<Adat_Külső_Gépjárművek> Adatok = Kéz_Járművek.Lista_Adatok();
                double id = Adatok.Any() ? Adatok.Max(a => a.Id) + 1 : 1;

                if (!double.TryParse(Telephely_Cégid.Text, out double CegId)) CegId = 0;
                bool vane = Adatok.Any(a => a.Cégid == CegId && a.Frsz.Trim() == Autó_FRSZ.Text.Trim());

                if (vane)
                {
                    Adat_Külső_Gépjárművek ADAT = new Adat_Külső_Gépjárművek(
                        0, //nincs szükség rá
                        Autó_FRSZ.Text.Trim(),
                        CegId,
                        Autó_státus.Text.Trim() != "Érvényes");
                    Kéz_Járművek.Módosítás(ADAT);
                }
                else
                {
                    Adat_Külső_Gépjárművek ADAT = new Adat_Külső_Gépjárművek(
                                id, //nincs szükség rá
                                Autó_FRSZ.Text.Trim(),
                                CegId,
                                false);
                    Kéz_Járművek.Rögzítés(ADAT);
                }
                Autó_tábla_lista();
                Autó_Ürítés();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Autó_Új_Click(object sender, EventArgs e)
        {
            Autó_Ürítés();
        }


        private void Tábla_autó_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tábla_autó.Rows.Count < 1) return;
            if (e.RowIndex < 0) return;

            Autó_FRSZ.Text = Tábla_autó.Rows[e.RowIndex].Cells[1].Value.ToString();
            Autó_státus.Text = Tábla_autó.Rows[e.RowIndex].Cells[3].Value.ToString();
        }


        private void Autó_beviteli_Click(object sender, EventArgs e)
        {
            try
            {
                if (Autó_Cégid.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve cég.");
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Autó beviteli tábla készítése",
                    FileName = $"Autó_Beviteli_tábla_{Autó_Cégid.Text.Trim()}-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Autó_fejléc();
                Tábla_autó.Visible = true;
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű);


                // fejléc kiírása
                for (int oszlop = 0; oszlop < Tábla_autó.ColumnCount; oszlop++)
                {
                    MyX.Kiir(Tábla_autó.Columns[oszlop].HeaderText.Trim(), MyF.Oszlopnév(oszlop + 1) + "1");
                    MyX.Oszlopszélesség(munkalap, $"{MyF.Oszlopnév(oszlop + 1)}:{MyF.Oszlopnév(oszlop + 1)}", 30);
                }

                // megformázzuk
                MyX.Rácsoz(munkalap, $"A1:{MyF.Oszlopnév(Tábla_autó.ColumnCount)}2");

                MyX.Betű(munkalap, $"A1:{MyF.Oszlopnév(Tábla_autó.ColumnCount)}1", BeBetűV);
                MyX.Háttérszín(munkalap, $"A1:{MyF.Oszlopnév(Tábla_autó.ColumnCount)}1", Color.Yellow);
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(Tábla_autó.ColumnCount)}2",

                    BalMargó = 5,
                    JobbMargó = 5,
                    FelsőMargó = 5,
                    AlsóMargó = 5,
                    FejlécMéret = 8,
                    LáblécMéret = 8,

                    LapMagas = 1,
                    LapSzéles = 1,

                    Papírméret = "A4",
                    Álló = false,
                    VízKözép = true,
                    FüggKözép = true
                };

                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                MessageBox.Show($"Elkészült az Excel tábla: {fájlexc}", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        private void Autó_beolvas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Autó_Cégid.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva érvényes cég.");

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Autó Adatok betöltése: " + Autó_cégnév.Text.Trim(),
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // megnyitjuk a beolvasandó táblát
                string munkalap = "Munka1";
                MyX.ExcelMegnyitás(fájlexc);

                // megnézzük, hogy hány sorból áll a tábla
                int ii = 1;
                int utolsó = 0;
                while (MyX.Beolvas(munkalap, $"b{ii}").Trim() != "_")
                {
                    utolsó = ii;
                    ii += 1;
                }
                Holtart.Be(utolsó);

                if (utolsó > 1)
                {
                    List<Adat_Külső_Gépjárművek> ADATOK = new List<Adat_Külső_Gépjárművek>();
                    for (int i = 2; i <= utolsó; i++)
                    {
                        // ha szóközzel van elválasztva akkor javítja és nagybetűsít
                        string rendszám = MyX.Beolvas(munkalap, $"b{i}").Trim().ToUpper().Replace(" ", "").Replace("-", "");
                        Adat_Külső_Gépjárművek ADAT = new Adat_Külső_Gépjárművek(
                            0,
                            rendszám,
                            Autó_Cégid.Text.Trim().ToÉrt_Double(),
                            false);
                        ADATOK.Add(ADAT);
                        Holtart.Lép();
                    }
                    Kéz_Járművek.Döntés(ADATOK);
                }


                // bezárjuk az excel táblát
                MyX.ExcelBezárás();

                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);

                Autó_tábla_lista();
                Autó_Ürítés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Autó_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_autó.Rows.Count == 0) throw new HibásBevittAdat("Nincs elem a táblázatban.");
                if (Tábla_autó.SelectedRows.Count == 0) throw new HibásBevittAdat("Nincs kijelölve elem a táblázatban.");

                List<Adat_Külső_Gépjárművek> Adatok = new List<Adat_Külső_Gépjárművek>();
                for (int i = 0; i < Tábla_autó.SelectedRows.Count; i++)
                {
                    Adat_Külső_Gépjárművek ADAT = new Adat_Külső_Gépjárművek(
                        Tábla_autó.SelectedRows[i].Cells[0].Value.ToString().ToÉrt_Double(),
                        true);
                    Adatok.Add(ADAT);
                }
                if (Adatok.Count > 0) Kéz_Járművek.Törlés(Adatok);
                Autó_tábla_lista();
            }
            catch (HibásBevittAdat ex)
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


        #region Dolgozó adatok
        private void Dolg_új_Click(object sender, EventArgs e)
        {
            Dolg_új_tiszta();
        }

        private void Dolg_új_tiszta()
        {
            try
            {
                Dolg_Dolgozónév.Text = "";

                Dolg_Személyi.Text = "";

                Dolg_Státus.Items.Clear();
                Dolg_Státus.Items.Add("Érvényes");
                Dolg_Státus.Items.Add("Törölt");
                Dolg_Státus.Text = "Érvényes";

                Dolg_Dolgozónév.Focus();
                AcceptButton = Dolg_Rögzít;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dolg_frissít_Click(object sender, EventArgs e)
        {
            Dolg_tábla_író();
        }

        private void Dolg_tábla_író()
        {
            try
            {
                if (Dolg_cégid.Text.Trim() == "") throw new HibásBevittAdat("Cég nincs kiválasztva.");

                Dolgozó_Tábla_fejléc();

                Adatok_Dolg = Kéz_Dolgozó.Lista_Adatok();
                Adatok_Dolg = (from a in Adatok_Dolg
                               where a.Státus == false &&
                               a.Cégid == Dolg_cégid.Text.Trim().ToÉrt_Double()
                               orderby a.Id
                               select a).ToList();

                string válasz = "Érvényes";
                foreach (Adat_Külső_Dolgozók rekord in Adatok_Dolg)
                {
                    Dolg_tábla.RowCount++;
                    int i = Dolg_tábla.RowCount - 1;
                    Dolg_tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Dolg_tábla.Rows[i].Cells[1].Value = rekord.Név.Trim();
                    Dolg_tábla.Rows[i].Cells[2].Value = rekord.Okmányszám.Trim();
                    Dolg_tábla.Rows[i].Cells[3].Value = rekord.Cégid;
                    if (!rekord.Státus)
                        válasz = "Érvényes";
                    else
                        válasz = "Törölt";

                    Dolg_tábla.Rows[i].Cells[4].Value = válasz;
                }
                Dolg_tábla_Formázás();
                Dolg_tábla.Visible = true;
                Dolg_tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dolgozó_Tábla_fejléc()
        {
            Dolg_tábla.Rows.Clear();
            Dolg_tábla.Columns.Clear();
            Dolg_tábla.Refresh();
            Dolg_tábla.Visible = false;
            Dolg_tábla.ColumnCount = 5;

            // fejléc elkészítése
            Dolg_tábla.Columns[0].HeaderText = "Sorszám";
            Dolg_tábla.Columns[0].Width = 80;
            Dolg_tábla.Columns[1].HeaderText = "Név";
            Dolg_tábla.Columns[1].Width = 300;
            Dolg_tábla.Columns[2].HeaderText = "Szem ig szám";
            Dolg_tábla.Columns[2].Width = 100;
            Dolg_tábla.Columns[3].HeaderText = "Cég kód";
            Dolg_tábla.Columns[3].Width = 100;
            Dolg_tábla.Columns[4].HeaderText = "Státus";
            Dolg_tábla.Columns[4].Width = 100;
        }

        private void Dolg_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolg_cégid.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva cég.");
                if (Dolg_Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("A dolgozó név mezőt ki kell tölteni.");
                if (Dolg_Személyi.Text.Trim() == "") throw new HibásBevittAdat("A személyi igazolványszám mezőt ki kell tölteni.");

                Adat_Külső_Dolgozók ADAT = new Adat_Külső_Dolgozók(
                     Dolg_Dolgozónév.Text.Trim(),
                     Dolg_Személyi.Text.Trim(),
                     Dolg_cégid.Text.Trim().ToÉrt_Double(),
                     Dolg_Státus.Text.Trim() != "Érvényes");
                Kéz_Dolgozó.Döntés(ADAT);

                Dolg_tábla_író();
                Dolg_új_tiszta();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dolg_tábla_Formázás()
        {
            // egész sor színezése ha törölt
            foreach (DataGridViewRow row in Dolg_tábla.Rows)
            {
                if (row.Cells[4].Value.ToString().Trim() == "Törölt")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                }
            }
        }

        private void Dolg_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dolg_tábla.Rows.Count < 1) return;
            if (e.RowIndex < 0) return;

            Dolg_Dolgozónév.Text = Dolg_tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            Dolg_Személyi.Text = Dolg_tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
        }

        private void Dolgozó_kivitel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolg_cégid.Text.Trim() == "") return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Dolgozói beviteli tábla készítése",
                    FileName = $"Dolgozói_Beviteli_tábla_{Dolg_cégid.Text.Trim()}-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                //  bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // Csak a fejlécet írjuk ki
                Dolgozó_Tábla_fejléc();
                Dolg_tábla.Visible = true;
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű);

                for (int oszlop = 0; oszlop < Dolg_tábla.ColumnCount; oszlop++)
                {
                    MyX.Kiir(Dolg_tábla.Columns[oszlop].HeaderText, MyF.Oszlopnév(oszlop + 1) + "1");
                    MyX.Oszlopszélesség(munkalap, $"{MyF.Oszlopnév(oszlop + 1)}:{MyF.Oszlopnév(oszlop + 1)}", 30);
                }
                MyX.Rácsoz(munkalap, $"A1:{MyF.Oszlopnév(Dolg_tábla.ColumnCount)}2");
                MyX.Háttérszín(munkalap, $"A1:{MyF.Oszlopnév(Dolg_tábla.ColumnCount)}1", Color.Yellow);
                MyX.Betű(munkalap, $"A1:{MyF.Oszlopnév(Dolg_tábla.ColumnCount)}1", BeBetűV);

                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(Dolg_tábla.ColumnCount)}2",

                    BalMargó = 5,
                    JobbMargó = 5,
                    FelsőMargó = 5,
                    AlsóMargó = 5,
                    FejlécMéret = 8,
                    LáblécMéret = 8,

                    LapMagas = 1,
                    LapSzéles = 1,
                    Papírméret = "A4",
                    Álló = false,
                    VízKözép = true,
                    FüggKözép = true
                };

                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
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

        private void Dolgozó_beolvas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolg_cégid.Text.Trim() == "") throw new HibásBevittAdat("Cég nincs kiválasztva.");

                var Idő = new DateTime(1900, 1, 1);

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Dolgozó Adatok betöltése: " + Dolg_cégneve.Text.Trim(),
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                // megnyitjuk a beolvasandó táblát
                string munkalap = "Munka1";
                MyX.ExcelMegnyitás(fájlexc);

                // megnézzük, hogy hány sorból áll a tábla
                int ii = 1;
                int utolsó = 0;
                while (MyX.Beolvas(munkalap, $"b{ii}").Trim() != "_")
                {
                    utolsó = ii;
                    ii += 1;
                }
                Holtart.Be(utolsó);

                if (utolsó > 1)
                {
                    List<Adat_Külső_Dolgozók> ADATOK = new List<Adat_Külső_Dolgozók>();
                    for (int i = 2; i <= utolsó; i++)
                    {
                        string Név = MyX.Beolvas(munkalap, $"b{i}").Trim().Replace(",", "");
                        string Személyi = MyX.Beolvas(munkalap, $"c{i}").Trim().Replace(",", "");
                        Adat_Külső_Dolgozók ADAT = new Adat_Külső_Dolgozók(
                            Név,
                            Személyi,
                            Dolg_cégid.Text.Trim().ToÉrt_Double(),
                            false);
                        ADATOK.Add(ADAT);
                        Holtart.Lép();
                    }
                    if (ADATOK.Count > 0) Kéz_Dolgozó.Döntés(ADATOK);
                }
                // bezárjuk az excel táblát
                MyX.ExcelBezárás();

                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);

                Dolg_tábla_író();
                Dolg_új_tiszta();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dolgozó_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolg_tábla.Rows.Count == 0) return;
                if (Dolg_tábla.SelectedRows.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                List<Adat_Külső_Dolgozók> Adatok = new List<Adat_Külső_Dolgozók>();
                for (int i = 0; i <= Dolg_tábla.SelectedRows.Count - 1; i++)
                {
                    Adat_Külső_Dolgozók ADAT = new Adat_Külső_Dolgozók(
                        Dolg_tábla.SelectedRows[i].Cells[0].Value.ToString().ToÉrt_Double(),
                        true);
                    Adatok.Add(ADAT);
                }
                if (Adatok.Count > 0) Kéz_Dolgozó.Törlés(Adatok);

                Dolg_tábla_író();
                MessageBox.Show("Az adatok törlése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
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


        #region Telephely
        private void Btn1szak_Click(object sender, EventArgs e)
        {
            Telephely_Tábla.Rows[0].Cells[0].Value = true;
            Telephely_Tábla.Rows[1].Cells[0].Value = true;
            Telephely_Tábla.Rows[2].Cells[0].Value = true;
        }

        private void Btn2szak_Click(object sender, EventArgs e)
        {
            Telephely_Tábla.Rows[3].Cells[0].Value = true;
            Telephely_Tábla.Rows[4].Cells[0].Value = true;
            Telephely_Tábla.Rows[5].Cells[0].Value = true;
            Telephely_Tábla.Rows[6].Cells[0].Value = true;
        }

        private void Btn3szak_Click(object sender, EventArgs e)
        {
            Telephely_Tábla.Rows[7].Cells[0].Value = true;
            Telephely_Tábla.Rows[8].Cells[0].Value = true;
            Telephely_Tábla.Rows[9].Cells[0].Value = true;
        }

        private void BtnKijelölcsop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 9; i++)
                Telephely_Tábla.Rows[i].Cells[0].Value = true;
        }

        private void Btnkilelöltörlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 9; i++)
                Telephely_Tábla.Rows[i].Cells[0].Value = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Telephely_tábla_alap_kiírás();
            Telephely_tábla_jog_kiírás();
        }

        private void Telephely_tábla_alap_kiírás()
        {
            try
            {

                Telephely_Tábla.Rows.Clear();
                Telephely_Tábla.Refresh();
                Telephely_Tábla.Visible = false;

                List<Adat_Behajtás_Engedélyezés> Adatok = Kéz_Behajtás_Engedély.Lista_Adatok().Where(a => a.Gondnok == true && a.Szakszolgálat == false).ToList();

                foreach (Adat_Behajtás_Engedélyezés rekord in Adatok)
                {

                    Telephely_Tábla.RowCount++;
                    int i = Telephely_Tábla.RowCount - 1;
                    Telephely_Tábla.Rows[i].Cells[0].Value = false;
                    Telephely_Tábla.Rows[i].Cells[1].Value = rekord.Telephely.Trim();
                    Telephely_Tábla.Rows[i].Cells[2].Value = rekord.Név.Trim();
                    Telephely_Tábla.Rows[i].Cells[3].Value = rekord.Beosztás.Trim();
                    Telephely_Tábla.Rows[i].Cells[4].Value = rekord.Emailcím.Trim();
                    Telephely_Tábla.Rows[i].Cells[5].Value = rekord.Telefonszám.Trim();
                }

                Telephely_Tábla.Visible = true;
                Telephely_Tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephely_tábla_jog_kiírás()
        {
            try
            {
                if (Telephely_Tábla.Rows.Count < 1) return;
                if (Telephely_Cégid.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva Cég.");


                Adatok_Külső_Telephelyek = Kéz_Külső_Telephelyek.Lista_Adatok();

                for (int i = 0; i <= Telephely_Tábla.Rows.Count - 1; i++)
                {
                    if (!double.TryParse(Telephely_Cégid.Text, out double CegId)) CegId = 0;
                    bool vane = Adatok_Külső_Telephelyek.Any(a =>
                        a.Cégid == CegId &&
                        a.Telephely.Trim() == Telephely_Tábla.Rows[i].Cells[1].Value.ToStrTrim() &&
                        a.Státus == true);
                    if (vane) Telephely_Tábla.Rows[i].Cells[0].Value = true;
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

        private void Telephely_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Telephely_Tábla.Rows.Count < 1) return;
                if (Telephely_Cégid.Text.Trim() == "" || Telephely_Cégid.Text.Trim() == "Cégid") throw new HibásBevittAdat("Nincs kiválasztva Cég.");

                List<Adat_Külső_Telephelyek> AdatokM = new List<Adat_Külső_Telephelyek>();
                List<Adat_Külső_Telephelyek> AdatokR = new List<Adat_Külső_Telephelyek>();
                for (int i = 0; i < Telephely_Tábla.Rows.Count; i++)
                {
                    if (!double.TryParse(Telephely_Cégid.Text, out double CegId)) CegId = 0;
                    bool vane = Adatok_Külső_Telephelyek.Any(a => a.Cégid == CegId && a.Telephely.Trim() == Telephely_Tábla.Rows[i].Cells[1].Value.ToStrTrim());
                    if (vane)
                    {
                        Adat_Külső_Telephelyek ADAT = new Adat_Külső_Telephelyek(
                            0,
                            Telephely_Tábla.Rows[i].Cells[1].Value.ToStrTrim(),
                           Telephely_Cégid.Text.ToÉrt_Double(),
                           bool.Parse(Telephely_Tábla.Rows[i].Cells[0].Value.ToString()));
                        AdatokM.Add(ADAT);
                    }
                    else
                    {
                        // ha nincs akkor újként rögzítjük
                        Adat_Külső_Telephelyek ADAT = new Adat_Külső_Telephelyek(
                             0,
                             Telephely_Tábla.Rows[i].Cells[1].Value.ToStrTrim(),
                            Telephely_Cégid.Text.ToÉrt_Double(),
                            bool.Parse(Telephely_Tábla.Rows[i].Cells[0].Value.ToString()));
                        AdatokR.Add(ADAT);
                    }
                }
                if (AdatokM.Count > 0) Kéz_Külső_Telephelyek.Módosítás(AdatokM);
                if (AdatokR.Count > 0) Kéz_Külső_Telephelyek.Rögzítés(AdatokR);

                MessageBox.Show("Az adat rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Engedélyezés
        private void Engedély_frissít_Click(object sender, EventArgs e)
        {
            Engedély_Tábla_író(1);
        }

        private void Engedély_Tábla_író(int sor)
        {
            try
            {
                List<Adat_Külső_Cégek> AdatokIdeig = Kéz_Külső_Cégek.Lista_Adatok();
                List<Adat_Külső_Cégek> Adatok = new List<Adat_Külső_Cégek>();
                if (sor == 1)
                {
                    if (Rádió_főmérnök)
                    {
                        Adatok = (from a in AdatokIdeig
                                  where a.Engedély == 1 &&
                                  a.Terület == "Főmérnökség"
                                  orderby a.Cégid
                                  select a).ToList();
                    }
                    else
                    {
                        // telephelyek meghatározása
                        for (int k = 0; k < Cmbtelephely.Items.Count; k++)
                        {
                            List<Adat_Külső_Cégek> AdatokI = (from a in AdatokIdeig
                                                              where a.Engedély == 1 &&
                                                              a.Terület.Trim() == Cmbtelephely.Items[k].ToStrTrim()
                                                              orderby a.Cégid
                                                              select a).ToList();
                            Adatok.AddRange(AdatokI);
                        }
                        Adatok = Adatok.OrderBy(y => y.Cégid).ToList();
                    }
                }
                else
                {
                    Adatok = (from a in AdatokIdeig
                              orderby a.Cégid
                              select a).ToList();
                }

                Engedély_tábla.Rows.Clear();
                Engedély_tábla.Columns.Clear();
                Engedély_tábla.Refresh();
                Engedély_tábla.Visible = false;
                Engedély_tábla.ColumnCount = 14;

                // fejléc elkészítése
                Engedély_tábla.Columns[0].HeaderText = "S.sz";
                Engedély_tábla.Columns[0].Width = 80;
                Engedély_tábla.Columns[1].HeaderText = "Cég";
                Engedély_tábla.Columns[1].Width = 300;
                Engedély_tábla.Columns[2].HeaderText = "Munkaleírása";
                Engedély_tábla.Columns[2].Width = 300;
                Engedély_tábla.Columns[3].HeaderText = "Cég címe";
                Engedély_tábla.Columns[3].Width = 200;
                Engedély_tábla.Columns[4].HeaderText = "Cég e-mail";
                Engedély_tábla.Columns[4].Width = 200;
                Engedély_tábla.Columns[5].HeaderText = "Felelős személy";
                Engedély_tábla.Columns[5].Width = 200;
                Engedély_tábla.Columns[6].HeaderText = "Felelős telefonszáma";
                Engedély_tábla.Columns[6].Width = 200;
                Engedély_tábla.Columns[7].HeaderText = "Munka ideje";
                Engedély_tábla.Columns[7].Width = 100;
                Engedély_tábla.Columns[8].HeaderText = "Kezdő dátum";
                Engedély_tábla.Columns[8].Width = 100;
                Engedély_tábla.Columns[9].HeaderText = "Befejező dátum";
                Engedély_tábla.Columns[9].Width = 100;
                Engedély_tábla.Columns[10].HeaderText = "Eng. dátuma";
                Engedély_tábla.Columns[10].Width = 100;
                Engedély_tábla.Columns[11].HeaderText = "Engedélyező";
                Engedély_tábla.Columns[11].Width = 100;
                Engedély_tábla.Columns[12].HeaderText = "Engedélyezve";
                Engedély_tábla.Columns[12].Width = 100;
                Engedély_tábla.Columns[13].HeaderText = "Státus";
                Engedély_tábla.Columns[13].Width = 100;

                foreach (Adat_Külső_Cégek rekord in Adatok)
                {

                    Engedély_tábla.RowCount++;
                    int i = Engedély_tábla.RowCount - 1;
                    Engedély_tábla.Rows[i].Cells[0].Value = rekord.Cégid;
                    Engedély_tábla.Rows[i].Cells[1].Value = rekord.Cég.Trim();
                    Engedély_tábla.Rows[i].Cells[2].Value = rekord.Munkaleírás.Trim();
                    Engedély_tábla.Rows[i].Cells[3].Value = rekord.Címe.Trim();
                    Engedély_tábla.Rows[i].Cells[4].Value = rekord.Cég_email.Trim();
                    Engedély_tábla.Rows[i].Cells[5].Value = rekord.Felelős_személy.Trim();
                    Engedély_tábla.Rows[i].Cells[6].Value = rekord.Felelős_telefonszám.Trim();
                    Engedély_tábla.Rows[i].Cells[7].Value = rekord.Mikor.Trim();
                    Engedély_tábla.Rows[i].Cells[8].Value = rekord.Érv_kezdet.ToString("yyyy.MM.dd");
                    Engedély_tábla.Rows[i].Cells[9].Value = rekord.Érv_vég.ToString("yyyy.MM.dd");
                    Engedély_tábla.Rows[i].Cells[10].Value = rekord.Engedélyezés_dátuma.ToString("yyyy.MM.dd");
                    Engedély_tábla.Rows[i].Cells[11].Value = rekord.Engedélyező.Trim();
                    switch (rekord.Engedély)
                    {
                        case 0:
                            {
                                Engedély_tábla.Rows[i].Cells[12].Value = "0 - Feltöltés alatt";
                                break;
                            }
                        case 1:
                            {
                                Engedély_tábla.Rows[i].Cells[12].Value = "1 - Engedélyezhető";
                                break;
                            }
                        case 5:
                            {
                                Engedély_tábla.Rows[i].Cells[12].Value = "5 - Engedélyezett";
                                break;
                            }
                        case 7:
                            {
                                Engedély_tábla.Rows[i].Cells[12].Value = "7 - Visszavont";
                                break;
                            }
                        case 8:
                            {
                                Engedély_tábla.Rows[i].Cells[12].Value = "8 - Lejárt";
                                break;
                            }
                        case 9:
                            {
                                Engedély_tábla.Rows[i].Cells[12].Value = "9 - Törölt";
                                break;
                            }
                    }
                    if (rekord.Státus)
                        Engedély_tábla.Rows[i].Cells[13].Value = "Törölt";
                    else
                        Engedély_tábla.Rows[i].Cells[13].Value = "Aktív";

                }
                Engedély_tábla_Formázás();
                Engedély_tábla.Visible = true;
                Engedély_tábla.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Engedély_tábla_Formázás()
        {
            try
            {
                // egész sor színezése ha törölt
                foreach (DataGridViewRow row in Engedély_tábla.Rows)
                {
                    switch (row.Cells[12].Value.ToString().Substring(0, 1))
                    {
                        case "0":
                            {
                                break;
                            }
                        // nem színezzük
                        case "1":
                            {
                                row.DefaultCellStyle.ForeColor = Color.Black;
                                row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Italic);
                                break;
                            }
                        case "5":
                            {
                                row.DefaultCellStyle.ForeColor = Color.Black;
                                row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                                break;
                            }

                        case "8":
                            {
                                row.DefaultCellStyle.ForeColor = Color.White;
                                row.DefaultCellStyle.BackColor = Color.Red;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f);
                                break;
                            }
                        case "9":
                            {
                                row.DefaultCellStyle.ForeColor = Color.White;
                                row.DefaultCellStyle.BackColor = Color.Red;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                                break;
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

        private void BtnSzakszeng_Click(object sender, EventArgs e)
        {
            try
            {
                if (Engedély_tábla.Rows.Count < 1) return;
                if (Engedély_tábla.SelectedRows.Count < 1) return;
                Email_kiírás();
                Engedély_sorszámok.Text = "";
                int volt = 0;

                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok();

                Holtart.Be(Engedély_tábla.Rows.Count + 1);

                List<Adat_Külső_Cégek> Adatok = new List<Adat_Külső_Cégek>();
                for (int ii = 0; ii < Engedély_tábla.SelectedRows.Count; ii++)
                {
                    if (!double.TryParse(Engedély_tábla.SelectedRows[ii].Cells[0].Value.ToStrTrim(), out double CegId)) CegId = 0;

                    bool vane = Adatok_Külső_Cégek.Any(a => a.Cégid == CegId && a.Engedély == 1);
                    if (vane)
                    {
                        Adat_Külső_Cégek Adat = new Adat_Külső_Cégek(
                            Engedély_tábla.SelectedRows[ii].Cells[0].Value.ToString().ToÉrt_Double(),
                            DateTime.Now,
                            Program.PostásNév,
                            5);
                        Adatok.Add(Adat);
                        Engedély_sorszámok.Text += Engedély_tábla.SelectedRows[ii].Cells[0].Value.ToString() + ", ";
                        volt = 1;
                        E_levél(ii);
                    }

                    Holtart.Lép();
                }
                Kéz_Külső_Cégek.Engedélyezés(Adatok);

                Engedély_Tábla_író(1);
                if (volt == 1)
                {
                    Gondnoki_email_új();
                    MessageBox.Show("Engedélyezési levelek el lettek küldve.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void E_levél(int sor)
        {
            Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            string Tábla_html;

            // betöltjük az engedélyezett adatokat a rögzítő lapra

            Cég_cég.Text = Engedély_tábla.Rows[sor].Cells[1].Value.ToString();
            Cég_Munkaleírás.Text = Engedély_tábla.Rows[sor].Cells[2].Value.ToString();
            Cég_címe.Text = Engedély_tábla.Rows[sor].Cells[3].Value.ToString();
            Cég_email.Text = Engedély_tábla.Rows[sor].Cells[4].Value.ToString();
            Cég_felelős_személy.Text = Engedély_tábla.Rows[sor].Cells[5].Value.ToString();
            Cég_felelős_telefon.Text = Engedély_tábla.Rows[sor].Cells[6].Value.ToString();
            Cég_sorszám.Text = Engedély_tábla.Rows[sor].Cells[0].Value.ToString();
            Cég_Érv_kezdet.Value = DateTime.Parse(Engedély_tábla.Rows[sor].Cells[8].Value.ToString());
            Cég_Érv_vég.Value = DateTime.Parse(Engedély_tábla.Rows[sor].Cells[9].Value.ToString());
            Cég_mikor.Text = Engedély_tábla.Rows[sor].Cells[7].Value.ToString();
            Cég_engedély_státus.Text = Engedély_tábla.Rows[sor].Cells[12].Value.ToString();
            if (Engedély_tábla.Rows[sor].Cells[13].Value.ToString().Trim() != "Törölt")
                Cég_Aktív.Checked = true;
            else
                Cég_Aktív.Checked = false;


            // autó lap
            Autó_cégnév.Text = Engedély_tábla.Rows[sor].Cells[1].Value.ToString().Trim();
            Autó_munka.Text = Engedély_tábla.Rows[sor].Cells[2].Value.ToString().Trim();
            Autó_Cégid.Text = Engedély_tábla.Rows[sor].Cells[0].Value.ToString().Trim();
            Autó_tábla_lista();

            // Dolgozólap
            Dolg_cégneve.Text = Engedély_tábla.Rows[sor].Cells[1].Value.ToString().Trim();
            Dolg_munka.Text = Engedély_tábla.Rows[sor].Cells[2].Value.ToString().Trim();
            Dolg_cégid.Text = Engedély_tábla.Rows[sor].Cells[0].Value.ToString().Trim();
            Dolg_tábla_író();

            // Telephely
            Telephely_Cégnév.Text = Engedély_tábla.Rows[sor].Cells[1].Value.ToString().Trim();
            Telephely_Munka.Text = Engedély_tábla.Rows[sor].Cells[2].Value.ToString().Trim();
            Telephely_Cégid.Text = Engedély_tábla.Rows[sor].Cells[0].Value.ToString().Trim();
            Telephely_tábla_alap_kiírás();
            Telephely_tábla_jog_kiírás();

            string telephelyekszöveg = "";

            // Adding adatsorok.
            foreach (DataGridViewRow row in Telephely_Tábla.Rows)
            {
                if (bool.Parse(row.Cells[0].Value.ToString()) == true)
                {
                    telephelyekszöveg += row.Cells[1].Value.ToString().Trim() + " üzem, ";
                }
            }
            mail.To = Cég_email.Text.Trim(); // címzett
            mail.CC = Email_másolat.Text.Trim(); // másolatot kap

            mail.Subject = "Belépési és Munkavégzési Engedély: " + Cég_cég.Text.Trim(); // üzenet tárgya

            mail.HTMLBody = "<html><body> ";
            // üzent szövege
            mail.HTMLBody += "<p>Tisztelt " + Cég_felelős_személy.Text.Trim() + " Úrhölgy/Úr !</p><br>";
            mail.HTMLBody += "<b style='font-size: 14pt'>Belépési és Munkavégzési Engedély</b>";
            mail.HTMLBody += "<p>Cég neve: " + Cég_cég.Text.Trim() + "</p>";
            mail.HTMLBody += "<p>Címe: " + Cég_címe.Text.Trim() + "</p>";
            mail.HTMLBody += "<p>E-mail: " + Cég_email.Text.Trim() + "</p>";
            mail.HTMLBody += "<p>Munkavégzés helye(k):" + telephelyekszöveg + "</p>"; // ide jönnek a telephelyek
            mail.HTMLBody += "<p>Érvényesség: " + Cég_Érv_kezdet.Value.ToString("yyyy.MM.dd") + " - " + Cég_Érv_vég.Value.ToString("yyyy.MM.dd") + "</p>";
            mail.HTMLBody += "<p>Munka rövid leírása: " + Cég_Munkaleírás.Text.Trim() + "</p>";
            mail.HTMLBody += "<p>Munkavégzésért felelős személy, elérhetősége: " + Cég_felelős_személy.Text.Trim() + " (" + Cég_felelős_telefon.Text.Trim() + ")</p>";

            mail.HTMLBody += "<b style='font-size: 14pt'>Munkát végző dolgozók adatai:</b><br>";

            // Betöltjük a dolgozó adatok

            // Table start.
            // Adding fejléc.
            Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";

            for (int ki = 1; ki <= 3; ki++)
            {
                Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Sorszám</th>";
                Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Név</th>";
                Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Szem ig szám</th>";
            }


            Tábla_html += "</tr>";
            // Adding adatsorok.
            int ioszlop = 0;
            foreach (DataGridViewRow row in Dolg_tábla.Rows)
            {
                if (row.Cells[4].Value.ToString().Trim() == "Érvényes")
                {
                    if (ioszlop == 0)
                        Tábla_html += "<tr>";
                    Tábla_html += "<td style='border: 1px solid #ccc'>" + row.Cells[0].Value.ToString() + "</td>";
                    Tábla_html += "<td style='border: 1px solid #ccc'>" + row.Cells[1].Value.ToString() + "</td>";
                    Tábla_html += "<td style='border: 1px solid #ccc'>" + row.Cells[2].Value.ToString() + "</td>";
                    ioszlop += 1;
                    if (ioszlop == 3)
                    {
                        Tábla_html += "</tr>";
                        ioszlop = 0;
                    }
                }
            }
            if (ioszlop > 0)
                Tábla_html += "</tr>";
            Tábla_html += "</table>";
            // Table end.

            mail.HTMLBody += Tábla_html + "<br>";

            mail.HTMLBody += "<b style='font-size: 14pt'>Gépjárművek:</b><br>";

            mail.HTMLBody += "<p>";
            {

                for (int j = 0; j < Tábla_autó.Rows.Count; j++)
                {
                    if (Tábla_autó.Rows[j].Cells[3].Value.ToString().Trim() == "Érvényes")
                    {
                        mail.HTMLBody += Tábla_autó.Rows[j].Cells[1].Value.ToString().Trim() + ", ";
                    }
                }
            }
            mail.HTMLBody += "</p>";
            mail.HTMLBody += "<b style='font-size: 14pt'>Felügyeletet biztosító szervezeti egység(ek):</b><br>";

            // Table start.
            // Adding fejléc.
            Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";
            foreach (DataGridViewColumn column in Telephely_Tábla.Columns)
            {
                if (column.Index != 0)
                {
                    Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.HeaderText + "</th>";
                }
            }
            Tábla_html += "</tr>";
            // Adding adatsorok.
            foreach (DataGridViewRow row in Telephely_Tábla.Rows)
            {
                if (bool.Parse(row.Cells[0].Value.ToString()))
                {
                    Tábla_html += "<tr>";

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ColumnIndex != 0)
                        {
                            Tábla_html += "<td style='border: 1px solid #ccc'>" + cell.Value.ToString() + "</td>";
                        }
                    }
                    Tábla_html += "</tr>";
                }
            }
            Tábla_html += "</table>";
            //  Table end.

            mail.HTMLBody += Tábla_html + "<br>";
            mail.HTMLBody += "<b style='font-size: 14pt'>A munkavégzést felügyelettel engedélyezem.</b>";
            mail.HTMLBody += "<p>Jelen engedély tűzveszélyes munkavégzésre nem érvényes.</p>";
            mail.HTMLBody += Email_Aláírás.Text.Trim();
            mail.HTMLBody += "</body></html>  ";

            // outlook
            mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceNormal;

            ((Microsoft.Office.Interop.Outlook._MailItem)mail).Send();
            MessageBox.Show("Üzenet el lett küldve.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void Engedély_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                // autó lap
                Autó_cégnév.Text = Engedély_tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                Autó_munka.Text = Engedély_tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                Autó_Cégid.Text = Engedély_tábla.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();

                // Dolgozólap
                Dolg_cégneve.Text = Engedély_tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                Dolg_munka.Text = Engedély_tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                Dolg_cégid.Text = Engedély_tábla.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();

                // Telephely
                Telephely_Cégnév.Text = Engedély_tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                Telephely_Munka.Text = Engedély_tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                Telephely_Cégid.Text = Engedély_tábla.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Engedély_teljes_lista_Click(object sender, EventArgs e)
        {
            Engedély_Tábla_író(0);
        }


        private void Engedély_elutasítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Engedély_tábla.Rows.Count < 1) return;


                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok();

                List<Adat_Külső_Cégek> Adatok = new List<Adat_Külső_Cégek>();
                for (int i = 0; i < Engedély_tábla.Rows.Count; i++)
                {
                    if (Engedély_tábla.Rows[i].Selected == true)
                    {
                        if (!double.TryParse(Engedély_tábla.Rows[i].Cells[0].Value.ToStrTrim(), out double CegId)) CegId = 0;
                        bool vane = Adatok_Külső_Cégek.Any(a => a.Cégid == CegId && a.Engedély == 1);
                        if (vane)
                        {
                            Adat_Külső_Cégek Adat = new Adat_Külső_Cégek(
                                Engedély_tábla.SelectedRows[i].Cells[0].Value.ToString().ToÉrt_Double(),
                                DateTime.Now,
                                Program.PostásNév,
                                7);
                            Adatok.Add(Adat);
                        }
                    }
                }
                Kéz_Külső_Cégek.Engedélyezés(Adatok);

                Engedély_Tábla_író(1);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Engedély_visszavonás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Engedély_tábla.Rows.Count < 1) return;
                int volt = 0;

                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok();

                Engedély_sorszámok.Text = "";

                List<Adat_Külső_Cégek> Adatok = new List<Adat_Külső_Cégek>();
                for (int i = 0; i < Engedély_tábla.SelectedRows.Count; i++)
                {
                    if (!double.TryParse(Engedély_tábla.SelectedRows[i].Cells[0].Value.ToStrTrim(), out double CegId)) CegId = 0;
                    bool vane = Adatok_Külső_Cégek.Any(a => a.Cégid == CegId && a.Engedély == 5);
                    if (vane)
                    {
                        Adat_Külső_Cégek Adat = new Adat_Külső_Cégek(
                               Engedély_tábla.SelectedRows[i].Cells[0].Value.ToString().ToÉrt_Double(),
                               DateTime.Now,
                               Program.PostásNév,
                               7);
                        Adatok.Add(Adat);
                        Engedély_sorszámok.Text += Engedély_tábla.SelectedRows[i].Cells[0].Value + ", ";
                        volt = 1;
                    }
                }
                Kéz_Külső_Cégek.Engedélyezés(Adatok);

                Engedély_Tábla_író(0);
                if (volt == 1)
                {
                    Gondnoki_email_Vissza();
                    MessageBox.Show("Üzenet el lett küldve a gondnokoknak.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Gondnoki_email_új()
        {
            try
            {
                Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail;
                mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                // ha a gondnoki tábla eredménye hogy van eleme, akkor küldünk e-mailt
                string címzett = "";

                Telephely_tábla_alap_kiírás();

                for (int i = 0; i < Telephely_Tábla.Rows.Count; i++)
                    címzett += Telephely_Tábla.Rows[i].Cells[4].Value.ToString().Trim() + ";";
                címzett = MyF.Szöveg_Tisztítás(címzett, 0, címzett.Length - 1);

                string tárgy = "Új Belépési és munkavégzési engedély került engedélyezése " + DateTime.Now.ToString("yyyyMMdd");
                string tartalom = "A következő sorszámú Belépési és Munkavégzési engedélyek kerültek engedélyezésre: ";
                tartalom += MyF.Szöveg_Tisztítás(Engedély_sorszámok.Text.Trim(), 0, Engedély_sorszámok.Text.Trim().Length - 1) + ".\n\r\n\r Ezt az e-mailt a Villamos program generálta.";
                if (!(címzett.Trim() == ""))
                {
                    // üzenet címzettje
                    mail.To = címzett;
                    // üzent szövege
                    mail.Body = tartalom;
                    // üzenet tárgya
                    mail.Subject = tárgy;

                    ((Microsoft.Office.Interop.Outlook._MailItem)mail).Send();
                    MessageBox.Show("Üzenet el lett küldve a gondnokoknak.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Gondnoki_email_Vissza()
        {
            try
            {
                Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail;
                mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                // ha a gondnoki tábla eredménye hogy van eleme, akkor küldünk e-mailt
                string címzett = "";

                Telephely_tábla_alap_kiírás();

                for (int i = 0; i < Telephely_Tábla.Rows.Count; i++)
                    címzett += Telephely_Tábla.Rows[i].Cells[4].Value.ToString().Trim() + ";";
                címzett = MyF.Szöveg_Tisztítás(címzett, 0, címzett.Length - 1);

                string tárgy = "Belépési és munkavégzési engedély került visszavonásra " + DateTime.Now.ToString("yyyyMMdd");
                string tartalom = "A következő sorszámú Belépési és Munkavégzési engedélyek került(ek) visszavonásra: ";
                tartalom += MyF.Szöveg_Tisztítás(Engedély_sorszámok.Text.Trim(), 0, Engedély_sorszámok.Text.Trim().Length - 1) + ".\n\r\n\r Ezt az e-mailt a Villamos program generálta.";
                if (címzett.Trim() != "")
                {
                    // üzenet címzettje
                    mail.To = címzett;
                    // üzent szövege
                    mail.Body = tartalom;
                    // üzenet tárgya
                    mail.Subject = tárgy;

                    ((Microsoft.Office.Interop.Outlook._MailItem)mail).Send();
                    MessageBox.Show("Üzenet el lett küldve a gondnokoknak.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region lejárat
        private void Engedély_lejárat()
        {
            try
            {
                int volt = 0;
                Engedély_sorszámok.Text = "";

                List<Adat_Külső_Cégek> Adatok = Kéz_Külső_Cégek.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Engedély == 5
                          && a.Érv_vég <= DateTime.Today
                          select a).ToList();

                List<Adat_Külső_Cégek> ADATOK = new List<Adat_Külső_Cégek>();
                foreach (Adat_Külső_Cégek rekord in Adatok)
                {
                    Adat_Külső_Cégek ADAT = new Adat_Külső_Cégek(rekord.Cégid, 8);
                    ADATOK.Add(ADAT);

                    // Módosítjuk az adatot
                    volt = 1;
                    Engedély_sorszámok.Text += rekord.Cégid + ", ";
                }
                Kéz_Külső_Cégek.Engedélyezésre(ADATOK);

                if (volt == 1)
                    Gondnoki_email_Lejárat();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Gondnoki_email_Lejárat()
        {

            Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem mail;
            mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            // ha a gondnoki tábla eredménye hogy van eleme, akkor küldünk e-mailt


            Telephely_tábla_alap_kiírás();

            string címzett = "";
            for (int i = 0; i < Telephely_Tábla.Rows.Count; i++)
                címzett += Telephely_Tábla.Rows[i].Cells[4].Value.ToString().Trim() + ";";
            címzett = MyF.Szöveg_Tisztítás(címzett, címzett.Length - 1, 0);

            string tárgy = "Belépési és munkavégzési engedély lejárat " + DateTime.Now.ToString("yyyyMMdd");
            string tartalom = "A következő sorszámú Belépési és Munkavégzési engedélyek járt(ak) le: ";
            tartalom += MyF.Szöveg_Tisztítás(Engedély_sorszámok.Text.Trim(), 0, Engedély_sorszámok.Text.Trim().Length - 1) + ".\n\r\n\r Ezt az e-mailt a Villamos program generálta.";
            if (!(címzett.Trim() == ""))
            {
                // üzenet címzettje
                mail.To = címzett;
                // üzent szövege
                mail.Body = tartalom;
                // üzenet tárgya
                mail.Subject = tárgy;
                ((Microsoft.Office.Interop.Outlook._MailItem)mail).Send();
                MessageBox.Show("Üzenet el lett küldve a gondnokoknak.", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion


        #region DolgozóListák
        private void Lekérd_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lekérdezés_tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Belépési_munkavégzési_" + Program.PostásTelephely + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Lekérdezés_tábla);
                MessageBox.Show($"Elkészült az Excel tábla: {fájlexc}.xlsx", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        private void Lekérd_dolgozó_Click(object sender, EventArgs e)
        {
            Lekérdezés_tábla_dolgozó();
        }


        private void Lekérdezés_tábla_dolgozó()
        {
            try
            {
                Lekérd_dolgozó_Lista_Elj();

                // excel kimenet készítése
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Berendezések adatlap készítés",
                    FileName = "Külső_Cég_Dolgozói_Listája_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
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
                MyX.Munkalap_betű(munkalap, BeBetű);


                // oszlopszélességek
                MyX.Oszlopszélesség(munkalap, "a:a", 30);
                MyX.Oszlopszélesség(munkalap, "b:b", 15);
                MyX.Oszlopszélesség(munkalap, "c:c", 30);
                MyX.Oszlopszélesség(munkalap, "d:d", 15);
                MyX.Oszlopszélesség(munkalap, "e:e", 30);
                MyX.Oszlopszélesség(munkalap, "f:f", 15);

                Holtart.Be(Adatok_Dolg.Count + 2);

                string cégneve = "";
                string munkaleírása = "";
                // Tartalom
                int sor = 0;
                int blokkeleje = 0;
                int három = 1;

                Adatok_Kieg_Jelenlétiív = Kéz_Kieg_Jelenlétiív.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Külső_Dolgozók> Dolgozók = Kéz_Dolgozó.Lista_Adatok();
                List<Adat_Külső_Telephelyek> Telephelyek = Kéz_Külső_Telephelyek.Lista_Adatok();
                List<Adat_Külső_Cégek> Cégek = Kéz_Külső_Cégek.Lista_Adatok();
                var Adatok =
                    from c in Cégek
                    join t in Telephelyek on c.Cégid equals t.Cégid
                    join d in Dolgozók on c.Cégid equals d.Cégid
                    where t.Telephely == Cmbtelephely.Text.Trim()
                          && t.Státus == true
                          && c.Engedély == 5
                          && d.Státus == false
                    orderby c.Cég, c.Munkaleírás, d.Név
                    select new
                    {
                        t.Telephely,
                        c.Engedély,
                        c.Cég,
                        c.Munkaleírás,
                        d.Név,
                        d.Okmányszám,
                        d.Státus
                    };

                foreach (var rekord in Adatok)
                {

                    if (cégneve.Trim() != rekord.Cég.Trim() || munkaleírása.Trim() != rekord.Munkaleírás.Trim())
                    {
                        // előső dolgozó nevek formázása
                        if (blokkeleje > 3)
                        {
                            MyX.Rácsoz(munkalap, $"a{blokkeleje}:f{sor}");
                            //MyX.Vastagkeret($"a{blokkeleje}:f{sor}");
                        }

                        // Cégfejléc készítés
                        sor++;
                        MyX.Egyesít(munkalap, $"a{sor}:b{sor}");
                        MyX.Egyesít(munkalap, $"c{sor}:f{sor}");
                        MyX.Kiir("Cég neve", $"a{sor}");
                        MyX.Kiir("Munkaleírása", $"c{sor}");
                        MyX.Háttérszín(munkalap, $"a{sor}:f{sor}", Color.Yellow);
                        MyX.Rácsoz(munkalap, $"a{sor}:f{sor}");
                        // MyX.Vastagkeret($"a{sor}:f{sor}");
                        // Cégadatok
                        sor++;
                        cégneve = rekord.Cég.Trim();
                        munkaleírása = rekord.Munkaleírás.Trim();
                        MyX.Egyesít(munkalap, $"a{sor}:b{sor}");
                        MyX.Egyesít(munkalap, $"c{sor}:f{sor}");
                        MyX.Kiir(cégneve, $"a{sor}");
                        MyX.Kiir(munkaleírása, $"c{sor}");
                        MyX.Sormagasság(munkalap, $"{sor}:{sor}", 30);
                        MyX.Igazít_vízszintes(munkalap, $"{sor}:{sor}", "közép");
                        MyX.Igazít_függőleges(munkalap, $"{sor}:{sor}", "közép");

                        MyX.Rácsoz(munkalap, $"a{sor}:f{sor}");
                        //MyX.Vastagkeret($"a{sor}:f{sor}");
                        // Dolgozó fejléc készítés
                        sor++;

                        MyX.Kiir("Név", $"a{sor}");
                        MyX.Kiir("Név", $"c{sor}");
                        MyX.Kiir("Név", $"e{sor}");

                        MyX.Kiir("Szem.ig.", $"b{sor}");
                        MyX.Kiir("Szem.ig.", $"d{sor}");
                        MyX.Kiir("Szem.ig.", $"f{sor}");

                        MyX.Rácsoz(munkalap, $"a{sor}:f{sor}");
                        MyX.Háttérszín(munkalap, $"a{sor}:f{sor}", Color.Yellow);
                        blokkeleje = sor + 1;
                        sor += 1;
                        három = 1;
                    }

                    if (három == 4)
                    {
                        // ha a negyediket kellene kiírni
                        sor += 1;
                        három = 1;
                    }
                    switch (három)
                    {
                        case 1:
                            {
                                MyX.Kiir(rekord.Név.Trim(), "a" + sor.ToString());
                                MyX.Kiir(rekord.Okmányszám.Trim(), "b" + sor.ToString());
                                break;
                            }
                        case 2:
                            {
                                MyX.Kiir(rekord.Név.Trim(), "c" + sor.ToString());
                                MyX.Kiir(rekord.Okmányszám.Trim(), "d" + sor.ToString());
                                break;
                            }
                        case 3:
                            {
                                MyX.Kiir(rekord.Név.Trim(), "e" + sor.ToString());
                                MyX.Kiir(rekord.Okmányszám.Trim(), "f" + sor.ToString());
                                break;
                            }
                    }
                    három += 1;
                    Holtart.Lép();
                }
                MyX.Rácsoz(munkalap, $"a{blokkeleje}:f{sor}");

                sor += 5;

                MyX.Kiir("Budapest," + DateTime.Today.ToString("yyyy.MM.dd"), $"a{sor}");
                MyX.Kiir("Gondnok", $"c{sor}");

                // nyomtatási terület kijelölése

                string helyicsop = Application.StartupPath + @"\Főmérnökség\adatok\BKV.jpg";

                string telephely = (from a in Adatok_Kieg_Jelenlétiív
                                    where a.Id == 4
                                    select a.Szervezet).FirstOrDefault() ?? "";

                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:C{sor}",

                    IsmétlődőSorok = "$1:$1",

                    Képútvonal = helyicsop,
                    FejlécKözép = "Gépjármű Behajtási Engedély Külső cég",
                    FejlécJobb = DateTime.Today.ToString("yyyy.MM.dd"),

                    LáblécJobb = "&P/&N",

                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 30,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,

                    VízKözép = false,
                    FüggKözép = false,

                    LapSzéles = 1,
                };

                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                Holtart.Ki();
                // bezárjuk az Excel-t
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
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


        private void Lekérd_dolgozó_lista_Click(object sender, EventArgs e)
        {
            Lekérd_dolgozó_Lista_Elj();
        }


        private void Lekérd_dolgozó_Lista_Elj()
        {
            try
            {
                //string szöveg = "Select Telephelyek.Telephely, Telephelyek.Státus, Cégek.Engedély, Cégek.Cég, Cégek.Munkaleírás, Dolgozók.Név, Dolgozók.Okmányszám, Dolgozók.Anyjaneve,";
                //szöveg += " Dolgozók.Születésihely, Dolgozók.Születésiidő, Dolgozók.Státus ";
                //szöveg += " FROM(Cégek INNER JOIN Telephelyek On Cégek.Cégid = Telephelyek.Cégid) INNER JOIN Dolgozók On Cégek.Cégid = Dolgozók.Cégid ";
                //szöveg += $" WHERE Telephelyek.Telephely ='{Cmbtelephely.Text.Trim()}' AND Telephelyek.Státus=True And Cégek.Engedély= 5 And ";
                //szöveg += " Dolgozók.Státus= False ORDER BY Cégek.Cég,Cégek.Munkaleírás,Dolgozók.Név";
                List<Adat_Külső_Dolgozók> Dolgozók = Kéz_Dolgozó.Lista_Adatok();
                List<Adat_Külső_Telephelyek> Telephelyek = Kéz_Külső_Telephelyek.Lista_Adatok();
                List<Adat_Külső_Cégek> Cégek = Kéz_Külső_Cégek.Lista_Adatok();
                var Adatok =
                    from c in Cégek
                    join t in Telephelyek on c.Cégid equals t.Cégid
                    join d in Dolgozók on c.Cégid equals d.Cégid
                    where t.Telephely == Cmbtelephely.Text.Trim()
                          && t.Státus == true
                          && c.Engedély == 5
                          && d.Státus == false
                    orderby c.Cég, c.Munkaleírás, d.Név
                    select new
                    {
                        t.Telephely,
                        c.Engedély,
                        c.Cég,
                        c.Munkaleírás,
                        d.Név,
                        d.Okmányszám,
                        d.Státus
                    };

                Lekérdezés_tábla.Rows.Clear();
                Lekérdezés_tábla.Columns.Clear();
                Lekérdezés_tábla.Refresh();
                Lekérdezés_tábla.Visible = false;
                Lekérdezés_tábla.ColumnCount = 4;

                // fejléc elkészítése
                Lekérdezés_tábla.Columns[0].HeaderText = "Név";
                Lekérdezés_tábla.Columns[0].Width = 250;
                Lekérdezés_tábla.Columns[1].HeaderText = "Szem.ig.";
                Lekérdezés_tábla.Columns[1].Width = 200;
                Lekérdezés_tábla.Columns[2].HeaderText = "Cég neve";
                Lekérdezés_tábla.Columns[2].Width = 400;
                Lekérdezés_tábla.Columns[3].HeaderText = "Munkaleírása";
                Lekérdezés_tábla.Columns[3].Width = 400;
                foreach (var rekord in Adatok)
                {

                    Lekérdezés_tábla.RowCount++;
                    int i = Lekérdezés_tábla.RowCount - 1;
                    Lekérdezés_tábla.Rows[i].Cells[0].Value = rekord.Név.Trim();
                    Lekérdezés_tábla.Rows[i].Cells[1].Value = rekord.Okmányszám.Trim();
                    Lekérdezés_tábla.Rows[i].Cells[2].Value = rekord.Cég.Trim();
                    Lekérdezés_tábla.Rows[i].Cells[3].Value = rekord.Munkaleírás.Trim();
                }

                Lekérdezés_tábla.Visible = true;
                Lekérdezés_tábla.Refresh();

            }
            catch (HibásBevittAdat ex)
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


        #region AutoListázás
        private void Lekérd_autó_Lista_Click(object sender, EventArgs e)
        {
            Lekérd_Autó_Lista_Elj();
        }


        private void Lekérd_Autó_Lista_Elj()
        {
            try
            {
                //string szöveg = "Select  Gépjárművek.Frsz, Cégek.Cég, Telephelyek.Telephely,  Cégek.Munkaleírás ";
                //szöveg += " FROM(Cégek INNER JOIN Telephelyek On Cégek.Cégid = Telephelyek.Cégid) INNER JOIN Gépjárművek On Cégek.Cégid = Gépjárművek.Cégid ";
                //szöveg += $" WHERE Telephelyek.Telephely ='{Cmbtelephely.Text.Trim()}' And Cégek.Engedély=5 ";
                //szöveg += " And Gépjárművek.Státus=false And Telephelyek.Státus= True ORDER BY Cégek.Cég, Cégek.Munkaleírás, Gépjárművek.Frsz";
                List<Adat_Külső_Gépjárművek> Gépjárművek = Kéz_Járművek.Lista_Adatok();
                List<Adat_Külső_Telephelyek> Telephelyek = Kéz_Külső_Telephelyek.Lista_Adatok();
                List<Adat_Külső_Cégek> Cégek = Kéz_Külső_Cégek.Lista_Adatok();

                var Adatok =
                    from c in Cégek
                    join t in Telephelyek on c.Cégid equals t.Cégid
                    join g in Gépjárművek on c.Cégid equals g.Cégid
                    where t.Telephely == Cmbtelephely.Text.Trim()
                          && c.Engedély == 5
                          && g.Státus == false
                          && t.Státus == true
                    orderby c.Cég, c.Munkaleírás, g.Frsz
                    select new
                    {
                        g.Frsz,
                        c.Cég,
                        t.Telephely,
                        c.Munkaleírás
                    };


                Lekérdezés_tábla.Rows.Clear();
                Lekérdezés_tábla.Columns.Clear();
                Lekérdezés_tábla.Refresh();
                Lekérdezés_tábla.Visible = false;
                Lekérdezés_tábla.ColumnCount = 4;

                // fejléc elkészítése
                Lekérdezés_tábla.Columns[0].HeaderText = "Rendszám";
                Lekérdezés_tábla.Columns[0].Width = 150;
                Lekérdezés_tábla.Columns[1].HeaderText = "Cég neve";
                Lekérdezés_tábla.Columns[1].Width = 400;
                Lekérdezés_tábla.Columns[2].HeaderText = "Munkaleírása";
                Lekérdezés_tábla.Columns[2].Width = 400;
                Lekérdezés_tábla.Columns[3].HeaderText = "Telephely";
                Lekérdezés_tábla.Columns[3].Width = 150;

                foreach (var rekord in Adatok)
                {
                    Lekérdezés_tábla.RowCount++;
                    int i = Lekérdezés_tábla.RowCount - 1;
                    Lekérdezés_tábla.Rows[i].Cells[0].Value = rekord.Frsz.Trim();
                    Lekérdezés_tábla.Rows[i].Cells[1].Value = rekord.Cég.Trim();
                    Lekérdezés_tábla.Rows[i].Cells[2].Value = rekord.Munkaleírás.Trim();
                    Lekérdezés_tábla.Rows[i].Cells[3].Value = rekord.Telephely.Trim();
                }

                Lekérdezés_tábla.Visible = true;
                Lekérdezés_tábla.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Lekérd_autó_Click(object sender, EventArgs e)
        {
            Lekérdezés_tábla_autó();
        }


        private void Lekérdezés_tábla_autó()
        {
            try
            {
                Lekérd_Autó_Lista_Elj();

                // excel kimenet készítése
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Berendezések adatlap készítés",
                    FileName = $"Külső_Cég_Gépjárműveses_listája_{DateTime.Now:yyyyMMddhhmmss}",
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
                MyX.Munkalap_betű(munkalap, BeBetű);

                MyX.Háttérszín(munkalap, "A1:c1", Color.Yellow);
                MyX.Kiir("Rendszám", "a1");
                MyX.Kiir("Cég neve", "b1");
                MyX.Kiir("Munkaleírása", "c1");
                MyX.Oszlopszélesség(munkalap, "a:a", 15);
                MyX.Oszlopszélesség(munkalap, "b:b", 45);
                MyX.Oszlopszélesség(munkalap, "c:c", 75);
                MyX.Rácsoz(munkalap, "a1:c1");
                //MyX.Vastagkeret("a1:c1");

                int sor;
                int blokkeleje;
                string cégneve = "";
                string munkaleírása = "";

                Holtart.Be(Adatok_autó.Count + 3);
                // Tartalom
                sor = 2;
                blokkeleje = 2;

                Adatok_Kieg_Jelenlétiív = Kéz_Kieg_Jelenlétiív.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Külső_Gépjárművek> Gépjárművek = Kéz_Járművek.Lista_Adatok();
                List<Adat_Külső_Telephelyek> Telephelyek = Kéz_Külső_Telephelyek.Lista_Adatok();
                List<Adat_Külső_Cégek> Cégek = Kéz_Külső_Cégek.Lista_Adatok();

                var Adatok =
                    from c in Cégek
                    join t in Telephelyek on c.Cégid equals t.Cégid
                    join g in Gépjárművek on c.Cégid equals g.Cégid
                    where t.Telephely == Cmbtelephely.Text.Trim()
                          && c.Engedély == 5
                          && g.Státus == false
                          && t.Státus == true
                    orderby c.Cég, c.Munkaleírás, g.Frsz
                    select new
                    {
                        g.Frsz,
                        c.Cég,
                        t.Telephely,
                        c.Munkaleírás
                    };

                foreach (var Rekord in Adatok)
                {

                    if (cégneve.Trim() == "")
                        cégneve = Rekord.Cég.Trim();
                    if (munkaleírása.Trim() == "")
                        munkaleírása = Rekord.Munkaleírás.Trim();
                    if (cégneve.Trim() != Rekord.Cég.Trim() || munkaleírása.Trim() != Rekord.Munkaleírás.Trim())
                    {
                        // ha változik akkor egyesítjük a mezőket       
                        Autó_Cégnév(munkalap, blokkeleje, sor, cégneve, munkaleírása);
                        blokkeleje = sor;
                        cégneve = Rekord.Cég.Trim();
                        munkaleírása = Rekord.Munkaleírás.Trim();
                    }

                    // kiírjuk a rendszámot
                    MyX.Kiir(Rekord.Frsz.Trim(), $"a{sor}");
                    sor += 1;
                    Holtart.Lép();
                }
                // kiírjuk az utolsókat
                Autó_Cégnév(munkalap, blokkeleje, sor, cégneve, munkaleírása);

                sor += 5;

                MyX.Kiir("Budapest," + DateTime.Today.ToString("yyyy.MM.dd"), $"A{sor}");
                MyX.Kiir("Gondnok", $"C{sor}");

                // nyomtatási terület kijelölése

                string helyicsop = $@"{Application.StartupPath}\Főmérnökség\adatok\BKV.jpg";

                string telephely = (from a in Adatok_Kieg_Jelenlétiív
                                    where a.Id == 4
                                    select a.Szervezet).FirstOrDefault() ?? "";

                Beállítás_Nyomtatás BeNyom_Engedély = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:C{sor}",

                    IsmétlődőSorok = "$1:$1",

                    Képútvonal = helyicsop,
                    FejlécKözép = "Gépjármű Behajtási Engedély Külső cég",
                    FejlécJobb = DateTime.Today.ToString("yyyy.MM.dd"),

                    LáblécJobb = "&P/&N",

                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 30,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,

                    VízKözép = false,
                    FüggKözép = false,

                    LapSzéles = 1,
                };

                // 2. Hívás
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom_Engedély);
                Holtart.Ki();
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

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


        private void Autó_Cégnév(string munkalap, int blokkeleje, int sor, string cégneve, string munkaleírása)
        {

            MyX.Egyesít(munkalap, $"B{blokkeleje}:B{sor - 1}");
            MyX.Egyesít(munkalap, $"C{blokkeleje}:C{sor - 1}");

            MyX.Sortörésseltöbbsorba(munkalap, $"B{blokkeleje}", true);
            MyX.Sortörésseltöbbsorba(munkalap, $"C{blokkeleje}", true);

            MyX.Rácsoz(munkalap, $"A{blokkeleje}:C{sor - 1}");

            MyX.Kiir(cégneve, $"B{blokkeleje}");
            MyX.Kiir(munkaleírása, $"C{blokkeleje}");

        }
        #endregion


        #region Email
        private void Email_rögzít_Click(object sender, EventArgs e)
        {

            try
            {
                Adatok_Külső_Email = Kéz_Külső_Email.Lista_Adatok();
                bool vane = Adatok_Külső_Email.Any(a => a.Id == Email_id);

                Adat_Külső_Email ADAT = new Adat_Külső_Email(
                    Convert.ToDouble(Email_id),
                    Email_másolat.Text.Trim(),
                    Email_Aláírás.Text.Trim());

                if (vane)
                {
                    Kéz_Külső_Email.Módosítás(ADAT);
                }
                else
                {
                    Kéz_Külső_Email.Rögzítés(ADAT);
                }

                Email_kiírás();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Email_kiírás()
        {
            try
            {
                if (Email_id == 0) return;

                Adatok_Külső_Email = Kéz_Külső_Email.Lista_Adatok();
                Adat_Külső_Email emailRecord = Adatok_Külső_Email.Where(a => a.Id == Email_id).FirstOrDefault();

                Email_másolat.Text = "";
                Email_Aláírás.Text = "";
                if (emailRecord != null)
                {
                    Email_másolat.Text = emailRecord.Másolat.Trim();
                    Email_Aláírás.Text = emailRecord.Aláírás.Trim().Replace("°", "'");
                }
                WebBrowser1.DocumentText = Email_Aláírás.Text;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Email_frissít_Click(object sender, EventArgs e)
        {
            Email_kiírás();
        }
        #endregion


        #region PDF
        private void PDF_feltöltés_Click(object sender, EventArgs e)
        {
            PDF_cégid.Text = "";
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog
            {
                Filter = "PDF Files |*.pdf"
            };
            if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
            {
                PDF_megjelenítés(OpenFileDialog1.FileName);
                TxtKérrelemPDF.Text = OpenFileDialog1.FileName;
            }
        }

        private void PDF_megjelenítés(string fileName)
        {
            try
            {
                PDF_néző.Visible = false;
                Kezelő_Pdf.PdfMegnyitás(PDF_néző, fileName);
            }
            catch (HibásBevittAdat ex)
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
                if (TxtKérrelemPDF.Text.Trim() == "")
                    return;
                string helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_PDF";
                string hova;
                string honnan;
                string szöveg;
                int maximum = int.Parse(Doksik.Text);
                int sorszám;
                string szövegelem;

                if (maximum == 0)
                {
                    sorszám = 0;
                }
                else
                {
                    szöveg = MyF.Szöveg_Tisztítás(PDF_lista.Items[PDF_lista.Items.Count - 1].ToString(), 0, PDF_lista.Items[PDF_lista.Items.Count - 1].ToString().Length - 4);
                    szövegelem = Cég_sorszám.Text.Trim() + "_" + Cég_Érv_kezdet.Value.ToString("yyyyMMdd") + "_" + Cég_Érv_vég.Value.ToString("yyyyMMdd") + "_";
                    string[] darabol = szöveg.Split('_');
                    sorszám = int.Parse(darabol[3]) + 1;
                }
                szöveg = $"{Cég_sorszám.Text.Trim()}_{Cég_Érv_kezdet.Value:yyyyMMdd}_{Cég_Érv_vég.Value:yyyyMMdd}_{sorszám}.pdf";
                hova = helyi + @"\" + szöveg;
                honnan = TxtKérrelemPDF.Text.Trim();
                File.Copy(honnan, hova);
                MessageBox.Show("A dokumentum feltöltése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Pdflistázása();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Pdflistázása()
        {
            try
            {
                PDF_lista.Items.Clear();
                string helyi = Application.StartupPath + @"\Főmérnökség\Adatok\Behajtási\Külső_PDF";
                if (!Directory.Exists(helyi) == false)
                    Directory.CreateDirectory(helyi);

                DirectoryInfo di = new DirectoryInfo(helyi);
                FileInfo[] aryFi = di.GetFiles("*.pdf");
                string szöveg = Cég_sorszám.Text.Trim() + "_" + Cég_Érv_kezdet.Value.ToString("yyyyMMdd") + "_" + Cég_Érv_vég.Value.ToString("yyyyMMdd");
                foreach (FileInfo fi in aryFi)
                {
                    if (fi.Name.Contains(szöveg))
                        PDF_lista.Items.Add(fi.Name);
                }

                Doksik.Text = PDF_lista.Items.Count.ToString();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PDF_lista_frissít_Click(object sender, EventArgs e)
        {
            Pdflistázása();
        }


        private void PDF_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PDF_lista.SelectedIndex < 0)
                return;

            string helyi = Application.StartupPath + @"\Főmérnökség\Adatok\Behajtási\Külső_PDF\" + PDF_lista.Items[PDF_lista.SelectedIndex];
            PDF_megjelenítés(helyi);
        }



        private void PDF_törlés_Click(object sender, EventArgs e)
        {
            if (PDF_lista.SelectedItems.Count < 1)
                throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");
            if (PDF_lista.SelectedItems[0].ToString().Trim() == "")
                throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");

            if (MessageBox.Show("Biztos, hogy a töröljük a fájlt?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string helypdf = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_PDF\";
                File.Delete(helypdf + PDF_lista.SelectedItems[0].ToString().Trim());
                Pdflistázása();
                PDF_néző.Visible = false;
                MessageBox.Show("A PDF fájl törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #endregion

        #region Lista

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