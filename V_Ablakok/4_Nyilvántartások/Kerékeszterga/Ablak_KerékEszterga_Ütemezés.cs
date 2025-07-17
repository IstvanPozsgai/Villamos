using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.Kerékeszterga;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyF = Függvénygyűjtemény;
using MyO = Microsoft.Office.Interop.Outlook;


namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_KerékEszterga_Ütemezés : Form
    {
        //public event Event_Kidobó Változás;
        DateTime DátumÉsIdő = DateTime.Today;
        List<Adat_Jármű> Honos = null;
        readonly Kezelő_Dolgozó_Beosztás_Új Kezelő_Beoszt_Új = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Kerék_Eszterga_Naptár KézNaptár = new Kezelő_Kerék_Eszterga_Naptár();

        List<Adat_Dolgozó_Beosztás_Új> Adatok_Beoszt_Új = new List<Adat_Dolgozó_Beosztás_Új>();

        public Ablak_KerékEszterga_Ütemezés()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_KerékEszterga_Ütemezés_Load(object sender, EventArgs e)
        {
            Automata_Jelentés();
        }

        private void Ablak_KerékEszterga_Ütemezés_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void Ablak_KerékEszterga_Ütemezés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Dolgozók?.Close();
            Új_Ablak_Eszterga_Választék?.Close();
            Új_Ablak_Eszterga_Segéd?.Close();
            Új_Ablak_Eszterga_Terjesztés?.Close();
            Új_Ablak_Eszterga_Beosztás?.Close();
        }


        void Start()
        {
            Dátum.Value = DateTime.Today;
            Telephelyekfeltöltése();

            GombLathatosagKezelo.Beallit(this);
            Jogosultságkiosztás();
            Fülekkitöltése();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerékeszterga";
            if (!Directory.Exists(hely))
                Directory.CreateDirectory(hely);

            hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Kerék_Törzs(hely);

            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Dátum.Value.Year}_Esztergálás.mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Kerék_Éves(hely);

            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.Year}_Igény.mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Kerék_Igény(hely);
            Telephelyek_Szűrő_feltöltése();


        }



        #region Alap
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Baross_Eszterga.html";
            Module_Excel.Megnyitás(hely);
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);

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

        private void Telephelyek_Szűrő_feltöltése()
        {
            try
            {
                Kezelő_kiegészítő_telephely KézTelep = new Kezelő_kiegészítő_telephely();
                List<Adat_kiegészítő_telephely> Adatok = KézTelep.Lista_Adatok().OrderBy(a => a.Telephelynév).ToList();

                Telephely.Items.Clear();
                Telephely.Items.Add("");
                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    Telephely.Items.Add(Elem.Telephelynév);

                Telephely.Refresh();
            }
            catch (HibásBevittAdat ex)
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
            Elkészült.Enabled = false;
            Visszaállítás.Enabled = false;
            Törölt.Enabled = false;
            BeosztásAdatok.Enabled = false;
            Esztergályosok.Enabled = false;
            Választék_Lista.Enabled = false;
            Terjesztési.Enabled = false;
            Heti_terv_küldés.Enabled = false;
            Heti_jelentés.Enabled = false;


            if (Program.PostásTelephely.Trim() == "Főmérnökség")
            {
                Elkészült.Visible = true;
                Visszaállítás.Visible = true;
                Terjesztési.Visible = true;

                BeosztásAdatok.Visible = true;
                Esztergályosok.Visible = true;
                Választék_Lista.Visible = true;
                Heti_terv_küldés.Visible = true;
                Heti_jelentés.Visible = true;
            }
            else
            {
                Elkészült.Visible = false;
                Visszaállítás.Visible = false;
                Terjesztési.Visible = false;

                BeosztásAdatok.Visible = false;
                Esztergályosok.Visible = false;
                Választék_Lista.Visible = false;
                Heti_terv_küldés.Visible = false;
                Heti_jelentés.Visible = false;
            }

            if (Program.PostásTelephely.Trim() == "Baross")
            {
                Elkészült.Visible = true;
                Visszaállítás.Visible = true;
                BeosztásAdatok.Visible = true;
                Esztergályosok.Visible = true;
                Terjesztési.Visible = true;
                Heti_terv_küldés.Visible = true;
                Heti_jelentés.Visible = true;
            }

            melyikelem = 165;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Elkészült.Enabled = true;
                Visszaállítás.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Törölt.Enabled = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BeosztásAdatok.Enabled = true;
                Esztergályosok.Enabled = true;
            }


            melyikelem = 166;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Választék_Lista.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Terjesztési.Enabled = true;
                Heti_terv_küldés.Enabled = true;
                Heti_jelentés.Enabled = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }


        private void Fülekkitöltése()
        {

            switch (Fülek.SelectedIndex)
            {
                case 1:
                    {
                        Igény_Típus_Feltöltés();
                        Státus_Feltöltés();
                        break;
                    }
                case 0:
                    {

                        break;
                    }
                case 2:
                    {
                        // beállítások

                        break;
                    }

            }
        }

        private void Státus_Feltöltés()
        {
            Igény_Státus.Items.Clear();
            Igény_Státus.Items.Add("");
            Igény_Státus.Items.Add("0 - Igény");
            Igény_Státus.Items.Add("2 - Ütemezett");
            Igény_Státus.Items.Add("7 - Elkészült");
            Igény_Státus.Items.Add("9 - Törölt");
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
            StringFormat sf = new StringFormat()
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
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }


        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }
        #endregion


        #region Igény


        void Igény_Típus_Feltöltés()
        {
            try
            {
                Igény_Típus.Items.Clear();
                Igény_Típus.Items.Add("");
                for (int ii = -1; ii < 1; ii++)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.AddYears(ii).Year}_Igény.mdb";
                    if (File.Exists(hely))
                    {
                        string jelszó = "RónaiSándor";
                        string szöveg = $"SELECT DISTINCT típus FROM Igény WHERE státus<8  ORDER BY  típus";
                        Kezelő_Általános_String kéz = new Kezelő_Általános_String();
                        List<string> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg, "típus");

                        foreach (string rekord in Adatok)
                        {
                            if (!Igény_Típus.Items.Contains(rekord.Trim()))
                                Igény_Típus.Items.Add(rekord);
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


        private void Lista_Tábla_Click(object sender, EventArgs e)
        {
            Lista_Tábla_kiírás();
        }

        void Lista_Tábla_kiírás()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 9;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Prioritás";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Igénylés ideje";
                Tábla.Columns[1].Width = 180;
                Tábla.Columns[2].HeaderText = "Pályaszám";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Telephely";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Típus";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Státus";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Megjegyzés";
                Tábla.Columns[6].Width = 400;
                Tábla.Columns[7].HeaderText = "Ütemezés dátuma";
                Tábla.Columns[7].Width = 120;
                Tábla.Columns[8].HeaderText = "Norma idő";
                Tábla.Columns[8].Width = 120;

                for (int ii = -1; ii < 1; ii++)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.AddYears(ii).Year}_Igény.mdb";
                    string jelszó = "RónaiSándor";
                    string szöveg;
                    if (File.Exists(hely))
                    {
                        if (Igény_Státus.Text.Trim() != "")
                        {
                            szöveg = "SELECT * FROM Igény WHERE ";
                            string[] darabol = Igény_Státus.Text.Trim().Split('-');
                            switch (darabol[0].Trim())
                            {
                                case "0":
                                    szöveg += " státus=0 ";
                                    break;
                                case "2":
                                    szöveg += " státus=2 ";
                                    break;
                                case "7":
                                    szöveg += " státus=7 ";
                                    break;
                                case "9":
                                    szöveg += " státus=9 ";
                                    break;
                            }
                        }
                        else
                            szöveg = "SELECT * FROM Igény WHERE státus<7 ";
                        if (Telephely.Text.Trim() != "")
                            szöveg += $" AND telephely='{Telephely.Text.Trim()}' ";


                        if (Igény_Típus.Text.Trim() == "")
                            szöveg += " ORDER BY Prioritás desc, Rögzítés_dátum ";
                        else
                            szöveg += $" AND típus='{Igény_Típus.Text.Trim()}' ORDER BY Prioritás desc, Rögzítés_dátum ";


                        Kezelő_Kerék_Eszterga_Igény kéz = new Kezelő_Kerék_Eszterga_Igény();
                        List<Adat_Kerék_Eszterga_Igény> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                        int i;

                        foreach (Adat_Kerék_Eszterga_Igény rekord in Adatok)
                        {
                            Tábla.RowCount++;
                            i = Tábla.RowCount - 1;
                            Tábla.Rows[i].Cells[0].Value = rekord.Prioritás;
                            Tábla.Rows[i].Cells[1].Value = rekord.Rögzítés_dátum.ToString();
                            Tábla.Rows[i].Cells[2].Value = rekord.Pályaszám.Trim();
                            Tábla.Rows[i].Cells[3].Value = rekord.Telephely.Trim();
                            Tábla.Rows[i].Cells[4].Value = rekord.Típus.Trim();
                            switch (rekord.Státus)
                            {
                                case 0:
                                    Tábla.Rows[i].Cells[5].Value = "Igény";
                                    break;
                                case 2:
                                    Tábla.Rows[i].Cells[5].Value = "Ütemezett";
                                    break;
                                case 7:
                                    Tábla.Rows[i].Cells[5].Value = "Elkészült";
                                    break;
                                case 9:
                                    Tábla.Rows[i].Cells[5].Value = "Törölt";
                                    break;
                            }
                            Tábla.Rows[i].Cells[6].Value = rekord.Megjegyzés.Trim();
                            Tábla.Rows[i].Cells[7].Value = rekord.Ütemezés_dátum.ToString("yyyy.MM.dd") == "1900.01.01" ? "" : rekord.Ütemezés_dátum.ToString("yyyy.MM.dd");
                            Tábla.Rows[i].Cells[8].Value = rekord.Norma;
                        }
                    }
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

        #endregion


        #region Munkaidő 
        void Beosztás_Adatok()
        {
            try
            {
                HétAlapAdatai();
                Beosztások_Esztergához();
                Munkaidő_Töröl();
                Munkaidő_Átír_vez();
                Terv_lista_elj();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HétAlapAdatai()
        {
            try
            {
                Holtart.Be(20);

                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value).AddHours(23).AddMinutes(30);
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Hételső.Year}_Esztergálás.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"SELECT * FROM naptár WHERE idő>=#{Hételső:MM-dd-yyyy HH:mm}# AND idő<=#{Hétutolsó:MM-dd-yyyy HH:mm}#";


                List<DateTime> Adatok = KézNaptár.Lista_Adatok_Idő(hely, jelszó, szöveg);

                DateTime FutóIdő = new DateTime(Hételső.Year, Hételső.Month, Hételső.Day, 0, 0, 0);
                DateTime VégeIdő = FutóIdő.AddDays(7);

                List<string> szövegGy = new List<string>();
                while (FutóIdő < VégeIdő)
                {
                    szöveg = "INSERT INTO Naptár (idő, munkaidő, foglalt, pályaszám, megjegyzés, betűszín, háttérszín, marad) VALUES (";
                    szöveg += $"'{FutóIdő}', false, false, '_', '',0 , 0, false )";
                    if (Adatok == null)
                        szövegGy.Add(szöveg);        //Ha nincs egy adat sem
                    else
                        if (!Adatok.Contains(FutóIdő)) szövegGy.Add(szöveg);    //Ha nincs még akkor létrehozza

                    FutóIdő = FutóIdő.AddMinutes(30);

                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);

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

        private void BeosztásAdatok_Click(object sender, EventArgs e)
        {
            Beosztás_Adatok();
        }

        /// <summary>
        /// Beosztásból kiszűri azon dolgozókat akik esztergálnak és csak az Ő adatai jelennek meg
        /// Így 2-3 fő adatai külön kezelhetők
        /// </summary>
        private void Beosztások_Esztergához()
        {
            try
            {
                //Betöltjük a dolgozókat akik beosztását nézzük
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
                if (!File.Exists(hely)) return;

                string szöveg = $"SELECT * FROM Esztergályos  ORDER BY dolgozószám ";

                Kezelő_Kerék_Eszterga_Esztergályos kézE = new Kezelő_Kerék_Eszterga_Esztergályos();
                List<Adat_Kerék_Eszterga_Esztergályos> AdatokE = kézE.Lista_Adatok();

                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);


                Holtart.Be(20);

                foreach (Adat_Kerék_Eszterga_Esztergályos rekord in AdatokE)
                {
                    Eszt_Beosztás_Törlés(rekord.Telephely.Trim(), Hételső, Hétutolsó);
                    Holtart.Lép();
                }


                // Új beosztás táblába áttöltjük, majd törölni kell az új beosztás esetén
                foreach (Adat_Kerék_Eszterga_Esztergályos rekord in AdatokE)
                {
                    //Eredeti csoport azon részé akik főállásból végzik
                    if (rekord.Státus == 1)
                        Eszt_Új_Beosztás(rekord.Telephely.Trim(), rekord.Dolgozószám.Trim(), Hételső, Hétutolsó);

                    //Telephelyen lévő, de besegítő
                    if (rekord.Státus == 2)

                        Eszt_Új_Beosztás_besegítő(rekord.Telephely.Trim(), rekord.Dolgozószám.Trim(), Hételső, Hétutolsó);

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

        private void Munkaidő_Töröl()
        {
            try
            {
                Holtart.Be(20);


                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Dátum.Value.Year}_Esztergálás.mdb";
                string jelszó = "RónaiSándor";
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);

                string szöveg = $"SELECT * FROM naptár";

                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(hely, jelszó, szöveg);

                bool vane = Adatok.Any(n =>
                n.Idő >= Hételső.Date &&
                n.Idő <= Hétutolsó.Date.AddDays(1).AddTicks(-1) &&
                n.Munkaidő == true);

                if (vane)
                {
                    szöveg = $"UPDATE naptár SET munkaidő=false WHERE [idő]>=# {Hételső:MM-dd-yyyy} 00:00:0#";
                    szöveg += $" and [idő]<=#{Hétutolsó:MM-dd-yyyy} 23:59:0# AND munkaidő=true";
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

        private void Munkaidő_Átír_vez()
        {
            try
            {
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);
                if (Hételső.Month != Hétutolsó.Month) Munkaidő_Átír(Hétutolsó);

                Munkaidő_Átír(Hételső);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Munkaidő_Átír(DateTime NapTÁR)
        {
            try
            {
                Holtart.Be(20);
                //Beosztás adatok betöltése
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM beosztáskódok WHERE számoló=true  ORDER BY beosztáskód";

                Kezelő_Kiegészítő_Beosztáskódok KézB = new Kezelő_Kiegészítő_Beosztáskódok();
                List<Adat_Kiegészítő_Beosztáskódok> AdatB = KézB.Lista_Adatok(hely, jelszó, szöveg);

                DateTime Hételső = MyF.Hét_elsőnapja(NapTÁR);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(NapTÁR);


                Kezelő_Dolgozó_Beosztás_Új KézBEO = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> AdatBEO;

                foreach (Adat_Kiegészítő_Beosztáskódok rekordBKód in AdatB)
                {
                    string helydolg = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Beosztás\{NapTÁR.Year}\EsztBeosztás{NapTÁR:yyyyMM}.mdb";
                    if (!File.Exists(helydolg)) Adatbázis_Létrehozás.Dolgozói_Beosztás_Adatok_Új(helydolg);
                    string jelszódolg = "kiskakas";
                    szöveg = $"SELECT * FROM beosztás where [nap]>=# {Hételső:MM-dd-yyyy} 00:00:0#";
                    szöveg += $" and [nap]<=#{Hétutolsó:MM-dd-yyyy} 23:59:0# AND (beosztáskód='{rekordBKód.Beosztáskód.Trim()}' OR beosztáskód='#' )";
                    szöveg += " ORDER BY nap";
                    AdatBEO = KézBEO.Lista_Adatok(helydolg, jelszódolg, szöveg);

                    hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Hételső.Year}_Esztergálás.mdb";
                    jelszó = "RónaiSándor";
                    List<string> szövegGy = new List<string>();

                    foreach (Adat_Dolgozó_Beosztás_Új rekord in AdatBEO)
                    {
                        if (rekord.Beosztáskód.Trim() != "#")
                        {
                            DateTime Munka_eleje = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, rekordBKód.Munkaidőkezdet.Hour, rekordBKód.Munkaidőkezdet.Minute, 0);
                            DateTime Munka_vége;
                            if (rekord.Túlóraok.Contains("&T"))
                                Munka_vége = Munka_eleje.AddMinutes(rekord.Túlóra + rekord.Ledolgozott);//Túlóra
                            else
                                Munka_vége = Munka_eleje.AddMinutes(rekord.Ledolgozott);//Elvont pihenő
                            szöveg = $"UPDATE naptár SET munkaidő=true WHERE [idő]>=#{Munka_eleje:MM-dd-yyyy H:m:s}#";
                            szöveg += $" and [idő]<#{Munka_vége:MM-dd-yyyy H:m:s}#";
                            szövegGy.Add(szöveg);
                        }
                        else
                        {
                            if (!rekord.Megjegyzés.Contains("#"))
                            {
                                DateTime Munka_eleje = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, rekord.Túlórakezd.Hour, rekord.Túlórakezd.Minute, 0);
                                DateTime Munka_vége = Munka_eleje.AddMinutes(rekord.Túlóra);
                                szöveg = $"UPDATE naptár SET munkaidő=true WHERE [idő]>=#{Munka_eleje:MM-dd-yyyy H:m:s}#";
                                szöveg += $" and [idő]<#{Munka_vége:MM-dd-yyyy H:m:s}#";
                                szövegGy.Add(szöveg);
                            }
                            else
                            {
                                string[] darabol = rekord.Megjegyzés.Trim().Split('-');
                                DateTime kezdet;
                                int munkaidő;
                                if (darabol.Length != 3)
                                {
                                    kezdet = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, 6, 0, 0);
                                    munkaidő = 30;
                                }

                                if (!DateTime.TryParse(darabol[1], out kezdet))
                                    kezdet = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, 6, 0, 0);
                                int óra = kezdet.Hour;
                                int perc = kezdet.Minute;
                                if (!int.TryParse(darabol[2], out munkaidő))
                                    munkaidő = 30;

                                DateTime Munka_eleje = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, óra, perc, 0);
                                DateTime Munka_vége = Munka_eleje.AddMinutes(munkaidő);

                                szöveg = $"UPDATE naptár SET munkaidő=true WHERE [idő]>=#{Munka_eleje:MM-dd-yyyy H:m:s}#";
                                szöveg += $" and [idő]<#{Munka_vége:MM-dd-yyyy H:m:s}#";
                                szövegGy.Add(szöveg);
                            }
                        }
                        Holtart.Lép();
                    }
                    MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        private void Eszt_Új_Beosztás(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                string helyold = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\Ebeosztás{Dátumtól:yyyyMM}.mdb";
                if (File.Exists(helyold))
                {
                    //Az új beosztásból vesszük az adatokat
                    if (Dátumtól.Year != Dátumig.Year)
                    {
                        //évváltás
                        DateTime Éveleje = MyF.Hónap_elsőnapja(Dátumig);
                        Új_Beosztás_hónap(Telephely, dolgozószám, Éveleje, Dátumig);
                    }
                    else
                    {
                        if (Dátumtól.Month != Dátumig.Month)
                        {
                            //hónapváltás
                            DateTime Hónapeleje = MyF.Hónap_elsőnapja(Dátumig);
                            Új_Beosztás_hónap(Telephely, dolgozószám, Hónapeleje, Dátumig);
                        }
                    }
                    Új_Beosztás_hónap(Telephely, dolgozószám, Dátumtól, Dátumig);
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

        private void Eszt_Új_Beosztás_besegítő(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                //Az új beosztásból vesszük az adatokat
                if (Dátumtól.Year != Dátumig.Year)
                {
                    //évváltás
                    DateTime Éveleje = MyF.Hónap_elsőnapja(Dátumig);
                    Új_Beosztás_hónap_besegítő(Telephely, dolgozószám, Éveleje, Dátumig);
                }
                else
                {
                    if (Dátumtól.Month != Dátumig.Month)
                    {
                        //hónapváltás
                        DateTime Hónapeleje = MyF.Hónap_elsőnapja(Dátumig);
                        Új_Beosztás_hónap_besegítő(Telephely, dolgozószám, Hónapeleje, Dátumig);
                    }
                }
                Új_Beosztás_hónap_besegítő(Telephely, dolgozószám, Dátumtól, Dátumig);
            }
            catch (HibásBevittAdat ex)
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
        /// Ez a változat az új adatbázisból emeli át az adatokat.
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="dolgozószám"></param>
        /// <param name="Dátumtól"></param>
        /// <param name="Dátumig"></param>
        private void Új_Beosztás_hónap_besegítő(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                string helyold = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\EBeosztás{Dátumtól:yyyyMM}.mdb";
                if (!File.Exists(helyold)) return;

                string helynew = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\EsztBeosztás{Dátumtól:yyyyMM}.mdb";
                if (!File.Exists(helynew))
                {
                    helynew = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}";
                    if (!Directory.Exists(helynew)) Directory.CreateDirectory(helynew);
                    helynew = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\EsztBeosztás{Dátumtól:yyyyMM}.mdb";
                    if (!File.Exists(helynew)) Adatbázis_Létrehozás.Dolgozói_Beosztás_Adatok_Új(helynew);
                }
                string jelszó = "kiskakas";
                string szöveg = $"SELECT * FROM Beosztás";
                Adatok_Beoszt_Új = Kezelő_Beoszt_Új.Lista_Adatok(helyold, jelszó, szöveg);
                bool vane = (from a in Adatok_Beoszt_Új
                             where a.Dolgozószám == dolgozószám.Trim()
                             select a).Any();
                if (vane)
                {
                    List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új>();
                    if (Dátumtól > Dátumig)
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                && a.Nap >= Dátumtól
                                  orderby a.Nap
                                  select a).ToList();
                    else
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                && a.Nap >= Dátumtól
                                && a.Nap <= Dátumig
                                  orderby a.Nap
                                  select a).ToList();

                    List<string> SzövegGy = new List<string>();
                    foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok)
                    {
                        if (rekord.Túlóraok.Contains('#') || rekord.Megjegyzés.Contains("#"))
                        {
                            szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, " +
                                                            "Túlóra, Túlórakezd, Túlóravég, Csúszóra, " +
                                                            "CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, " +
                                                            "Szabiok, kért, Csúszok, AFTóra, " +
                                                            "AFTok ) VALUES (";
                            szöveg += $"'{dolgozószám}', ";   //    Dolgozószám,
                            szöveg += $"'{rekord.Nap}', ";   //    Nap,
                            szöveg += $"'#', ";   //    Beosztáskód,
                            szöveg += $"{rekord.Ledolgozott}, ";   //    Ledolgozott,
                            szöveg += $"{rekord.Túlóra}, ";   //    Túlóra,
                            szöveg += $"'{rekord.Túlórakezd}', ";   //    Túlórakezd,
                            szöveg += $"'{rekord.Túlóravég}', ";   //    Túlóravég,
                            szöveg += $"{rekord.Csúszóra}, ";   //    Csúszóra,
                            szöveg += $"'{rekord.CSúszórakezd}', ";   //    CSúszórakezd,
                            szöveg += $"'{rekord.Csúszóravég}', ";   //    Csúszóravég,
                            szöveg += $"'{rekord.Megjegyzés}', ";   //    Megjegyzésváltozó,
                            szöveg += $"'{rekord.Túlóraok}', ";   //    Túlóraok,
                            szöveg += $"'{rekord.Szabiok}', ";   //    Szabiok,
                            szöveg += $"{rekord.Kért} , ";   //    kért,
                            szöveg += $"'{rekord.Csúszok}', ";   //    Csúszok,
                            szöveg += $"{rekord.AFTóra}, ";   //    AFTóra,
                            szöveg += $"'{rekord.AFTok}' ) ";   //    AFTok,
                            SzövegGy.Add(szöveg);
                        }

                    }
                    MyA.ABMódosítás(helynew, jelszó, SzövegGy);
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

        private void Új_Beosztás_hónap(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {

                string helyold = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\EBeosztás{Dátumtól:yyyyMM}.mdb";
                if (!File.Exists(helyold)) return;

                string helynew = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\EsztBeosztás{Dátumtól:yyyyMM}.mdb";
                if (!File.Exists(helynew))
                {
                    helynew = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}";
                    if (!Directory.Exists(helynew)) Directory.CreateDirectory(helynew);
                    helynew = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\EsztBeosztás{Dátumtól:yyyyMM}.mdb";
                    if (!File.Exists(helynew)) Adatbázis_Létrehozás.Dolgozói_Beosztás_Adatok_Új(helynew);
                }
                string jelszó = "kiskakas";

                string szöveg = $"SELECT * FROM Beosztás";
                Adatok_Beoszt_Új = Kezelő_Beoszt_Új.Lista_Adatok(helyold, jelszó, szöveg);
                bool vane = (from a in Adatok_Beoszt_Új
                             where a.Dolgozószám == dolgozószám.Trim()
                             select a).Any();
                if (vane)
                {
                    List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új>();
                    if (Dátumtól > Dátumig)
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                  && a.Nap >= Dátumtól
                                  orderby a.Nap
                                  select a).ToList();
                    else
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                  && a.Nap >= Dátumtól
                                  && a.Nap <= Dátumig
                                  orderby a.Nap
                                  select a).ToList();

                    List<string> SzövegGy = new List<string>();
                    foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok)
                    {
                        szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, " +
                                                        "Túlóra, Túlórakezd, Túlóravég, Csúszóra, " +
                                                        "CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, " +
                                                        "Szabiok, kért, Csúszok, AFTóra, " +
                                                        "AFTok ) VALUES (";
                        szöveg += $"'{dolgozószám}', ";   //    Dolgozószám,
                        szöveg += $"'{rekord.Nap}', ";   //    Nap,
                        szöveg += $"'{rekord.Beosztáskód}', ";   //    Beosztáskód,
                        szöveg += $"{rekord.Ledolgozott}, ";   //    Ledolgozott,
                        szöveg += $"{rekord.Túlóra}, ";   //    Túlóra,
                        szöveg += $"'{rekord.Túlórakezd}', ";   //    Túlórakezd,
                        szöveg += $"'{rekord.Túlóravég}', ";   //    Túlóravég,
                        szöveg += $"{rekord.Csúszóra}, ";   //    Csúszóra,
                        szöveg += $"'{rekord.CSúszórakezd}', ";   //    CSúszórakezd,
                        szöveg += $"'{rekord.Csúszóravég}', ";   //    Csúszóravég,
                        szöveg += $"'{rekord.Megjegyzés}', ";   //    MegjegyzésVáltozó,
                        szöveg += $"'{rekord.Túlóraok}', ";   //    Túlóraok,
                        szöveg += $"'{rekord.Szabiok}', ";   //    Szabiok,
                        szöveg += $"{rekord.Kért} , ";   //    kért,
                        szöveg += $"'{rekord.Csúszok}', ";   //    Csúszok,
                        szöveg += $"{rekord.AFTóra}, ";   //    AFTóra,
                        szöveg += $"'{rekord.AFTok}' ) ";   //    AFTok,
                        SzövegGy.Add(szöveg);
                    }
                    MyA.ABMódosítás(helynew, jelszó, SzövegGy);
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

        private void Eszt_Beosztás_Törlés(string Telephely, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumtól.Year}\EsztBeosztás{Dátumtól:yyyyMM}.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "kiskakas";
                string szöveg = $"SELECT * FROM Beosztás";
                bool vane;

                Adatok_Beoszt_Új = Kezelő_Beoszt_Új.Lista_Adatok(hely, jelszó, szöveg);
                if (Dátumtól.Month != Dátumig.Month)
                {
                    vane = Adatok_Beoszt_Új.Any(a => a.Nap >= Dátumtól && a.Nap <= Dátumig);
                    if (vane)
                    {
                        szöveg = $"DELETE FROM beosztás WHERE  nap>=#{Dátumtól:yyyy-MM-dd}# AND nap<=#{Dátumig:yyyy-MM-dd}# ";
                        MyA.ABtörlés(hely, jelszó, szöveg);
                    }
                }

                hely = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Beosztás\{Dátumig.Year}\EsztBeosztás{Dátumig:yyyyMM}.mdb";
                if (!File.Exists(hely)) return;
                jelszó = "kiskakas";
                vane = Adatok_Beoszt_Új.Any(a => a.Nap >= Dátumtól && a.Nap <= Dátumig);
                if (vane)
                {
                    szöveg = $"DELETE FROM beosztás WHERE nap>=#{Dátumtól:yyyy-MM-dd} # AND nap<=# {Dátumig:yyyy-MM-dd}# ";
                    MyA.ABtörlés(hely, jelszó, szöveg);
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


        #region Terv
        private void Terv_Lista_Click(object sender, EventArgs e)
        {
            Terv_lista_elj();
        }


        private void Terv_lista_elj()
        {
            try
            {
                Terv_Tábla.Rows.Clear();
                Terv_Tábla.Columns.Clear();
                Terv_Tábla.Refresh();
                Terv_Tábla.Visible = false;
                Terv_Tábla.ColumnCount = 8;
                Terv_Tábla.RowCount = 48;

                Terv_Tábla.Columns[0].HeaderText = "Óra";
                Terv_Tábla.Columns[0].Width = 80;
                Terv_Tábla.Columns[0].Frozen = true;
                Terv_Tábla.Columns[1].HeaderText = "Hétfő";
                Terv_Tábla.Columns[1].Width = 200;
                Terv_Tábla.Columns[2].HeaderText = "Kedd";
                Terv_Tábla.Columns[2].Width = 200;
                Terv_Tábla.Columns[3].HeaderText = "Szerda";
                Terv_Tábla.Columns[3].Width = 200;
                Terv_Tábla.Columns[4].HeaderText = "Csütörtök";
                Terv_Tábla.Columns[4].Width = 200;
                Terv_Tábla.Columns[5].HeaderText = "Péntek";
                Terv_Tábla.Columns[5].Width = 200;
                Terv_Tábla.Columns[6].HeaderText = "Szombat";
                Terv_Tábla.Columns[6].Width = 200;
                Terv_Tábla.Columns[7].HeaderText = "Vasárnap";
                Terv_Tábla.Columns[7].Width = 200;

                DateTime óra = new DateTime(1900, 1, 1, 0, 0, 0);
                for (int i = 0; i < 48; i++)
                {
                    Terv_Tábla.Rows[i].Cells[0].Value = óra.ToString("HH:mm");

                    óra = óra.AddMinutes(30);
                }

                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime IdeigDát = Hételső;
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);

                string szöveg = $"SELECT * FROM naptár where [idő]>=# {Hételső:MM-dd-yyyy} 00:00:0#";
                szöveg += $" and [idő]<=#{Hétutolsó:MM-dd-yyyy} 23:59:0# ORDER BY idő";
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Hételső.Year}_Esztergálás.mdb";
                string jelszó = "RónaiSándor";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Éves(hely);


                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(hely, jelszó, szöveg);

                if (Hételső.Year != Hétutolsó.Year)
                {
                    hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Hétutolsó.Year}_Esztergálás.mdb";
                    if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Éves(hely);
                    List<Adat_Kerék_Eszterga_Naptár> Adatokköv = KézNaptár.Lista_Adatok(hely, jelszó, szöveg);
                    Adatok.AddRange(Adatokköv);
                }





                Szín_kódolás Szín;

                HétAlapAdatai();

                int k = 1;
                while (k < 8)
                {
                    Terv_Tábla.Columns[k].HeaderText = IdeigDát.ToString("MM.dd  dddd");
                    IdeigDát = IdeigDát.AddDays(1);
                    k++;
                }



                foreach (Adat_Kerék_Eszterga_Naptár rekord in Adatok)
                {
                    int oszlop = MyF.Hét_Melyiknapja(rekord.Idő);
                    int sor = (2 * rekord.Idő.Hour) + (rekord.Idő.Minute == 30 ? 1 : 0);

                    if (rekord.Munkaidő)
                        Terv_Tábla.Rows[sor].Cells[oszlop].Style.BackColor = Color.Green;
                    if (rekord.Foglalt)
                    {
                        Terv_Tábla.Rows[sor].Cells[oszlop].Value = rekord.Pályaszám.Trim();
                        Szín = MyColor.Szín_váltó(rekord.BetűSzín);
                        Terv_Tábla.Rows[sor].Cells[oszlop].Style.ForeColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                        Szín = MyColor.Szín_váltó(rekord.HáttérSzín);
                        Terv_Tábla.Rows[sor].Cells[oszlop].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                        if (!rekord.Munkaidő)
                        {
                            // Ha időközben kikerült a munkaidő akkor tovább alakítjuk
                            Terv_Tábla.Rows[sor].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 6, FontStyle.Italic);
                        }
                    }
                }
                Terv_Tábla.FirstDisplayedScrollingRowIndex = 12;

                Terv_Tábla.Visible = true;
            }
            catch (HibásBevittAdat ex)
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


        #region Terv küldés
        private void Heti_terv_küldés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Terv_Tábla.Rows.Count < 1)
                    throw new HibásBevittAdat("A terv táblának nincs érvényes adata.");
                Email();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Email()
        {
            try
            {
                //Levél érdemi része
                string Html_szöveg = "<html><body>";
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);
                string Border = $"border-width:1px;border-style:solid;border-color:{MyColor.ColorToHex(Color.Black)}";

                Html_szöveg += $"<p><i>Barossi kerékeszterga {Hételső:yyyy.MM.dd}-{Hétutolsó:yyyy.MM.dd} közötti esztergálási terve</i></p></br>";
                Html_szöveg += $"</br>";
                Html_szöveg += $"<table cellpadding='5' cellspacing='0' style='{Border};font-size: 12pt'>";

                //Fejléc
                Html_szöveg += $"<tr><th style='background-color: #B8DBFD;{Border}'>Óra</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[1].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[2].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[3].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[4].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[5].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[6].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[7].HeaderText}</th></tr>";

                for (int i = 0; i < Terv_Tábla.Rows.Count - 1; i++)
                {
                    string szöveg0 = Terv_Tábla.Rows[i].Cells[0].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[0].Value.ToString()) : "";
                    string szöveg1 = Terv_Tábla.Rows[i].Cells[1].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[1].Value.ToString()) : "";
                    string szöveg2 = Terv_Tábla.Rows[i].Cells[2].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[2].Value.ToString()) : "";
                    string szöveg3 = Terv_Tábla.Rows[i].Cells[3].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[3].Value.ToString()) : "";
                    string szöveg4 = Terv_Tábla.Rows[i].Cells[4].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[4].Value.ToString()) : "";
                    string szöveg5 = Terv_Tábla.Rows[i].Cells[5].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[5].Value.ToString()) : "";
                    string szöveg6 = Terv_Tábla.Rows[i].Cells[6].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[6].Value.ToString()) : "";
                    string szöveg7 = Terv_Tábla.Rows[i].Cells[7].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[7].Value.ToString()) : "";

                    Color szín1 = Terv_Tábla.Rows[i].Cells[0].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[0].Style.BackColor;
                    Color szín2 = Terv_Tábla.Rows[i].Cells[1].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[1].Style.BackColor;
                    Color szín3 = Terv_Tábla.Rows[i].Cells[2].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[2].Style.BackColor;
                    Color szín4 = Terv_Tábla.Rows[i].Cells[3].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[3].Style.BackColor;
                    Color szín5 = Terv_Tábla.Rows[i].Cells[4].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[4].Style.BackColor;
                    Color szín6 = Terv_Tábla.Rows[i].Cells[5].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[5].Style.BackColor;
                    Color szín7 = Terv_Tábla.Rows[i].Cells[6].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[6].Style.BackColor;
                    Color szín8 = Terv_Tábla.Rows[i].Cells[7].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[7].Style.BackColor;

                    Html_szöveg += $"<tr>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín1)};{Border}'>{szöveg0}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín2)};{Border}'>{szöveg1}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín3)};{Border}'>{szöveg2}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín4)};{Border}'>{szöveg3}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín5)};{Border}'>{szöveg4}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín6)};{Border}'>{szöveg5}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín7)};{Border}'>{szöveg6}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín8)};{Border}'>{szöveg7}</td></tr>";
                }

                Html_szöveg += "</table>";
                Html_szöveg += "<p>Ez az e-mail a Villamos program által készített automatikus üzenet .</p>";
                Html_szöveg += "</body></html>";



                MyO._Application _app = new MyO.Application();
                MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);

                // címzettek
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"SELECT * FROM terjesztés WHERE változat=1 OR változat=3 ORDER BY név";
                Kezelő_Kerék_Eszterga_Terjesztés Kéz = new Kezelő_Kerék_Eszterga_Terjesztés();
                List<Adat_Kerék_Eszterga_Terjesztés> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                string címzettek = "";
                foreach (Adat_Kerék_Eszterga_Terjesztés rekord in Adatok)
                    címzettek += rekord.Email + "; ";

                mail.To = címzettek;

                // üzenet tárgya
                mail.Subject = $"Kerékeszterga {MyF.Hét_Sorszáma(Dátum.Value)}.-ik heti tervezett ütemterve ";
                // üzent szövege
                mail.HTMLBody = Html_szöveg;
                mail.Importance = MyO.OlImportance.olImportanceNormal;

                ((MyO._MailItem)mail).Send();

                MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        string Pályaszám_ellenőrzés(string vezényelt)
        {
            if (Honos == null) Honos_feltöltés();
            string válasz = vezényelt.Trim();
            string[] darabol = vezényelt.Split('=');
            string pályaszám;

            if (darabol.Length > 1)
            {
                if (darabol[0].Contains('-'))
                {
                    string[] tovább = darabol[0].Split('-');
                    pályaszám = tovább[0].Trim();
                }
                else
                    pályaszám = darabol[0].Trim();

                string telephely = "";
                //var telephelyek = from Elem in Honos
                //                  where Elem.Azonosító.Trim() == pályaszám.Trim()
                //                  select Elem.Üzem;
                List<Adat_Jármű> telephelyek = Honos.Where(Elem => Elem.Azonosító.Trim() == pályaszám.Trim()).ToList();


                foreach (Adat_Jármű item in telephelyek)
                {
                    telephely = item.Üzem.Trim();
                }

                if (telephely.Trim() != "" && darabol[1].Trim() != telephely.Trim())
                    válasz += "<br>Honos:" + telephely.Trim();
            }
            return válasz;
        }

        private void Honos_feltöltés()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM állománytábla where törölt=0 order by  azonosító";

            Kezelő_Jármű Kéz = new Kezelő_Jármű();
            Honos = Kéz.Lista_Adatok(hely, jelszó, szöveg);
        }

        #endregion


        #region Lejelentés
        private void Automata_Jelentés()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
            string jelszó = "RónaiSándor";
            string szöveg = "SELECT * FROM automata ORDER BY UtolsóÜzenet ";
            Kezelő_Kerék_Eszterga_Automata Kéz = new Kezelő_Kerék_Eszterga_Automata();

            Adat_Kerék_Eszterga_Automata Egy = Kéz.Egy_Adat(hely, jelszó, szöveg);
            DateTime Utolsó = Egy.UtolsóÜzenet;

            if (Egy != null)
            {
                List<Adat_Kerék_Eszterga_Automata> Lista = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                //ha a héten még nem küldött üzenetet
                if (Utolsó < MyF.Hét_elsőnapja(DateTime.Today))
                {
                    //ha benne van a listában
                    bool volt = false;
                    foreach (Adat_Kerék_Eszterga_Automata rekord in Lista)
                    {
                        if (rekord.FelhasználóiNév.Trim() == Program.PostásNév.Trim()) volt = true;
                    }
                    //küldi 
                    if (volt)
                    {
                        while (Utolsó < MyF.Hét_elsőnapja(DateTime.Today))
                        {
                            Dátum.Value = Utolsó;
                            Heti_jelentés_eljárás();
                            Utolsó = Utolsó.AddDays(7);
                        }

                        szöveg = $"UPDATE automata SET UtolsóÜzenet='{DateTime.Today:yyyy.MM.dd}' ";
                        MyA.ABMódosítás(hely, jelszó, szöveg);

                        Dátum.Value = DateTime.Today;
                    }
                }
            }
        }


        void Heti_jelentés_eljárás()
        {
            try
            {
                Beosztás_Adatok();
                string fájlexc;

                fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\Eszterga_Lejelentés_{MyF.Hét_Sorszáma(Dátum.Value)}_heti_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                Holtart.Be();
                Kerékeszterga_Excel KerExc = new Kerékeszterga_Excel(fájlexc, Dátum.Value);
                Holtart.Lép();
                KerExc.Excel_alaptábla();
                Holtart.Lép();
                Email_jelentés(fájlexc);
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


        private void Heti_jelentés_Click(object sender, EventArgs e)
        {
            Heti_jelentés_eljárás();

            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
            string jelszó = "RónaiSándor";
            string szöveg = $"UPDATE automata SET UtolsóÜzenet='{DateTime.Today:yyyy.MM.dd}' ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


        private void Email_jelentés(string fájlexc)
        {
            try
            {
                string Border = $"border-width:1px;border-style:solid;border-color:{MyColor.ColorToHex(Color.Black)}";

                //Levél érdemi része
                string Html_szöveg = "<html><body>";
                Html_szöveg += $"<b>Kerékeszterga {MyF.Hét_Sorszáma(Dátum.Value)}.-ik heti elvégzett tevékenységeinek összefoglaló jelentését mellékelten megküldöm.</b><br><br>";
                Html_szöveg += "<p>Ez az e-mail a Villamos program által készített automatikus üzenet .</p>";
                Html_szöveg += "</body></html>";

                MyO._Application _app = new MyO.Application();
                MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);

                // címzettek
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"SELECT * FROM terjesztés WHERE változat=2 OR változat=3 ORDER BY név";
                Kezelő_Kerék_Eszterga_Terjesztés Kéz = new Kezelő_Kerék_Eszterga_Terjesztés();
                List<Adat_Kerék_Eszterga_Terjesztés> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                string címzettek = "";
                foreach (Adat_Kerék_Eszterga_Terjesztés rekord in Adatok)
                    címzettek += rekord.Email + "; ";

                mail.To = címzettek;

                // üzenet tárgya
                mail.Subject = $"Kerékeszterga {MyF.Hét_Sorszáma(Dátum.Value)}.-ik heti elvégzett tevékenységeinek összefoglaló jelentése";
                // üzent szövege
                mail.HTMLBody = Html_szöveg;
                mail.Importance = MyO.OlImportance.olImportanceNormal;
                //csatolmány
                mail.Attachments.Add(fájlexc);
                ((MyO._MailItem)mail).Send();

                MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Beállítások
        Ablak_Eszterga_Dolgozók Új_Ablak_Eszterga_Dolgozók;
        private void Esztergályosok_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Dolgozók?.Close();

            Új_Ablak_Eszterga_Dolgozók = new Ablak_Eszterga_Dolgozók(Cmbtelephely.Text.Trim());
            Új_Ablak_Eszterga_Dolgozók.FormClosed += Ablak_Eszterga_Dolgozók_Closed;
            Új_Ablak_Eszterga_Dolgozók.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Dolgozók.Show();
        }

        private void Ablak_Eszterga_Dolgozók_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Dolgozók = null;
        }




        Ablak_Eszterga_Választék Új_Ablak_Eszterga_Választék;
        private void Választék_Lista_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Választék?.Close();

            Új_Ablak_Eszterga_Választék = new Ablak_Eszterga_Választék();
            Új_Ablak_Eszterga_Választék.FormClosed += Ablak_Eszterga_Választék_Closed;
            Új_Ablak_Eszterga_Választék.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Választék.Show();
        }

        private void Ablak_Eszterga_Választék_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Választék = null;
        }


        #endregion


        #region Rögzítés

        Ablak_Eszterga_Segéd Új_Ablak_Eszterga_Segéd;
        private void Terv_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;


            DateTime idő = DateTime.Parse(Terv_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString());
            DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
            DateTime dátum = Hételső.AddDays(e.ColumnIndex - 1);
            DátumÉsIdő = new DateTime(dátum.Year, dátum.Month, dátum.Day, idő.Hour, idő.Minute, 0);

            if (Új_Ablak_Eszterga_Segéd != null)
            {
                Új_Ablak_Eszterga_Segéd = new Ablak_Eszterga_Segéd(DátumÉsIdő, 0);
                Új_Ablak_Eszterga_Segéd.FormClosed += Ablak_Eszterga_Segéd_Closed;
                Új_Ablak_Eszterga_Segéd.Változás += Terv_lista_elj;
                Új_Ablak_Eszterga_Segéd.StartPosition = FormStartPosition.CenterScreen;
                Új_Ablak_Eszterga_Segéd.Show();
            }
        }

        private void Ablak_Eszterga_Segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Segéd = null;
        }


        void Ablak_nyitás(DateTime DátumÉsIdő, int Mód)
        {
            Új_Ablak_Eszterga_Segéd?.Close();

            Új_Ablak_Eszterga_Segéd = new Ablak_Eszterga_Segéd(DátumÉsIdő, Mód);
            Új_Ablak_Eszterga_Segéd.FormClosed += Ablak_Eszterga_Segéd_Closed;
            Új_Ablak_Eszterga_Segéd.Változás += Terv_lista_elj;
            Új_Ablak_Eszterga_Segéd.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Segéd.Show();
        }


        private void Rögzítés_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 0);

        }


        private void Sor_Beszúrása_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 1);
        }


        private void Sor_törlése_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 2);
        }

        private void Munkaközi_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 3);
        }

        #endregion


        public void Státus_állítás(string Pályaszám, int Státus_Lesz, DateTime Dátum)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Dátum.Year}_Igény.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"UPDATE igény SET státus={Státus_Lesz}";
                szöveg += $"   WHERE  pályaszám='{Pályaszám.Trim()}'";
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


        private void Elkészült_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve módosítandó sor.");
                foreach (DataGridViewRow SOR in Tábla.SelectedRows)
                {
                    //Csak az ütemezett kocsikkal foglalkozunk
                    if ("Ütemezett" == SOR.Cells[5].Value.ToString().Trim())
                    {
                        string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Parse(SOR.Cells[1].Value.ToString()).Year}_Igény.mdb";
                        if (File.Exists(hely))
                        {
                            Státus_állítás(SOR.Cells[2].Value.ToString(), 7, DateTime.Parse(SOR.Cells[1].Value.ToString()));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Csak Ütemezett feladatokat lehet készre jelenteni.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Lista_Tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Visszaállítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve módosítandó sor.");
                foreach (DataGridViewRow SOR in Tábla.SelectedRows)
                {
                    //Csak az Elkészült kocsikkal foglalkozunk
                    if ("Elkészült" == SOR.Cells[5].Value.ToString().Trim())
                    {
                        string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Parse(SOR.Cells[1].Value.ToString()).Year}_Igény.mdb";
                        if (File.Exists(hely))
                        {
                            Státus_állítás(SOR.Cells[2].Value.ToString(), 2, DateTime.Parse(SOR.Cells[1].Value.ToString()));
                        }
                    }
                    else
                        MessageBox.Show("Csak Elkészült feladatokat lehet visszaállítani ütemezettre.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Lista_Tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Törölt_Click(object sender, EventArgs e)
        {
            try
            {

                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve módosítandó sor.");
                foreach (DataGridViewRow SOR in Tábla.SelectedRows)
                {
                    //Csak a saját telephely igényeit lehet törölni
                    if (Cmbtelephely.Text.Trim() == SOR.Cells[3].Value.ToString().Trim())
                    {
                        //Csak az Elkészült kocsikkal foglalkozunk
                        if ("Igény" == SOR.Cells[5].Value.ToString().Trim())
                        {
                            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Parse(SOR.Cells[1].Value.ToString()).Year}_Igény.mdb";
                            if (File.Exists(hely))
                            {
                                Státus_állítás(SOR.Cells[2].Value.ToString(), 9, DateTime.Parse(SOR.Cells[1].Value.ToString()));
                            }
                        }
                        else
                            MessageBox.Show("Csak Igény státusú feladatokat lehet törölni.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Csak a saját telephely igény státusú feladatait lehet törölni.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Lista_Tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excel_készítés_Click(object sender, EventArgs e)
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
                    FileName = "Eszterga_Igény_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Module_Excel.DataGridViewToExcel(fájlexc, Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Module_Excel.Megnyitás(fájlexc + ".xlsx");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Terjesztési lista

        Ablak_Eszterga_Terjesztés Új_Ablak_Eszterga_Terjesztés;
        private void Terjesztési_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Terjesztés?.Close();

            Új_Ablak_Eszterga_Terjesztés = new Ablak_Eszterga_Terjesztés();
            Új_Ablak_Eszterga_Terjesztés.FormClosed += Ablak_Eszterga_Terjesztés_Closed;
            Új_Ablak_Eszterga_Terjesztés.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Terjesztés.Show();
        }


        private void Ablak_Eszterga_Terjesztés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Terjesztés = null;
        }


        #endregion


        #region Jobb egér


        private void RögzítésTörlésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 0);

        }

        private void BeszúrásCsúsztatássalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 1);
        }

        private void TörlésCsúsztatássalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 2);
        }

        private void MunkaköziSzünetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Ablak_nyitás(DátumÉsIdő, 3);
        }



        #endregion

        #region beosztásAblak

        Ablak_Eszterga_Beosztás Új_Ablak_Eszterga_Beosztás;
        private void MiniBeosztás_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Beosztás?.Close();

            Új_Ablak_Eszterga_Beosztás = new Ablak_Eszterga_Beosztás(Dátum.Value, Application.StartupPath);
            Új_Ablak_Eszterga_Beosztás.FormClosed += Új_Ablak_Eszterga_Beosztás_Closed;
            Új_Ablak_Eszterga_Beosztás.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Beosztás.Show();
        }

        private void Új_Ablak_Eszterga_Beosztás_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Beosztás = null;
        }
        #endregion
    }
}
