using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyO = Microsoft.Office.Interop.Outlook;

namespace Villamos
{
    public partial class AblakMenetkimaradás
    {
        readonly Kezelő_Menetkimaradás KézMenet = new Kezelő_Menetkimaradás();
        readonly Kezelő_Kiegészítő_Szolgálat KézSzolg = new Kezelő_Kiegészítő_Szolgálat();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Telep_Kiegészítő_SAP KézSap = new Kezelő_Telep_Kiegészítő_SAP();
        readonly Kezelő_Kiegészítő_Adatok_Terjesztés KézTerjeszt = new Kezelő_Kiegészítő_Adatok_Terjesztés();
        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgTelep = new Kezelő_Kiegészítő_Szolgálattelepei();

        //Különszálas beolvasás
        string Felelősmunkahely = "";
        string Telephely = "";
        DateTime DátumKüld = DateTime.Now;
        string Fájlexc = "";

        Adat_Kiegészítő_Adatok_Terjesztés EgyTerjesztés;

        int szakszolgálat = 0;
        string Html_szöveg = "";
        int idszám_;
        string TelepHely = "";

        #region Alap
        /// <summary>
        /// konstruktor
        /// </summary>
        public AblakMenetkimaradás()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// Ablak betöltésekor elvégzendő műveletek
        /// </summary>
        private void Start()
        {
            // beállítjuk a dátumot az előző napra mert mai adat még nincs
            Dátum.Value = DateTime.Now.AddDays(-1);
            DátumTól.Value = MyF.Hónap_elsőnapja(DateTime.Today);
            DátumIg.Value = MyF.Hónap_utolsónapja(DateTime.Today);
            Telephelyekfeltöltése();
            Szolgálatoklista();
            Telephelyek_Feltöltése_lista();

            // ha járműkiadó telephely, akkor csak a saját telephelyet kezeli.
            Panel1.Visible = Cmbtelephely.Enabled;
            Panel2.Visible = Cmbtelephely.Enabled;

            Pályaszámokfeltöltése();
            Jogosultságkiosztás();  // Jogosultságok beállítása
        }

        private void Menetkimaradás_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Telephelyek feltöltése a comboboxba
        /// </summary>
        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);

                cmbtelephely1.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    cmbtelephely1.Items.Add(Elem);
                Cmbtelephely.Enabled = false;

                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                {
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
                    cmbtelephely1.Text = cmbtelephely1.Items[0].ToStrTrim();
                }
                else
                {
                    Cmbtelephely.Text = Program.PostásTelephely;
                    cmbtelephely1.Text = Program.PostásTelephely;
                }

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
        /// Listába feltölti a szolgálatokat
        /// </summary>
        private void Szolgálatoklista()
        {
            try
            {
                // szolgálatok listázása
                Lstszolgálatok.Items.Clear();
                List<Adat_Kiegészítő_Szolgálat> Adatok = KézSzolg.Lista_Adatok();
                foreach (Adat_Kiegészítő_Szolgálat Elem in Adatok)
                    Lstszolgálatok.Items.Add(Elem.Szolgálatnév);
                Lstszolgálatok.Refresh();
            }
            catch (HibásBevittAdat ex)
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
        /// Telephelyek feltöltése a listboxba
        /// </summary>
        private void Telephelyek_Feltöltése_lista()
        {
            try
            {
                List<Adat_Kiegészítő_Szolgálattelepei> Adatok = KézSzolgTelep.Lista_Adatok().OrderBy(a => a.Telephelynév).ToList();
                Lstüzemek.Items.Clear();
                foreach (Adat_Kiegészítő_Szolgálattelepei Elem in Adatok)
                    Lstüzemek.Items.Add(Elem.Telephelynév);
                Lstüzemek.Refresh();
            }
            catch (HibásBevittAdat ex)
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
        /// Üzemelő kocsik pályaszámok feltöltése a comboboxba
        /// </summary>
        private void Pályaszámokfeltöltése()
        {
            try
            {
                Pályaszámok.Items.Clear();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Státus == 0
                          orderby a.Azonosító
                          select a).ToList();
                foreach (Adat_Jármű Elem in Adatok)
                    Pályaszámok.Items.Add(Elem.Azonosító.Trim());
                Pályaszámok.Refresh();
            }
            catch (HibásBevittAdat ex)
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
        /// Jogosultságok kiosztása a gombokhoz és egyéb elemekhez
        /// </summary>
        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false

            BtnSap.Enabled = false;
            BtnFőmérnükség.Visible = false;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            Button4.Enabled = false;

            melyikelem = 20;

            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnSap.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {

                Panel1.Visible = true;
                Panel2.Visible = true;
                BtnFőmérnükség.Visible = true;
            }

            melyikelem = 21;
            // Megjelenítés
            if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "1")
            {
                // e-mail
                Button4.Enabled = true;
            }

            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {

                // I szakszolgálat
                Button1.Visible = true;
                Button4.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                // II szakszolgálat
                Button2.Visible = true;
                Button4.Enabled = true;
            }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                // III szakszolgálat
                Button3.Visible = true;
                Button4.Enabled = true;
            }
        }

        /// <summary>
        /// Ha a segédablakok nyitva vannak akkor bezárja őket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AblakMenetkimaradás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Menetrögítés?.Close();
            Új_Ablak_Menetkimaradás_Kiegészítő?.Close();
        }

        /// <summary>
        /// Súgó gomb megnyomásakor megnyitja a súgó fájlt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\menetkimaradás.html";
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
        #endregion


        #region Beolvasás
        private async void BtnSap_Click(object sender, EventArgs e)
        {
            try
            {
                // ha nincs kiválasztva telephely, akkor nem tudunk feltölteni adatot.
                if (Cmbtelephely.Text.Trim() == "") return;
                Telephely = Cmbtelephely.Text.Trim();
                DátumKüld = Dátum.Value;  // a dátumot elküldjük a külön szálra, hogy tudja melyik évben kell keresni az adatokat.

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    Fájlexc = OpenFileDialog1.FileName;
                else
                    return;
                timer1.Enabled = true;
                Holtart.Be();
                Felelősmunkahely = Felelős_Munkahely();   // beolvassuk a felelős munkahelyet
                await Task.Run(() => SAP_Adatokbeolvasása.Menet_beolvasó(Telephely, DátumKüld.Year, Fájlexc, Felelősmunkahely));
                timer1.Enabled = false;
                Holtart.Ki();

                // kitöröljük a betöltött fájlt
                File.Delete(Fájlexc);
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
        /// <summary>
        /// Beolvasott adatok feldolgozása közben lépteti a Holtartot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        /// <summary>
        /// A telephelyhez tartozó felelős munkahely lekérdezése
        /// </summary>
        /// <returns></returns>
        private string Felelős_Munkahely()
        {
            string válasz = "";
            try
            {
                List<Adat_Telep_Kiegészítő_SAP> AdatokSAP = KézSap.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Telep_Kiegészítő_SAP RekordSAP = (from a in AdatokSAP
                                                       where a.Id == 1
                                                       select a).FirstOrDefault();

                if (RekordSAP != null) válasz = RekordSAP.Felelősmunkahely;
            }
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
        #endregion


        #region Excel
        /// <summary>
        /// Listázott adatok Excel fájlba mentése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnexcel_Click(object sender, EventArgs e)
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
                    FileName = $"Menetkimaradás_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Tábla);
                MyE.Megnyitás(fájlexc);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
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


        #region Szakszolgálati gombok eseménykezelői
        /// <summary>
        /// Szakszolgálati gomb beírja a táblázatba az adatokat, majd elküldés gomb látszódik, ha van tartalom.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            Button1.Visible = false;
            szakszolgálat = 1;
            MelyikSzakSzolgálat();
            Excelbeíró();
            Button4.Visible = Html_szöveg.Trim() != "";
            Button1.Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Button2.Visible = false;
            szakszolgálat = 2;
            MelyikSzakSzolgálat();
            Excelbeíró();
            Button4.Visible = Html_szöveg.Trim() != "";
            Button2.Visible = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Button3.Visible = false;
            szakszolgálat = 3;
            MelyikSzakSzolgálat();
            Excelbeíró();
            Button4.Visible = Html_szöveg.Trim() != "";
            Button3.Visible = true;
        }

        /// <summary>
        /// Szakszolgálat kiválasztása után beolvassa a kiválasztott szakszolgálat adatait
        /// </summary>
        private void MelyikSzakSzolgálat()
        {
            List<Adat_Kiegészítő_Adatok_Terjesztés> Adatok = KézTerjeszt.Lista_Adatok();

            EgyTerjesztés = (from a in Adatok
                             where a.Id == szakszolgálat
                             select a).FirstOrDefault();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Email();
        }

        private void Email()
        {
            try
            {

                if (EgyTerjesztés != null)
                {
                    MyO._Application _app = new MyO.Application();
                    MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);
                    // címzett
                    mail.To = EgyTerjesztés.Email;
                    // üzenet tárgya
                    mail.Subject = $"Események {DateTime.Today.AddDays(-1):yyyy-MM-dd}";
                    // üzent szövege
                    mail.HTMLBody = Html_szöveg;
                    mail.Importance = MyO.OlImportance.olImportanceNormal;
                    mail.Attachments.Add(EgyTerjesztés.Szöveg);
                    ((MyO._MailItem)mail).Send();

                    MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Nem található a megadott ID-hoz tartozó rekord.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (HibásBevittAdat ex)
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
        /// Rózsahegyi Excel fájlba írja a kimaradt eseményeket, és a dátumokat.
        /// </summary>
        private void Excelbeíró()
        {
            // beolvassuk az elérési utat
            try
            {
                if (!Exists(EgyTerjesztés.Szöveg)) throw new HibásBevittAdat("Nem létezik az elérési út/ vagy az Excel tábla.");

                Html_szöveg = "<html><body>";
                // ha létezik, akkor benyitjuk az excel táblát.
                Holtart.Be(10);

                MyE.ExcelMegnyitás(EgyTerjesztés.Szöveg);

                int vége = 0;

                // hány oszlopból áll
                Holtart.Lép();
                int oszlopmax = 0;
                int i = 1;
                while (vége == 0)
                {
                    i++;
                    if (MyE.Beolvas(MyE.Oszlopnév(i) + "1") == "_")
                    {
                        vége = 1;
                        oszlopmax = i - 1;
                    }
                }


                Holtart.Lép();
                i = 1;
                int szám = 0;
                vége = 0;
                while (vége == 0)
                {
                    i++;
                    if (MyE.Beolvas($"a{i}").ToUpper() == "X")
                    {
                        vége = 1;
                        szám = i;
                    }
                }
                // töröljük az utolsó hogy melyik dátum volt az utolsó
                MyE.Kiir("", $"a{szám}");
                string szöveg1;
                string szöveg2;
                string szöveg_html;
                DateTime utolsónap = DateTime.Parse(MyE.Beolvas($"b{szám}"));
                i = 1;
                Holtart.Lép();

                while (utolsónap.ToString("MM/dd/yyyy") != DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"))
                {
                    Holtart.Lép();
                    utolsónap = utolsónap.AddDays(1);
                    szám++;
                    Html_szöveg += $"<p>{utolsónap:yyyy.MM.dd}</p> ";
                    MyE.Kiir(utolsónap.ToString("yyyy.MM.dd"), $"b{szám}");
                    MyE.Kiir(utolsónap.ToString("ddd"), $"c{szám}");
                    for (int j = 4; j <= oszlopmax; j++)
                    {
                        Holtart.Lép();
                        string telep = MyE.Beolvas(MyE.Oszlopnév(j) + "1").Trim();
                        //Megvizsgáljuk, hogy telephelynév-e
                        if (Lstüzemek.Items.Contains(telep))
                        {
                            List<Adat_Menetkimaradás> Adatok = KézMenet.Lista_Adatok(telep, Dátum.Value.Year);
                            Adatok = (from a in Adatok
                                      where a.Bekövetkezés >= MyF.Nap0000(utolsónap)
                                      && a.Bekövetkezés <= MyF.Nap2359(utolsónap)
                                      && a.Eseményjele != "_"
                                      orderby a.Eseményjele, a.Típus
                                      select a).ToList();

                            szöveg1 = "";
                            szöveg_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'>";
                            szöveg_html += $"<tr><td style='background-color: #B8DBFD;border: 1px solid #ccc'>{telep}</td></tr>";

                            if (Adatok.Count != 0)
                            {
                                //Fejléc
                                szöveg_html += "<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc'>Jel</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Viszonylat</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Típus</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Psz</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Járművezetői beírás</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Javítás</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Menet</th></tr>";
                                foreach (Adat_Menetkimaradás rekord in Adatok)
                                {
                                    szöveg1 += " " + rekord.Eseményjele.Trim();
                                    szöveg1 += " " + rekord.Viszonylat.Trim();
                                    szöveg1 += " " + rekord.Típus.Trim();
                                    szöveg1 += " " + rekord.Azonosító.Trim();
                                    szöveg1 += " " + rekord.Jvbeírás.Trim();
                                    szöveg1 += " - " + rekord.Javítás.Trim();
                                    szöveg1 += " " + rekord.Kimaradtmenet.ToString() + " menet\n";

                                    szöveg_html += $"<tr><td style='border: 1px solid #ccc'>{rekord.Eseményjele.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Viszonylat.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Típus.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Azonosító.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Jvbeírás.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Javítás.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Kimaradtmenet}</td></tr>";
                                }
                            }
                            else
                            {
                                szöveg1 += "OK";
                                szöveg_html += $"<tr><td style='border: 1px solid #ccc'> OK </td></tr>";
                            }
                            szöveg_html += "</table>";
                            Html_szöveg += szöveg_html;

                            szöveg2 = MyE.Beolvas(MyE.Oszlopnév(j) + $"{szám}");
                            if (szöveg2.Trim() != "_")
                                szöveg1 = szöveg2 + "\n" + szöveg1;
                            MyE.Kiir(szöveg1, MyE.Oszlopnév(j) + szám.ToString());

                        }
                    }
                }
                MyE.Kiir("X", "a" + szám.ToString());
                MyE.Kiir(szám.ToString(), "aa1");
                MyE.Aktív_Cella("Munka1", "A" + szám.ToString());
                MyE.ExcelMentés();
                MyE.ExcelBezárás();
                Html_szöveg += "</body></html>";

                Holtart.Ki();
                MyE.Megnyitás(EgyTerjesztés.Szöveg);
            }
            catch (HibásBevittAdat ex)
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
        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            Táblalistázás();
        }

        /// <summary>
        /// Töröljük a táblázatot, és újra feltöltjük az adatokat
        /// </summary>
        private void Táblatörlése()
        {
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
            Tábla.ClearSelection();
        }

        private void Táblalistázás()
        {
            try
            {
                // ha üres a telephely választó akkor nem listáz
                if (cmbtelephely1.Text.Trim() == "") throw new HibásBevittAdat("A telephelyet meg kell adni.");
                DateTime FutóDátum = DátumTól.Value;
                List<Adat_Menetkimaradás> Adatok = new List<Adat_Menetkimaradás>();
                while (FutóDátum <= DátumIg.Value)
                {
                    // lekérdezzük az adott telephelyre a dátumot
                    List<Adat_Menetkimaradás> AdatokT = KézMenet.Lista_Adatok(cmbtelephely1.Text.Trim(), FutóDátum.Year);
                    Adatok.AddRange(AdatokT);
                    FutóDátum = FutóDátum.AddYears(1);
                }
                Adatok = (from a in Adatok
                          where a.Bekövetkezés >= MyF.Nap0000(DátumTól.Value)
                          && a.Bekövetkezés <= MyF.Nap2359(DátumIg.Value)
                          orderby a.Bekövetkezés
                          select a).ToList();
                if (Pályaszámok.Text.Trim() != "") Adatok = (from a in Adatok
                                                             where a.Azonosító.Trim() == Pályaszámok.Text.Trim()
                                                             select a).ToList();

                Tábla.ColumnCount = 13;
                Tábla.RowCount = 0;
                Tábla.Visible = false;
                // Táblázat fejléce
                Tábla.Columns[0].HeaderText = "Srsz";
                Tábla.Columns[1].HeaderText = "ABC";
                Tábla.Columns[2].HeaderText = "Visz.";
                Tábla.Columns[3].HeaderText = "Típus";
                Tábla.Columns[4].HeaderText = "Psz";
                Tábla.Columns[5].HeaderText = "Járművezetői beírás";
                Tábla.Columns[6].HeaderText = "Javítás";
                Tábla.Columns[7].HeaderText = "Idő";
                Tábla.Columns[8].HeaderText = "Menet";
                Tábla.Columns[9].HeaderText = "Törölt";
                Tábla.Columns[10].HeaderText = "Jelentés";
                Tábla.Columns[11].HeaderText = "Tétel";
                Tábla.Columns[12].HeaderText = "Telephely";
                Tábla.Columns[0].Width = 55;
                Tábla.Columns[1].Width = 45;
                Tábla.Columns[2].Width = 45;
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].Width = 50;
                Tábla.Columns[5].Width = 250;
                Tábla.Columns[6].Width = 250;
                Tábla.Columns[7].Width = 180;
                Tábla.Columns[8].Width = 60;
                Tábla.Columns[9].Width = 45;
                Tábla.Columns[10].Width = 90;
                Tábla.Columns[11].Width = 45;
                Tábla.Columns[12].Width = 90;

                foreach (Adat_Menetkimaradás rekord in Adatok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla.Rows[i].Cells[1].Value = rekord.Eseményjele;
                    Tábla.Rows[i].Cells[2].Value = rekord.Viszonylat;
                    Tábla.Rows[i].Cells[3].Value = rekord.Típus;
                    Tábla.Rows[i].Cells[4].Value = rekord.Azonosító;
                    Tábla.Rows[i].Cells[5].Value = rekord.Jvbeírás;
                    Tábla.Rows[i].Cells[6].Value = rekord.Javítás;
                    Tábla.Rows[i].Cells[7].Value = rekord.Bekövetkezés;
                    Tábla.Rows[i].Cells[8].Value = rekord.Kimaradtmenet;
                    Tábla.Rows[i].Cells[9].Value = rekord.Törölt ? "Törölt" : "Aktív";
                    Tábla.Rows[i].Cells[10].Value = rekord.Jelentés;
                    Tábla.Rows[i].Cells[11].Value = rekord.Tétel;
                    Tábla.Rows[i].Cells[12].Value = cmbtelephely1.Text.Trim();
                }
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

        private void BtnVonal_Click(object sender, EventArgs e)
        {
            try
            {
                Táblatörlése();
                if (cmbtelephely1.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve telephely.");

                List<Adat_Menetkimaradás> Adatok = KézMenet.Lista_Adatok(cmbtelephely1.Text.Trim(), DátumTól.Value.Year);

                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(DátumTól.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(DátumTól.Value);

                Adatok = (from z in Adatok
                          where z.Bekövetkezés >= MyF.Nap0000(hónapelsőnapja)
                          && z.Bekövetkezés <= MyF.Nap2359(hónaputolsónapja)
                          && z.Viszonylat != "_"
                          && z.Eseményjele != "_"
                          && z.Törölt == false
                          orderby z.Viszonylat, z.Típus, z.Eseményjele, z.Bekövetkezés
                          select z).ToList();
                if (Adatok.Count < 1) throw new HibásBevittAdat("Nincs a feltételeknek megfelelő adat.");

                // kiírjuk az adatokat a táblába
                Tábla.Visible = false;

                Tábla.ColumnCount = 1;
                Tábla.RowCount = 34;

                Tábla.Columns[0].HeaderText = "Nap";
                Tábla.Columns[0].Width = 45;

                //sorok kiírása
                for (int i = 1; i <= 31; i++)
                    Tábla.Rows[1 + i].Cells[0].Value = i;
                Tábla.Rows[33].Cells[0].Value = "Össz.";

                string előzőviszonylat = "";
                string előzőtípus = "";
                string[] szöveg1 = { "A db", "A menet", "B db", "B menet", "C db" };
                long a = 0;
                long b = 0;
                long c = 0;
                long am = 0;
                long bm = 0;
                int sor;
                int oszlop = 1;


                foreach (Adat_Menetkimaradás rekord in Adatok)
                {
                    // fejléc készítés
                    if (előzőviszonylat.Trim() != rekord.Viszonylat.Trim())
                    {
                        if (Tábla.ColumnCount > 5)
                        {
                            // kiirjuk az összesítést
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 5].Value = a;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 4].Value = am;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 3].Value = b;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 2].Value = bm;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 1].Value = c;
                        }
                        előzőviszonylat = rekord.Viszonylat.Trim();
                        előzőtípus = rekord.Típus.Trim();
                        a = 0;
                        b = 0;
                        c = 0;
                        am = 0;
                        bm = 0;
                        if (Tábla.ColumnCount == 1)
                            oszlop = 1;
                        else
                            oszlop = Tábla.ColumnCount;

                        Tábla.ColumnCount += 5;

                        for (int i = 0; i < 5; i++)
                        {
                            Tábla.Columns[oszlop + i].HeaderText = rekord.Viszonylat.Trim();
                            Tábla.Rows[0].Cells[oszlop + i].Value = rekord.Típus.Trim();
                            Tábla.Rows[1].Cells[oszlop + i].Value = szöveg1[i].Trim();
                        }

                    }

                    if (előzőtípus.Trim() != rekord.Típus.Trim())
                    {
                        előzőtípus = rekord.Típus.Trim();
                        if (Tábla.ColumnCount > 5)
                        {
                            // kiirjuk az összesítést
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 5].Value = a;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 4].Value = am;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 3].Value = b;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 2].Value = bm;
                            Tábla.Rows[33].Cells[Tábla.ColumnCount - 1].Value = c;
                        }
                        a = 0;
                        b = 0;
                        c = 0;
                        am = 0;
                        bm = 0;
                        if (Tábla.ColumnCount == 1)
                            oszlop = 1;
                        else
                            oszlop = Tábla.ColumnCount;

                        Tábla.ColumnCount += 5;

                        for (int i = 0; i < 5; i++)
                        {
                            Tábla.Columns[oszlop + i].HeaderText = rekord.Viszonylat.Trim();
                            Tábla.Rows[0].Cells[oszlop + i].Value = rekord.Típus.Trim();
                            Tábla.Rows[1].Cells[oszlop + i].Value = szöveg1[i].Trim();
                        }
                        oszlop = Tábla.ColumnCount - 1;
                    }
                    // Adatokat kiírjuk
                    switch (rekord.Eseményjele.ToUpper())
                    {
                        case "A":
                            {
                                a++;
                                am += rekord.Kimaradtmenet;
                                oszlop = Tábla.ColumnCount - 5;
                                sor = rekord.Bekövetkezés.Day + 1;
                                if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToStrTrim() == "")
                                    Tábla.Rows[sor].Cells[oszlop].Value = 1;
                                else
                                    Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + 1;

                                oszlop = Tábla.ColumnCount - 4;
                                if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToStrTrim() == "")
                                    Tábla.Rows[sor].Cells[oszlop].Value = 0;
                                Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + rekord.Kimaradtmenet;
                                break;
                            }

                        case "B":
                            {
                                b++;
                                bm += rekord.Kimaradtmenet;
                                oszlop = Tábla.ColumnCount - 3;
                                sor = rekord.Bekövetkezés.Day + 1;
                                if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToStrTrim() == "")
                                    Tábla.Rows[sor].Cells[oszlop].Value = 1;
                                else
                                    Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + 1;

                                oszlop = Tábla.ColumnCount - 2;
                                if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToStrTrim() == "")
                                    Tábla.Rows[sor].Cells[oszlop].Value = 0;
                                Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + rekord.Kimaradtmenet;
                                break;
                            }

                        case "C":
                            {
                                c++;
                                oszlop = Tábla.ColumnCount - 1;
                                sor = rekord.Bekövetkezés.Day + 1;
                                if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToStrTrim() == "")
                                    Tábla.Rows[sor].Cells[oszlop].Value = 1;
                                else
                                    Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + 1;
                                break;
                            }
                    }
                }

                // kiirjuk az összesítést
                Tábla.Rows[33].Cells[Tábla.ColumnCount - 5].Value = a;
                Tábla.Rows[33].Cells[Tábla.ColumnCount - 4].Value = am;
                Tábla.Rows[33].Cells[Tábla.ColumnCount - 3].Value = b;
                Tábla.Rows[33].Cells[Tábla.ColumnCount - 2].Value = bm;
                Tábla.Rows[33].Cells[Tábla.ColumnCount - 1].Value = c;

                Tábla.Visible = true;

                Tábla.Columns[0].Width = 50;
                for (int i = 1; i < Tábla.ColumnCount; i++)
                    Tábla.Columns[i].Width = 75;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHavi_Click(object sender, EventArgs e)
        {
            try
            {

                Táblatörlése();
                if (cmbtelephely1.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva telephely.");
                long dbö = 0;
                long menetö = 0;
                long aö = 0;
                long bö = 0;
                long cö = 0;
                long amö = 0;
                long bmö = 0;
                long a = 0;
                long b = 0;
                long c = 0;
                long am = 0;
                long bm = 0;
                string előzőtípus = "";
                int oszlop;
                int sor;

                List<Adat_Menetkimaradás> Adatok = KézMenet.Lista_Adatok(cmbtelephely1.Text.Trim(), DátumTól.Value.Year);

                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(DátumTól.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(DátumTól.Value);

                Adatok = (from z in Adatok
                          where z.Bekövetkezés >= MyF.Nap0000(hónapelsőnapja)
                          && z.Bekövetkezés <= MyF.Nap2359(hónaputolsónapja)
                          && z.Viszonylat != "_"
                          && z.Eseményjele != "_"
                          && z.Törölt == false
                          orderby z.Viszonylat, z.Típus, z.Eseményjele, z.Bekövetkezés
                          select z).ToList();

                Tábla.Visible = false;

                Tábla.ColumnCount = 1;
                Tábla.RowCount = 6;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Jel";

                string[] szövegt = { "A", "B", "C" };
                for (int i = 0; i < 3; i++)
                    Tábla.Rows[i + 2].Cells[0].Value = szövegt[i];
                Tábla.Rows[5].Cells[0].Value = "Össz.";

                foreach (Adat_Menetkimaradás rekord in Adatok)
                {
                    // fejléc készítés
                    if (előzőtípus.Trim() != rekord.Típus.Trim() && előzőtípus != "")
                    {
                        Tábla.ColumnCount += 2;
                        // ha másik típus akkor kiírja az adatokat
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 0;
                        Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 0;
                        Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 1;
                        Tábla.Rows[sor].Cells[oszlop].Value = "darab";
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 1;
                        Tábla.Rows[sor].Cells[oszlop].Value = "menet";
                        aö += a;
                        amö += am;
                        bö += b;
                        bmö += bm;
                        cö += c;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 2;
                        Tábla.Rows[sor].Cells[oszlop].Value = a;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 2;
                        Tábla.Rows[sor].Cells[oszlop].Value = am;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 3;
                        Tábla.Rows[sor].Cells[oszlop].Value = b;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 3;
                        Tábla.Rows[sor].Cells[oszlop].Value = bm;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 4;
                        Tábla.Rows[sor].Cells[oszlop].Value = c;
                        dbö = dbö + a + b + c;
                        menetö = menetö + am + bm;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 5;
                        Tábla.Rows[sor].Cells[oszlop].Value = a + b + c;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 5;
                        Tábla.Rows[sor].Cells[oszlop].Value = am + bm;

                        a = 0;
                        b = 0;
                        c = 0;
                        am = 0;
                        bm = 0;

                    }
                    előzőtípus = rekord.Típus.Trim();
                    // Adatokat kiírjuk
                    switch (rekord.Eseményjele.Trim())
                    {
                        case "A":
                            {
                                a++;
                                am += rekord.Kimaradtmenet;
                                break;
                            }
                        case "B":
                            {
                                b++;
                                bm += rekord.Kimaradtmenet;
                                break;
                            }
                        case "C":
                            {
                                c++;
                                break;
                            }
                    }

                }


                Tábla.ColumnCount += 2;
                oszlop = Tábla.ColumnCount - 2;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                oszlop = Tábla.ColumnCount - 1;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                oszlop = Tábla.ColumnCount - 2;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "darab";
                oszlop = Tábla.ColumnCount - 1;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "menet";
                oszlop = Tábla.ColumnCount - 2;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = a;
                oszlop = Tábla.ColumnCount - 1;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = am;
                oszlop = Tábla.ColumnCount - 2;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = b;
                oszlop = Tábla.ColumnCount - 1;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = bm;
                oszlop = Tábla.ColumnCount - 2;
                sor = 4;
                Tábla.Rows[sor].Cells[oszlop].Value = c;
                aö += a;
                amö += am;
                bö += b;
                bmö += bm;
                cö += c;
                dbö = dbö + a + b + c;
                menetö = menetö + am + bm;
                oszlop = Tábla.ColumnCount - 2;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = a + b + c;
                oszlop = Tábla.ColumnCount - 1;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = am + bm;
                Tábla.ColumnCount += 2;
                oszlop = Tábla.ColumnCount - 2;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = "Összesen:";
                oszlop = Tábla.ColumnCount - 1;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = "Összesen:";
                oszlop = Tábla.ColumnCount - 2;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "darab";
                oszlop = Tábla.ColumnCount - 1;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "menet";
                oszlop = Tábla.ColumnCount - 2;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = aö;
                oszlop = Tábla.ColumnCount - 1;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = amö;
                oszlop = Tábla.ColumnCount - 2;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = bö;
                oszlop = Tábla.ColumnCount - 1;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = bmö;
                oszlop = Tábla.ColumnCount - 2;
                sor = 4;
                Tábla.Rows[sor].Cells[oszlop].Value = cö;
                oszlop = Tábla.ColumnCount - 2;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = dbö;
                oszlop = Tábla.ColumnCount - 1;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = menetö;
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


        /// <summary>
        /// Dátumot nem engedjük úgy beállítani, hogy keresztezze egymást.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DátumIg_ValueChanged(object sender, EventArgs e)
        {
            if (DátumTól.Value > DátumIg.Value) DátumTól.Value = DátumIg.Value;
        }

        private void DátumTól_ValueChanged(object sender, EventArgs e)
        {
            if (DátumTól.Value > DátumIg.Value) DátumIg.Value = DátumTól.Value;
        }
        #endregion



        #region MindenEgyéb
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // melyik sorra kattintottunk
                if (e.RowIndex < 0) return;
                idszám_ = 0;
                TelepHely = "";
                if (Tábla.Columns[0].HeaderCell.Value.ToString() == "Srsz")
                {
                    idszám_ = int.Parse(Tábla.Rows[e.RowIndex].Cells[0].Value.ToString());
                    TelepHely = Tábla.Rows[e.RowIndex].Cells[12].Value.ToString();

                }
                if (Tábla.Columns[0].HeaderCell.Value.ToString() == "Telephely")
                {
                    idszám_ = int.Parse(Tábla.Rows[e.RowIndex].Cells[9].Value.ToString());
                    TelepHely = Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
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


        #region Oldal Panelen Lévő   
        /// <summary>
        /// Szolgálatok listájának kiválasztása, és az üzemek kijelölése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lstszolgálatok_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // kitöröljük az üzemek jelölését
                for (int i = 0; i < Lstüzemek.Items.Count; i++)
                    Lstüzemek.SetItemChecked(i, false);

                List<Adat_Kiegészítő_Szolgálattelepei> Adatok = KézSzolgTelep.Lista_Adatok();
                for (int i = 0; i < Lstszolgálatok.CheckedItems.Count; i++)
                {
                    List<Adat_Kiegészítő_Szolgálattelepei> EgySzolg = Adatok.Where(a => a.Szolgálatnév.Trim() == Lstszolgálatok.CheckedItems[i].ToStrTrim()).ToList();
                    for (int j = 0; j < Lstüzemek.Items.Count; j++)
                        if (EgySzolg.Any(a => a.Telephelynév == Lstüzemek.Items[j].ToStrTrim())) Lstüzemek.SetItemChecked(j, true);
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

        private void BtnNapilista_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lstüzemek.CheckedItems.Count == 0) throw new HibásBevittAdat("Nincs kijelölve egy üzem sem.");
                NapiOldalTábla();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapiOldalTábla()
        {
            try
            {
                Táblatörlése();
                Tábla.ColumnCount = 10;
                Tábla.RowCount = 0;
                Tábla.Visible = false;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Telephely";
                Tábla.Columns[0].Width = 110;
                Tábla.Columns[1].HeaderText = "ABC";
                Tábla.Columns[1].Width = 45;
                Tábla.Columns[2].HeaderText = "Visz.";
                Tábla.Columns[2].Width = 45;
                Tábla.Columns[3].HeaderText = "Típus";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Psz";
                Tábla.Columns[4].Width = 60;
                Tábla.Columns[5].HeaderText = "Járművezetői beírás";
                Tábla.Columns[5].Width = 250;
                Tábla.Columns[6].HeaderText = "Javítás";
                Tábla.Columns[6].Width = 250;
                Tábla.Columns[7].HeaderText = "Menet";
                Tábla.Columns[7].Width = 60;
                Tábla.Columns[8].HeaderText = "Bekövetkezés";
                Tábla.Columns[8].Width = 180;
                Tábla.Columns[9].HeaderText = "ID";
                Tábla.Columns[9].Width = 80;

                for (int j = 0; j < Lstüzemek.CheckedItems.Count; j++)
                {
                    List<Adat_Menetkimaradás> Adatok = KézMenet.Lista_Adatok(Lstüzemek.CheckedItems[j].ToStrTrim(), Dátum.Value.Year);
                    Adatok = (from a in Adatok
                              where a.Bekövetkezés >= MyF.Nap0000(Dátum.Value)
                              && a.Bekövetkezés <= MyF.Nap2359(Dátum.Value)
                              orderby a.Eseményjele, a.Típus
                              select a).ToList();
                    if (CheckBox1.Checked) Adatok = Adatok.Where(a => a.Eseményjele != "_").ToList();

                    foreach (Adat_Menetkimaradás rekord in Adatok)
                    {
                        Tábla.RowCount++;
                        int i = Tábla.RowCount - 1;
                        Tábla.Rows[i].Cells[0].Value = Lstüzemek.CheckedItems[j];
                        Tábla.Rows[i].Cells[1].Value = rekord.Eseményjele;
                        Tábla.Rows[i].Cells[2].Value = rekord.Viszonylat;
                        Tábla.Rows[i].Cells[3].Value = rekord.Típus;
                        Tábla.Rows[i].Cells[4].Value = rekord.Azonosító;
                        Tábla.Rows[i].Cells[5].Value = rekord.Jvbeírás;
                        Tábla.Rows[i].Cells[6].Value = rekord.Javítás;
                        Tábla.Rows[i].Cells[7].Value = rekord.Kimaradtmenet;
                        Tábla.Rows[i].Cells[8].Value = rekord.Bekövetkezés;
                        Tábla.Rows[i].Cells[9].Value = rekord.Id;
                    }
                }
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

        /// <summary>
        /// Listázza a kiválasztott üzemek adatait a kijelölt napon, ha van kiválasztva üzem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            if (Lstüzemek.CheckedItems.Count != 0) NapiOldalTábla();
        }
        #endregion


        #region Főmérnökségi panel
        Ablak_Menetkimaradás_Kiegészítő Új_Ablak_Menetkimaradás_Kiegészítő = null;
        private void BtnFőmérnükség_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Menetkimaradás_Kiegészítő == null)
            {
                Új_Ablak_Menetkimaradás_Kiegészítő = new Ablak_Menetkimaradás_Kiegészítő();
                Új_Ablak_Menetkimaradás_Kiegészítő.FormClosed += Ablak_Menetkimaradás_Kiegészítő_FormClosed;

                Új_Ablak_Menetkimaradás_Kiegészítő.Show();
            }
            else
            {
                Új_Ablak_Menetkimaradás_Kiegészítő.Activate();
                Új_Ablak_Menetkimaradás_Kiegészítő.WindowState = FormWindowState.Normal;
            }
        }

        private void Ablak_Menetkimaradás_Kiegészítő_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Menetkimaradás_Kiegészítő = null;
        }
        #endregion


        #region Részletes Adatok
        Ablak_Menetrögítés Új_Ablak_Menetrögítés = null;
        private void BtnRészletes_Click(object sender, EventArgs e)
        {
            if (TelepHely.Trim() == "") return;
            AdatRészletes();
        }

        private void AdatRészletes()
        {
            try
            {
                List<Adat_Menetkimaradás> Adatok = new List<Adat_Menetkimaradás>();
                Adatok = KézMenet.Lista_Adatok(TelepHely, Dátum.Value.Year);

                Adat_Menetkimaradás ADAT = Adatok.Where(a => a.Id == idszám_).FirstOrDefault();

                if (ADAT != null)
                {
                    Új_Ablak_Menetrögítés?.Close();
                    Új_Ablak_Menetrögítés = new Ablak_Menetrögítés(ADAT);
                    Új_Ablak_Menetrögítés.FormClosed += Ablak_Menetrögítés_FormClosed;
                    Új_Ablak_Menetrögítés.Show();
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

        private void Ablak_Menetrögítés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Menetrögítés = null;
        }


        #endregion


    }
}