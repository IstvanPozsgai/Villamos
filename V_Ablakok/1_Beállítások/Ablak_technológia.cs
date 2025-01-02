using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_technológia : Form
    {
        readonly Kezelő_Technológia KézAdat = new Kezelő_Technológia();
        readonly Kezelő_Technológia_Ciklus KezelőCiklus = new Kezelő_Technológia_Ciklus();
        readonly Kezelő_Technológia_Kivételek KTK_kéz = new Kezelő_Technológia_Kivételek();

        List<Adat_Technológia> AdatokTech = new List<Adat_Technológia>();
        List<Adat_Technológia_Kivételek> AdatokKiv = new List<Adat_Technológia_Kivételek>();

        long Kiválasztott_Sor = -1;
        int Kivétel_sor = -1;
        string hely_;
        string jelszó_;
        string szöveg_;
        string Járműtípus_;

        public Ablak_technológia()
        {
            InitializeComponent();
        }

        private void Ablak_technológia_Load(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\Technológia";
            if (Directory.Exists(hely) == false) Directory.CreateDirectory(hely);
            hely += @"\technológia.mdb";
            if (System.IO.File.Exists(hely) == false) Adatbázis_Létrehozás.Technológia_ALAPAdat(hely);
            Jogosultságkiosztás();
            Típus_feltöltés();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

        }


        #region Alap

        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Fülekkitöltése();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }
        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {

                            break;
                        }
                    case 1:
                        {
                            Altípus_Feltöltés();
                            break;
                        }

                    case 2:
                        {
                            //Beállítási adatok
                            Elérés_feltöltés();
                            Típusok_feltöltése();
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

        private void Jogosultságkiosztás()
        {
            int melyikelem;

            Beviteli_táblakészítés.Enabled = false;
            Adatok_beolvasása.Enabled = false;
            Adat_Módosítás.Enabled = false;

            Sor_beszúrás.Enabled = false;
            Sor_törlés.Enabled = false;

            Típus_Rögzítés.Enabled = false;

            Ciklus_rögzít.Enabled = false;
            Ciklus_Törlés.Enabled = false;

            Kivétel_Rögzít.Enabled = false;
            Kivétel_töröl.Enabled = false;

            Típusok_rögzítése.Enabled = false;
            Törlés_JTípus.Enabled = false;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            // csak főmérnökségi belépéssel törölhető
            if (Program.PostásTelephely == "Főmérnökség")
            {
                Beviteli_táblakészítés.Visible = true;
                Adatok_beolvasása.Visible = true;
                Adat_Módosítás.Visible = true;
                Sor_beszúrás.Visible = true;
                Sor_törlés.Visible = true;

                Típus_Rögzítés.Visible = true;

                Ciklus_rögzít.Visible = true;
                Ciklus_Törlés.Visible = true;

                //Kivétel_Rögzít.Visible = true;
                //Kivétel_töröl.Visible = true;

                Típusok_rögzítése.Visible = true;
                Törlés_JTípus.Visible = true;

            }
            else
            {
                Beviteli_táblakészítés.Visible = false;
                Adatok_beolvasása.Visible = false;
                Adat_Módosítás.Visible = false;
                Sor_beszúrás.Visible = false;
                Sor_törlés.Visible = false;

                Típus_Rögzítés.Visible = false;

                Ciklus_rögzít.Visible = false;
                Ciklus_Törlés.Visible = false;

                //Kivétel_Rögzít.Visible = false;
                //Kivétel_töröl.Visible = false;

                Típusok_rögzítése.Visible = false;
                Törlés_JTípus.Visible = false;
            }

            melyikelem = 16;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Beviteli_táblakészítés.Enabled = true;
                Adatok_beolvasása.Enabled = true;
                Adat_Módosítás.Enabled = true;
                Sor_beszúrás.Enabled = true;
                Sor_törlés.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Típus_Rögzítés.Enabled = true;

                Ciklus_rögzít.Enabled = true;
                Ciklus_Törlés.Enabled = true;

                Kivétel_Rögzít.Enabled = true;
                Kivétel_töröl.Enabled = true;

                Típusok_rögzítése.Enabled = true;
                Törlés_JTípus.Enabled = true;

            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void Típus_feltöltés()
        {
            try
            {
                Járműtípus.Items.Clear();
                List_típus.Items.Clear();

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\technológia\technológia.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT *  FROM Típus_tábla ORDER BY típus";

                Járműtípus.BeginUpdate();
                Járműtípus.Items.Add("");
                Járműtípus.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                Járműtípus.EndUpdate();
                Járműtípus.Refresh();


                List_típus.BeginUpdate();
                List_típus.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                List_típus.EndUpdate();
                List_típus.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Technológia.html";
            Module_Excel.Megnyitás(hely);
        }

        private void Járműtípus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Combo_KarbCiklusEleje.Items.Clear();
            Combo_KarbCiklusVége.Items.Clear();
            if (Járműtípus.Text.Trim() == "") return;

            string hely = Application.StartupPath + @"\Főmérnökség\adatok\Technológia\" + Járműtípus.Text.Trim() + ".mdb";
            string jelszó = "Bezzegh";
            string szöveg = "SELECT *  FROM karbantartás ORDER BY sorszám";

            Combo_KarbCiklusEleje.BeginUpdate();
            Combo_KarbCiklusEleje.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "fokozat"));
            Combo_KarbCiklusEleje.EndUpdate();
            Combo_KarbCiklusEleje.Refresh();

            Combo_KarbCiklusVége.BeginUpdate();
            Combo_KarbCiklusVége.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "fokozat"));
            Combo_KarbCiklusVége.EndUpdate();
            Combo_KarbCiklusVége.Refresh();

            Text_Típus.Text = Járműtípus.Text.Trim();
            Ciklus_Lista();
        }

        private void LapFülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Határozza meg, hogy melyik lap van jelenleg kiválasztva
                TabPage SelectedTab = Fülek.TabPages[e.Index];

                // Szerezze be a lap fejlécének területét
                Rectangle HeaderRect = Fülek.GetTabRect(e.Index);

                // Hozzon létreecsetet a szöveg megfestéséhez
                SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

                // Állítsa be a szöveg igazítását
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

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
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        void Altípus_Feltöltés()
        {
            try
            {

                if (Járműtípus.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kitöltve a járműtípus, ezért nem kerül feltöltése.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Járműtípus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                string szöveg;
                szöveg = $"SELECT distinct(altípus)  FROM  Technológia ORDER BY altípus";

                Kezelő_Technológia Kéz = new Kezelő_Technológia();
                List<string> Adatok = Kéz.Lista_Altípus(hely, jelszó, szöveg);

                Combo_Altípus.Items.Clear();
                foreach (string Elem in Adatok)
                    Combo_Altípus.Items.Add(Elem.Trim());

            }
            catch (HibásBevittAdat ex)
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


        #region Adatok táblázatot nézet lapfül

        void Alap_tábla_író()
        {
            try
            {
                //kérdés
                Holtart.Visible = true;
                Holtart.Refresh();
                if (Járműtípus_ == "")
                    throw new HibásBevittAdat("Jármű típushoz tartozó címet választani kell.");

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Technológia\" + Járműtípus_ + ".mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT technológia.*, Karbantartás.fokozat, Karbantartás_1.fokozat FROM ";
                szöveg += " (Karbantartás INNER JOIN technológia ON Karbantartás.sorszám = technológia.Karb_ciklus_eleje) INNER JOIN Karbantartás AS Karbantartás_1 ON  Karbantartás_1.sorszám=technológia.Karb_ciklus_vége ";

                if (Érvényes.Checked || Szűr_R_E.Text.Trim() != "")
                    szöveg += " WHERE ";

                if (Érvényes.Checked)
                    szöveg += $"  (technológia.Érv_kezdete<=#{DateTime.Now:yyyy-MM-dd}# AND technológia.Érv_vége>=#{DateTime.Now:yyyy-MM-dd}#) ";

                if (Szűr_R_E.Text.Trim() != "")
                {
                    if (Érvényes.Checked)
                        szöveg += " AND ";
                    szöveg += "  Részegység='" + Szűr_R_E.Text.Trim() + "' ";
                }

                if (UtasításSzám.Text.Trim() != "")
                {
                    if ((Szűr_R_E.Text.Trim() != "") || Érvényes.Checked)
                        szöveg += " AND ";
                    szöveg += "  Munka_utasítás_szám='" + UtasításSzám.Text.Trim() + "' ";
                }
                szöveg += " ORDER BY Részegység, Munka_utasítás_szám, Utasítás_leírás, Érv_kezdete  ";
                AdatokTech = KézAdat.Lista_Adatok(hely, jelszó, szöveg);

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("ID");
                AdatTábla.Columns.Add("Rész- egység");
                AdatTábla.Columns.Add("Utasítás szám");
                AdatTábla.Columns.Add("Utasítás cím");
                AdatTábla.Columns.Add("Utasítás leírása");
                AdatTábla.Columns.Add("Paraméterek");
                AdatTábla.Columns.Add("Karb ciklus eleje");
                AdatTábla.Columns.Add("Karb ciklus vége");
                AdatTábla.Columns.Add("Érvényesség kezdete");
                AdatTábla.Columns.Add("Érvényesség vége");
                AdatTábla.Columns.Add("Szakmai bontás");
                AdatTábla.Columns.Add("Munkaterület bontás");
                AdatTábla.Columns.Add("Altípus");
                AdatTábla.Columns.Add("Kenés");

                AdatTábla.Clear();
                foreach (Adat_Technológia adat in AdatokTech)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["ID"] = adat.ID;
                    Soradat["Rész- egység"] = adat.Részegység;
                    Soradat["Utasítás szám"] = adat.Munka_utasítás_szám;
                    Soradat["Utasítás cím"] = adat.Utasítás_Cím.Replace("\n", " ");
                    Soradat["Utasítás leírása"] = adat.Utasítás_leírás.Replace("\n", " ");
                    Soradat["Paraméterek"] = adat.Paraméter.Replace("\n", " ");
                    Soradat["Karb ciklus eleje"] = adat.Karb_ciklus_eleje.Fokozat;
                    Soradat["Karb ciklus vége"] = adat.Karb_ciklus_vége.Fokozat;
                    Soradat["Érvényesség kezdete"] = adat.Érv_kezdete;
                    Soradat["Érvényesség vége"] = adat.Érv_vége;
                    Soradat["Szakmai bontás"] = adat.Szakmai_bontás;
                    Soradat["Munkaterület bontás"] = adat.Munkaterületi_bontás;
                    Soradat["Altípus"] = adat.Altípus;
                    Soradat["Kenés"] = adat.Kenés == true ? "Igen" : "Nem";

                    AdatTábla.Rows.Add(Soradat);
                }

                Tábla.DataSource = AdatTábla;

                Tábla.Columns["ID"].Width = 70;
                Tábla.Columns["Rész- egység"].Width = 70;
                Tábla.Columns["Utasítás szám"].Width = 70;
                Tábla.Columns["Utasítás cím"].Width = 250;
                Tábla.Columns["Utasítás leírása"].Width = 400;
                Tábla.Columns["Paraméterek"].Width = 200;
                Tábla.Columns["Karb ciklus eleje"].Width = 70;
                Tábla.Columns["Karb ciklus vége"].Width = 70;
                Tábla.Columns["Érvényesség kezdete"].Width = 90;
                Tábla.Columns["Érvényesség vége"].Width = 90;
                Tábla.Columns["Szakmai bontás"].Width = 120;
                Tábla.Columns["Munkaterület bontás"].Width = 120;
                Tábla.Columns["Altípus"].Width = 120;
                Tábla.Columns["Kenés"].Width = 80;

                Tábla.Visible = true;
                Tábla.Refresh();
                Tábla.ClearSelection();

            }
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
                Holtart.Ki();
                Holtart.Refresh();
            }

        }


        private void Alap_Frissít_Click(object sender, EventArgs e)
        {
            Járműtípus_ = Járműtípus.Text.Trim();
            Alap_tábla_író();

        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            long sorszám = long.Parse(Tábla.Rows[e.RowIndex].Cells[0].Value.ToString());
            Egy_adat_Kiírása(sorszám);
            Tábla.Rows[e.RowIndex].Selected = true;
            Kiválasztott_Sor = sorszám;


        }

        private void Excel_mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Technológia_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla, false);
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

        private void Sor_beszúrás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kiválasztott_Sor == -1)
                    throw new HibásBevittAdat("Nincs kiválasztva egy sor sem.");
                if (Tábla.Rows.Count < 1)
                    throw new HibásBevittAdat("Nincs kitöltve a táblázat.");

                Holtart.Be();
                timer1.Enabled = true;

                hely_ = Application.StartupPath + @"\Főmérnökség\adatok\Technológia\" + Járműtípus.Text.Trim() + ".mdb";
                jelszó_ = "Bezzegh";
                szöveg_ = $"SELECT * FROM technológia WHERE id>={Kiválasztott_Sor.ToString()}";


                SZál_Sorbeszúrás(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                    MessageBox.Show("A sor beszúrás elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Alap_tábla_író();
                });
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SZál_Sorbeszúrás(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                List<Adat_Technológia> Adatok = new List<Adat_Technológia>();
                Adatok = KézAdat.Lista_Adatok(hely_, jelszó_, szöveg_);

                KézAdat.Egy_Beszúrás(hely_, jelszó_, Kiválasztott_Sor, Adatok);
                this.Invoke(callback, new object[] { });



            });
            proc.Start();
        }

        private void Sor_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kiválasztott_Sor == -1)
                    throw new HibásBevittAdat("Nincs kiválasztva egy sor sem.");
                if (Tábla.Rows.Count < 1)
                    throw new HibásBevittAdat("Nincs kitöltve a táblázat.");

                Holtart.Be();
                timer1.Enabled = true;

                hely_ = Application.StartupPath + @"\Főmérnökség\adatok\Technológia\" + Járműtípus.Text.Trim() + ".mdb";
                jelszó_ = "Bezzegh";
                szöveg_ = $"SELECT * FROM technológia WHERE id>{Kiválasztott_Sor.ToString()}";
                SZál_SorTörlés(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                    MessageBox.Show("A sor törlés elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Alap_tábla_író();
                });


            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SZál_SorTörlés(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                List<Adat_Technológia> Adatok = new List<Adat_Technológia>();
                Adatok = KézAdat.Lista_Adatok(hely_, jelszó_, szöveg_);

                KézAdat.Egy_Törlése(hely_, jelszó_, Kiválasztott_Sor, Adatok);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }
        #endregion


        #region Adatok szerkesztése lapfül

        private void Adat_frissítés_Click(object sender, EventArgs e)
        {
            long sorszám = long.Parse(Text_id.Text.Trim());
            Egy_adat_Kiírása(sorszám);
        }

        public void Egy_adat_Kiírása(long id)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Technológia\" + Járműtípus.Text.Trim() + ".mdb";
                string jelszó = "Bezzegh";
                if (!System.IO.File.Exists(hely))
                    throw new HibásBevittAdat($"Nincs még {Járműtípus.Text.Trim()} adatbázis létrehozva.");

                Adat_Technológia Adat = KézAdat.Egy_Adat(hely, jelszó, id);

                Text_id.Text = Adat.ID.ToString();
                Text_részegység.Text = Adat.Részegység.Trim();
                Text_Munkautasításszáma.Text = Adat.Munka_utasítás_szám.Trim();
                Text_UtasításCíme.Text = Adat.Utasítás_Cím.Trim();
                Rich_UtasításLeírása.Text = Adat.Utasítás_leírás.Trim();
                Rich_Paraméterek.Text = Adat.Paraméter.Trim();
                Combo_KarbCiklusEleje.Text = Adat.Karb_ciklus_eleje.Fokozat.Trim();
                Combo_KarbCiklusVége.Text = Adat.Karb_ciklus_vége.Fokozat.Trim();
                Date_ÉrvKezdete.Value = Adat.Érv_kezdete;
                Date_ÉrvVége.Value = Adat.Érv_vége;
                Text_Szakmai.Text = Adat.Szakmai_bontás.Trim();
                Text_Munkaterület.Text = Adat.Munkaterületi_bontás.Trim();
                Combo_Altípus.Text = Adat.Altípus.Trim();
                Check_Kenés.Checked = Adat.Kenés;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Beviteli_táblakészítés_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Excel tábla készítés adatok beolvasásához",
                    FileName = "Beolvasó_" + Járműtípus.Text.Trim() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.ExcelLétrehozás();

                MyE.Kiir("Id", "A1");
                MyE.Kiir("Részegység", "B1");
                MyE.Kiir("Munka_utasítás_szám", "C1");
                MyE.Kiir("Utasítás_Cím", "D1");
                MyE.Kiir("Utasítás_leírás", "E1");
                MyE.Kiir("Paraméter", "F1");
                MyE.Kiir("Karb_ciklus_eleje", "G1");
                MyE.Kiir("Karb_ciklus_vége", "H1");
                MyE.Kiir("Érv_kezdete", "I1");
                MyE.Kiir("Érv_vége", "J1");
                MyE.Kiir("Szakmai_bontás", "K1");
                MyE.Kiir("Munkaterületi_bontás", "L1");
                MyE.Kiir("Altípus", "M1");
                MyE.Kiir("Kenés", "N1");
                MyE.Oszlopszélesség("Munka1", "A:N");
                MyE.Rácsoz("a1:n5");
                MyE.NyomtatásiTerület_részletes("Munka1", "A1:N5", "", "", true);
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

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

        private void Új_elem_Click(object sender, EventArgs e)
        {
            Kiüríti();
        }

        private void Kiüríti()
        {
            Text_id.Text = "";
            Text_részegység.Text = "";
            Text_Munkautasításszáma.Text = "";
            Text_UtasításCíme.Text = "";
            Rich_UtasításLeírása.Text = "";
            Rich_Paraméterek.Text = "";
            Combo_KarbCiklusEleje.Text = "";
            Combo_KarbCiklusVége.Text = "";
            Date_ÉrvKezdete.Value = new DateTime(1900, 1, 1);
            Date_ÉrvVége.Value = new DateTime(1900, 1, 1);
            Text_Szakmai.Text = "";
            Text_Munkaterület.Text = "";
            Combo_Altípus.Text = "";
            Check_Kenés.Checked = false;
        }

        private void KarbantartásLista()
        {
            try
            {
                AdatokTech.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Járműtípus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "Select * FROM technológia ";
                AdatokTech = KézAdat.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Adat_Módosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva/ megadva adatbázis a rögzítéshez.");
                KarbantartásLista();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Járműtípus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                string szöveg;

                long id = Text_id.Text.Trim() == "" ? 0 : long.Parse(Text_id.Text.Trim());

                if (id == 0)
                {
                    id = AdatokTech.Max(a => a.ID) + 1;
                    Text_id.Text = id.ToString();
                }
                szöveg = $"SELECT * FROM karbantartás WHERE fokozat='{Combo_KarbCiklusEleje.Text.Trim()}'";
                Adat_technológia_Ciklus AdatCikluse = KezelőCiklus.Egy_Adat(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM karbantartás WHERE fokozat='{Combo_KarbCiklusVége.Text.Trim()}'";
                Adat_technológia_Ciklus AdatCiklusv = KezelőCiklus.Egy_Adat(hely, jelszó, szöveg);

                Adat_Technológia Adat = new Adat_Technológia(
                        id,
                        Text_részegység.Text.Trim(),
                        Text_Munkautasításszáma.Text.Trim(),
                        Text_UtasításCíme.Text.Trim(),
                        Rich_UtasításLeírása.Text.Trim(),
                        Rich_Paraméterek.Text.Trim(),
                        AdatCikluse,
                        AdatCiklusv,
                        Date_ÉrvKezdete.Value,
                        Date_ÉrvVége.Value,
                        Text_Szakmai.Text.Trim(),
                        Text_Munkaterület.Text.Trim(),
                        Combo_Altípus.Text.Trim(),
                        Check_Kenés.Checked
                           );
                KézAdat.Rögzít_adat(hely, jelszó, Adat);

                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Adatok_beolvasása_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Technológia\" + Járműtípus.Text.Trim() + ".mdb";
                string jelszó = "Bezzegh";
                if (!System.IO.File.Exists(hely))
                    throw new HibásBevittAdat("Az adatbázist és a Karbantartási ciklus adatokat először be kell állítani!");
                //Feltöltjük a ciklus listát
                List<Adat_technológia_Ciklus> Adatok_ciklus = new List<Adat_technológia_Ciklus>();
                if (Adatok_ciklus.Count < 0)
                    throw new HibásBevittAdat("A Karbantartási ciklus adatokat először be kell állítani!");

                string szöveg = "SELECT * FROM Karbantartás ORDER BY sorszám";
                Adatok_ciklus = KezelőCiklus.Lista_Adatok(hely, jelszó, szöveg);

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Technológia Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                //megnézzük, hogy milyen az alap tábla
                List<Adat_Alap_Beolvasás> Adatok = new List<Adat_Alap_Beolvasás>();
                Kezelő_Alap_Beolvasás KKezelő = new Kezelő_Alap_Beolvasás();
                string helybeo = Application.StartupPath + @"\Főmérnökség\Adatok\beolvasás.mdb";
                string jelszóbeo = "sajátmagam";
                szöveg = "SELECT * FROM tábla WHERE csoport='technológi'";
                Adatok = KKezelő.Lista_Adatok(helybeo, jelszóbeo, szöveg);

                string ellenőrző = "";
                foreach (Adat_Alap_Beolvasás A in Adatok)
                {
                    ellenőrző += A.Fejléc.Trim();
                }


                MyE.ExcelMegnyitás(fájlexc);
                string munkalap = "Munka1";
                string valós = "";
                //leellenőrizzük a fejlécet, hogy egyforma-e
                int maxoszlop = MyE.Utolsóoszlop(munkalap);
                for (int i = 1; i <= maxoszlop; i++)
                {
                    valós += MyE.Beolvas(MyE.Oszlopnév(i) + "1").Trim();
                }

                if (ellenőrző != valós)
                {
                    MyE.ExcelBezárás();
                    throw new HibásBevittAdat("A beolvasanó Exceltábla nem egyezik meg a várt formátummal.");
                }

                int sormax = MyE.Utolsósor(munkalap);
                Holtart.Be(sormax + 1);

                for (int i = 2; i <= sormax; i++)
                {
                    bool Kenés = bool.TryParse(MyE.Beolvas("N" + i.ToString()).Trim(), out bool result);

                    string Karb_fok = MyE.Beolvas("G" + i.ToString()).Trim();
                    int Karb_sor = Adatok_ciklus.First(x => x.Fokozat.Trim() == Karb_fok).Sorszám;
                    Adat_technológia_Ciklus AdatCiklus1 = new Adat_technológia_Ciklus(Karb_sor, Karb_fok);

                    Karb_fok = MyE.Beolvas("H" + i.ToString()).Trim();
                    Karb_sor = Adatok_ciklus.First(x => x.Fokozat.Trim() == Karb_fok).Sorszám;
                    Adat_technológia_Ciklus AdatCiklus2 = new Adat_technológia_Ciklus(Karb_sor, Karb_fok);

                    Adat_Technológia Adat = new Adat_Technológia(
                        int.Parse(MyE.Beolvas("A" + i.ToString()).Trim()),
                        MyE.Beolvas("B" + i.ToString()).Trim(),
                        MyE.Beolvas("C" + i.ToString()).Trim(),
                        MyE.Beolvas("D" + i.ToString()).Trim(),
                        MyE.Beolvas("E" + i.ToString()).Trim(),
                        MyE.Beolvas("F" + i.ToString()).Trim(),
                        AdatCiklus1,
                        AdatCiklus2,
                        MyE.BeolvasDátum("I" + i.ToString()),
                        MyE.BeolvasDátum("J" + i.ToString()),
                        MyE.Beolvas("K" + i.ToString()).Trim(),
                        MyE.Beolvas("L" + i.ToString()).Trim(),
                        MyE.Beolvas("M" + i.ToString()).Trim(),
                        result
                        );
                    Holtart.Lép();
                    KézAdat.Rögzít_adat(hely, jelszó, Adat);
                }
                Holtart.Ki();
                MyE.ExcelBezárás();
                System.IO.File.Delete(fájlexc);
                MessageBox.Show("Az adatok betöltése elkészült", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            catch (HibásBevittAdat ex)
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


        #region Típusok rögzítése

        private void Típus_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Text_Típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a típus mező, így nem lehet rögzíteni");

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Technológia\technológia.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT * FROM Típus_tábla ";

                Kezelő_Technológia_TípusT Kéz = new Kezelő_Technológia_TípusT();
                List<Adat_Technológia_TípusT> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                long id = 1;
                if (Adatok.Count > 0) id = Adatok.Max(a => a.Id) + 1;

                Adat_Technológia_TípusT Adat = new Adat_Technológia_TípusT(id, Text_Típus.Text.Trim());
                KézAdat.Rögzít_Tech_típus(hely, jelszó, Adat);


                //Létrehozzuk az adatbázist

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                if (!System.IO.File.Exists(hely))
                    Adatbázis_Létrehozás.Technológia_Adat(hely);


                Típus_feltöltés();
                Text_Típus.Text = "";
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Típus_frissítés_Click(object sender, EventArgs e)
        {
            Típus_feltöltés();
        }


        private void List_típus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (List_típus.SelectedItems.Count > 0)
            {
                Text_Típus.Text = List_típus.SelectedItem.ToString().Trim();
                Ciklus_Lista();
                Típus_listázása_kapcs();
                Altípusok_feltöltése();
                Pályaszámok_feltöltése();
                Kivétel_Tábla_kiírás();
            }

        }
        #endregion


        #region Ciklus


        private void Elérés_feltöltés()
        {
            Combo_elérés.Items.Clear();
            Combo_elérés.Items.Add("Alap");
            Combo_elérés.Items.Add("T5C5_E2");
            Combo_elérés.Items.Add("T5C5_E3");
            Combo_elérés.Items.Add("T5C5_V1");
            Combo_elérés.Items.Add("T5C5_V2");
            Combo_elérés.Items.Add("T5C5_V3");


            //Combo_elérés.Items.Add("");
            Combo_elérés.Sorted = true;
        }

        private void Ciklus_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Text_sorszám.Text.Trim(), out int Sorszám)) throw new HibásBevittAdat("A Sorszámnak egész számnak kell lennie");
                if (Text_Típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva egy típus sem.");
                if (Combo_elérés.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva egy elérési mód sem.");


                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                if (!System.IO.File.Exists(hely)) Adatbázis_Létrehozás.Technológia_Adat(hely);
                string jelszó = "Bezzegh";
                int csoport;
                if (Chk_csoportos.Checked)
                    csoport = 1;
                else if (Chk_Egy.Checked)
                    csoport = 2;
                else
                    csoport = 3;


                Adat_technológia_Ciklus Adat = new Adat_technológia_Ciklus(
                                Sorszám,
                                Text_fokozat.Text.Trim(),
                                csoport,
                                Combo_elérés.Text.Trim(),
                                TextVerzió.Text.Trim()
                                );

                KézAdat.Rögzít_Ciklus(hely, jelszó, Adat);
                Ciklus_Lista();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ciklus_listáz_Click(object sender, EventArgs e)
        {
            Ciklus_Lista();
        }

        private void Ciklus_Lista()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                if (!File.Exists(hely)) throw new HibásBevittAdat("Nem létezik ehhez az adatbázishoz Ciklus besorolás!");

                string szöveg = "SELECT * FROM karbantartás  ORDER BY sorszám";

                List<Adat_technológia_Ciklus> Adatok = new List<Adat_technológia_Ciklus>();
                Adatok = KezelőCiklus.Lista_Adatok(hely, jelszó, szöveg);

                Ciklus_tábla.Rows.Clear();
                Ciklus_tábla.Columns.Clear();
                Ciklus_tábla.Refresh();
                Ciklus_tábla.Visible = false;
                Ciklus_tábla.ColumnCount = 5;

                // fejléc elkészítése
                Ciklus_tábla.Columns[0].HeaderText = "ID";
                Ciklus_tábla.Columns[0].Width = 105;
                Ciklus_tábla.Columns[1].HeaderText = "Fokozat";
                Ciklus_tábla.Columns[1].Width = 105;
                Ciklus_tábla.Columns[2].HeaderText = "Csoportos/\nEgy kocsi";
                Ciklus_tábla.Columns[2].Width = 105;
                Ciklus_tábla.Columns[3].HeaderText = "Elérés";
                Ciklus_tábla.Columns[3].Width = 105;
                Ciklus_tábla.Columns[4].HeaderText = "Verzió";
                Ciklus_tábla.Columns[4].Width = 105;

                int i;
                foreach (Adat_technológia_Ciklus adat in Adatok)
                {

                    Ciklus_tábla.RowCount++;
                    i = Ciklus_tábla.RowCount - 1;
                    Ciklus_tábla.Rows[i].Cells[0].Value = adat.Sorszám;
                    Ciklus_tábla.Rows[i].Cells[1].Value = adat.Fokozat;
                    Ciklus_tábla.Rows[i].Cells[2].Value = adat.Csoportos == 1 ? "Csoportos" : "Egy kocsi";
                    Ciklus_tábla.Rows[i].Cells[3].Value = adat.Elérés;
                    Ciklus_tábla.Rows[i].Cells[4].Value = adat.Verzió;
                }
                Ciklus_tábla.Visible = true;
                Ciklus_tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Ciklus_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                if (!System.IO.File.Exists(hely)) throw new HibásBevittAdat("Nem létezik ehhez az adatbázishoz Ciklus besorolás!");
                if (!int.TryParse(Text_sorszám.Text.Trim(), out int Sorszám)) throw new HibásBevittAdat("A Sorszámnak egész számnak kell lennie");
                KézAdat.Törlés_Ciklus_adat(hely, jelszó, Sorszám);
                Ciklus_Lista();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Ciklus_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Text_sorszám.Text = Ciklus_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                Text_fokozat.Text = Ciklus_tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (Ciklus_tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() == "Csoportos")
                    Chk_csoportos.Checked = true;
                else
                    Chk_Egy.Checked = true;
                Combo_elérés.Text = Ciklus_tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
                TextVerzió.Text = Ciklus_tábla.Rows[e.RowIndex].Cells[4].Value.ToString();
            }

        }

        #endregion


        #region Típus-Típus kapcsolat

        private void Típusok_feltöltése()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT DISTINCT(Valóstípus) FROM állománytábla ";

            List<string> Adatok = new List<string>();
            Kezelő_Jármű KEZJármű = new Kezelő_Jármű();
            Adatok = KEZJármű.List_Jármű_típusok(hely, jelszó, szöveg);

            Combo_JTípus.Items.Clear();

            foreach (string adat in Adatok)
            {
                Combo_JTípus.Items.Add(adat);
            }

        }

        private void Típusok_rögzítése_Click(object sender, EventArgs e)
        {
            try
            {
                if (Combo_JTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus!");
                if (Text_Típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva Technológia!");
                if (List_Típusok.Text.Contains(Combo_JTípus.Text.Trim())) throw new HibásBevittAdat("Van már ilyen típus hozzáadva a technológiához!");


                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                if (!System.IO.File.Exists(hely)) Adatbázis_Létrehozás.Technológia_Adat(hely);
                string jelszó = "Bezzegh";
                string szöveg = "SELECT * FROM típus_tábla ORDER BY id desc";

                Kezelő_Technológia_TípusT Kéz = new Kezelő_Technológia_TípusT();
                List<Adat_Technológia_TípusT> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                long id = 1;
                if (Adatok.Count > 0) id = Adatok.Max(a => a.Id) + 1;

                Adat_Technológia_TípusT Adat = new Adat_Technológia_TípusT(
                    id,
                    Combo_JTípus.Text.Trim()
                    );

                KézAdat.Rögzít_Tech_típus(hely, jelszó, Adat);

                Típus_listázása_kapcs();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Típus_lista_Click(object sender, EventArgs e)
        {
            Típus_listázása_kapcs();
        }

        private void Típus_listázása_kapcs()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT * FROM típus_tábla ORDER BY típus";

                List<Adat_Technológia_TípusT> Adatok = new List<Adat_Technológia_TípusT>();
                Adatok = KézAdat.List_Tech_típus(hely, jelszó, szöveg);

                List_Típusok.Items.Clear();
                foreach (Adat_Technológia_TípusT elem in Adatok)
                {
                    List_Típusok.Items.Add(elem.Típus);
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

        private void Törlés_JTípus_Click(object sender, EventArgs e)
        {
            try
            {
                if (List_Típusok.SelectedItems.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy elem sem a törléshez.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                KézAdat.Törlés_Technológia_Jtípus(hely, jelszó, List_Típusok.SelectedItem.ToString());
                Típus_listázása_kapcs();
            }

            catch (HibásBevittAdat ex)
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


        #region Kivételek kezelése
        void Altípusok_feltöltése()
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
            string jelszó = "Bezzegh";
            if (!System.IO.File.Exists(hely))
                return;

            string szöveg = "SELECT distinct technológia.Altípus FROM technológia WHERE technológia.Altípus<>'_' And technológia.Altípus Is Not Null And technológia.Altípus<>''";
            Kivétel_ALtípus.Items.Clear();
            Kivétel_ALtípus.BeginUpdate();
            Kivétel_ALtípus.Items.Add("");
            Kivétel_ALtípus.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "altípus"));
            Kivétel_ALtípus.EndUpdate();
            Kivétel_ALtípus.Refresh();
        }

        void Pályaszámok_feltöltése()
        {
            if (List_Típusok.Items.Count < 1) return;
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT Azonosító FROM állománytábla WHERE ";
            szöveg += $" valóstípus='{List_Típusok.Items[0].ToString().Trim()}'";
            if (List_Típusok.Items.Count > 1)
            {
                for (int i = 1; i < List_Típusok.Items.Count; i++)
                {
                    szöveg += $" Or valóstípus='{List_Típusok.Items[i].ToString().Trim()}'";
                }
            }
            szöveg += " ORDER BY azonosító";

            Kivétel_Pályaszám.Items.Clear();
            Kivétel_Pályaszám.BeginUpdate();
            Kivétel_Pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "Azonosító"));
            Kivétel_Pályaszám.EndUpdate();
            Kivétel_Pályaszám.Refresh();

        }


        private void KivételekListaFeltöltése()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = $"SELECT * FROM Kivételek";
                if (!System.IO.File.Exists(hely)) return;
                AdatokKiv = KTK_kéz.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kivétel_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT * FROM kivételek Order By azonosító, id";
                if (!System.IO.File.Exists(hely)) return;

                if (Kivétel_Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva egy pályaszám sem.");
                if (Kivétel_ALtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva egy altípus sem.");

                KivételekListaFeltöltése();
                Adat_Technológia_Kivételek Elem = (from a in AdatokKiv
                                                   where a.Altípus == Kivétel_ALtípus.Text.Trim()
                                                   && a.Azonosító == Kivétel_Pályaszám.Text.Trim()
                                                   select a).FirstOrDefault();

                if (Elem == null)
                {
                    // rögzítjuk új elemkésnt megkeressük az utolsó sorszámot;
                    long ID = 1;
                    if (AdatokKiv.Count > 0) ID = AdatokKiv.Max(a => a.Id) + 1;
                    szöveg = $"INSERT INTO kivételek  (id, azonosító, altípus ) VALUES ({ID}, '{Kivétel_Pályaszám.Text.Trim()}', '{Kivétel_ALtípus.Text.Trim()}')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    Kivétel_Tábla_kiírás();
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


        private void Kivétel_Frissít_Click(object sender, EventArgs e)
        {
            Kivétel_Tábla_kiírás();
        }


        void Kivétel_Tábla_kiírás()
        {
            try
            {
                KivételekListaFeltöltése();

                Altípus_tábla.Rows.Clear();
                Altípus_tábla.Columns.Clear();
                Altípus_tábla.Refresh();
                Altípus_tábla.Visible = false;
                Altípus_tábla.ColumnCount = 3;

                // fejléc elkészítése
                Altípus_tábla.Columns[0].HeaderText = "ID";
                Altípus_tábla.Columns[0].Width = 50;
                Altípus_tábla.Columns[1].HeaderText = "Pályaszám";
                Altípus_tábla.Columns[1].Width = 90;
                Altípus_tábla.Columns[2].HeaderText = "Altípus";
                Altípus_tábla.Columns[2].Width = 120;

                List<Adat_Technológia_Kivételek> Adatok = new List<Adat_Technológia_Kivételek>();
                if (Kivétel_ALtípus.Text.Trim() == "")
                    Adatok.AddRange(AdatokKiv);
                else
                    Adatok = (from a in AdatokKiv
                              where a.Altípus == Kivétel_ALtípus.Text.Trim()
                              select a).ToList();

                int i;
                foreach (Adat_Technológia_Kivételek rekord in Adatok)
                {
                    Altípus_tábla.RowCount++;
                    i = Altípus_tábla.RowCount - 1;
                    Altípus_tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Altípus_tábla.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                    Altípus_tábla.Rows[i].Cells[2].Value = rekord.Altípus.Trim();
                }


                Altípus_tábla.Visible = true;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Kivétel_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kivétel_sor == -1) throw new HibásBevittAdat("Nincs kiválasztva a táblázat egy érvényes sora sem a törléshez.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Text_Típus.Text.Trim()}.mdb";
                string jelszó = "Bezzegh";
                if (!System.IO.File.Exists(hely)) return;

                KivételekListaFeltöltése();
                Adat_Technológia_Kivételek Elem = (from a in AdatokKiv
                                                   where a.Id == Kivétel_sor
                                                   select a).FirstOrDefault();


                if (Elem != null)
                {
                    string szöveg = $"DELETE FROM Kivételek WHERE id={Kivétel_sor}";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
                Kivétel_sor = -1;
                Kivétel_Tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Altípus_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Kivétel_sor = int.Parse(Altípus_tábla.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        #endregion
    }
}
