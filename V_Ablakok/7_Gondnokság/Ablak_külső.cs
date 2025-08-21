using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_külső
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_adatok.mdb";
        readonly string jelszó = "Janda";
        int Email_id = 0;
        bool Rádió_főmérnök = false;
        string Telephely_választott = "";

        readonly Kezelő_Külső_Lekérdezés_Autó Kéz_autó = new Kezelő_Külső_Lekérdezés_Autó();
        readonly Kezelő_Külső_Lekérdezés_Személy Kéz_Dolg = new Kezelő_Külső_Lekérdezés_Személy();
        readonly Kezelő_Külső_Cégek Kéz_Külső_Cégek = new Kezelő_Külső_Cégek();
        readonly Kezelő_Behajtás_Engedélyezés Kéz_Behajtás_Engedély = new Kezelő_Behajtás_Engedélyezés();
        readonly Kezelő_Külső_Telephelyek Kéz_Külső_Telephelyek = new Kezelő_Külső_Telephelyek();
        readonly Kezelő_Kiegészítő_Jelenlétiív Kéz_Kieg_Jelenlétiív = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Külső_Email Kéz_Külső_Email = new Kezelő_Külső_Email();


        List<Adat_Külső_Lekérdezés_Autó> Adatok_autó = new List<Adat_Külső_Lekérdezés_Autó>();
        List<Adat_Külső_Lekérdezés_Személy> Adatok_Dolg = new List<Adat_Külső_Lekérdezés_Személy>();
        List<Adat_Külső_Cégek> Adatok_Külső_Cégek = new List<Adat_Külső_Cégek>();
        List<Adat_Behajtás_Engedélyezés> Adatok_Behajtás_Engedély = new List<Adat_Behajtás_Engedélyezés>();
        List<Adat_Külső_Telephelyek> Adatok_Külső_Telephelyek = new List<Adat_Külső_Telephelyek>();
        List<Adat_Kiegészítő_Jelenlétiív> Adatok_Kieg_Jelenlétiív = new List<Adat_Kiegészítő_Jelenlétiív>();
        List<Adat_Külső_Email> Adatok_Külső_Email = new List<Adat_Külső_Email>();

        public Ablak_külső()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_külső_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_külső_Shown(object sender, EventArgs e)
        {

        }

        #region alap
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
                string helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\";
                if (!Directory.Exists(helyi)) Directory.CreateDirectory(helyi);

                helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_PDF";
                if (!Directory.Exists(helyi)) Directory.CreateDirectory(helyi);

                if (!File.Exists(hely)) Adatbázis_Létrehozás.Külsős_Táblák(hely);


                Telephelyekfeltöltése();

                GombLathatosagKezelo.Beallit(this);
                Jogosultságkiosztás();

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
            MyE.Megnyitás(helyi);
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

                string szöveg = "SELECT * FROM Cégek";
                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok(hely, jelszó, szöveg);

                // Megkeressük a soron következőt
                if (Cég_sorszám.Text.Trim() == "")
                {
                    double rekord = Adatok_Külső_Cégek.Any() ? Adatok_Külső_Cégek.Max(a => a.Cégid) + 1 : 1;
                    Cég_sorszám.Text = rekord.ToString();

                    szöveg = "INSERT INTO Cégek (cégid, cég, címe, cég_email, felelős_személy, Felelős_telefonszám, munkaleírás,";
                    szöveg += " mikor, érv_kezdet, érv_vég, Engedélyezés_dátuma, engedélyező, engedély, státus, terület)  VALUES (";
                    szöveg += Cég_sorszám.Text.Trim() + ", "; // cégid
                    szöveg += "'" + Cég_cég.Text.Trim() + "', "; // cég
                    szöveg += "'" + Cég_címe.Text.Trim().Replace(",", ";") + "', "; // címe
                    szöveg += "'" + Cég_email.Text.Trim() + "', "; // cég_email
                    szöveg += "'" + Cég_felelős_személy.Text.Trim() + "', "; // felelős_személy
                    szöveg += "'" + Cég_felelős_telefon.Text.Trim() + "', "; // Felelős_telefonszám
                    szöveg += "'" + Cég_Munkaleírás.Text.Trim() + "', "; // munkaleírás
                    szöveg += "'" + Cég_mikor.Text.Trim() + "', "; // Mikor
                    szöveg += "'" + Cég_Érv_kezdet.Value.ToString("yyyy.MM.dd").Trim() + "', "; // érv_kezdet
                    szöveg += "'" + Cég_Érv_vég.Value.ToString("yyyy.MM.dd").Trim() + "', "; // érv_vég
                    szöveg += "'" + new DateTime(1900, 1, 1).ToString("yyyy.MM.dd").Trim() + "', ";  // endegélyezés_dátuma
                    szöveg += "'_', "; // engedélyező
                    szöveg += " 0, "; // engedély új rögzítés
                    szöveg += " false, ";  // státus
                    if (Rádió_főmérnök)
                        szöveg += "'Főmérnökség')";
                    else
                        szöveg += $"'{Cmbtelephely.Text.Trim()}')";
                }
                else
                {
                    szöveg = "UPDATE Cégek  Set ";
                    szöveg += " cég='" + Cég_cég.Text.Trim() + "', "; // cég
                    szöveg += " címe='" + Cég_címe.Text.Trim() + "', "; // címe
                    szöveg += " cég_email='" + Cég_email.Text.Trim() + "', "; // cég_email
                    szöveg += " felelős_személy='" + Cég_felelős_személy.Text.Trim() + "', "; // felelős_személy
                    szöveg += " Felelős_telefonszám='" + Cég_felelős_telefon.Text.Trim() + "', "; // Felelős_telefonszám
                    szöveg += " munkaleírás='" + Cég_Munkaleírás.Text.Trim() + "', "; // munkaleírás
                    szöveg += " Mikor='" + Cég_mikor.Text.Trim() + "', "; // Mikor
                    szöveg += " érv_kezdet='" + Cég_Érv_kezdet.Value.ToString("yyyy.MM.dd").Trim() + "', "; // érv_kezdet
                    szöveg += " érv_vég='" + Cég_Érv_vég.Value.ToString("yyyy.MM.dd").Trim() + "', "; // érv_vég
                                                                                                      // státus
                    if (Cég_Aktív.Checked)
                    {
                        szöveg += " engedély=9, ";  // engedély
                        szöveg += " státus=True ";
                    }
                    else
                    {
                        szöveg += " engedély=0, ";  // engedély
                        szöveg += " státus=false ";
                    }
                    szöveg += " WHERE [Cégid]=" + Cég_sorszám.Text.Trim();
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

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
                if (Cég_tábla.Rows.Count <= 0)
                    return;
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

                MyE.DataGridViewToExcel(fájlexc, Cég_tábla);
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
                string szöveg = "SELECT * FROM Cégek Order By cégid ";
                Kezelő_Külső_Cégek Kéz = new Kezelő_Külső_Cégek();
                List<Adat_Külső_Cégek> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);



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
            if (e.RowIndex < 0)
                return;
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
                    if (System.IO.File.Exists(hely) == false)
                    {
                        Dolg_Rögzít.Visible = false;
                        Dolgozó_töröl.Visible = false;
                    }
                    else if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    {
                        Dolg_Rögzít.Visible = true;
                        Dolgozó_töröl.Visible = true;
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

        private void Cégek_engedélyezésre_Click(object sender, EventArgs e)
        {
            try
            {

                int volt = 0;
                int hiba = 0;
                //a kijelöléseken végig megyünk és csak a 0 státust hagyjuk bejelölve
                string szöveg = "SELECT * FROM Cégek";
                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok(hely, jelszó, szöveg);

                for (int i = 0; i < Cég_tábla.Rows.Count; i++)
                {
                    if (Cég_tábla.Rows[i].Cells[12].Value.ToString().Trim().Substring(0, 1) != "0" || Cég_tábla.Rows[i].Cells[12].Value.ToString().Trim() == "Törölt")
                        Cég_tábla.Rows[i].Selected = false;
                }

                List<string> SzövegGy = new List<string>();
                for (int iii = 0; iii < Cég_tábla.Rows.Count; iii++)
                {
                    if (Cég_tábla.Rows[iii].Selected == true)
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
                                    szöveg = "UPDATE Cégek  SET ";
                                    szöveg += " engedély=1"; // engedély
                                    szöveg += " WHERE [Cégid]=" + Cég_tábla.Rows[iii].Cells[0].Value;
                                    SzövegGy.Add(szöveg);
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
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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
                if (Engedély_tábla.Rows.Count < 1)
                    return;

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
                if (Autó_Cégid.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva cég.");

                string szöveg = $"SELECT * FROM gépjárművek WHERE státus=false AND  cégid={Autó_Cégid.Text.Trim()} order by id";
                Kezelő_Külső_Gépjárművek Kéz = new Kezelő_Külső_Gépjárművek();
                List<Adat_Külső_Gépjárművek> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                Autó_fejléc();

                string válasz = "Érvényes";
                foreach (Adat_Külső_Gépjárművek rekord in Adatok)
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
                if (Autó_Cégid.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva érvényes cég.");
                if (Autó_FRSZ.Text.Trim() == "")
                    throw new HibásBevittAdat("Az autó rendszáma mező nem lehet üres.");
                if (Autó_státus.Text.Trim() == "") Autó_státus.Text = "Érvényes";

                // ha szóközzel van elválasztva akkor javítja és nagybetűsít
                Autó_FRSZ.Text = Autó_FRSZ.Text.ToUpper().Replace(" ", "").Replace("-", "");

                Autó_Rögzítés(Autó_Cégid.Text.Trim(), Autó_FRSZ.Text.Trim(), Autó_státus.Text.Trim());
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

        private void Autó_Rögzítés(string CégID, string RendSzám, string StátusA)
        {
            string szöveg = "SELECT * FROM Gépjárművek";
            Kezelő_Külső_Gépjárművek Kéz = new Kezelő_Külső_Gépjárművek();
            List<Adat_Külső_Gépjárművek> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
            double id = Adatok.Any() ? Adatok.Max(a => a.Id) + 1 : 1;

            if (!double.TryParse(Telephely_Cégid.Text, out double CegId)) CegId = 0;
            bool vane = Adatok.Any(a => a.Cégid == CegId && a.Frsz.Trim() == RendSzám.Trim());

            if (vane)
            {
                szöveg = "UPDATE Gépjárművek SET ";
                if (StátusA.Trim() == "Érvényes")
                    szöveg += " státus=false ";
                else
                    szöveg += " státus=true ";

                szöveg += $" WHERE Cégid={CégID} AND frsz='{RendSzám}'";
            }
            else
            {
                szöveg = "INSERT INTO Gépjárművek (id, frsz, cégid, státus) VALUES (";
                szöveg += $"{id}, "; // id
                szöveg += $"'{RendSzám}', "; // frsz
                szöveg += $"'{CégID}', "; // cégid
                if (StátusA.Trim() == "Érvényes")
                    szöveg += " false) ";
                else
                    szöveg += " true) ";
            }
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        private void Autó_Új_Click(object sender, EventArgs e)
        {
            Autó_Ürítés();
        }


        private void Tábla_autó_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tábla_autó.Rows.Count < 1)
                return;
            if (e.RowIndex < 0)
                return;

            Autó_FRSZ.Text = Tábla_autó.Rows[e.RowIndex].Cells[1].Value.ToString();
            Autó_státus.Text = Tábla_autó.Rows[e.RowIndex].Cells[3].Value.ToString();

        }


        private void Autó_beviteli_Click(object sender, EventArgs e)
        {
            try
            {
                if (Autó_Cégid.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve cég.");
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
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                Autó_fejléc();
                Tábla_autó.Visible = true;

                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("Arial", 12);
                string munkalap = "Munka1";

                // fejléc kiírása
                for (int oszlop = 0; oszlop < Tábla_autó.ColumnCount; oszlop++)
                {
                    MyE.Kiir(Tábla_autó.Columns[oszlop].HeaderText.Trim(), MyE.Oszlopnév(oszlop + 1) + "1");
                    MyE.Oszlopszélesség(munkalap, $"{MyE.Oszlopnév(oszlop + 1)}:{MyE.Oszlopnév(oszlop + 1)}", 30);
                }

                // megformázzuk
                MyE.Rácsoz($"A1:{MyE.Oszlopnév(Tábla_autó.ColumnCount)}2");

                MyE.Betű($"A1:{MyE.Oszlopnév(Tábla_autó.ColumnCount)}1", false, false, true);
                MyE.Háttérszín($"A1:{MyE.Oszlopnév(Tábla_autó.ColumnCount)}1", Color.Yellow);
                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:{MyE.Oszlopnév(Tábla_autó.ColumnCount)}2", "", "", true);

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MessageBox.Show($"Elkészült az Excel tábla: {fájlexc}.xlsx", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                // megnézzük, hogy hány sorból áll a tábla
                int ii = 1;
                int utolsó = 0;
                while (MyE.Beolvas($"b{ii}").Trim() != "_")
                {
                    utolsó = ii;
                    ii += 1;
                }
                Holtart.Be(utolsó);

                if (utolsó > 1)
                {

                    for (int i = 2; i <= utolsó; i++)
                    {
                        // ha szóközzel van elválasztva akkor javítja és nagybetűsít
                        string rendszám = MyE.Beolvas($"b{i}").Trim().ToUpper().Replace(" ", "").Replace("-", "");

                        Autó_Rögzítés(Autó_Cégid.Text.Trim(), rendszám.Trim(), "Érvényes");
                        Holtart.Lép();
                    }
                }
                // bezárjuk az excel táblát
                MyE.ExcelBezárás();


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
                if (Tábla_autó.Rows.Count == 0)
                    throw new HibásBevittAdat("Nincs elem a táblázatban.");
                if (Tábla_autó.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Nincs kijelölve elem a táblázatban.");

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Tábla_autó.SelectedRows.Count; i++)
                {
                    string szöveg = "UPDATE Gépjárművek  SET ";
                    szöveg += "státus= true ";
                    szöveg += " WHERE id=" + Tábla_autó.SelectedRows[i].Cells[0].Value.ToString().Trim();
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
                if (Dolg_cégid.Text.Trim() == "")
                    throw new HibásBevittAdat("Cég nincs kiválasztva.");

                Dolgozó_Tábla_fejléc();

                string szöveg = "SELECT * FROM Dolgozók WHERE státus=false AND cégid=" + Dolg_cégid.Text.Trim() + " ORDER BY id ";
                Kezelő_Külső_Dolgozók Kéz = new Kezelő_Külső_Dolgozók();
                List<Adat_Külső_Dolgozók> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                string válasz = "Érvényes";
                foreach (Adat_Külső_Dolgozók rekord in Adatok)
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
                if (Dolg_cégid.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva cég.");
                if (Dolg_Dolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("A dolgozó név mezőt ki kell tölteni.");
                if (Dolg_Személyi.Text.Trim() == "")
                    throw new HibásBevittAdat("A személyi igazolványszám mezőt ki kell tölteni.");

                Dolgozó_Rögzítés(Dolg_cégid.Text.Trim(), Dolg_Dolgozónév.Text.Trim(), Dolg_Személyi.Text.Trim(), Dolg_Státus.Text.Trim());

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


        private void Dolgozó_Rögzítés(string Cég_ID, string Dolg_Név, string OKmány, string Státus)
        {
            DateTime Idő = new DateTime(1900, 1, 1);

            string szöveg = "SELECT * FROM Dolgozók";
            Kezelő_Külső_Dolgozók Kéz = new Kezelő_Külső_Dolgozók();
            List<Adat_Külső_Dolgozók> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
            double id = Adatok.Any() ? Adatok.Max(a => a.Id) + 1 : 1;

            if (!double.TryParse(Telephely_Cégid.Text, out double CegId)) CegId = 0;

            bool vane = Adatok.Any(a => a.Cégid == CegId && a.Név.Trim() == Dolg_Név.Trim() && a.Okmányszám.Trim() == OKmány.Trim());
            if (vane)
            {
                szöveg = "UPDATE Dolgozók  SET ";
                szöveg += $"okmányszám='{OKmány}', "; // okmányszám
                if (Státus.Trim() == "Érvényes")
                    szöveg += "státus=false ";
                else
                    szöveg += "státus= true ";

                szöveg += $" WHERE Cégid={Cég_ID} AND név='{Dolg_Név}'";
                szöveg += $" AND okmányszám='{OKmány}'";
            }
            else
            {
                szöveg = "INSERT INTO Dolgozók (id, név, okmányszám, anyjaneve, születésihely, születésiidő, cégid, státus) VALUES (";
                szöveg += $"{id}, "; // id X
                szöveg += $"'{Dolg_Név}', "; // név X
                szöveg += $"'{OKmány}', "; // okmányszám
                szöveg += "'_', "; // anyjaneve X
                szöveg += "'_', "; // születésihely
                szöveg += $"'{Idő:yyyy.MM.dd}', ";
                szöveg += $"{Cég_ID}, "; // cégid X
                if (Státus.Trim() == "Érvényes")
                    szöveg += " false) ";
                else
                    szöveg += " true) ";
            }
            MyA.ABMódosítás(hely, jelszó, szöveg);

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
            if (Dolg_tábla.Rows.Count < 1)
                return;
            if (e.RowIndex < 0)
                return;

            Dolg_Dolgozónév.Text = Dolg_tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            Dolg_Személyi.Text = Dolg_tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
        }


        private void Dolgozó_kivitel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolg_cégid.Text.Trim() == "")
                    return;
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

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                // Csak a fejlécet írjuk ki
                Dolgozó_Tábla_fejléc();
                Dolg_tábla.Visible = true;

                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("Arial", 12);
                string munkalap = "Munka1";

                for (int oszlop = 0; oszlop < Dolg_tábla.ColumnCount; oszlop++)
                {
                    MyE.Kiir(Dolg_tábla.Columns[oszlop].HeaderText, MyE.Oszlopnév(oszlop + 1) + "1");
                    MyE.Oszlopszélesség(munkalap, $"{MyE.Oszlopnév(oszlop + 1)}:{MyE.Oszlopnév(oszlop + 1)}", 30);
                }
                MyE.Rácsoz($"A1:{MyE.Oszlopnév(Dolg_tábla.ColumnCount)}2");
                MyE.Háttérszín($"A1:{MyE.Oszlopnév(Dolg_tábla.ColumnCount)}1", Color.Yellow);
                MyE.Betű($"A1:{MyE.Oszlopnév(Dolg_tábla.ColumnCount)}1", false, false, true);

                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:{MyE.Oszlopnév(Dolg_tábla.ColumnCount)}2", "", "", true);

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
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


        private void Dolgozó_beolvas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolg_cégid.Text.Trim() == "")
                    throw new HibásBevittAdat("Cég nincs kiválasztva.");

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
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                // megnyitjuk a beolvasandó táblát

                MyE.ExcelMegnyitás(fájlexc);

                // megnézzük, hogy hány sorból áll a tábla
                int ii = 1;
                int utolsó = 0;
                while (MyE.Beolvas($"b{ii}").Trim() != "_")
                {
                    utolsó = ii;
                    ii += 1;
                }
                Holtart.Be(utolsó);

                if (utolsó > 1)
                {
                    for (int i = 2; i <= utolsó; i++)
                    {
                        string Név = MyE.Beolvas($"b{i}").Trim().Replace(",", "");
                        string Személyi = MyE.Beolvas($"c{i}").Trim().Replace(",", "");
                        Dolgozó_Rögzítés(Dolg_cégid.Text.Trim(), Név, Személyi, "Érvényes");

                        Holtart.Lép();
                    }
                }
                // bezárjuk az excel táblát
                MyE.ExcelBezárás();

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
                if (Dolg_tábla.Rows.Count == 0)
                    return;
                if (Dolg_tábla.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string szöveg;
                {

                    List<string> SzövegGy = new List<string>();
                    for (int i = 0; i <= Dolg_tábla.SelectedRows.Count - 1; i++)
                    {
                        szöveg = "UPDATE Dolgozók  SET ";
                        szöveg += "státus= true ";
                        szöveg += " WHERE id=" + Dolg_tábla.SelectedRows[i].Cells[0].Value.ToString().Trim();
                        SzövegGy.Add(szöveg);
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }
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

                string szöveg = "SELECT * FROM telephelyek";
                Adatok_Külső_Telephelyek = Kéz_Külső_Telephelyek.Lista_Adatok(hely, jelszó, szöveg);

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

                string szöveg = "SELECT * FROM telephelyek";
                Adatok_Külső_Telephelyek = Kéz_Külső_Telephelyek.Lista_Adatok(hely, jelszó, szöveg);

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Telephely_Tábla.Rows.Count; i++)
                {
                    if (!double.TryParse(Telephely_Cégid.Text, out double CegId)) CegId = 0;
                    bool vane = Adatok_Külső_Telephelyek.Any(a => a.Cégid == CegId && a.Telephely.Trim() == Telephely_Tábla.Rows[i].Cells[1].Value.ToStrTrim());
                    if (vane)
                    {
                        // ha van ilyen akkor módosítjuk
                        szöveg = "UPDATE telephelyek  SET ";
                        if (!bool.Parse(Telephely_Tábla.Rows[i].Cells[0].Value.ToString()))
                            szöveg += "státus=false ";
                        else
                            szöveg += "státus=true ";

                        szöveg += " WHERE  cégid=" + Telephely_Cégid.Text.Trim() + " AND telephely='" + Telephely_Tábla.Rows[i].Cells[1].Value.ToString().Trim() + "'";
                    }
                    else
                    {
                        // ha nincs akkor újként rögzítjük
                        double id = Adatok_Külső_Telephelyek.Any() ? Adatok_Külső_Telephelyek.Max(a => a.Id) + 1 : 1;

                        szöveg = "INSERT INTO telephelyek (id, telephely, cégid, státus ) VALUES (";
                        szöveg += id.ToString() + ", ";
                        szöveg += $"'{Telephely_Tábla.Rows[i].Cells[1].Value.ToString().Trim()}', ";
                        szöveg += Telephely_Cégid.Text.Trim() + ", ";
                        if (!bool.Parse(Telephely_Tábla.Rows[i].Cells[0].Value.ToString()))
                            szöveg += " false )";
                        else
                            szöveg += " true )";
                    }
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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
                string szöveg;
                if (sor == 1)
                {
                    if (Rádió_főmérnök)
                    {
                        szöveg = "SELECT * FROM Cégek WHERE engedély=1 AND terület='Főmérnökség' Order By cégid ";
                    }
                    else
                    {
                        // telephelyek meghatározása

                        szöveg = "SELECT * FROM Cégek WHERE engedély=1 AND ( ";
                        for (int k = 0; k < Cmbtelephely.Items.Count; k++)
                            szöveg += $" terület='{Cmbtelephely.Items[k].ToString().Trim()}' OR";
                        szöveg = szöveg.Substring(0, szöveg.Length - 2);
                        szöveg += ") Order By cégid ";
                    }
                }
                else
                {
                    szöveg = "SELECT * FROM Cégek Order By cégid ";
                }

                Kezelő_Külső_Cégek Kéz = new Kezelő_Külső_Cégek();
                List<Adat_Külső_Cégek> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);


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

                string szöveg = "SELECT * FROM Cégek";
                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok(hely, jelszó, szöveg);

                Holtart.Be(Engedély_tábla.Rows.Count + 1);

                List<string> SzövegGy = new List<string>();
                for (int ii = 0; ii < Engedély_tábla.SelectedRows.Count; ii++)
                {
                    if (!double.TryParse(Engedély_tábla.SelectedRows[ii].Cells[0].Value.ToStrTrim(), out double CegId)) CegId = 0;

                    bool vane = Adatok_Külső_Cégek.Any(a => a.Cégid == CegId && a.Engedély == 1);
                    if (vane)
                    {
                        szöveg = "UPDATE Cégek  SET ";
                        szöveg += " engedély=5, "; // engedély
                        szöveg += " Engedélyezés_dátuma='" + DateTime.Now.ToString("yyyy.MM.dd HH:mm") + "', ";
                        szöveg += " Engedélyező='" + Program.PostásTelephely + "'";
                        szöveg += " WHERE [Cégid]=" + Engedély_tábla.SelectedRows[ii].Cells[0].Value.ToString();
                        SzövegGy.Add(szöveg);
                        Engedély_sorszámok.Text += Engedély_tábla.SelectedRows[ii].Cells[0].Value.ToString() + ", ";
                        volt = 1;
                        E_levél(ii);
                    }

                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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
                if (e.RowIndex < 0)
                    return;

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

                string szöveg = "SELECT * FROM Cégek";
                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok(hely, jelszó, szöveg);

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Engedély_tábla.Rows.Count; i++)
                {
                    if (Engedély_tábla.Rows[i].Selected == true)
                    {
                        if (!double.TryParse(Engedély_tábla.Rows[i].Cells[0].Value.ToStrTrim(), out double CegId)) CegId = 0;
                        bool vane = Adatok_Külső_Cégek.Any(a => a.Cégid == CegId && a.Engedély == 1);
                        if (vane)
                        {
                            szöveg = "UPDATE Cégek  SET ";
                            szöveg += " engedély=7"; // Elutasított
                            szöveg += " WHERE [Cégid]=" + Engedély_tábla.Rows[i].Cells[0].Value;
                            SzövegGy.Add(szöveg);
                        }
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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

                string szöveg = "SELECT * FROM Cégek";
                Adatok_Külső_Cégek = Kéz_Külső_Cégek.Lista_Adatok(hely, jelszó, szöveg);

                Engedély_sorszámok.Text = "";

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Engedély_tábla.SelectedRows.Count; i++)
                {
                    if (!double.TryParse(Engedély_tábla.SelectedRows[i].Cells[0].Value.ToStrTrim(), out double CegId)) CegId = 0;
                    bool vane = Adatok_Külső_Cégek.Any(a => a.Cégid == CegId && a.Engedély == 5);
                    if (vane)
                    {
                        szöveg = "UPDATE Cégek  SET ";
                        szöveg += " engedély=7"; // visszavont
                        szöveg += " WHERE [Cégid]=" + Engedély_tábla.SelectedRows[i].Cells[0].Value;
                        SzövegGy.Add(szöveg);
                        Engedély_sorszámok.Text += Engedély_tábla.SelectedRows[i].Cells[0].Value + ", ";
                        volt = 1;
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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

                string szöveg = "SELECT * FROM cégek WHERE engedély=5 AND érv_vég<#" + DateTime.Today.ToString("MM-dd-yyyy") + " 00:00:0#";

                Kezelő_Külső_Cégek Kéz = new Kezelő_Külső_Cégek();
                List<Adat_Külső_Cégek> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Cégek rekord in Adatok)
                {

                    // Módosítjuk az adatot
                    szöveg = $"UPDATE Cégek SET  engedély=8 WHERE [Cégid]={rekord.Cégid}";
                    SzövegGy.Add(szöveg);
                    volt = 1;
                    Engedély_sorszámok.Text += rekord.Cégid + ", ";
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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

                MyE.DataGridViewToExcel(fájlexc, Lekérdezés_tábla);
                MessageBox.Show($"Elkészült az Excel tábla: {fájlexc}.xlsx", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("Arial", 12);
                string munkalap = "Munka1";

                // oszlopszélességek
                MyE.Oszlopszélesség(munkalap, "a:a", 30);
                MyE.Oszlopszélesség(munkalap, "b:b", 15);
                MyE.Oszlopszélesség(munkalap, "c:c", 30);
                MyE.Oszlopszélesség(munkalap, "d:d", 15);
                MyE.Oszlopszélesség(munkalap, "e:e", 30);
                MyE.Oszlopszélesség(munkalap, "f:f", 15);

                Holtart.Be(Adatok_Dolg.Count + 2);

                string cégneve = "";
                string munkaleírása = "";
                // Tartalom
                int sor = 0;
                int blokkeleje = 0;
                int három = 1;

                string helyi = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                string jelszói = "Mocó";
                string szöveg = "SELECT * FROM jelenlétiív";
                Adatok_Kieg_Jelenlétiív = Kéz_Kieg_Jelenlétiív.Lista_Adatok(helyi, jelszói, szöveg);

                foreach (Adat_Külső_Lekérdezés_Személy rekord in Adatok_Dolg)
                {

                    if (cégneve.Trim() != rekord.Cég.Trim() || munkaleírása.Trim() != rekord.Munkaleírás.Trim())
                    {
                        // előső dolgozó nevek formázása
                        if (blokkeleje > 3)
                        {
                            MyE.Rácsoz($"a{blokkeleje}:f{sor}");
                            MyE.Vastagkeret($"a{blokkeleje}:f{sor}");
                        }

                        // Cégfejléc készítés
                        sor++;
                        MyE.Egyesít(munkalap, $"a{sor}:b{sor}");
                        MyE.Egyesít(munkalap, $"c{sor}:f{sor}");
                        MyE.Kiir("Cég neve", $"a{sor}");
                        MyE.Kiir("Munkaleírása", $"c{sor}");
                        MyE.Háttérszín($"a{sor}:f{sor}", Color.Yellow);
                        MyE.Rácsoz($"a{sor}:f{sor}");
                        MyE.Vastagkeret($"a{sor}:f{sor}");
                        // Cégadatok
                        sor++;
                        cégneve = rekord.Cég.Trim();
                        munkaleírása = rekord.Munkaleírás.Trim();
                        MyE.Egyesít(munkalap, $"a{sor}:b{sor}");
                        MyE.Egyesít(munkalap, $"c{sor}:f{sor}");
                        MyE.Kiir(cégneve, $"a{sor}");
                        MyE.Kiir(munkaleírása, $"c{sor}");
                        MyE.Sormagasság($"{sor}:{sor}", 30);
                        MyE.Igazít_vízszintes($"{sor}:{sor}", "közép");
                        MyE.Igazít_függőleges($"{sor}:{sor}", "közép");

                        MyE.Rácsoz($"a{sor}:f{sor}");
                        MyE.Vastagkeret($"a{sor}:f{sor}");
                        // Dolgozó fejléc készítés
                        sor++;

                        MyE.Kiir("Név", $"a{sor}");
                        MyE.Kiir("Név", $"c{sor}");
                        MyE.Kiir("Név", $"e{sor}");

                        MyE.Kiir("Szem.ig.", $"b{sor}");
                        MyE.Kiir("Szem.ig.", $"d{sor}");
                        MyE.Kiir("Szem.ig.", $"f{sor}");

                        MyE.Rácsoz($"a{sor}:f{sor}");
                        MyE.Vastagkeret($"a{sor}:f{sor}");
                        MyE.Háttérszín($"a{sor}:f{sor}", Color.Yellow);
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
                                MyE.Kiir(rekord.Név.Trim(), "a" + sor.ToString());
                                MyE.Kiir(rekord.Okmányszám.Trim(), "b" + sor.ToString());
                                break;
                            }
                        case 2:
                            {
                                MyE.Kiir(rekord.Név.Trim(), "c" + sor.ToString());
                                MyE.Kiir(rekord.Okmányszám.Trim(), "d" + sor.ToString());
                                break;
                            }
                        case 3:
                            {
                                MyE.Kiir(rekord.Név.Trim(), "e" + sor.ToString());
                                MyE.Kiir(rekord.Okmányszám.Trim(), "f" + sor.ToString());
                                break;
                            }
                    }
                    három += 1;
                    Holtart.Lép();
                }
                MyE.Rácsoz($"a{blokkeleje}:f{sor}");
                MyE.Vastagkeret($"a{blokkeleje}:f{sor}");

                sor += 5;

                MyE.Kiir("Budapest," + DateTime.Today.ToString("yyyy.MM.dd"), $"a{sor}");
                MyE.Kiir("Gondnok", $"c{sor}");

                // nyomtatási terület kijelölése

                string helyicsop = Application.StartupPath + @"\Főmérnökség\adatok\BKV.jpg";

                string telephely = (from a in Adatok_Kieg_Jelenlétiív
                                    where a.Id == 4
                                    select a.Szervezet).FirstOrDefault() ?? "";

                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:F{sor}", "", "",
                                              $"&G\n{telephely}", "Gépjármű Behajtási Engedély Külső cég", "&D",
                                              "", "", "&P/&N",
                                              helyicsop,
                                              0.393700787401575, 0.393700787401575,
                                              0.590551181102362, 1.18110236220472,
                                              0.511811023622047, 0.511811023622047,
                                              false, false, "1", "",
                                              true, "A4");

                Holtart.Ki();
                // bezárjuk az Excel-t
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


        private void Lekérd_dolgozó_lista_Click(object sender, EventArgs e)
        {
            Lekérd_dolgozó_Lista_Elj();
        }


        private void Lekérd_dolgozó_Lista_Elj()
        {
            try
            {
                string szöveg = "Select Telephelyek.Telephely, Telephelyek.Státus, Cégek.Engedély, Cégek.Cég, Cégek.Munkaleírás, Dolgozók.Név, Dolgozók.Okmányszám, Dolgozók.Anyjaneve,";
                szöveg += " Dolgozók.Születésihely, Dolgozók.Születésiidő, Dolgozók.Státus ";
                szöveg += " FROM(Cégek INNER JOIN Telephelyek On Cégek.Cégid = Telephelyek.Cégid) INNER JOIN Dolgozók On Cégek.Cégid = Dolgozók.Cégid ";
                szöveg += $" WHERE Telephelyek.Telephely ='{Cmbtelephely.Text.Trim()}' AND Telephelyek.Státus=True And Cégek.Engedély= 5 And ";
                szöveg += " Dolgozók.Státus= False ORDER BY Cégek.Cég,Cégek.Munkaleírás,Dolgozók.Név";

                Adatok_Dolg = Kéz_Dolg.Lista_Adatok(hely, jelszó, szöveg);

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
                foreach (Adat_Külső_Lekérdezés_Személy rekord in Adatok_Dolg)
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
                string szöveg = "Select  Gépjárművek.Frsz, Cégek.Cég, Telephelyek.Telephely,  Cégek.Munkaleírás ";
                szöveg += " FROM(Cégek INNER JOIN Telephelyek On Cégek.Cégid = Telephelyek.Cégid) INNER JOIN Gépjárművek On Cégek.Cégid = Gépjárművek.Cégid ";
                szöveg += $" WHERE Telephelyek.Telephely ='{Cmbtelephely.Text.Trim()}' And Cégek.Engedély=5 ";
                szöveg += " And Gépjárművek.Státus=false And Telephelyek.Státus= True ORDER BY Cégek.Cég, Cégek.Munkaleírás, Gépjárművek.Frsz";

                Adatok_autó = Kéz_autó.Lista_Adatok(hely, jelszó, szöveg);

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

                foreach (Adat_Külső_Lekérdezés_Autó rekord in Adatok_autó)
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
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("Arial", 12);
                string munkalap = "Munka1";

                MyE.Háttérszín("A1:c1", Color.Yellow);
                MyE.Kiir("Rendszám", "a1");
                MyE.Kiir("Cég neve", "b1");
                MyE.Kiir("Munkaleírása", "c1");
                MyE.Oszlopszélesség(munkalap, "a:a", 15);
                MyE.Oszlopszélesség(munkalap, "b:b", 45);
                MyE.Oszlopszélesség(munkalap, "c:c", 75);
                MyE.Rácsoz("a1:c1");
                MyE.Vastagkeret("a1:c1");

                int sor;
                int blokkeleje;
                string cégneve = "";
                string munkaleírása = "";

                Holtart.Be(Adatok_autó.Count + 3);
                // Tartalom
                sor = 2;
                blokkeleje = 2;

                string helyi = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                string jelszói = "Mocó";
                string szöveg = "SELECT * FROM jelenlétiív";
                Adatok_Kieg_Jelenlétiív = Kéz_Kieg_Jelenlétiív.Lista_Adatok(helyi, jelszói, szöveg);

                foreach (Adat_Külső_Lekérdezés_Autó Rekord in Adatok_autó)
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
                    MyE.Kiir(Rekord.Frsz.Trim(), $"a{sor}");
                    sor += 1;
                    Holtart.Lép();
                }
                // kiírjuk az utolsókat
                Autó_Cégnév(munkalap, blokkeleje, sor, cégneve, munkaleírása);

                sor += 5;

                MyE.Kiir("Budapest," + DateTime.Today.ToString("yyyy.MM.dd"), $"A{sor}");
                MyE.Kiir("Gondnok", $"C{sor}");

                // nyomtatási terület kijelölése

                string helyicsop = $@"{Application.StartupPath}\Főmérnökség\adatok\BKV.jpg";

                string telephely = (from a in Adatok_Kieg_Jelenlétiív
                                    where a.Id == 4
                                    select a.Szervezet).FirstOrDefault() ?? "";

                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:C{sor}", "$1:$1", "",
                    $"&G\n{telephely}", "Gépjármű Behajtási Engedély Külső cég", "&D",
                    "", "", "&P/&N",
                    helyicsop,
                    0.393700787401575, 0.393700787401575,
                    0.590551181102362, 1.18110236220472,
                    0.511811023622047, 0.511811023622047,
                    false, false, "1", "",
                    true, "A4");

                Holtart.Ki();
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


        private void Autó_Cégnév(string munkalap, int blokkeleje, int sor, string cégneve, string munkaleírása)
        {

            MyE.Egyesít(munkalap, $"B{blokkeleje}:B{sor - 1}");
            MyE.Egyesít(munkalap, $"C{blokkeleje}:C{sor - 1}");

            MyE.Sortörésseltöbbsorba($"B{blokkeleje}", true);
            MyE.Sortörésseltöbbsorba($"C{blokkeleje}", true);

            MyE.Rácsoz($"A{blokkeleje}:C{sor - 1}");
            MyE.Vastagkeret($"A{blokkeleje}:C{sor - 1}");

            MyE.Kiir(cégneve, $"B{blokkeleje}");
            MyE.Kiir(munkaleírása, $"C{blokkeleje}");

        }
        #endregion


        #region Email
        private void Email_rögzít_Click(object sender, EventArgs e)
        {

            try
            {
                string szöveg = "SELECT * FROM Email";
                Adatok_Külső_Email = Kéz_Külső_Email.Lista_Adatok(hely, jelszó, szöveg);
                bool vane = Adatok_Külső_Email.Any(a => a.Id == Email_id);
                if (vane)
                {
                    szöveg = "UPDATE Email  SET ";
                    szöveg += " Másolat='" + Email_másolat.Text.Trim().Replace(",", "").Replace("'", "°") + "', ";
                    szöveg += " Aláírás='" + Email_Aláírás.Text.Trim().Replace(",", "").Replace("'", "°") + "' ";
                    szöveg += " WHERE id=" + Email_id;
                }
                else
                {
                    szöveg = "INSERT INTO Email  (id, Másolat, Aláírás  ) VALUES (";
                    szöveg += Email_id;
                    szöveg += ", '" + Email_másolat.Text.Trim().Replace(",", "").Replace("'", "°") + "', ";
                    szöveg += "'" + Email_Aláírás.Text.Trim().Replace(",", "").Replace("'", "°") + "') ";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
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

                string szöveg = "SELECT * FROM Email";
                Adatok_Külső_Email = Kéz_Külső_Email.Lista_Adatok(hely, jelszó, szöveg);
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
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
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
    }
}