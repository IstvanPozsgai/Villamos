using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Ablakok._4_Nyilvántartások.Takarítás;
using Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public partial class Ablak_Épülettakarítás
    {
        #region Kezelők
        readonly Kezelő_Épület_Naptár KézÉpületNaptár = new Kezelő_Épület_Naptár();
        readonly Kezelő_Épület_Takarításrakijelölt KézTakarításrakijelölt = new Kezelő_Épület_Takarításrakijelölt();
        readonly Kezelő_Épület_Adattábla KézAdatTábla = new Kezelő_Épület_Adattábla();
        readonly Kezelő_Épület_Takarítás_Osztály KézOsztály = new Kezelő_Épület_Takarítás_Osztály();
        readonly Kezelő_Váltós_Naptár KézNaptár = new Kezelő_Váltós_Naptár();
        #endregion


        #region Listákdef
        List<Adat_Épület_Takarításrakijelölt> AdatokKijelöltek = new List<Adat_Épület_Takarításrakijelölt>();
        List<Adat_Épület_Naptár> AdatokÉNaptár = new List<Adat_Épület_Naptár>();
        List<Adat_Épület_Adattábla> AdatokAdatTábla = new List<Adat_Épület_Adattábla>();
        List<Adat_Épület_Takarítás_Osztály> AdatokTakOsztály = new List<Adat_Épület_Takarítás_Osztály>();

        #endregion
        public string Telephely_ = "";
        public DateTime Dátum_ = new DateTime(1900, 1, 1);
        public string fájlexcel_ = "";


        string HelységKód = "";
        string KapcsoltHelység = "";
        int VálasztottElem = -1;

        #region Alap
        public Ablak_Épülettakarítás()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            AlapHelyzet();
        }

        private void AlapHelyzet()
        {
            Idő_lakat.Left = 535;
            Idő_lakat.Top = 95;
            Dátum.Value = DateTime.Today;
            Dátum1.Value = DateTime.Today;
            Dátum2.Value = DateTime.Today;
            LapFülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            LapFülek.SelectedIndex = 0;
            Fülekkitöltése();
            Idő_lakat_működés();
            Lakat_állapot();
        }

        private void Ablak_Épülettakarítás_Load(object sender, EventArgs e)
        {
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false

                Nyitva.Enabled = false;
                Zárva.Enabled = false;
                Terv_Rögzítés.Enabled = false;

                Mentés.Enabled = false;
                Zárva1.Enabled = false;
                Nyitva1.Enabled = false;

                Alap_Rögzít.Enabled = false;

                melyikelem = 234;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Nyitva.Enabled = true;
                    Zárva.Enabled = true;
                    Terv_Rögzítés.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Mentés.Enabled = true;
                    Zárva1.Enabled = true;
                    Nyitva1.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Alap_Rögzít.Enabled = true;
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
                switch (LapFülek.SelectedIndex)
                {
                    case 0:
                        {
                            Osztálylistafeltöltés();
                            break;
                        }
                    case 1:
                        {
                            Szűrésilista1feltöltés();
                            break;
                        }
                    case 2:
                        {
                            Naptárkiirása();
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\épülettakarítás.html";
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

        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
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

        private void LapFülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
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
            catch (HibásBevittAdat ex)
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


        #region Naptár
        private void Naptár_átvétel()
        {
            try
            {
                //ha nincs még kész a munkaidőnaptár akkor, kilépünk
                DateTime eleje = MyF.Hónap_elsőnapja(Dátum2.Value);
                DateTime vége = MyF.Hónap_utolsónapja(Dátum2.Value);
                List<Adat_Váltós_Naptár> Adatok = KézNaptár.Lista_Adatok(Dátum2.Value.Year, "");
                Adatok = (from a in Adatok
                          where a.Dátum >= eleje
                          && a.Dátum <= vége
                          orderby a.Dátum
                          select a).ToList();
                if (Adatok.Count < 1) return;

                string újszöveg = "";
                foreach (Adat_Váltós_Naptár rekord in Adatok)
                {
                    if (rekord.Nap.ToStrTrim() == "1")
                        újszöveg += "1";
                    else
                        újszöveg += "0";
                }

                Adat_Épület_Naptár ADAT = new Adat_Épület_Naptár(
                                false,
                                Dátum2.Value.Month,
                                false,
                                újszöveg.Trim());
                KézÉpületNaptár.Rögzítés(Cmbtelephely.Text.Trim(), Dátum2.Value.Year, ADAT);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Naptárkiirása()
        {
            try
            {
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum2.Value.Year);

                int hónapnap = MyF.Hónap_hossza(Dátum2.Value);

                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum2.Value.Month
                                           select a).FirstOrDefault();
                // ha nincs ilyen akkor átvesszük a munkaidő naptárból ha van.
                if (Elem == null)
                {
                    Naptár_átvétel();
                    AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum2.Value.Year);
                }


                Holtart.Be(hónapnap + 2);

                Naptár_Tábla.Rows.Clear();
                Naptár_Tábla.Refresh();
                Naptár_Tábla.Visible = false;
                Naptár_Tábla.RowCount = hónapnap;
                for (int i = 0; i < hónapnap; i++)
                    Naptár_Tábla.Rows[i].Cells[0].Value = i + 1;


                Adat_Épület_Naptár rekord = (from a in AdatokÉNaptár
                                             where a.Hónap == Dátum2.Value.Month
                                             select a).FirstOrDefault();
                if (rekord != null)
                {
                    for (int i = 0; i < hónapnap; i++)
                    {
                        if (rekord.Napok.Substring(i, 1) == "1")
                        {
                            Naptár_Tábla.Rows[i].Cells["Munkanap"].Value = true;
                            Naptár_Tábla.Rows[i].Cells["Hétvége"].Value = false;
                        }
                        else
                        {
                            Naptár_Tábla.Rows[i].Cells["Munkanap"].Value = false;
                            Naptár_Tábla.Rows[i].Cells["Hétvége"].Value = true;
                        }
                    }
                    Holtart.Lép();
                }
                Naptár_Tábla.Visible = true;
                Naptár_Tábla.Refresh();
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

        private void Táblázat_frissítése_Click(object sender, EventArgs e)
        {
            Naptárkiirása();
        }

        private void Naptár_Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Naptár_Tábla.RowCount < 0) return;
                foreach (DataGridViewRow row in Naptár_Tábla.Rows)
                {

                    if (row.Cells["Hétvége"].Value != null && bool.Parse(row.Cells["Hétvége"].Value.ToString()) == true)
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f);
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

        private void Dátum2_ValueChanged(object sender, EventArgs e)
        {
            Naptárkiirása();
        }

        private void Alap_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum2.Value.Year);

                int hónapnap = MyF.Hónap_hossza(Dátum2.Value);
                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum2.Value.Month
                                           select a).FirstOrDefault();
                string szöveg1 = "";
                for (int i = 0; i < hónapnap; i++)
                {
                    if (Naptár_Tábla.Rows[i].Cells["Hétvége"].Value != null && bool.Parse(Naptár_Tábla.Rows[i].Cells["Hétvége"].Value.ToString()) == true)
                        szöveg1 += "0";
                    else
                        szöveg1 += "1";
                }

                if (Elem == null)
                {
                    // új
                    Adat_Épület_Naptár Adat = new Adat_Épület_Naptár(false, Dátum2.Value.Month, false, szöveg1);
                    KézÉpületNaptár.Rögzítés(Cmbtelephely.Text.Trim(), Dátum2.Value.Year, Adat);
                }
                else
                {
                    // módosít
                    KézÉpületNaptár.Módosítás(Cmbtelephely.Text.Trim(), Dátum2.Value.Year, szöveg1, Dátum2.Value.Month);
                }

                Naptárkiirása();
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
        #endregion


        #region Havi elkészülés rögzítés
        private void Command2_Click(object sender, EventArgs e)
        {
            Épület_tábla_lista();
            Gombokfel2();
        }

        private void Frissítiadarabszámokat()
        {
            try
            {
                Holtart.Be();
                List<Adat_Épület_Takarításrakijelölt> AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum1.Value.Year);
                AdatokKijelöltek = (from a in AdatokKijelöltek
                                    where a.Hónap == Dátum1.Value.Month
                                    select a).ToList();

                List<Adat_Épület_Takarításrakijelölt> AdatokGY = new List<Adat_Épület_Takarításrakijelölt>();
                foreach (Adat_Épület_Takarításrakijelölt rekord in AdatokKijelöltek)
                {
                    int E1db = rekord.E1rekijelölt.Count(c => c == '1'); ;
                    int E2db = rekord.E2rekijelölt.Count(c => c == '1'); ;
                    int E3db = rekord.E3rekijelölt.Count(c => c == '1'); ;
                    Adat_Épület_Takarításrakijelölt Adat = new Adat_Épület_Takarításrakijelölt(E1db, E2db, E3db, rekord.Helységkód.Trim(), Dátum1.Value.Month);
                    AdatokGY.Add(Adat);
                    Holtart.Lép();
                }
                KézTakarításrakijelölt.Módosítás(Cmbtelephely.Text.Trim(), Dátum1.Value.Year, AdatokGY);
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

        private void Szűrésilista1feltöltés()
        {
            try
            {
                AdatokTakOsztály = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokTakOsztály = (from a in AdatokTakOsztály
                                    where a.Státus == false
                                    orderby a.Id
                                    select a).ToList();
                List1.Items.Clear();
                List1.Items.Add("<Összes>");

                foreach (Adat_Épület_Takarítás_Osztály Elem in AdatokTakOsztály)
                    List1.Items.Add(Elem.Osztály);

                List1.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Épület_tábla_lista()
        {
            try
            {
                Frissítiadarabszámokat();
                AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum1.Value.Year);

                // kilistázzuk a adatbázis adatait
                AdatokAdatTábla = KézAdatTábla.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokAdatTábla = AdatokAdatTábla.Where(a => a.Státus == false).ToList();

                List<Adat_Épület_Adattábla> Adatok = new List<Adat_Épület_Adattábla>();
                // ha nem összes
                if (!List1.GetItemChecked(0))
                {
                    // további típus szűrés
                    for (int j = 0; j < List1.CheckedItems.Count; j++)
                    {
                        List<Adat_Épület_Adattábla> Ideig = (from a in AdatokAdatTábla
                                                             where a.Osztály == List1.Items[j].ToStrTrim()
                                                             select a).ToList();
                        Adatok.AddRange(Ideig);
                    }
                }
                else
                    Adatok.AddRange(AdatokAdatTábla);
                Adatok = Adatok.OrderBy(a => a.ID).ToList();

                Holtart.Be(20);
                {
                    Tábla1.Rows.Clear();
                    Tábla1.Columns.Clear();
                    Tábla1.Refresh();
                    Tábla1.Visible = false;
                    Tábla1.ColumnCount = 10;

                    // fejléc elkészítése

                    Tábla1.Columns[0].HeaderText = "Épületkód";
                    Tábla1.Columns[0].Width = 80;
                    Tábla1.Columns[0].ReadOnly = true;
                    Tábla1.Columns[1].HeaderText = "Sorszám";
                    Tábla1.Columns[1].Width = 80;
                    Tábla1.Columns[1].ReadOnly = true;
                    Tábla1.Columns[2].HeaderText = "Osztály";
                    Tábla1.Columns[2].Width = 300;
                    Tábla1.Columns[2].ReadOnly = true;
                    Tábla1.Columns[3].HeaderText = "Megnevezés";
                    Tábla1.Columns[3].Width = 300;
                    Tábla1.Columns[3].ReadOnly = true;
                    Tábla1.Columns[4].HeaderText = "E1 db";
                    Tábla1.Columns[4].Width = 80;
                    Tábla1.Columns[4].ReadOnly = true;
                    Tábla1.Columns[5].HeaderText = "E2 db";
                    Tábla1.Columns[5].Width = 80;
                    Tábla1.Columns[5].ReadOnly = true;
                    Tábla1.Columns[6].HeaderText = "E3 db";
                    Tábla1.Columns[6].Width = 80;
                    Tábla1.Columns[6].ReadOnly = true;
                    Tábla1.Columns[7].HeaderText = "E1 kész db";
                    Tábla1.Columns[7].Width = 80;
                    Tábla1.Columns[8].HeaderText = "E2 kész db";
                    Tábla1.Columns[8].Width = 80;
                    Tábla1.Columns[9].HeaderText = "E3 kész db";
                    Tábla1.Columns[9].Width = 80;

                    foreach (Adat_Épület_Adattábla rekord in Adatok)
                    {

                        Tábla1.RowCount++;
                        int i = Tábla1.RowCount - 1;
                        Tábla1.Rows[i].Cells[0].Value = rekord.Helységkód.Trim();
                        Tábla1.Rows[i].Cells[1].Value = int.TryParse(rekord.Helységkód.ToString().Replace("E", ""), out int Sorszám) ? Sorszám : 0;
                        Tábla1.Rows[i].Cells[2].Value = rekord.Osztály.Trim();
                        Tábla1.Rows[i].Cells[3].Value = rekord.Megnevezés.Trim();
                        Adat_Épület_Takarításrakijelölt Adat = (from a in AdatokKijelöltek
                                                                where a.Helységkód.Trim() == rekord.Helységkód.Trim()
                                                                && a.Hónap == Dátum1.Value.Month
                                                                select a).FirstOrDefault();
                        if (Adat != null)
                        {
                            Tábla1.Rows[i].Cells[4].Value = Adat.E1kijelöltdb;
                            Tábla1.Rows[i].Cells[5].Value = Adat.E2kijelöltdb;
                            Tábla1.Rows[i].Cells[6].Value = Adat.E3kijelöltdb;
                            Tábla1.Rows[i].Cells[7].Value = Adat.E1elvégzettdb;
                            Tábla1.Rows[i].Cells[8].Value = Adat.E2elvégzettdb;
                            Tábla1.Rows[i].Cells[9].Value = Adat.E3elvégzettdb;
                        }
                        else
                        {
                            Tábla1.Rows[i].Cells[4].Value = 0;
                            Tábla1.Rows[i].Cells[5].Value = 0;
                            Tábla1.Rows[i].Cells[6].Value = 0;
                            Tábla1.Rows[i].Cells[7].Value = 0;
                            Tábla1.Rows[i].Cells[8].Value = 0;
                            Tábla1.Rows[i].Cells[9].Value = 0;
                        }

                        Holtart.Lép();
                    }
                    Tábla1.Visible = true;
                    Tábla1.Refresh();
                }

                Call_fel1_Click();
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

        private void Le1_Click(object sender, EventArgs e)
        {
            Fel1.Visible = true;
            Le1.Visible = false;
            List1.Height = 400;
        }

        private void Fel1_Click(object sender, EventArgs e)
        {
            Call_fel1_Click();
        }

        private void Call_fel1_Click()
        {
            Le1.Visible = true;
            Fel1.Visible = false;
            List1.Height = 25;
        }

        private void Zárva1_Click(object sender, EventArgs e)
        {
            try
            {
                // Hónap lezárása
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum1.Value.Year);

                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum1.Value.Month
                                           select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézÉpületNaptár.Módosítás_igazolás(Cmbtelephely.Text.Trim(), Dátum1.Value.Year, true, Dátum1.Value.Month);
                }
                else
                {
                    Adat_Épület_Naptár Adat = new Adat_Épület_Naptár(
                        false,
                        Dátum1.Value.Month,
                        true,
                        "0000000000000000000000000000000");
                    KézÉpületNaptár.Rögzítés(Cmbtelephely.Text.Trim(), Dátum1.Value.Year, Adat);
                }
                Gombokfel2();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nyitva1_Click(object sender, EventArgs e)
        {
            try
            {
                // Hónap lezárása
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum1.Value.Year);

                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum1.Value.Month
                                           select a).FirstOrDefault();
                if (Elem != null)
                {
                    KézÉpületNaptár.Módosítás_igazolás(Cmbtelephely.Text.Trim(), Dátum1.Value.Year, false, Dátum1.Value.Month);
                }
                else
                {
                    Adat_Épület_Naptár Adat = new Adat_Épület_Naptár(
                                false,
                                Dátum1.Value.Month,
                                false,
                                "0000000000000000000000000000000");
                    KézÉpületNaptár.Rögzítés(Cmbtelephely.Text.Trim(), Dátum1.Value.Year, Adat);
                }
                Gombokfel2();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Gombokfel2()
        {
            try
            {
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum1.Value.Year);
                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum1.Value.Month
                                           select a).FirstOrDefault();

                if (Elem != null)
                {
                    if (Elem.Igazolás)
                    {
                        Nyitva1.Visible = true;
                        Zárva1.Visible = false;
                        Mentés.Visible = true;
                        Command9.Visible = false;
                        Command10.Visible = false;
                        Opció_Megrendelés.Visible = false;
                        Opció_kifizetés.Visible = false;
                    }
                    else
                    {
                        Zárva1.Visible = true;
                        Nyitva1.Visible = false;
                        Mentés.Visible = false;
                        Command9.Visible = true;
                        Command10.Visible = true;
                        Opció_Megrendelés.Visible = true;
                        Opció_kifizetés.Visible = true;
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

        private void ValidateKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((char)(e.KeyChar) >= 48 && (char)(e.KeyChar) <= 57))
            {
                MessageBox.Show("Csak számot lehet beírni!");
                e.Handled = true;
            }
        }

        private void Tábla1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (Tábla1.CurrentCell.ColumnIndex > 5) // put columnindextovalidate
                {
                    e.Control.KeyPress -= ValidateKeyPress;
                    e.Control.KeyPress -= ValidateKeyPress;
                    e.Control.KeyPress += ValidateKeyPress;
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

        private void Tábla1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (Tábla1.RowCount < 0) return;
            {
                foreach (DataGridViewRow row in Tábla1.Rows)
                {
                    if (row.Cells[7].Value == null) row.Cells[7].Value = 0;
                    if (row.Cells[8].Value == null) row.Cells[8].Value = 0;
                    if (row.Cells[9].Value == null) row.Cells[9].Value = 0;


                    if (row.Cells[4].Value.ToString() == row.Cells[7].Value.ToString())
                        row.Cells[7].Style.BackColor = Color.Green;
                    if (row.Cells[5].Value.ToString() == row.Cells[8].Value.ToString())
                        row.Cells[8].Style.BackColor = Color.Green;
                    if (row.Cells[6].Value.ToString() == row.Cells[9].Value.ToString())
                        row.Cells[9].Style.BackColor = Color.Green;

                    if (row.Cells[4].Value.ToString() != row.Cells[7].Value.ToString())
                        row.Cells[7].Style.BackColor = Color.IndianRed;
                    if (row.Cells[5].Value.ToString() != row.Cells[8].Value.ToString())
                        row.Cells[8].Style.BackColor = Color.IndianRed;
                    if (row.Cells[6].Value.ToString() != row.Cells[9].Value.ToString())
                        row.Cells[9].Style.BackColor = Color.IndianRed;
                }
            }
        }

        private void Dátum1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Gombokfel2();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count < 1) return;
                Holtart.Be(Tábla1.Rows.Count + 2);

                List<Adat_Épület_Takarításrakijelölt> AdatokGy = new List<Adat_Épület_Takarításrakijelölt>();
                for (int i = 0; i < Tábla1.Rows.Count; i++)
                {
                    Adat_Épület_Takarításrakijelölt Adat = new Adat_Épület_Takarításrakijelölt(
                        Tábla1.Rows[i].Cells[0].Value.ToStrTrim(),
                        Dátum1.Value.Month,
                        Tábla1.Rows[i].Cells[7].Value.ToÉrt_Int(),
                        Tábla1.Rows[i].Cells[8].Value.ToÉrt_Int(),
                        Tábla1.Rows[i].Cells[9].Value.ToÉrt_Int());
                    AdatokGy.Add(Adat);
                    Holtart.Lép();
                }
                KézTakarításrakijelölt.Módosítás(Dátum1.Value.Year, Cmbtelephely.Text.Trim(), AdatokGy);
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // kimeneti fájl helye és neve
                string fájlexc;

                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Takarítási teljesítési igazolás Terv-Tény-Eltérés",
                    FileName = "Takarítási_teljesítési_igazolás_TTE_" + Dátum1.Value.ToString("yyyyMM"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();


                // *********************************************
                // ********* Osztály tábla *********************
                // *********************************************
                // fejléc elkészítése
                MyE.Kiir("Megnevezés", "a1");
                MyE.Kiir("E1 Egységár [db]", "c1");
                MyE.Kiir("E2 Egységár [Ft/m2]", "d1");
                MyE.Kiir("E3 Egységár [Ft/m2]", "e1");

                int sor = 2;
                int idE1db;
                int idE2db;
                int idE3db;
                int idE1dbv;
                int idE2dbv;
                int idE3dbv;

                AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum1.Value.Year);
                AdatokTakOsztály = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokTakOsztály = (from a in AdatokTakOsztály
                                    where a.Státus == false
                                    orderby a.Id
                                    select a).ToList();

                Holtart.Be(20);

                if (AdatokTakOsztály != null)
                {
                    foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokTakOsztály)
                    {
                        MyE.Kiir(rekord.Osztály.Trim(), "a" + sor.ToString());
                        MyE.Kiir(rekord.E1Ft.ToString().Replace(",", "."), "c" + sor.ToString());
                        MyE.Kiir(rekord.E2Ft.ToString().Replace(",", "."), "d" + sor.ToString());
                        MyE.Kiir(rekord.E3Ft.ToString().Replace(",", "."), "e" + sor.ToString());
                        Holtart.Lép();
                        sor += 1;
                    }
                }
                string munkalap = "Munka1";
                MyE.Oszlopszélesség(munkalap, "A:A");
                MyE.Oszlopszélesség(munkalap, "B:B");
                MyE.Oszlopszélesség(munkalap, "C:E");
                MyE.OszlopRejtés(munkalap, "B:B");
                MyE.Rácsoz("a1:e" + (sor - 1).ToString());
                MyE.Munkalap_átnevezés(munkalap, "Összesítő");
                munkalap = Cmbtelephely.Text.Trim();
                MyE.Új_munkalap(Cmbtelephely.Text.Trim());
                MyE.Munkalap_betű("Calibri", 10);

                // ************************************************
                // ************ fejléc elkészítése  ***************
                // ************************************************
                MyE.Egyesít(munkalap, "b1:b2");
                MyE.Kiir("Helyiség", "b1");
                MyE.Egyesít(munkalap, "c1:c2");
                MyE.Kiir("Alapterület [m2]", "c1");
                MyE.Egyesít(munkalap, "d1:o1");
                MyE.Kiir("Megrendelt- Teljesített- Eltérés mennyiségek", "d1");
                MyE.Kiir("Szolgálatási jegyzék kódja", "d2");
                MyE.Kiir("Megrendelt mennyiség", "e2");
                MyE.Kiir("Teljesített mennyiség", "f2");
                MyE.Kiir("Eltérés mennyiség", "g2");

                MyE.Kiir("Szolgálatási jegyzék kódja", "h2");
                MyE.Kiir("Megrendelt mennyiség", "i2");
                MyE.Kiir("Teljesített mennyiség", "j2");
                MyE.Kiir("Eltérés mennyiség", "k2");


                MyE.Kiir("Szolgálatási jegyzék kódja", "l2");
                MyE.Kiir("Megrendelt mennyiség", "m2");
                MyE.Kiir("Teljesített mennyiség", "n2");
                MyE.Kiir("Eltérés mennyiség", "o2");
                MyE.Egyesít(munkalap, "p1:p2");
                MyE.Kiir("E1 Egységár [Ft/alkalom]", "p1");
                MyE.Sortörésseltöbbsorba_egyesített("p1:p2");

                MyE.Egyesít(munkalap, "q1:q2");
                MyE.Kiir("E2 Egységár [Ft/alkalom]", "q1");
                MyE.Sortörésseltöbbsorba_egyesített("q1:q2");

                MyE.Egyesít(munkalap, "r1:r2");
                MyE.Kiir("E3 Egységár [Ft/alkalom]", "r1");
                MyE.Sortörésseltöbbsorba_egyesített("r1:r2");

                MyE.Egyesít(munkalap, "s1:s2");
                MyE.Kiir("Megrendelt E1 érték", "s1");
                MyE.Sortörésseltöbbsorba_egyesített("s1:s2");

                MyE.Egyesít(munkalap, "t1:t2");
                MyE.Kiir("Teljesített E1 érték", "t1");
                MyE.Sortörésseltöbbsorba_egyesített("t1:t2");

                MyE.Egyesít(munkalap, "u1:u2");
                MyE.Kiir("Eltérés E1 érték", "u1");
                MyE.Sortörésseltöbbsorba_egyesített("u1:u2");

                MyE.Egyesít(munkalap, "v1:v2");
                MyE.Kiir("Megrendelt E2 érték", "v1");
                MyE.Sortörésseltöbbsorba_egyesített("v1:v2");

                MyE.Egyesít(munkalap, "w1:w2");
                MyE.Kiir("Teljesített E2 érték", "w1");
                MyE.Sortörésseltöbbsorba_egyesített("w1:w2");

                MyE.Egyesít(munkalap, "x1:x2");
                MyE.Kiir("Eltérés E2 érték", "x1");
                MyE.Sortörésseltöbbsorba_egyesített("x1:x2");

                MyE.Egyesít(munkalap, "y1:y2");
                MyE.Kiir("Megrendelt E3 érték", "y1");
                MyE.Sortörésseltöbbsorba_egyesített("y1:y2");

                MyE.Egyesít(munkalap, "z1:z2");
                MyE.Kiir("Teljesített E3 érték", "z1");
                MyE.Sortörésseltöbbsorba_egyesített("z1:z2");

                MyE.Egyesít(munkalap, "aa1:aa2");
                MyE.Kiir("Eltérés E3 érték", "aa1");
                MyE.Sortörésseltöbbsorba_egyesített("aa1:aa2");

                MyE.Egyesít(munkalap, "ab1:ab2");
                MyE.Kiir("Megrendelt Összesen", "ab1");
                MyE.Sortörésseltöbbsorba_egyesített("ab1:ab2");

                MyE.Egyesít(munkalap, "ac1:ac2");
                MyE.Kiir("Teljesített Összesen", "ac1");
                MyE.Sortörésseltöbbsorba_egyesített("ac1:ac2");

                MyE.Egyesít(munkalap, "ad1:ad2");
                MyE.Kiir("Eltérés Összesen", "ad1");
                MyE.Sortörésseltöbbsorba_egyesített("ad1:ad2");

                MyE.Egyesít(munkalap, "ae1:ae2");
                MyE.Kiir("Szemetes", "ae1");
                MyE.Sortörésseltöbbsorba_egyesített("ae1:ae2");

                MyE.Egyesít(munkalap, "af1:af2");
                MyE.Kiir("Helység kapcsolat", "af1");
                MyE.Sortörésseltöbbsorba_egyesített("af1:af2");

                MyE.Sormagasság("1:1", 47);
                MyE.Sormagasság("2:2", 39);
                MyE.Oszlopszélesség(munkalap, "B:B", 46);
                MyE.Oszlopszélesség(munkalap, "c:o", 11);

                MyE.Sortörésseltöbbsorba("c1");
                MyE.Sortörésseltöbbsorba("d2:o2");
                MyE.Oszlopszélesség(munkalap, "A:A");

                // a táblázat érdemi része
                sor = 2;

                AdatokAdatTábla = KézAdatTábla.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Épület_Takarításrakijelölt EgyA;

                if (AdatokTakOsztály != null)
                {
                    foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokTakOsztály)
                    {
                        sor += 1;
                        MyE.Egyesít(munkalap, "b" + sor.ToString() + ":af" + sor.ToString());
                        MyE.Igazít_vízszintes("b" + sor.ToString() + ":p" + sor.ToString(), "bal");
                        MyE.Háttérszín("b" + sor.ToString() + ":af" + sor.ToString(), 13434828L);
                        MyE.Kiir(rekord.Osztály.Trim(), "b" + sor.ToString());
                        MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 20);

                        List<Adat_Épület_Adattábla> AdatokAlapSzűrt = (from a in AdatokAdatTábla
                                                                       where a.Státus == false
                                                                       && a.Osztály == rekord.Osztály.Trim()
                                                                       orderby a.ID
                                                                       select a).ToList();

                        if (AdatokAlapSzűrt != null)
                        {
                            foreach (Adat_Épület_Adattábla rekord1 in AdatokAlapSzűrt)
                            {
                                sor += 1;
                                MyE.Kiir(rekord1.Osztály.Trim(), "A" + sor.ToString());
                                MyE.Kiir(rekord1.Megnevezés.Trim(), "b" + sor.ToString());
                                MyE.Kiir(rekord1.Méret.ToString().Replace(",", "."), "c" + sor.ToString());
                                MyE.Kiir("E1", "d" + sor.ToString());
                                MyE.Kiir("E2", "h" + sor.ToString());
                                MyE.Kiir("E3", "l" + sor.ToString());
                                if (rekord1.Szemetes)
                                    MyE.Kiir("Van", "ae" + sor.ToString());

                                if (rekord1.Kapcsolthelység != null)
                                    MyE.Kiir(rekord1.Kapcsolthelység.Trim(), "af" + sor.ToString());
                                // megrendelt
                                idE1db = 0;
                                idE2db = 0;
                                idE3db = 0;
                                // elvégzett
                                idE1dbv = 0;
                                idE2dbv = 0;
                                idE3dbv = 0;

                                EgyA = (from a in AdatokKijelöltek
                                        where a.Hónap == Dátum1.Value.Month
                                        && a.Helységkód == rekord1.Helységkód.Trim()
                                        select a).FirstOrDefault();

                                if (EgyA != null)
                                {
                                    idE1db = EgyA.E1kijelöltdb;
                                    idE2db = EgyA.E2kijelöltdb;
                                    idE3db = EgyA.E3kijelöltdb;
                                    idE1dbv = EgyA.E1elvégzettdb;
                                    idE2dbv = EgyA.E2elvégzettdb;
                                    idE3dbv = EgyA.E3elvégzettdb;
                                }
                                MyE.Kiir(idE1db.ToString(), "e" + sor.ToString());
                                MyE.Kiir(idE2db.ToString(), "i" + sor.ToString());
                                MyE.Kiir(idE3db.ToString(), "m" + sor.ToString());
                                MyE.Kiir(idE1dbv.ToString(), "f" + sor.ToString());
                                MyE.Kiir(idE2dbv.ToString(), "j" + sor.ToString());
                                MyE.Kiir(idE3dbv.ToString(), "n" + sor.ToString());
                                MyE.Kiir(rekord.E1Ft.ToString().Replace(",", "."), "p" + sor.ToString());
                                MyE.Kiir((rekord.E2Ft * rekord1.Méret).ToString().Replace(",", "."), "q" + sor.ToString());
                                MyE.Kiir((rekord.E3Ft * rekord1.Méret).ToString().Replace(",", "."), "r" + sor.ToString());
                                MyE.Kiir("=RC[-3]*RC[-14]", "s" + sor.ToString());
                                MyE.Kiir("=RC[-4]*RC[-14]", "t" + sor.ToString());
                                MyE.Kiir("=RC[-5]*RC[-14]", "u" + sor.ToString());
                                MyE.Kiir("=RC[-5]*RC[-13]", "v" + sor.ToString());
                                MyE.Kiir("=RC[-6]*RC[-13]", "w" + sor.ToString());
                                MyE.Kiir("=RC[-7]*RC[-13]", "x" + sor.ToString());
                                MyE.Kiir("=RC[-7]*RC[-12]", "y" + sor.ToString());
                                MyE.Kiir("=RC[-8]*RC[-12]", "z" + sor.ToString());
                                MyE.Kiir("=RC[-9]*RC[-12]", "aa" + sor.ToString());
                                MyE.Kiir("=RC[-9]+RC[-6]+RC[-3]", "ab" + sor.ToString());
                                MyE.Kiir("=RC[-9]+RC[-6]+RC[-3]", "ac" + sor.ToString());
                                MyE.Kiir("=RC[-9]+RC[-6]+RC[-3]", "ad" + sor.ToString());
                                MyE.Kiir("=RC[-2]-RC[-1]", "g" + sor.ToString());
                                MyE.Kiir("=RC[-2]-RC[-1]", "k" + sor.ToString());
                                MyE.Kiir("=RC[-2]-RC[-1]", "o" + sor.ToString());
                            }
                        }
                        Holtart.Lép();
                    }
                }

                // 'összesítő sor
                sor += 1;
                MyE.Igazít_vízszintes("b" + sor.ToString() + ":af" + sor.ToString(), "bal");
                MyE.Háttérszín("b" + sor.ToString() + ":af" + sor.ToString(), 13434828L);
                MyE.Egyesít(munkalap, "b" + sor.ToString() + ":r" + sor.ToString());
                MyE.Kiir(Cmbtelephely.Text.Trim() + " Összesen/hó", "b" + sor.ToString() + ":r" + sor.ToString());
                MyE.Betű("b" + sor.ToString() + ":o" + sor.ToString(), false, false, true);
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "s" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "t" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "u" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "v" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "w" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "x" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "y" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "z" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "aa" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "ab" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "ac" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C:R[-1]C)", "ad" + sor.ToString());
                MyE.Rácsoz("b1:af" + sor.ToString());
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 25);
                MyE.OszlopRejtés(munkalap, "A:A");

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
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

        private async void Command9_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                DateTime Eleje = DateTime.Now;
                Telephely_ = Cmbtelephely.Text.Trim();
                Dátum_ = Dátum1.Value;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Jármű Takarítási teljesítési igazolás készítés",
                    FileName = $"Épület_TIG_{Dátum1.Value.Year}_év_{Dátum1.Value:MMMM}_hó_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexcel_ = SaveFileDialog1.FileName;
                else
                    return;

                timer1.Enabled = true;
                await Task.Run(() =>
                {
                    Takarítás_teljesítés_Igazolás Fájl = new Takarítás_teljesítés_Igazolás(Dátum_, false, Telephely_);
                    Fájl.ExcelÉpületTábla(fájlexcel_);
                });
                timer1.Enabled = false;
                Holtart.Ki();
                DateTime Vége = DateTime.Now;
                MessageBox.Show($"A feladat {Vége - Eleje} idő alatt végrehajtásra került.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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
        #endregion


        #region E1_E2_E3
        private void E1Munkanap_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                {
                    if (Tábla_terv.Rows[i].Cells[4].Value != null && !bool.Parse(Tábla_terv.Rows[i].Cells[4].Value.ToString()))
                        Tábla_terv.Rows[i].Cells[1].Value = true;
                    else
                        Tábla_terv.Rows[i].Cells[1].Value = false;
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

        private void E2Munkanap_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                {
                    if (Tábla_terv.Rows[i].Cells[4].Value != null && !bool.Parse(Tábla_terv.Rows[i].Cells[4].Value.ToString()))
                        Tábla_terv.Rows[i].Cells[2].Value = true;
                    else
                        Tábla_terv.Rows[i].Cells[2].Value = false;

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

        private void E3Munkanap_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                {
                    if (Tábla_terv.Rows[i].Cells[4].Value != null && !bool.Parse(Tábla_terv.Rows[i].Cells[4].Value.ToString()))
                        Tábla_terv.Rows[i].Cells[3].Value = true;
                    else
                        Tábla_terv.Rows[i].Cells[3].Value = false;

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

        private void E1MindenNap_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                    Tábla_terv.Rows[i].Cells[1].Value = true;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void E2MindenNap_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                    Tábla_terv.Rows[i].Cells[2].Value = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void E3MindenNap_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                    Tábla_terv.Rows[i].Cells[3].Value = true;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void E1Minden_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                    Tábla_terv.Rows[i].Cells[1].Value = false;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void E2Minden_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                    Tábla_terv.Rows[i].Cells[2].Value = false;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void E3Minden_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_terv.Rows.Count < 1) return;

                for (int i = 0; i < Tábla_terv.Rows.Count; i++)
                    Tábla_terv.Rows[i].Cells[3].Value = false;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Command14_Click(object sender, EventArgs e)
        {
            if (Tábla_terv.Rows.Count < 1) return;

            for (int i = 0; i < Tábla_terv.Rows.Count; i++)
            {
                Tábla_terv.Rows[i].Cells[1].Value = false;
                Tábla_terv.Rows[i].Cells[2].Value = false;
                Tábla_terv.Rows[i].Cells[3].Value = false;
            }

            Osztálylista_minden_töröl();
        }
        #endregion


        #region Osztály_gombok
        private void Nyit_Click(object sender, EventArgs e)
        {
            Osztálylista.Height = 400;
        }

        private void Csuk_Click(object sender, EventArgs e)
        {
            Osztálylista.Height = 25;
        }

        private void Jelöltcsoport_Click(object sender, EventArgs e)
        {
            Jelöltcsoport_pipálás();
            Tábla_terv_Ürítés();
        }

        private void Jelöltcsoport_pipálás()
        {
            try
            {
                Helyiséglista.Items.Clear();
                AdatokAdatTábla = KézAdatTábla.Lista_Adatok(Cmbtelephely.Text.Trim());

                for (int i = 0; i < Osztálylista.Items.Count; i++)
                {
                    if (Osztálylista.GetItemChecked(i) == true)
                    {
                        // osztálylistatagokat kiválogatja
                        List<Adat_Épület_Adattábla> AdatokSzűrt = (from a in AdatokAdatTábla
                                                                   where a.Státus == false
                                                                   && a.Osztály == Osztálylista.Items[i].ToStrTrim()
                                                                   orderby a.ID
                                                                   select a).ToList();

                        foreach (Adat_Épület_Adattábla rekord in AdatokSzűrt)
                        {
                            Helyiséglista.Items.Add(rekord.Helységkód.Trim() + " - " + rekord.Megnevezés.Trim());
                        }
                    }
                }
                Osztálylista.Height = 25;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Csoportkijelöltmind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Osztálylista.Items.Count; i++)
                Osztálylista.SetItemChecked(i, true);
            Osztálylista.Height = 25;
            Jelöltcsoport_pipálás();
            Alapra_állít();
        }

        private void CsoportVissza_Click(object sender, EventArgs e)
        {
            Osztálylista_minden_töröl();
            Alapra_állít();
        }

        private void Osztálylistafeltöltés()
        {
            try
            {
                Osztálylista.Items.Clear();
                AdatokTakOsztály = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokTakOsztály = (from a in AdatokTakOsztály
                                    where a.Státus == false
                                    orderby a.Id
                                    select a).ToList();
                foreach (Adat_Épület_Takarítás_Osztály Elem in AdatokTakOsztály)
                    Osztálylista.Items.Add(Elem.Osztály);

                Osztálylista.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Osztálylista_minden_töröl()
        {
            try
            {
                for (int i = 0; i < Osztálylista.Items.Count; i++)
                    Osztálylista.SetItemChecked(i, false);
                Osztálylista.Height = 25;
                Jelöltcsoport_pipálás();
            }
            catch (HibásBevittAdat ex)
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


        #region Helyiség
        private void Helység_friss_Click(object sender, EventArgs e)
        {
            Tábla_terv_Ürítés();
        }

        private void Helyiséglista_Click(object sender, EventArgs e)
        {
            try
            {
                if (Helyiséglista.SelectedIndex < 0) return;
                Szemetes.Visible = false;
                KapcsoltHelységFő.Visible = false;
                KapcsoltHelységAl.Visible = false;

                VálasztottElem = Helyiséglista.SelectedIndex;

                string[] darabol = Helyiséglista.Items[Helyiséglista.SelectedIndex].ToString().Split('-');
                HelységKód = darabol[0].Trim();

                AdatokAdatTábla = KézAdatTábla.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Státus == false).ToList();
                // A szemetes és a kapcsolt helyiség ellenőrzés kiirás

                Adat_Épület_Adattábla AlapAdat = (from a in AdatokAdatTábla
                                                  where a.Státus == false
                                                  && a.Helységkód == HelységKód.Trim()
                                                  select a).FirstOrDefault();

                if (AlapAdat != null)
                {
                    Szemetes.Visible = AlapAdat.Szemetes;
                    KapcsoltHelység = AlapAdat.Kapcsolthelység;
                    // ha üres a kapcsolthelység, akkor fő lehet
                    if (!(KapcsoltHelység.Trim() == "" || KapcsoltHelység.Trim() == "_")) KapcsoltHelységAl.Visible = true;

                    Adat_Épület_Adattábla KapcsoltHelységElem = (from a in AdatokAdatTábla
                                                                 where a.Státus == false
                                                                 && a.Kapcsolthelység == HelységKód.Trim()
                                                                 select a).FirstOrDefault();

                    if (KapcsoltHelységElem != null)
                    {
                        KapcsoltHelységFő.Visible = true;
                        KapcsoltHelység = "";
                    }
                }
                // hogy ki tudja listázni a kiválasztott elemet
                Tábla_terv_listázás();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ÖsszesKijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Helyiséglista.Items.Count; i++)
                Helyiséglista.SetItemChecked(i, true);
            Alapra_állít();
        }

        private void Mindtöröl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Helyiséglista.Items.Count; i++)
                Helyiséglista.SetItemChecked(i, false);
            Alapra_állít();
        }

        private void Alapra_állít()
        {
            KapcsoltHelység = "";
            Tábla_terv_listázás();
        }
        #endregion


        #region Rögzítés lapfül

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Idő_lakat_működés();
                Lakat_állapot();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ablak_Épülettakarítás_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 17) Chk_CTRL.Checked = true;
        }

        private void Tábla_terv_listázás()
        {
            try
            {
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Holtart.Be(hónapnap + 2);
                Tábla_terv_Ürítés();

                // kiírjuk ha van terv
                if (HelységKód.Trim() != "")
                {
                    Adat_Épület_Takarításrakijelölt rekord = (from a in AdatokKijelöltek
                                                              where a.Hónap == Dátum.Value.Month
                                                              && a.Helységkód == HelységKód.Trim()
                                                              select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        for (int i = 0; i < hónapnap; i++)
                        {

                            if (MyF.Szöveg_Tisztítás(rekord.E1rekijelölt, i, 1) == "1")
                                Tábla_terv.Rows[i].Cells["e1"].Value = true;
                            else
                                Tábla_terv.Rows[i].Cells["e1"].Value = false;

                            if (MyF.Szöveg_Tisztítás(rekord.E2rekijelölt, i, 1) == "1")
                                Tábla_terv.Rows[i].Cells["e2"].Value = true;
                            else
                                Tábla_terv.Rows[i].Cells["e2"].Value = false;

                            if (MyF.Szöveg_Tisztítás(rekord.E3rekijelölt, i, 1) == "1")
                                Tábla_terv.Rows[i].Cells["e3"].Value = true;
                            else
                                Tábla_terv.Rows[i].Cells["e3"].Value = false;

                        }

                    }
                    Tábla_terv.Visible = true;
                    Tábla_terv.Refresh();
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

        private void Tábla_terv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Tábla_terv.RowCount < 0)
                    return;
                foreach (DataGridViewRow row in Tábla_terv.Rows)
                {
                    if (row.Cells[4].Value != null && bool.Parse(row.Cells[4].Value.ToString()))
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f);
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

        private void Terv_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Helyiséglista.CheckedItems.Count == 0) return;

                int hónapnap = MyF.Hónap_hossza(Dátum.Value);

                AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                AdatokAdatTábla = KézAdatTábla.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Státus == false).ToList();

                Holtart.Be(Helyiséglista.Items.Count + 2);

                //Előkészítjük a rögzítést
                string e1 = "";
                string e2 = "";
                string e3 = "";
                int E1db = 0;
                int E2db = 0;
                int E3db = 0;
                //kitöltöt táblázatot elemezzük és meghatározzuk az E1, E2, E3 értékeket
                for (int k = 0; k < hónapnap; k++)
                {
                    if (Tábla_terv.Rows[k].Cells[1].Value != null && bool.Parse(Tábla_terv.Rows[k].Cells[1].Value.ToString()))
                    {
                        E1db += 1;
                        e1 += "1";
                    }
                    else
                    {
                        e1 += "0";
                    }

                    if (Tábla_terv.Rows[k].Cells[2].Value != null && bool.Parse(Tábla_terv.Rows[k].Cells[2].Value.ToString()))
                    {
                        E2db += 1;
                        e2 += "1";
                    }
                    else
                    {
                        e2 += "0";
                    }

                    if (Tábla_terv.Rows[k].Cells[3].Value != null && bool.Parse(Tábla_terv.Rows[k].Cells[3].Value.ToString()))
                    {
                        E3db += 1;
                        e3 += "1";
                    }
                    else
                    {
                        e3 += "0";
                    }
                }

                List<Adat_Épület_Takarításrakijelölt> AdatokM = new List<Adat_Épület_Takarításrakijelölt>();
                List<Adat_Épület_Takarításrakijelölt> AdatokR = new List<Adat_Épület_Takarításrakijelölt>();
                for (int i = 0; i < Helyiséglista.CheckedItems.Count; i++)
                {

                    // töröljük a pipát


                    string[] darabol = Helyiséglista.CheckedItems[i].ToString().Split('-');
                    string helységkód = darabol[0];
                    string Megnevezés = darabol[1];
                    Helyiséglista.SetItemChecked(i, false);

                    Adat_Épület_Adattábla ÉpAdat = (from a in AdatokAdatTábla
                                                    where a.Helységkód == helységkód.Trim()
                                                    select a).FirstOrDefault();
                    if (ÉpAdat != null)
                    {
                        Megnevezés = ÉpAdat.Megnevezés.Trim();
                        string osztály = ÉpAdat.Osztály.Trim();
                        Adat_Épület_Takarításrakijelölt KijelöltElem = (from a in AdatokKijelöltek
                                                                        where a.Hónap == Dátum.Value.Month
                                                                        && a.Helységkód == helységkód.Trim()
                                                                        select a).FirstOrDefault();

                        if (KijelöltElem == null)
                        {
                            Adat_Épület_Takarításrakijelölt Adat = new Adat_Épület_Takarításrakijelölt(
                                          0, E1db, e1.Trim(),
                                          0, E2db, e2.Trim(),
                                          0, E3db, e3.Trim(),
                                          helységkód,
                                          Dátum.Value.Month,
                                          Megnevezés.Trim().Replace(",", "."),
                                          osztály.Trim().Replace(",", "."));
                            AdatokR.Add(Adat);
                        }
                        else
                        {
                            Adat_Épület_Takarításrakijelölt Adat = new Adat_Épület_Takarításrakijelölt(
                                        E1db, E2db, E3db,
                                        helységkód.Trim(),
                                        Dátum.Value.Month,
                                        e1.Trim(), e2.Trim(), e3.Trim());
                            AdatokM.Add(Adat);
                        }
                    }
                    Holtart.Lép();
                }
                if (AdatokR.Count > 0) KézTakarításrakijelölt.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, AdatokR);
                if (AdatokM.Count > 0) KézTakarításrakijelölt.Módosítás(AdatokM, Dátum.Value.Year, Cmbtelephely.Text.Trim());

                Holtart.Ki();
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

        private void Tábla_terv_Ürítés()
        {
            try
            {
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                Tábla_terv.Rows.Clear();
                Tábla_terv.Refresh();
                Tábla_terv.Visible = false;
                Tábla_terv.RowCount = hónapnap;
                for (int i = 0; i < hónapnap; i++)
                    Tábla_terv.Rows[i].Cells[0].Value = i + 1;

                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                if (AdatokÉNaptár.Count > 0)
                {
                    Adat_Épület_Naptár Adat = (from a in AdatokÉNaptár
                                               where a.Hónap == Dátum.Value.Month
                                               select a).FirstOrDefault();

                    if (Adat != null)
                    {
                        for (int i = 0; i < hónapnap; i++)
                        {
                            if (MyF.Szöveg_Tisztítás(Adat.Napok, i, 1) == "0")
                                Tábla_terv.Rows[i].Cells["nap"].Value = true;
                            else
                                Tábla_terv.Rows[i].Cells["nap"].Value = false;
                            Tábla_terv.Rows[i].Cells[1].Value = false;
                            Tábla_terv.Rows[i].Cells[2].Value = false;
                            Tábla_terv.Rows[i].Cells[3].Value = false;

                        }
                    }
                }
                Tábla_terv.Visible = true;
            }
            catch (HibásBevittAdat ex)
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


        #region Ellenőrzőlap
        private void Command4_Click(object sender, EventArgs e)
        {
            Nyomtat_Lapot();
        }

        private void Nyomtat_Lapot()
        {
            try
            {
                if (Helyiséglista.CheckedItems.Count < 1) return;
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);

                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                AdatokAdatTábla = KézAdatTábla.Lista_Adatok(Cmbtelephely.Text.Trim());

                int l = 0;
                Holtart.Be(Helyiséglista.Items.Count + 1);

                for (l = 0; l < Helyiséglista.CheckedItems.Count; l++)
                {
                    // helyiség kód visszafejtése
                    string[] darabol = Helyiséglista.Items[l].ToString().Split('-');
                    string helységkód = darabol[0].Trim();

                    string fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    fájlexc += @"\Takarítási_napló_" + helységkód.Trim() + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xlsx";
                    string munkalap = "Munka1";
                    // megnyitjuk az excelt
                    MyE.ExcelLétrehozás();
                    MyE.Munkalap_betű("Arial", 12);
                    MyE.Sormagasság("1:50", 18);

                    // oszlop széleségek beállítása
                    MyE.Oszlopszélesség(munkalap, "a:n", 5);
                    MyE.Oszlopszélesség(munkalap, "a:a", 7);
                    MyE.Oszlopszélesség(munkalap, "e:f", 8);
                    MyE.Oszlopszélesség(munkalap, "g:g", 10);
                    MyE.Oszlopszélesség(munkalap, "j:k", 10);
                    MyE.Oszlopszélesség(munkalap, "n:n", 10);
                    // '**********************************************
                    // '**          fejléc          ******************
                    // '**********************************************
                    MyE.Egyesít(munkalap, "a1:N1");
                    MyE.Kiir(Dátum.Value.ToString("yyyy MMMM"), "a1");
                    MyE.Betű("a1", false, false, true);
                    MyE.Egyesít(munkalap, "a2:n4");
                    MyE.Kiir("helyiség", "a2");

                    MyE.Egyesít(munkalap, "a5:n5");
                    MyE.Kiir("Takarítási napló", "a5");
                    MyE.Betű("a5", false, false, true);
                    MyE.Vastagkeret("a7");
                    MyE.Egyesít(munkalap, "b7:g7");
                    MyE.Vastagkeret("b7:g7");
                    MyE.Egyesít(munkalap, "h7:n7");
                    MyE.Vastagkeret("h7:n7");
                    MyE.Kiir("Szolgáltató tölti ki", "b7");
                    MyE.Betű("b7", false, true, false);
                    MyE.Kiir("BKV szervezeti igazolója tölti ki", "h7");
                    MyE.Betű("h7", false, true, false);
                    MyE.Sormagasság("8:8", 51);
                    MyE.Egyesít(munkalap, "a8:a9");
                    MyE.Kiir("Dátum", "a8");
                    MyE.Egyesít(munkalap, "b8:d8");
                    MyE.Kiir("Szolg. jegyzék kódja", "b8");
                    MyE.Sortörésseltöbbsorba_egyesített("B8");

                    MyE.Kiir("E1", "b9");
                    MyE.Kiir("E2", "c9");
                    MyE.Kiir("E3", "d9");
                    MyE.Egyesít(munkalap, "e8:f8");
                    MyE.Kiir("Takarítás ideje", "e8");
                    MyE.Kiir("-tól", "e9");
                    MyE.Kiir("-ig", "f9");
                    MyE.Egyesít(munkalap, "g8:g9");
                    MyE.Kiir("Aláírás", "g8");
                    MyE.Egyesít(munkalap, "h8:i8");
                    MyE.Kiir("Megfelelő", "h8");
                    MyE.Kiir("I", "h9");
                    MyE.Kiir("N", "i9");
                    MyE.Egyesít(munkalap, "j8:j9");
                    MyE.Kiir("Igazolta", "j8");
                    MyE.Egyesít(munkalap, "k8:k9");
                    MyE.Kiir("Pót. Határ- idő", "k8");
                    MyE.Sortörésseltöbbsorba("k8");
                    MyE.Egyesít(munkalap, "l8:m8");
                    MyE.Kiir("Megfelelő", "l8");
                    MyE.Kiir("I", "l9");
                    MyE.Kiir("N", "m9");
                    MyE.Egyesít(munkalap, "n8:n9");
                    MyE.Kiir("Igazolta", "n8");
                    MyE.Rácsoz("a7:n9");
                    MyE.Vastagkeret("a8");
                    MyE.Vastagkeret("b8:g9");
                    MyE.Vastagkeret("h8:n9");
                    int sor = 1;

                    Adat_Épület_Takarításrakijelölt rekord = (from a in AdatokKijelöltek
                                                              where a.Hónap == Dátum.Value.Month
                                                              && a.Helységkód == helységkód.Trim()
                                                              select a).FirstOrDefault();

                    if (rekord != null)
                    {
                        // kiirjuk a helység nevét
                        string szöveg1 = rekord.Helységkód.Trim() + " - " + rekord.Megnevezés.Trim();

                        List<Adat_Épület_Adattábla> AdatokÉ = (from a in AdatokAdatTábla
                                                               where a.Státus == false
                                                               && a.Kapcsolthelység == helységkód.Trim()
                                                               select a).ToList();

                        if (AdatokÉ != null)
                        {
                            foreach (Adat_Épület_Adattábla rekordép in AdatokÉ)
                                szöveg1 += "; " + rekordép.Helységkód.Trim() + " - " + rekordép.Megnevezés.Trim();
                        }

                        MyE.Kiir(szöveg1, "a2");
                        MyE.Sortörésseltöbbsorba_egyesített("a2");
                        MyE.Igazít_vízszintes("a2", "közép");

                        sor = 10;

                        for (int i = 0; i < hónapnap; i++)
                        {
                            if (MyF.Szöveg_Tisztítás(rekord.E1rekijelölt, i, 1) == "0")
                                MyE.Háttérszín("b" + sor.ToString(), 12632256L);
                            if (MyF.Szöveg_Tisztítás(rekord.E2rekijelölt, i, 1) == "0")
                                MyE.Háttérszín("c" + sor.ToString(), 12632256L);
                            if (MyF.Szöveg_Tisztítás(rekord.E3rekijelölt, i, 1) == "0")
                                MyE.Háttérszín("d" + sor.ToString(), 12632256L);
                            sor += 1;
                        }
                    }

                    sor = 10;

                    for (int i = 0; i < hónapnap; i++)
                    {
                        MyE.Kiir((i + 1).ToString(), "a" + sor.ToString());
                        sor += 1;
                    }
                    MyE.Kiir("Össz", "a" + sor.ToString());
                    MyE.Betű("a" + sor.ToString(), false, false, true);
                    MyE.Rácsoz("a10:n" + sor.ToString());
                    MyE.Vastagkeret("b10:g" + sor.ToString());
                    MyE.Vastagkeret("h10:n" + sor.ToString());
                    MyE.Vastagkeret("a" + sor.ToString() + ":n" + sor.ToString());
                    // Szombat vasárnap
                    Adat_Épület_Naptár Naptár = (from a in AdatokÉNaptár
                                                 where a.Hónap == Dátum.Value.Month
                                                 select a).FirstOrDefault();

                    if (Naptár != null)
                    {
                        sor = 10;
                        for (int i = 0; i < hónapnap; i++)
                        {
                            if (MyF.Szöveg_Tisztítás(Naptár.Napok, i, 1) == "0")
                            {
                                // ferde vonal
                                MyE.FerdeVonal($"B{sor}:N{sor}");
                            }
                            sor += 1;
                        }
                    }

                    sor += 2;
                    // jelmagyarázat
                    MyE.Kiir("Jelmagyarázat", "a" + sor.ToString());
                    sor += 1;
                    MyE.Vékonykeret("a" + sor.ToString());
                    MyE.Kiir("Megrendelt takarítás", "b" + sor.ToString());
                    sor += 1;
                    MyE.Vékonykeret("a" + sor.ToString());
                    MyE.Háttérszín("a" + sor.ToString(), 12632256L);
                    MyE.Kiir("Nincs megrendelve a takarítás", "b" + sor.ToString());
                    sor += 1;
                    MyE.Vékonykeret("a" + sor.ToString());
                    MyE.FerdeVonal($"A{sor}");
                    MyE.Háttérszín("a" + sor.ToString(), 12632256L);
                    MyE.Kiir("Munkaszüneti nap", "b" + sor.ToString());

                    // **********************************************
                    // **Nyomtatási beállítások                    **
                    // **********************************************
                    MyE.NyomtatásiTerület_részletes(munkalap, "a1:n" + sor,
                        balMargó: 0.393700787401575d, jobbMargó: 0.393700787401575d,
                        alsóMargó: 0.590551181102362d, felsőMargó: 0.590551181102362d,
                        fejlécMéret: 0.511811023622047d, LáblécMéret: 0.511811023622047d, oldalszéles: "1", oldalmagas: "1");

                    // **********************************************
                    // **Nyomtatás                                 **
                    // **********************************************
                    if (Option9.Checked) MyE.Nyomtatás(munkalap, 1, 1);

                    // bezárjuk az Excel-t
                    MyE.Aktív_Cella(munkalap, "A1");
                    MyE.ExcelMentés(fájlexc);
                    MyE.ExcelBezárás();

                    if (Option10.Checked == true)
                        File.Delete(fájlexc);


                    Holtart.Lép();
                }
                Holtart.Ki();

                MessageBox.Show("A kiválasztott elemek nyomtatása befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Takarítási megrendelő
        private void Excellekérdezés_Click(object sender, EventArgs e)
        {
            try
            {// kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Takarítási megrendelő készítése",
                    FileName = "Takarítási megrendelő_" + Dátum.Value.ToString("yyyyMM"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                string munkalap = "Munka1";
                MyE.ExcelLétrehozás();
                // megnyitjuk az excelt

                Holtart.Be();
                // *********************************************
                // ********* Osztály tábla *********************
                // *********************************************
                // fejléc elkészítése
                MyE.Kiir("Megnevezés", "a1");
                MyE.Kiir("E1 Egységár [Ft/m2]", "c1");
                MyE.Kiir("E2 Egységár [Ft/m2]", "d1");
                MyE.Kiir("E3 Egységár [Ft/m2]", "e1");

                AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                AdatokTakOsztály = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokAdatTábla = KézAdatTábla.Lista_Adatok(Cmbtelephely.Text.Trim());

                AdatokTakOsztály = (from a in AdatokTakOsztály
                                    where a.Státus == false
                                    orderby a.Id
                                    select a).ToList();
                Holtart.Be(20);

                int sor = 2;
                foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokTakOsztály)
                {
                    MyE.Kiir(rekord.Osztály.Trim(), "a" + sor.ToString());
                    MyE.Kiir(rekord.E1Ft.ToString().Replace(",", "."), "c" + sor.ToString());
                    MyE.Kiir(rekord.E2Ft.ToString().Replace(",", "."), "d" + sor.ToString());
                    MyE.Kiir(rekord.E3Ft.ToString().Replace(",", "."), "e" + sor.ToString());
                    Holtart.Lép();
                    sor += 1;
                }
                MyE.Oszlopszélesség(munkalap, "A:A");
                MyE.Oszlopszélesség(munkalap, "B:B");
                MyE.Oszlopszélesség(munkalap, "C:E");
                MyE.Rácsoz("a1:e" + (sor - 1).ToString());

                MyE.Munkalap_átnevezés("Munka1", "Összesítő");
                MyE.OszlopRejtés("Összesítő", "B:B");

                MyE.Új_munkalap(Cmbtelephely.Text.Trim());
                MyE.Munkalap_betű("Calibri", 10);
                munkalap = Cmbtelephely.Text.Trim();
                // ************************************************
                // ************ fejléc elkészítése  ***************
                // ************************************************
                MyE.Egyesít(munkalap, "b1:b2");
                MyE.Kiir("Helyiség", "b1");
                MyE.Egyesít(munkalap, "c1:c2");
                MyE.Kiir("Alapterület [m2]", "c1");
                MyE.Egyesít(munkalap, "d1:k1");
                MyE.Kiir("Gyakoriság", "d1");
                MyE.Kiir("Szolgálatási jegyzék kódja", "d2");
                MyE.Kiir("Szolgálatási jegyzék kódja", "g2");
                MyE.Kiir("Szolgálatási jegyzék kódja", "j2");
                MyE.Kiir("Gyakoriság alkalom /év", "e2");
                MyE.Kiir("Gyakoriság alkalom /év", "h2");
                MyE.Kiir("Gyakoriság alkalom /hó", "f2");
                MyE.Kiir("Gyakoriság alkalom /hó", "i2");
                MyE.Kiir("Gyakoriság alkalom /hó", "k2");
                MyE.Egyesít(munkalap, "l1:l2");
                MyE.Kiir("E1 Egységár [Ft/alkalom]", "l1");
                MyE.Egyesít(munkalap, "m1:m2");
                MyE.Kiir("E2 Egységár [Ft/alkalom]", "m1");
                MyE.Egyesít(munkalap, "n1:n2");
                MyE.Kiir("E3 Egységár [Ft/alkalom]", "n1");
                MyE.Egyesít(munkalap, "o1:o2");
                MyE.Kiir("E1 Egységár [Ft/hó]", "o1");
                MyE.Egyesít(munkalap, "p1:p2");
                MyE.Kiir("E2 Egységár [Ft/hó]", "p1");
                MyE.Egyesít(munkalap, "q1:q2");
                MyE.Kiir("E3 Egységár [Ft/hó]", "q1");
                MyE.Egyesít(munkalap, "r1:r2");
                MyE.Kiir("Összesen: [Ft/hó]", "r1");
                MyE.Egyesít(munkalap, "s1:t2");
                MyE.Kiir("Feladatellátás tervezett időpontja", "s1");
                MyE.Egyesít(munkalap, "u1:w1");
                MyE.Kiir("Minőségellenőrzésért, teljesítési igazolásért felelős személy", "u1");
                MyE.Kiir("Neve", "u2");
                MyE.Kiir("Telefonszám", "v2");
                MyE.Kiir("E-mail cím", "w2");
                MyE.Sormagasság("1:1", 47);
                MyE.Sormagasság("2:2", 39);
                MyE.Oszlopszélesség(munkalap, "B:B", 46);
                MyE.Oszlopszélesség(munkalap, "c:k", 11);
                MyE.Oszlopszélesség(munkalap, "l:n", 13);
                MyE.Oszlopszélesség(munkalap, "o:v", 15);
                MyE.Oszlopszélesség(munkalap, "w:W", 20);
                MyE.Sortörésseltöbbsorba_egyesített("c1");
                MyE.Sortörésseltöbbsorba("d2:k2");
                MyE.Sortörésseltöbbsorba_egyesített("l1");
                MyE.Sortörésseltöbbsorba_egyesített("m1");
                MyE.Sortörésseltöbbsorba_egyesített("n1");
                MyE.Sortörésseltöbbsorba_egyesített("o1");
                MyE.Sortörésseltöbbsorba_egyesített("p1");
                MyE.Sortörésseltöbbsorba_egyesített("r1");
                MyE.OszlopRejtés(munkalap, "A:A");

                // a táblázat érdemi része

                sor = 2;
                Adat_Épület_Takarításrakijelölt rekordép;

                foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokTakOsztály)
                {
                    sor += 1;
                    MyE.Egyesít(munkalap, "b" + sor.ToString() + ":W" + sor.ToString());
                    MyE.Igazít_vízszintes("b" + sor.ToString() + ":W" + sor.ToString(), "bal");
                    MyE.Háttérszín("b" + sor.ToString() + ":W" + sor.ToString(), 13434828L);
                    MyE.Kiir(rekord.Osztály.Trim(), "b" + sor.ToString());
                    MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 20);

                    List<Adat_Épület_Adattábla> AdatokA = (from a in AdatokAdatTábla
                                                           where a.Státus == false
                                                           && a.Osztály == rekord.Osztály.Trim()
                                                           orderby a.ID
                                                           select a).ToList();

                    foreach (Adat_Épület_Adattábla rekord1 in AdatokA)
                    {
                        sor++;
                        MyE.Kiir(rekord1.Osztály.Trim(), "A" + sor.ToString());
                        MyE.Kiir(rekord1.Megnevezés.Trim(), "b" + sor.ToString());
                        MyE.Kiir(rekord1.Méret.ToString(), "c" + sor.ToString());
                        MyE.Kiir("E1", "d" + sor.ToString());
                        MyE.Kiir(rekord1.E1évdb.ToString(), "e" + sor.ToString());
                        MyE.Kiir("E2", "g" + sor.ToString());
                        MyE.Kiir(rekord1.E2évdb.ToString(), "h" + sor.ToString());
                        MyE.Kiir("E3", "j" + sor.ToString());
                        int idE1db = 0;
                        int idE2db = 0;
                        int idE3db = 0;

                        rekordép = (from a in AdatokKijelöltek
                                    where a.Hónap == Dátum.Value.Month
                                    && a.Helységkód == rekord1.Helységkód.Trim()
                                    select a).FirstOrDefault();

                        if (rekordép != null)
                        {
                            idE1db = rekordép.E1kijelöltdb;
                            idE2db = rekordép.E2kijelöltdb;
                            idE3db = rekordép.E3kijelöltdb;
                        }

                        MyE.Kiir(idE1db.ToString(), "f" + sor.ToString());
                        MyE.Kiir(idE2db.ToString(), "i" + sor.ToString());
                        MyE.Kiir(idE3db.ToString(), "k" + sor.ToString());
                        MyE.Kiir(rekord.E1Ft.ToString().Replace(",", "."), "l" + sor.ToString()); //Ez darabra megy
                        MyE.Kiir((rekord.E2Ft * rekord1.Méret).ToString().Replace(",", "."), "m" + sor.ToString());
                        MyE.Kiir((rekord.E3Ft * rekord1.Méret).ToString().Replace(",", "."), "n" + sor.ToString());
                        MyE.Kiir("=RC[-3]*RC[-9]", "o" + sor.ToString());
                        MyE.Kiir("=RC[-3]*RC[-7]", "p" + sor.ToString());
                        MyE.Kiir("=RC[-3]*RC[-6]", "q" + sor.ToString());
                        MyE.Kiir("=SUM(RC[-3]:RC[-1])", "r" + sor.ToString());
                        MyE.Kiir(rekord1.Kezd.Trim(), "s" + sor.ToString());
                        MyE.Kiir(rekord1.Végez.Trim(), "t" + sor.ToString());
                        MyE.Kiir(rekord1.Ellenőrneve.Trim(), "u" + sor.ToString());
                        MyE.Kiir(rekord1.Ellenőrtelefonszám.Trim(), "v" + sor.ToString());
                        MyE.Kiir(rekord1.Ellenőremail.Trim(), "w" + sor.ToString());
                    }
                    Holtart.Lép();
                }


                // összesítő sor
                sor += 1;
                MyE.Igazít_vízszintes("b" + sor.ToString() + ":W" + sor.ToString(), "bal");
                MyE.Háttérszín("b" + sor.ToString() + ":W" + sor.ToString(), 13434828L);
                MyE.Egyesít(munkalap, "b" + sor.ToString() + ":n" + sor.ToString());
                MyE.Kiir(Cmbtelephely.Text.Trim() + " Összesen/hó", "b" + sor.ToString() + ":n" + sor.ToString());
                MyE.Betű("b" + sor.ToString() + ":n" + sor.ToString(), false, false, true);
                MyE.Egyesít(munkalap, "b" + sor.ToString() + ":n" + sor.ToString());
                MyE.Egyesít(munkalap, "o" + sor.ToString() + ":r" + sor.ToString());
                MyE.Kiir("=SUM(R[-" + (sor - 3).ToString() + "]C[3]:R[-1]C[3])", "o" + sor.ToString() + ":r" + sor.ToString());
                MyE.Rácsoz("b1:W" + sor.ToString());
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 25);

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
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
        #endregion


        #region Kiegészítő ablakok
        Ablak_Épülettakarítás_kieg Új_Ablak_Épülettakarítás_kieg;
        private void KapcsolHelység_Click(object sender, EventArgs e)
        {
            try
            {
                Új_Ablak_Épülettakarítás_kieg?.Close();

                Új_Ablak_Épülettakarítás_kieg = new Ablak_Épülettakarítás_kieg(Cmbtelephely.Text.Trim(), HelységKód, true);
                Új_Ablak_Épülettakarítás_kieg.FormClosed += Ablak_Épülettakarítás_kieg_Closed;
                Új_Ablak_Épülettakarítás_kieg.Show();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KapcsoltHelységAl_Click(object sender, EventArgs e)
        {
            try
            {
                Új_Ablak_Épülettakarítás_kieg?.Close();

                Új_Ablak_Épülettakarítás_kieg = new Ablak_Épülettakarítás_kieg(Cmbtelephely.Text.Trim(), HelységKód, false);
                Új_Ablak_Épülettakarítás_kieg.FormClosed += Ablak_Épülettakarítás_kieg_Closed;
                Új_Ablak_Épülettakarítás_kieg.Show();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ablak_Épülettakarítás_kieg_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Épülettakarítás_kieg = null;
        }

        private void Ablak_Épülettakarítás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Épülettakarítás_kieg?.Close();
        }
        #endregion


        #region Lakatkezelés
        private void Zárva_Click(object sender, EventArgs e)
        {
            try
            {
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum.Value.Month
                                           select a).FirstOrDefault();

                if (Elem == null)
                {
                    // új
                    Adat_Épület_Naptár Adat = new Adat_Épület_Naptár(
                            true,
                            Dátum.Value.Month,
                            false,
                            "0000000000000000000000000000000");
                    KézÉpületNaptár.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, Adat);
                }
                else
                {
                    // módosít
                    KézÉpületNaptár.Módosítás_előterv(Cmbtelephely.Text.Trim(), Dátum.Value.Year, true, Dátum.Value.Month);
                }
                Lakat_állapot();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nyitva_Click(object sender, EventArgs e)
        {
            try
            {
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum.Value.Month
                                           select a).FirstOrDefault();

                if (Elem == null)
                {
                    // új
                    Adat_Épület_Naptár Adat = new Adat_Épület_Naptár(
                        false,
                        Dátum.Value.Month,
                        false,
                        "0000000000000000000000000000000");
                    KézÉpületNaptár.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, Adat);
                }
                else
                {
                    // módosít
                    KézÉpületNaptár.Módosítás_előterv(Cmbtelephely.Text.Trim(), Dátum.Value.Year, false, Dátum.Value.Month);
                }
                Lakat_állapot();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Idő_lakat_működés()
        {
            try
            {
                //Ha aktív és múlt hónap
                if (Dátum.Value.Month <= DateTime.Today.Month)
                {
                    Idő_lakat.BackColor = Color.HotPink;
                    Idő_lakat.Visible = true;
                }
                else
                {
                    Idő_lakat.BackColor = Color.DeepSkyBlue;
                    Idő_lakat.Visible = false;
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

        private void Lakat_állapot()
        {
            try
            {
                AdatokÉNaptár = KézÉpületNaptár.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Adat_Épület_Naptár Elem = (from a in AdatokÉNaptár
                                           where a.Hónap == Dátum.Value.Month
                                           select a).FirstOrDefault();

                if (Elem != null)
                {
                    if (Elem.Előterv)
                    {
                        Zárva.Visible = false;
                        Nyitva.Visible = true;

                        Terv_Rögzítés.Visible = true;
                        Excellekérdezés.Visible = false;
                        Command4.Visible = false;
                    }
                    else
                    {
                        Zárva.Visible = true;
                        Nyitva.Visible = false;

                        Terv_Rögzítés.Visible = false;
                        Excellekérdezés.Visible = true;
                        Command4.Visible = true;
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

        private void Idő_lakat_Click(object sender, EventArgs e)
        {
            if (Chk_CTRL.Checked == true)
            {
                Idő_lakat.Visible = false;
            }
        }
        #endregion


        #region Opció
        Ablak_Opció Új_Ablak_Opció;
        private void Opció_Megrendelés_Click(object sender, EventArgs e)
        {
            Új_Ablak_Opció?.Close();
            Új_Ablak_Opció = new Ablak_Opció(Dátum1.Value, false, Cmbtelephely.Text.Trim());
            Új_Ablak_Opció.FormClosed += Új_Ablak_Opció_FormClosed;
            Új_Ablak_Opció.Show();
        }

        private void Új_Ablak_Opció_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Opció = null;
        }

        private void Opció_kifizetés_Click(object sender, EventArgs e)
        {
            Új_Ablak_Opció?.Close();

            Új_Ablak_Opció = new Ablak_Opció(Dátum1.Value, true, Cmbtelephely.Text.Trim());
            Új_Ablak_Opció.FormClosed += Új_Ablak_Opció_FormClosed;
            Új_Ablak_Opció.Show();
        }
        #endregion


        #region BMR
        Ablak_BMR Új_Ablak_BMR;
        private void BMR_Click(object sender, EventArgs e)
        {
            Új_Ablak_BMR?.Close();
            Új_Ablak_BMR = new Ablak_BMR(Dátum1.Value, false, Cmbtelephely.Text.Trim());
            Új_Ablak_BMR.FormClosed += Új_Ablak_BMR_FormClosed;
            Új_Ablak_BMR.Show();
        }

        private void Új_Ablak_BMR_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_BMR = null;
        }
        #endregion
    }
}