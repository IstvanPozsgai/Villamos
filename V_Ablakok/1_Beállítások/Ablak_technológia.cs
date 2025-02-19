using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_technológia : Form
    {
        readonly Kezelő_Technológia KézAdat = new Kezelő_Technológia();
        readonly Kezelő_Technológia_Ciklus KézCiklus = new Kezelő_Technológia_Ciklus();
        readonly Kezelő_Technológia_Kivételek KTK_kéz = new Kezelő_Technológia_Kivételek();
        readonly Kezelő_Technológia_Alap KézAlap = new Kezelő_Technológia_Alap();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Technológia_TípusT KézTípus = new Kezelő_Technológia_TípusT();
        readonly Kezelő_Alap_Beolvasás KKezelő = new Kezelő_Alap_Beolvasás();

        long Kiválasztott_Sor = -1;
        int Kivétel_sor = -1;
        string Járműtípus_;

        #region Alap
        public Ablak_technológia()
        {
            InitializeComponent();
        }

        private void Ablak_technológia_Load(object sender, EventArgs e)
        {
            Jogosultságkiosztás();
            Típus_feltöltés();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            DátumBef.Value = DateTime.Today;
            DátumKezd.Value = DateTime.Today;
        }

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

        private void Timer1_Tick(object sender, EventArgs e)
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
            CsoportosBefejezés.Enabled = false;
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
                CsoportosBefejezés.Visible = true;
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
                CsoportosBefejezés.Visible = false;
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
                CsoportosBefejezés.Enabled = true;
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

                List<Adat_Technológia_Alap> Adatok = KézAlap.Lista_Adatok();

                Járműtípus.Items.Add("");
                foreach (Adat_Technológia_Alap Elem in Adatok)
                {
                    Járműtípus.Items.Add(Elem.Típus);
                    List_típus.Items.Add(Elem.Típus);
                }
                Járműtípus.Refresh();
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
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Technológia.html";
                Module_Excel.Megnyitás(hely);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Járműtípus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Combo_KarbCiklusEleje.Items.Clear();
            Combo_KarbCiklusVége.Items.Clear();
            if (Járműtípus.Text.Trim() == "") return;

            List<Adat_technológia_Ciklus> Adatok = KézCiklus.Lista_Adatok(Járműtípus.Text.Trim());

            foreach (Adat_technológia_Ciklus Elem in Adatok)
            {
                Combo_KarbCiklusEleje.Items.Add(Elem.Fokozat);
                Combo_KarbCiklusVége.Items.Add(Elem.Fokozat);
            }
            Combo_KarbCiklusEleje.Refresh();
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

        private void Altípus_Feltöltés()
        {
            try
            {
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a járműtípus, ezért nem kerül feltöltése.");

                List<Adat_Technológia_Új> AdatokÖ = KézAdat.Lista_Adatok(Járműtípus.Text.Trim());
                List<string> Adatok = AdatokÖ.Select(a => a.Altípus).Distinct().ToList();

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
        private void Alap_tábla_író()
        {
            try
            {
                //kérdés
                Holtart.Visible = true;
                Holtart.Refresh();
                if (Járműtípus_ == "") throw new HibásBevittAdat("Jármű típushoz tartozó címet választani kell.");
                List<Adat_technológia_Ciklus> AdatokCiklus = KézCiklus.Lista_Adatok(Járműtípus_);

                List<Adat_Technológia_Új> AdatokÖ = KézAdat.Lista_Adatok(Járműtípus_);

                if (Érvényes.Checked)
                    AdatokÖ = (from a in AdatokÖ
                               where a.Érv_kezdete <= DateTime.Now
                               && a.Érv_vége >= DateTime.Now
                               select a).ToList();

                if (Szűr_R_E.Text.Trim() != "")
                    AdatokÖ = (from a in AdatokÖ
                               where a.Részegység == Szűr_R_E.Text.Trim()
                               select a).ToList();

                if (UtasításSzám.Text.Trim() != "")
                    AdatokÖ = (from a in AdatokÖ
                               where a.Munka_utasítás_szám == UtasításSzám.Text.Trim()
                               select a).ToList();

                AdatokÖ = (from a in AdatokÖ
                           orderby a.Részegység, a.Munka_utasítás_szám, a.Utasítás_leírás, a.Érv_kezdete
                           select a).ToList();

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
                foreach (Adat_Technológia_Új adat in AdatokÖ)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["ID"] = adat.ID;
                    Soradat["Rész- egység"] = adat.Részegység;
                    Soradat["Utasítás szám"] = adat.Munka_utasítás_szám;
                    Soradat["Utasítás cím"] = adat.Utasítás_Cím.Replace("\n", " ");
                    Soradat["Utasítás leírása"] = adat.Utasítás_leírás.Replace("\n", " ");
                    Soradat["Paraméterek"] = adat.Paraméter.Replace("\n", " ");
                    Adat_technológia_Ciklus KarbCEleje = AdatokCiklus.Where(a => a.Sorszám == adat.Karb_ciklus_eleje).FirstOrDefault();
                    if (KarbCEleje != null)
                        Soradat["Karb ciklus eleje"] = KarbCEleje.Fokozat;
                    else
                        Soradat["Karb ciklus eleje"] = "";
                    Adat_technológia_Ciklus KarbCVége = AdatokCiklus.Where(a => a.Sorszám == adat.Karb_ciklus_vége).FirstOrDefault();
                    if (KarbCEleje != null)
                        Soradat["Karb ciklus vége"] = KarbCVége.Fokozat;
                    else
                        Soradat["Karb ciklus vége"] = "";
                    Soradat["Érvényesség kezdete"] = adat.Érv_kezdete.ToShortDateString();
                    Soradat["Érvényesség vége"] = adat.Érv_vége.ToShortDateString();
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
                Tábla.Columns["Érvényesség kezdete"].Width = 110;
                Tábla.Columns["Érvényesség vége"].Width = 110;
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
            try
            {
                if (e.RowIndex < 0) return;
                if (!long.TryParse(Tábla.Rows[e.RowIndex].Cells[0].Value.ToString(), out long sorszám)) throw new HibásBevittAdat("Nincs kiválasztva egy sor sem.");
                Egy_adat_Kiírása(sorszám);
                Tábla.Rows[e.RowIndex].Selected = true;
                Kiválasztott_Sor = sorszám;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excel_mentés_Click(object sender, EventArgs e)
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
                    FileName = "Technológia_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla, true);
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
                if (Kiválasztott_Sor == -1) throw new HibásBevittAdat("Nincs kiválasztva egy sor sem.");
                if (Tábla.Rows.Count < 1) throw new HibásBevittAdat("Nincs kitöltve a táblázat.");

                Holtart.Be();
                timer1.Enabled = true;

                List<Adat_Technológia_Új> Adatok = KézAdat.Lista_Adatok(Járműtípus_);
                Adatok = Adatok.Where(a => a.ID >= Kiválasztott_Sor).OrderBy(a => a.ID).ToList();
                KézAdat.Egy_Beszúrás(Járműtípus_, Kiválasztott_Sor, Adatok);

                Holtart.Ki();
                MessageBox.Show("A sor beszúrás elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Alap_tábla_író();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Sor_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kiválasztott_Sor == -1) throw new HibásBevittAdat("Nincs kiválasztva egy sor sem.");
                if (Tábla.Rows.Count < 1) throw new HibásBevittAdat("Nincs kitöltve a táblázat.");

                Holtart.Be();
                timer1.Enabled = true;

                List<Adat_Technológia_Új> Adatok = new List<Adat_Technológia_Új>();
                Adatok = KézAdat.Lista_Adatok(Járműtípus.Text.Trim()).Where(a => a.ID >= Kiválasztott_Sor + 1).OrderBy(a => a.ID).ToList();

                KézAdat.Egy_Törlése(Járműtípus_, Kiválasztott_Sor, Adatok);


                Holtart.Ki();
                MessageBox.Show("A sor törlés elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Alap_tábla_író();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CsoportosBefejezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve a táblázatban egy sor sem.");
                List<long> Sorszámok = new List<long>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    if (long.TryParse(Tábla.SelectedRows[i].Cells[0].Value.ToString(), out long sorszám))
                        Sorszámok.Add(sorszám);
                }

                KézAdat.Befejezés(Járműtípus.Text.Trim(), Sorszámok, DátumBef.Value);
                Alap_tábla_író();
                MessageBox.Show("A kijelölt sorok befejező dátuma átállításra került!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CsoportosKezd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve a táblázatban egy sor sem.");
                List<long> Sorszámok = new List<long>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    if (long.TryParse(Tábla.SelectedRows[i].Cells[0].Value.ToString(), out long sorszám))
                        Sorszámok.Add(sorszám);
                }

                KézAdat.Kezdés(Járműtípus.Text.Trim(), Sorszámok, DátumKezd.Value);
                Alap_tábla_író();
                MessageBox.Show("A kijelölt sorok befejező dátuma átállításra került!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Adatok szerkesztése lapfül
        private void Adat_frissítés_Click(object sender, EventArgs e)
        {
            if (Text_id.Text.Trim() == "") return;
            long sorszám = long.Parse(Text_id.Text.Trim());
            Egy_adat_Kiírása(sorszám);
        }

        public void Egy_adat_Kiírása(long id)
        {
            try
            {
                List<Adat_Technológia_Új> Adatok = KézAdat.Lista_Adatok(Járműtípus.Text.Trim()) ?? throw new HibásBevittAdat($"Nincs még {Járműtípus.Text.Trim()} adatbázis létrehozva.");
                Adat_Technológia_Új Adat = Adatok.Where(a => a.ID == id).FirstOrDefault();

                List<Adat_technológia_Ciklus> AdatokCiklus = KézCiklus.Lista_Adatok(Járműtípus_);
                Adat_technológia_Ciklus KarbCEleje = AdatokCiklus.Where(a => a.Sorszám == Adat.Karb_ciklus_eleje).FirstOrDefault();
                Adat_technológia_Ciklus KarbCVége = AdatokCiklus.Where(a => a.Sorszám == Adat.Karb_ciklus_vége).FirstOrDefault();
                if (Adat != null)
                {
                    Text_id.Text = Adat.ID.ToString();
                    Text_részegység.Text = Adat.Részegység.Trim();
                    Text_Munkautasításszáma.Text = Adat.Munka_utasítás_szám.Trim();
                    Text_UtasításCíme.Text = Adat.Utasítás_Cím.Trim();
                    Rich_UtasításLeírása.Text = Adat.Utasítás_leírás.Trim();
                    Rich_Paraméterek.Text = Adat.Paraméter.Trim();
                    if (KarbCEleje != null)
                        Combo_KarbCiklusEleje.Text = KarbCEleje.Fokozat;
                    else
                        Combo_KarbCiklusEleje.Text = "";
                    if (KarbCEleje != null)
                        Combo_KarbCiklusVége.Text = KarbCVége.Fokozat;
                    else
                        Combo_KarbCiklusVége.Text = "";
                    Date_ÉrvKezdete.Value = Adat.Érv_kezdete;
                    Date_ÉrvVége.Value = Adat.Érv_vége;
                    Text_Szakmai.Text = Adat.Szakmai_bontás.Trim();
                    Text_Munkaterület.Text = Adat.Munkaterületi_bontás.Trim();
                    Combo_Altípus.Text = Adat.Altípus.Trim();
                    Check_Kenés.Checked = Adat.Kenés;
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

        private void Adat_Módosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva/ megadva adatbázis a rögzítéshez.");
                List<Adat_Technológia_Új> AdatokTech = KézAdat.Lista_Adatok(Járműtípus.Text.Trim());
                List<Adat_technológia_Ciklus> AdatokCiklus = KézCiklus.Lista_Adatok(Járműtípus.Text.Trim());

                if (!long.TryParse(Text_id.Text.Trim(), out long id)) id = 0;

                Adat_technológia_Ciklus Elem = AdatokCiklus.Where(a => a.Fokozat == Combo_KarbCiklusEleje.Text.Trim()).FirstOrDefault();
                int AdatCikluse = 0;
                if (Elem != null) AdatCikluse = Elem.Sorszám;

                Elem = AdatokCiklus.Where(a => a.Fokozat == Combo_KarbCiklusEleje.Text.Trim()).FirstOrDefault();
                int AdatCiklusv = 0;
                if (Elem != null) AdatCiklusv = Elem.Sorszám;

                Adat_Technológia_Új Adat = new Adat_Technológia_Új(
                        id,
                        MyF.Szöveg_Tisztítás(Text_részegység.Text.Trim(), 0, 10),
                        MyF.Szöveg_Tisztítás(Text_Munkautasításszáma.Text.Trim(), 0, 10),
                        MyF.Szöveg_Tisztítás(Text_UtasításCíme.Text.Trim(), 0, 250),
                        Rich_UtasításLeírása.Text.Trim(),
                        Rich_Paraméterek.Text.Trim(),
                        AdatCikluse,
                        AdatCiklusv,
                        Date_ÉrvKezdete.Value,
                        Date_ÉrvVége.Value,
                        MyF.Szöveg_Tisztítás(Text_Szakmai.Text.Trim(), 0, 50),
                        MyF.Szöveg_Tisztítás(Text_Munkaterület.Text.Trim(), 0, 50),
                        Combo_Altípus.Text.Trim(),
                        Check_Kenés.Checked
                           );
                KézAdat.Rögzítés(Járműtípus.Text.Trim(), Adat);

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
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva/ megadva adatbázis a rögzítéshez.");
                List<Adat_technológia_Ciklus> Adatok_ciklus = KézCiklus.Lista_Adatok(Járműtípus.Text.Trim());
                if (Adatok_ciklus.Count == 0) throw new HibásBevittAdat("A Karbantartási ciklus adatokat először be kell állítani!");

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
                List<Adat_Alap_Beolvasás> Adatok = KKezelő.Lista_Adatok();
                Adatok = Adatok.Where(a => a.Csoport == "Technológi").ToList();

                string ellenőrző = "";
                foreach (Adat_Alap_Beolvasás A in Adatok)
                    ellenőrző += A.Fejléc.Trim();

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
                List<Adat_Technológia_Új> BeAdatok = new List<Adat_Technológia_Új>();
                for (int i = 2; i <= sormax; i++)
                {
                    if (!bool.TryParse(MyE.Beolvas($"N{i}"), out bool Kenés)) Kenés = false;

                    string Karb_fok = MyE.Beolvas($"G{i}").Trim();
                    int Karb_sor1 = Adatok_ciklus.First(x => x.Fokozat == Karb_fok).Sorszám;

                    Karb_fok = MyE.Beolvas($"H{i}").Trim();
                    int Karb_sor2 = Adatok_ciklus.First(x => x.Fokozat == Karb_fok).Sorszám;


                    Adat_Technológia_Új Adat = new Adat_Technológia_Új(
                        int.Parse(MyE.Beolvas($"A{i}")),
                        MyF.Szöveg_Tisztítás(MyE.Beolvas($"B{i}").Trim(), 0, 10),
                        MyF.Szöveg_Tisztítás(MyE.Beolvas($"C{i}").Trim(), 0, 10),
                        MyF.Szöveg_Tisztítás(MyE.Beolvas($"D{i}").Trim(), 0, 250),
                        MyE.Beolvas($"E{i}").Trim(),
                        MyE.Beolvas($"F{i}").Trim(),
                        Karb_sor1,
                        Karb_sor2,
                        MyE.BeolvasDátum($"I{i}"),
                        MyE.BeolvasDátum($"J{i}"),
                        MyF.Szöveg_Tisztítás(MyE.Beolvas($"K{i}").Trim(), 0, 50),
                        MyF.Szöveg_Tisztítás(MyE.Beolvas($"L{i}").Trim(), 0, 50),
                        MyE.Beolvas($"M{i}").Trim(),
                        Kenés
                        );
                    BeAdatok.Add(Adat);
                    Holtart.Lép();

                }
                KézAdat.Rögzítés(Járműtípus.Text.Trim(), BeAdatok);
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
                if (Text_Típus.Text.Trim().Length > 20) throw new HibásBevittAdat("Azonosító maximum 20 karakter hosszú lehet!");
                if (MyF.Szöveg_Tisztítás(Text_Típus.Text) != Text_Típus.Text) throw new HibásBevittAdat("Nem lehet különleges karakter pl. , ; / stb.");
                Adat_Technológia_Alap Elem = KézAlap.Lista_Adatok().Where(a => a.Típus == Text_Típus.Text.Trim()).FirstOrDefault();
                if (Elem == null)
                {
                    Adat_Technológia_Alap ADAT = new Adat_Technológia_Alap(0, Text_Típus.Text.Trim());
                    KézAlap.Rögzítés(ADAT);
                    MessageBox.Show("Az adatok rögzítése elkészült", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
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
                Járműtípus.Text = List_típus.SelectedItem.ToStrTrim();
                Text_Típus.Text = List_típus.SelectedItem.ToStrTrim();
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

                int csoport;
                if (Chk_csoportos.Checked)
                    csoport = 1;
                else if (Chk_Egy.Checked)
                    csoport = 2;
                else
                    csoport = 3;

                Adat_technológia_Ciklus ADAT = new Adat_technológia_Ciklus(
                                Sorszám,
                                Text_fokozat.Text.Trim(),
                                csoport,
                                Combo_elérés.Text.Trim(),
                                TextVerzió.Text.Trim()
                                );
                KézCiklus.Rögzítés(Text_Típus.Text.Trim(), ADAT);
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
                List<Adat_technológia_Ciklus> Adatok = KézCiklus.Lista_Adatok(Text_Típus.Text.Trim());
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
                if (!int.TryParse(Text_sorszám.Text.Trim(), out int Sorszám)) throw new HibásBevittAdat("A Sorszámnak egész számnak kell lennie");
                KézCiklus.Törlés(Text_Típus.Text.Trim(), Sorszám);

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
            Combo_JTípus.Items.Clear();
            List<Adat_Jármű> AdatokÖ = KézJármű.Lista_Adatok("Főmérnökség");
            List<string> Adatok = AdatokÖ.Select(a => a.Valóstípus).Distinct().OrderBy(a => a).ToList();
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

                Adat_Technológia_Alap ADAT = new Adat_Technológia_Alap(0, Combo_JTípus.Text.Trim());
                KézTípus.Rögzítés(Text_Típus.Text.Trim(), ADAT);
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
                List<Adat_Technológia_Alap> Adatok = KézTípus.Lista_Adatok(Text_Típus.Text.Trim());

                List_Típusok.Items.Clear();
                foreach (Adat_Technológia_Alap elem in Adatok)
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
                if (List_Típusok.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy elem sem a törléshez.");
                KézTípus.Törlés(Text_Típus.Text.Trim(), List_Típusok.SelectedItem.ToString());
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
        private void Altípusok_feltöltése()
        {
            try
            {
                if (Text_Típus.Text.Trim() == "") return;
                List<Adat_Technológia_Új> Adatok = KézAdat.Lista_Adatok(Text_Típus.Text.Trim());
                List<string> Elemek = (from a in Adatok
                                       where a.Altípus != "_"
                                       && a.Altípus != ""
                                       && a.Altípus != null
                                       select a.Altípus).Distinct().ToList();

                Kivétel_ALtípus.Items.Clear();
                Kivétel_ALtípus.Items.Add("");
                foreach (string Elem in Elemek)
                    Kivétel_ALtípus.Items.Add(Elem);

                Kivétel_ALtípus.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pályaszámok_feltöltése()
        {
            try
            {
                if (List_Típusok.Items.Count < 1) return;

                List<Adat_Jármű> AdatokÖ = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();

                for (int i = 0; i < List_Típusok.Items.Count; i++)
                {
                    List<Adat_Jármű> Ideig = AdatokÖ.Where(a => a.Valóstípus == List_Típusok.Items[i].ToString().Trim()).ToList();
                    if (Ideig != null) Adatok.AddRange(Ideig);
                }

                Adatok.OrderBy(a => a.Azonosító);
                Kivétel_Pályaszám.Items.Clear();
                foreach (Adat_Jármű elem in Adatok)
                    Kivétel_Pályaszám.Items.Add(elem.Azonosító);

                Kivétel_Pályaszám.Refresh();
            }
            catch (HibásBevittAdat ex)
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
                if (Kivétel_Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva egy pályaszám sem.");
                if (Kivétel_ALtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva egy altípus sem.");

                List<Adat_Technológia_Kivételek> AdatokKiv = KTK_kéz.Lista_Adatok(Text_Típus.Text.Trim());
                Adat_Technológia_Kivételek Elem = (from a in AdatokKiv
                                                   where a.Altípus == Kivétel_ALtípus.Text.Trim()
                                                   && a.Azonosító == Kivétel_Pályaszám.Text.Trim()
                                                   select a).FirstOrDefault();

                if (Elem == null)
                {
                    Adat_Technológia_Kivételek ADAT = new Adat_Technológia_Kivételek(0, Kivétel_Pályaszám.Text.Trim(), Kivétel_ALtípus.Text.Trim());
                    KTK_kéz.Rögzítés(Text_Típus.Text.Trim(), ADAT);
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

        private void Kivétel_Tábla_kiírás()
        {
            try
            {
                List<Adat_Technológia_Kivételek> AdatokKiv = KTK_kéz.Lista_Adatok(Text_Típus.Text.Trim());

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

                List<Adat_Technológia_Kivételek> AdatokKiv = KTK_kéz.Lista_Adatok(Text_Típus.Text.Trim());
                Adat_Technológia_Kivételek Elem = (from a in AdatokKiv
                                                   where a.Id == Kivétel_sor
                                                   select a).FirstOrDefault();

                if (Elem != null)
                    KTK_kéz.Törlés(Text_Típus.Text.Trim(), Kivétel_sor);
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
