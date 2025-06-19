using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.Közös;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Rezsi
    {
        readonly Kezelő_Rezsi_Könyvelés KézRezsi = new Kezelő_Rezsi_Könyvelés();
        readonly Kezelő_Rezsi_Törzs KézTörzs = new Kezelő_Rezsi_Törzs();
        readonly Kezelő_Rezsi_Hely KézHely = new Kezelő_Rezsi_Hely();
        readonly Kezelő_Rezsi_Napló KézNapló = new Kezelő_Rezsi_Napló();

        List<Adat_Rezsi_Törzs> AdatokTörzs = new List<Adat_Rezsi_Törzs>();
        List<Adat_Rezsi_Hely> AdatokHely = new List<Adat_Rezsi_Hely>();
        List<Adat_Rezsi_Lista> AdatokLista = new List<Adat_Rezsi_Lista>();
        List<Adat_Rezsi_Listanapló> AdatokNapló = new List<Adat_Rezsi_Listanapló>();

#pragma warning disable 
        DataTable AdatTáblaTörzs = new DataTable();
        DataTable AdatTáblaTár = new DataTable();
        DataTable AdatTábla = new DataTable();
        DataTable AdatTáblaNapló = new DataTable();
#pragma warning restore 

        #region Alap
        public Ablak_Rezsi()
        {
            InitializeComponent();
        }

        private void Ablak_Rezsi_könyvelés_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();
            Dátumtól.Value = new DateTime(DateTime.Now.Year, 1, 1);
            Dátumig.Value = DateTime.Today;

            AdatokTörzs = KézTörzs.Lista_Adatok();
            AdatokHely = KézHely.Lista_Adatok(Cmbtelephely.Text.Trim()); ;
            AdatokLista = KézRezsi.Lista_Adatok(Cmbtelephely.Text.Trim());
            AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);

            Lapfülek.SelectedIndex = 3;
            Fülekkitöltése();

            Jogosultságkiosztás();
            Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            AzonosítóRendez();
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdatokTörzs = KézTörzs.Lista_Adatok();
            AdatokHely = KézHely.Lista_Adatok(Cmbtelephely.Text.Trim()); ;
            AdatokLista = KézRezsi.Lista_Adatok(Cmbtelephely.Text.Trim());
            AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
        }

        private void AzonosítóRendez()
        {
            KézTörzs.Nagybetűs();
        }

        private void Ablak_Rezsi_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fénykép_Betöltés?.Close();
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false
            KépHozzáad.Enabled = false;
            Rögzítteljes.Enabled = false;
            Tárolásihelyrögzítés.Enabled = false;
            BeRögzít.Enabled = false;
            Kirögzít.Enabled = false;

            if (Program.Postás_Vezér)
            {
                Rögzítteljes.Visible = true;
            }
            else
            {
                Rögzítteljes.Visible = false;
            }

            melyikelem = 220;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Rögzítteljes.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Tárolásihelyrögzítés.Enabled = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
                KépHozzáad.Enabled = true;
            }

            melyikelem = 221;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BeRögzít.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Kirögzít.Enabled = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }
        }

        private void Fülekkitöltése()
        {
            switch (Lapfülek.SelectedIndex)
            {
                case 0:
                    {
                        Törzs_azonosító_feltöltés();
                        Törzs_csoport_feltöltés();
                        Törzs_tábla_kiíró();
                        break;
                    }
                case 1:
                    {
                        Tár_azonosító_feltöltés();
                        Tár_tábla_kiíró();
                        Tárazonosítókiírás();
                        break;
                    }
                case 2:
                    {
                        Fény_azonosító_feltöltés();
                        Fényazonosítókiírás();
                        Képeklistázása();
                        break;
                    }
                case 3:
                    {
                        Lista_csoport_feltöltés();
                        break;
                    }
                case 4:
                    {
                        Be_azonosító_feltöltés();
                        Beirány();
                        break;
                    }
                case 5:
                    {
                        Lenyílókfeltöltés();
                        KI_azonosító_feltöltés();
                        break;
                    }
                case 6:
                    {
                        Napló_azonosító_feltöltés();
                        break;
                    }
            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\rezsi.html";
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
                foreach (string Elem in Listák.TelephelyLista_Rezsi())
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
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Lapfülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Lapfülek.GetTabRect(e.Index);

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


        #region Törzs karbantartás
        private void Törzs_azonosító_feltöltés()
        {

            List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
            List<string> Adatok = (from a in AdatokTörzs
                                   orderby a.Azonosító
                                   select a.Azonosító).ToList().Distinct().ToList();
            Azonosító.Items.Clear();
            Azonosító.BeginUpdate();
            foreach (string elem in Adatok)
                Azonosító.Items.Add(elem);
            Azonosító.EndUpdate();
            Azonosító.Refresh();
        }

        private void Törzs_csoport_feltöltés()
        {
            List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
            List<string> Adatok = (from a in AdatokTörzs
                                   where a.Státusz == 0
                                   orderby a.Csoport
                                   select a.Csoport).ToList().Distinct().ToList();
            CsoportCombo.Items.Clear();
            CsoportCombo.BeginUpdate();
            foreach (string elem in Adatok)
                CsoportCombo.Items.Add(elem);
            CsoportCombo.EndUpdate();
            CsoportCombo.Refresh();
        }

        private void Rögzítteljes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");
                if (Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Az megnevezés mező kitöltése kötelező.");
                if (Méret.Text.Trim() == "") Méret.Text = "-";
                if (CsoportCombo.Text.Trim() == "") CsoportCombo.Text = "-";
                Megnevezés.Text = MyF.Szöveg_Tisztítás(Megnevezés.Text);
                Méret.Text = MyF.Szöveg_Tisztítás(Méret.Text);
                Azonosító.Text = MyF.Szöveg_Tisztítás(Azonosító.Text).ToUpper();

                List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
                Adat_Rezsi_Törzs Elem = (from a in AdatokTörzs
                                         where a.Azonosító == Azonosító.Text.Trim()
                                         select a).FirstOrDefault();
                Adat_Rezsi_Törzs ADAT = new Adat_Rezsi_Törzs(
                                        Azonosító.Text.Trim().ToUpper(),
                                        Megnevezés.Text.Trim(),
                                        Méret.Text.Trim(),
                                        Aktív.Checked ? 1 : 0,
                                        CsoportCombo.Text.Trim());
                if (Elem != null)
                    KézTörzs.Módosítás(ADAT);
                else
                    KézTörzs.Rögzítés(ADAT);

                Törzs_azonosító_feltöltés();
                Törzs_csoport_feltöltés();
                Törzs_Ürít();
                Törzs_tábla_kiíró();
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

        private void Azonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Azonosítókiirás();
        }

        private void Azonosítókiirás()
        {
            try
            {
                Megnevezés.Text = "";
                Méret.Text = "";
                CsoportCombo.Text = "";
                Aktív.Checked = false;

                if (Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");

                List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
                Adat_Rezsi_Törzs rekord = (from a in AdatokTörzs
                                           where a.Azonosító == Azonosító.Text.Trim()
                                           select a).FirstOrDefault();

                if (rekord != null)
                {
                    Megnevezés.Text = rekord.Megnevezés.Trim();
                    Méret.Text = rekord.Méret.Trim();
                    if (rekord.Státusz == 1)
                        Aktív.Checked = true;
                    else
                        Aktív.Checked = false;

                    CsoportCombo.Text = rekord.Csoport.Trim();
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

        private void Törzs_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Törzs_tábla.Rows.Count <= 0) throw new HibásBevittAdat("A táblázatos rész nem tartalmaz adatot, így csak egy üres Excelt lehetne létrehozni.");
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Rezsi_törzs_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Törzs_tábla, false);
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

        private void Törzs_Új_adat_Click(object sender, EventArgs e)
        {
            Törzs_Ürít();
        }

        private void Törzs_Ürít()
        {
            Megnevezés.Text = "";
            Méret.Text = "";
            CsoportCombo.Text = "";
            Aktív.Checked = false;
            Azonosító.Text = "";
        }

        private void Törzs_Frissít_Click(object sender, EventArgs e)
        {
            Törzs_tábla_kiíró();
        }

        private void Törzs_tábla_kiíró()
        {
            try
            {
                Törzs_tábla.Visible = false;
                Törzs_tábla.CleanFilterAndSort();
                TözsTáblaFejléc();
                TözsTáblaTartalom();
                Törzs_tábla.DataSource = AdatTáblaTörzs;
                TözsTáblaOszlopSzélesség();

                Törzs_tábla.Visible = true;
                Törzs_tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TözsTáblaTartalom()
        {
            try
            {
                AdatTáblaTörzs.Clear();
                List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
                foreach (Adat_Rezsi_Törzs rekord in AdatokTörzs)
                {
                    DataRow Soradat = AdatTáblaTörzs.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Megnevezés"] = rekord.Megnevezés.Trim();
                    Soradat["Méret"] = rekord.Méret.Trim();
                    Soradat["Csoport"] = rekord.Csoport.Trim();
                    Soradat["Aktív"] = rekord.Státusz == 0 ? "Aktív" : "Törölt";
                    AdatTáblaTörzs.Rows.Add(Soradat);
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

        private void TözsTáblaOszlopSzélesség()
        {
            Törzs_tábla.Columns["Azonosító"].Width = 150;
            Törzs_tábla.Columns["Megnevezés"].Width = 400;
            Törzs_tábla.Columns["Méret"].Width = 200;
            Törzs_tábla.Columns["Csoport"].Width = 200;
            Törzs_tábla.Columns["Aktív"].Width = 100;
        }

        private void TözsTáblaFejléc()
        {
            try
            {
                AdatTáblaTörzs.Columns.Clear();
                AdatTáblaTörzs.Columns.Add("Azonosító");
                AdatTáblaTörzs.Columns.Add("Megnevezés");
                AdatTáblaTörzs.Columns.Add("Méret");
                AdatTáblaTörzs.Columns.Add("Csoport");
                AdatTáblaTörzs.Columns.Add("Aktív");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Törzs_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Törzs_Ürít();
            Azonosító.Text = Törzs_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void Törzs_tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (Törzs_tábla.RowCount < 1) return;

            foreach (DataGridViewRow row in Törzs_tábla.Rows)
            {
                if (row.Cells[4].Value.ToString() == "Törölt")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f);
                }
            }
        }
        #endregion


        #region Tárolási hely
        private void Tár_azonosító_feltöltés()
        {
            List<string> Adatok = (from a in AdatokTörzs
                                   orderby a.Azonosító
                                   select a.Azonosító).ToList().Distinct().ToList();

            TárAzonosító.Items.Clear();
            TárAzonosító.BeginUpdate();
            foreach (string elem in Adatok)
                TárAzonosító.Items.Add(elem);
            TárAzonosító.EndUpdate();
            TárAzonosító.Refresh();
        }

        private void TárAzonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tárazonosítókiírás();
        }

        private void Tárürít()
        {
            TárMegnevezés.Text = "";
            Helyiség.Text = "";
            Polc.Text = "";
            Állvány.Text = "";
            Megjegyzés.Text = "";
        }

        private void Tárazonosítókiírás()
        {
            try
            {
                Tárürít();

                if (TárAzonosító.Text.Trim() == "") return;

                Adat_Rezsi_Hely rekord = (from a in AdatokHely
                                          where a.Azonosító == TárAzonosító.Text.Trim()
                                          select a).FirstOrDefault();

                if (rekord != null)
                {
                    Helyiség.Text = rekord.Helyiség;
                    Polc.Text = rekord.Polc;
                    Állvány.Text = rekord.Állvány;
                    Megjegyzés.Text = rekord.Megjegyzés;
                }

                Adat_Rezsi_Törzs Elem = (from a in AdatokTörzs
                                         where a.Azonosító == TárAzonosító.Text.Trim()
                                         select a).FirstOrDefault();
                if (Elem != null) TárMegnevezés.Text = Elem.Megnevezés;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tár_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tár_tábla.Rows.Count <= 0) throw new HibásBevittAdat("A táblázatos rész nem tartalmaz adatot, így csak egy üres Excelt lehetne létrehozni.");

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Rezsi_tárolásihely_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tár_tábla, false);
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

        private void Tár_frissít_Click(object sender, EventArgs e)
        {
            Tár_tábla_kiíró();
            Tárazonosítókiírás();
        }

        private void Tár_tábla_kiíró()
        {
            try
            {
                Tár_tábla.Visible = false;
                Tár_tábla.CleanFilterAndSort();
                TárTáblaFejléc();
                TárTáblaTartalom();
                Tár_tábla.DataSource = AdatTáblaTár;
                TárTáblaOszlopSzélesség();
                Tár_tábla.Visible = true;
                Tár_tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TárTáblaTartalom()
        {
            try
            {
                AdatokHely = KézHely.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatTáblaTár.Clear();
                List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
                foreach (Adat_Rezsi_Törzs rekord in AdatokTörzs)
                {
                    DataRow Soradat = AdatTáblaTár.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Megnevezés"] = rekord.Megnevezés.Trim();
                    Adat_Rezsi_Hely rekordszer = (from a in AdatokHely
                                                  where a.Azonosító == rekord.Azonosító
                                                  select a).FirstOrDefault();
                    if (rekordszer == null)
                    {
                        Soradat["Helység"] = "";
                        Soradat["Állvány"] = "";
                        Soradat["Polc"] = "";
                        Soradat["Megjegyzés"] = "";
                    }
                    else
                    {
                        Soradat["Helység"] = rekordszer.Helyiség;
                        Soradat["Állvány"] = rekordszer.Állvány;
                        Soradat["Polc"] = rekordszer.Polc;
                        Soradat["Megjegyzés"] = rekordszer.Megjegyzés;
                    }
                    AdatTáblaTár.Rows.Add(Soradat);
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

        private void TárTáblaOszlopSzélesség()
        {
            Tár_tábla.Columns["Azonosító"].Width = 150;
            Tár_tábla.Columns["Megnevezés"].Width = 400;
            Tár_tábla.Columns["Helység"].Width = 200;
            Tár_tábla.Columns["Állvány"].Width = 200;
            Tár_tábla.Columns["Polc"].Width = 100;
            Tár_tábla.Columns["Megjegyzés"].Width = 300;
        }

        private void TárTáblaFejléc()
        {
            try
            {
                AdatTáblaTár.Columns.Clear();
                AdatTáblaTár.Columns.Add("Azonosító");
                AdatTáblaTár.Columns.Add("Megnevezés");
                AdatTáblaTár.Columns.Add("Helység");
                AdatTáblaTár.Columns.Add("Állvány");
                AdatTáblaTár.Columns.Add("Polc");
                AdatTáblaTár.Columns.Add("Megjegyzés");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tár_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Tárürít();
            TárAzonosító.Text = Tár_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void Tárolásihelyrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (TárAzonosító.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");
                if (Helyiség.Text.Trim() == "") Helyiség.Text = "_";
                if (Állvány.Text.Trim() == "") Állvány.Text = "_";
                if (Polc.Text.Trim() == "") Polc.Text = "_";
                if (Megjegyzés.Text.Trim() == "") Megjegyzés.Text = "_";

                Adat_Rezsi_Hely Elem = (from a in AdatokHely
                                        where a.Azonosító == TárAzonosító.Text.Trim()
                                        select a).FirstOrDefault();

                Adat_Rezsi_Hely ADAT = new Adat_Rezsi_Hely(
                                    TárAzonosító.Text.Trim(),
                                    Állvány.Text.Trim(),
                                    Polc.Text.Trim(),
                                    Helyiség.Text.Trim(),
                                    Megjegyzés.Text.Trim());

                if (Elem != null)
                    KézHely.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézHely.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                MessageBox.Show("Az adat rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Tár_tábla_kiíró();
            }
            catch (HibásBevittAdat ex)
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


        #region Fényképek
        private void Fényképfrissítés_Click(object sender, EventArgs e)
        {
            Fényazonosítókiírás();
            Képeklistázása();
        }

        private void Fény_azonosító_feltöltés()
        {
            List<string> Adatok = (from a in AdatokTörzs
                                   orderby a.Azonosító
                                   select a.Azonosító).ToList().Distinct().ToList();

            Fényazonosító.Items.Clear();
            Fényazonosító.BeginUpdate();
            foreach (string elem in Adatok)
                Fényazonosító.Items.Add(elem);

            Fényazonosító.EndUpdate();
            Fényazonosító.Refresh();
        }

        private void Fényazonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fényazonosítókiírás();
            Képeklistázása();
        }

        private void Fényazonosítókiírás()
        {
            try
            {
                if (Fényazonosító.Text.Trim() == "") return;

                Adat_Rezsi_Törzs Elem = (from a in AdatokTörzs
                                         where a.Azonosító == Fényazonosító.Text.Trim()
                                         select a).FirstOrDefault();
                if (Elem != null) FényMegnevezés.Text = Elem.Megnevezés;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Képeklistázása()
        {
            try
            {
                FényképLista.Items.Clear();
                if (Fényazonosító.Text.Trim() == "") return;

                // létrehozzuk a fénykép könyvtárat, ha még nincs
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Rezsiképek".KönyvSzerk();

                DirectoryInfo di = new System.IO.DirectoryInfo(hely);
                System.IO.FileInfo[] aryFi = di.GetFiles("*.jpg");
                string szöveg = Fényazonosító.Text.Trim();
                foreach (FileInfo fi in aryFi)
                {
                    if (fi.Name.Contains(szöveg)) FényképLista.Items.Add(fi.Name);
                }
                FénySorszám.Text = FényképLista.Items.Count.ToString();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FényképLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (FényképLista.SelectedItems.Count == 0) return;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Rezsiképek\{FényképLista.SelectedItems[0]}";
                if (!Exists(hely)) throw new HibásBevittAdat("A kiválaszott kép nem létezik.");

                Kezelő_Kép.KépMegnyitás(KépKeret, hely, toolTip1);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Ablak_Fénykép_Betöltés Új_Ablak_Fénykép_Betöltés;
        private void KépHozzáad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fényazonosító.Text.Trim() == "") return;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Rezsiképek".KönyvSzerk();

                Új_Ablak_Fénykép_Betöltés?.Close();

                Új_Ablak_Fénykép_Betöltés = new Ablak_Fénykép_Betöltés(hely, Fényazonosító.Text.Trim(), SorszámMax(hely, Fényazonosító.Text.Trim()) + 1);
                Új_Ablak_Fénykép_Betöltés.FormClosed += Új_Ablak_Fénykép_Betöltés_Closed;
                Új_Ablak_Fénykép_Betöltés.Top = 50;
                Új_Ablak_Fénykép_Betöltés.Left = 50;
                Új_Ablak_Fénykép_Betöltés.Változás += ÚjraListáz;
                Új_Ablak_Fénykép_Betöltés.Show();

                // 'képek másolása átnevezése
                if (FénySorszám.Text.Trim() == "") return;
                Képeklistázása();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int SorszámMax(string hely, string azonosító)
        {
            int sorszám = 0;
            //Megnézzük, hogy melyik az utolsó fénykép az adott azonosítóból
            DirectoryInfo dir = new DirectoryInfo(hely);
            foreach (FileInfo Elem in dir.GetFiles($"*{azonosító.Trim()}*.jpg"))
            {
                string[] darabol = Elem.Name.Split('_');
                string[] ideig = darabol[1].Split('.');
                if (int.TryParse(ideig[0], out int sor))
                    if (sorszám < sor) sorszám = sor;
            }
            return sorszám;
        }

        private void ÚjraListáz()
        {
            Képeklistázása();
        }

        private void Új_Ablak_Fénykép_Betöltés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fénykép_Betöltés = null;
        }

        private void KépTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                string honnan = $@"{Application.StartupPath}\Főmérnökség\adatok\Rezsiképek\";
                KépKeret.Visible = false;
                if (FényképLista.SelectedItems.Count == 0) return;
                if (MessageBox.Show("A kijelölt képeket biztos törölni akarja ?!", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    for (int i = 0; i < FényképLista.SelectedItems.Count; i++)
                        Delete(honnan + FényképLista.SelectedItems[i].ToString().Trim());

                    MessageBox.Show("A Képek törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Képeklistázása();
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


        #region Lista
        private void Lista_csoport_feltöltés()
        {
            try
            {
                List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
                List<string> Adatok = (from a in AdatokTörzs
                                       where a.Státusz == 0
                                       orderby a.Csoport
                                       select a.Csoport).ToList().Distinct().ToList();
                ListaCsoportCombo.Items.Clear();
                ListaCsoportCombo.BeginUpdate();
                foreach (string elem in Adatok)
                    ListaCsoportCombo.Items.Add(elem);
                ListaCsoportCombo.EndUpdate();
                ListaCsoportCombo.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Command20_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }

        int Képekszáma(string azonosító)
        {
            int válasz = 0;
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Rezsiképek".KönyvSzerk();
                DirectoryInfo di = new System.IO.DirectoryInfo(hely);
                System.IO.FileInfo[] aryFi = di.GetFiles($"*{azonosító}*");
                válasz = aryFi.Length;
            }
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


        private void Táblaíró()
        {
            try
            {
                Tábla.Visible = false;
                Tábla.CleanFilterAndSort();
                TáblaFejléc();
                TáblaTartalom();
                Tábla.DataSource = AdatTábla;
                TáblaOszlopSzélesség();
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
        }

        private void TáblaTartalom()
        {
            try
            {
                AdatTábla.Clear();
                AdatokTörzs = KézTörzs.Lista_Adatok();
                List<Adat_Rezsi_Törzs> Adatok = AdatokTörzs.Where(a => a.Státusz == 0).ToList();

                if (ListaCsoportCombo.Text.Trim() != "") Adatok = Adatok.Where(a => a.Csoport == ListaCsoportCombo.Text.Trim()).ToList();
                if (Lista_megnevezés_szűrő.Text.Trim() != "") Adatok = Adatok.Where(a => a.Megnevezés.ToUpper().Contains(Lista_megnevezés_szűrő.Text.Trim().ToUpper())).ToList();

                List<string> telephelyek = new List<string>();
                for (int e = 0; e < Cmbtelephely.Items.Count; e++)
                    telephelyek.Add(Cmbtelephely.Items[e].ToString());
                List<Adat_Rezsi_Lista> AdatokKész = KézRezsi.Lista_Adatok(telephelyek);

                foreach (Adat_Rezsi_Törzs rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Megnevezés"] = rekord.Megnevezés.Trim().ToUpper();
                    Soradat["Fénykép"] = Képekszáma(rekord.Azonosító);

                    double összesen = 0;
                    for (int g = 0; g < Cmbtelephely.Items.Count; g++)
                    {
                        Adat_Rezsi_Lista Elem = AdatokKész.Where(a => a.Azonosító == rekord.Azonosító && a.Telephely == Cmbtelephely.Items[g].ToString()).FirstOrDefault();
                        if (Elem != null)
                        {
                            Soradat[Cmbtelephely.Items[g].ToString()] = Elem.Mennyiség;
                            összesen += Elem.Mennyiség;
                        }
                        else
                            Soradat[Cmbtelephely.Items[g].ToString()] = 0;
                    }
                    Soradat["Összesen"] = összesen;
                    AdatTábla.Rows.Add(Soradat);
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

        private void TáblaFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();

                AdatTábla.Columns.Add("Azonosító");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Fénykép");
                for (int e = 0; e < Cmbtelephely.Items.Count; e++)
                    AdatTábla.Columns.Add(Cmbtelephely.Items[e].ToString());
                AdatTábla.Columns.Add("Összesen");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TáblaOszlopSzélesség()
        {
            Tábla.Columns["Azonosító"].Width = 150;
            Tábla.Columns["Megnevezés"].Width = 400;
            Tábla.Columns["Fénykép"].Width = 100;
            Tábla.Columns["Összesen"].Width = 100;
            for (int e = 0; e < Cmbtelephely.Items.Count; e++)
                Tábla.Columns[Cmbtelephely.Items[e].ToString()].Width = 100;
        }

        private void Excel_Click(object sender, EventArgs e)
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
                    FileName = $"Rezsi_készlet_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla, false);
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                if (e.RowIndex < 0)
                    return;
                Fényazonosító.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                Azonosító.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                TárAzonosító.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }
        #endregion


        #region Beraktározás
        private void Be_azonosító_feltöltés()
        {
            List<string> Adatok = (from a in AdatokTörzs
                                   orderby a.Azonosító
                                   select a.Azonosító).ToList().Distinct().ToList();

            BeAzonosító.Items.Clear();
            BeAzonosító.BeginUpdate();
            foreach (string elem in Adatok)
                BeAzonosító.Items.Add(elem);
            BeAzonosító.EndUpdate();
            BeAzonosító.Refresh();
        }

        private void BeHonnanraktár_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BeHonnanraktár.Text == "Raktár")
                BehovaRaktár.Text = "Rezsi Raktár";
            else
                BehovaRaktár.Text = "Raktár";
        }

        private void Beirány()
        {
            // raktári beérkezés
            BeHonnanraktár.Items.Clear();
            BeHonnanraktár.Items.Add("Raktár");
            BeHonnanraktár.Items.Add("Rezsi Raktár");
            BeHonnanraktár.Text = "Raktár";
        }

        private void BeAzonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Beazonosítókiírás();
        }

        private void Beazonosítókiírás()
        {
            try
            {
                if (BeAzonosító.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");

                Adat_Rezsi_Törzs Elem = (from a in AdatokTörzs
                                         where a.Azonosító == BeAzonosító.Text.Trim()
                                         select a).FirstOrDefault();
                if (Elem != null) BeMegnevezés.Text = Elem.Megnevezés;


                Adat_Rezsi_Lista Készlet = (from a in AdatokLista
                                            where a.Azonosító == BeAzonosító.Text.Trim()
                                            select a).FirstOrDefault();
                Bekészlet.Text = "0";
                if (Készlet != null) Bekészlet.Text = Készlet.Mennyiség.ToString();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BeRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                KézRezsi.Nagybetűs(Cmbtelephely.Text.Trim());
                if (BeMegnevezés.Text.Trim() == "") throw new HibásBevittAdat("Az Megnevezés mező kitöltése kötelező.");
                if (BeMennyiség.Text.Trim() == "") throw new HibásBevittAdat("Az Mennyiség mező kitöltése kötelező.");
                if (!double.TryParse(BeMennyiség.Text, out double mennyiségbe)) throw new HibásBevittAdat("A Mennyiség mezőnek számnak kell lennie.");
                if (!double.TryParse(Bekészlet.Text, out double készletbe)) throw new HibásBevittAdat("A Készlet mezőnek számnak kell lennie.");
                if (BeHonnanraktár.Text.Trim() == "Raktár" && készletbe + mennyiségbe < 0) throw new HibásBevittAdat("Negatív készlet nem lehet könyvelni!");
                if (BeHonnanraktár.Text.Trim() == "Rezsi Raktár" && készletbe + mennyiségbe < 0) throw new HibásBevittAdat("Negatív készlet nem lehet könyvelni!");

                Adat_Rezsi_Lista Elem = (from a in AdatokLista
                                         where a.Azonosító == BeAzonosító.Text.Trim()
                                         select a).FirstOrDefault();
                double Mennyiség;
                if (BeHonnanraktár.Text.Trim() == "Raktár")
                    Mennyiség = készletbe + mennyiségbe;
                else
                    Mennyiség = készletbe - mennyiségbe;

                Adat_Rezsi_Lista ADAT = new Adat_Rezsi_Lista(
                                        BeAzonosító.Text.Trim(),
                                        Mennyiség,
                                        DateTime.Today,
                                        false);

                if (Elem != null)
                    KézRezsi.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézRezsi.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                Adat_Rezsi_Listanapló ADATNapló = new Adat_Rezsi_Listanapló(
                                      MyF.Szöveg_Tisztítás(BeAzonosító.Text.Trim(), 0, 18),
                                      BeHonnanraktár.Text.Trim(),
                                      BehovaRaktár.Text.Trim(),
                                      mennyiségbe,
                                      "_",
                                      Program.PostásNév.Trim(),
                                      DateTime.Now,
                                      false);
                KézNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Now.Year, ADATNapló);

                AdatokLista = KézRezsi.Lista_Adatok(Cmbtelephely.Text.Trim());
                BeMennyiség.Text = "";

                Beazonosítókiírás();
                AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("Az adat rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion


        #region Kiraktár
        private void Lenyílókfeltöltés()
        {
            // raktári kiadás
            KiHonnanRaktár.Text = "Rezsi Raktár";
            KiHovaRaktár.Items.Clear();
            KiHovaRaktár.Items.Add("Kiadás");
            KiHovaRaktár.Items.Add("Készlet korrekció");
            KiHovaRaktár.Text = "Kiadás";
        }

        private void KI_azonosító_feltöltés()
        {
            List<string> Adatok = (from a in AdatokTörzs
                                   orderby a.Azonosító
                                   select a.Azonosító).ToList().Distinct().ToList();

            Kiazonosító.Items.Clear();
            Kiazonosító.BeginUpdate();
            foreach (string elem in Adatok)
                Kiazonosító.Items.Add(elem);
            Kiazonosító.EndUpdate();
            Kiazonosító.Refresh();
        }

        private void Kiazonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            KiAzonosítókiírás();
        }

        private void KiAzonosítókiírás()
        {
            try
            {
                if (Kiazonosító.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");

                Adat_Rezsi_Törzs Elem = (from a in AdatokTörzs
                                         where a.Azonosító == Kiazonosító.Text.Trim()
                                         select a).FirstOrDefault();
                KiMegnevezés.Text = "";
                if (Elem != null) KiMegnevezés.Text = Elem.Megnevezés;

                // készlet kiírása
                KiKészlet.Text = "0";
                Adat_Rezsi_Lista Készlet = (from a in AdatokLista
                                            where a.Azonosító == Kiazonosító.Text.Trim()
                                            select a).FirstOrDefault();

                if (Készlet != null) KiKészlet.Text = Készlet.Mennyiség.ToString();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kirögzít_Click(object sender, EventArgs e)
        {
            try
            {
                KézRezsi.Nagybetűs(Cmbtelephely.Text.Trim());
                if (Kiazonosító.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");
                if (KiMegnevezés.Text.Trim() == "") throw new HibásBevittAdat("Az Megnevezés mező kitöltése kötelező.");
                if (KiMennyiség.Text.Trim() == "") throw new HibásBevittAdat("Adja meg a mennyiséget.");
                if (KiFelhasználás.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a felhasználás mezőt.");
                if (!double.TryParse(KiMennyiség.Text, out double mennyiségki)) throw new HibásBevittAdat("A mennyiség mezőnek számnak kell lennie.");
                if (!double.TryParse(KiKészlet.Text, out double készletki)) throw new HibásBevittAdat("A Készlet mezőnek számnak kell lennie.");
                if (készletki - mennyiségki < 0) throw new HibásBevittAdat("Negatív készlet nem lehet könyvelni!");

                Adat_Rezsi_Lista Készlet = (from a in AdatokLista
                                            where a.Azonosító == Kiazonosító.Text.Trim()
                                            select a).FirstOrDefault();
                double Mennyiség;
                if (KiHovaRaktár.Text.Trim() == "Kiadás")
                    Mennyiség = készletki - mennyiségki;
                else
                    Mennyiség = készletki + mennyiségki;

                Adat_Rezsi_Lista ADAT = new Adat_Rezsi_Lista(
                                    Kiazonosító.Text.Trim(),
                                    Mennyiség,
                                    DateTime.Today,
                                    false);

                if (Készlet != null)
                    KézRezsi.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézRezsi.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                Adat_Rezsi_Listanapló ADATNapló = new Adat_Rezsi_Listanapló(
                      MyF.Szöveg_Tisztítás(Kiazonosító.Text.Trim(), 0, 18),
                      KiHonnanRaktár.Text.Trim(),
                      KiHovaRaktár.Text.Trim(),
                      készletki,
                      KiFelhasználás.Text.Trim(),
                      Program.PostásNév.Trim(),
                      DateTime.Now,
                      false);
                KézNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Now.Year, ADATNapló);

                AdatokLista = KézRezsi.Lista_Adatok(Cmbtelephely.Text.Trim());
                KiAzonosítókiírás();
                AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
                KiMennyiség.Text = "";
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


        #region Napló
        private void Napló_azonosító_feltöltés()
        {
            List<string> Adatok = (from a in AdatokTörzs
                                   orderby a.Azonosító
                                   select a.Azonosító).ToList().Distinct().ToList();
            Azonosító_napló.Items.Clear();
            Azonosító_napló.BeginUpdate();
            foreach (string elem in Adatok)
                Azonosító_napló.Items.Add(elem);
            Azonosító_napló.EndUpdate();
            Azonosító_napló.Refresh();
        }

        private void Excelclick_Click(object sender, EventArgs e)
        {
            try
            {
                if (Napló_tábla.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Rezsi_napló_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Napló_tábla, false);
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

        private void Azonosító_napló_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Azonosító_napló.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");

            Adat_Rezsi_Törzs Elem = (from a in AdatokTörzs
                                     where a.Azonosító == Azonosító_napló.Text.Trim()
                                     select a).FirstOrDefault();
            if (Elem != null) Megnevezés_napló.Text = Elem.Megnevezés;
        }

        private void Listáz_Click(object sender, EventArgs e)
        {
            Táblaíró_napló();
        }

        private void Táblaíró_napló()
        {
            try
            {
                KézNapló.Nagybetűs(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);

                Napló_tábla.Visible = false;
                Tár_tábla.CleanFilterAndSort();
                NaplóTáblaFejléc();
                NaplóTáblaTartalom();
                Napló_tábla.DataSource = AdatTáblaNapló;
                NaplóTáblaOszlopSzélesség();
                Napló_tábla.Visible = true;
                Napló_tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NaplóTáblaFejléc()
        {
            try
            {
                AdatTáblaNapló.Columns.Clear();
                AdatTáblaNapló.Columns.Add("Azonosító");
                AdatTáblaNapló.Columns.Add("Megnevezés");
                AdatTáblaNapló.Columns.Add("Honnan");
                AdatTáblaNapló.Columns.Add("Hova");
                AdatTáblaNapló.Columns.Add("Mennyiség");
                AdatTáblaNapló.Columns.Add("Ki vitte el");
                AdatTáblaNapló.Columns.Add("Rögzítő");
                AdatTáblaNapló.Columns.Add("Rögzítés dátuma");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NaplóTáblaOszlopSzélesség()
        {
            Napló_tábla.Columns["Azonosító"].Width = 150;
            Napló_tábla.Columns["Megnevezés"].Width = 400;
            Napló_tábla.Columns["Honnan"].Width = 150;
            Napló_tábla.Columns["Hova"].Width = 150;
            Napló_tábla.Columns["Mennyiség"].Width = 100;
            Napló_tábla.Columns["Ki vitte el"].Width = 200;
            Napló_tábla.Columns["Rögzítő"].Width = 200;
            Napló_tábla.Columns["Rögzítés dátuma"].Width = 170;
        }

        private void NaplóTáblaTartalom()
        {
            try
            {
                AdatTáblaNapló.Clear();

                AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);

                List<Adat_Rezsi_Listanapló> Adatok;
                if (Azonosító_napló.Text.Trim() != "")
                    Adatok = (from a in AdatokNapló
                              where a.Módosításidátum >= Dátumtól.Value
                              && a.Módosításidátum <= Dátumig.Value.AddDays(1)
                              && a.Azonosító == Azonosító_napló.Text.Trim()
                              orderby a.Módosításidátum
                              select a).ToList();
                else
                    Adatok = (from a in AdatokNapló
                              where a.Módosításidátum >= Dátumtól.Value
                              && a.Módosításidátum <= Dátumig.Value.AddDays(1)
                              orderby a.Módosításidátum
                              select a).ToList();

                Holtart.Be(Adatok.Count + 1);
                AdatokTörzs = KézTörzs.Lista_Adatok();
                foreach (Adat_Rezsi_Listanapló rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaNapló.NewRow();
                    Adat_Rezsi_Törzs rekordszer = (from a in AdatokTörzs
                                                   where a.Azonosító.ToUpper() == rekord.Azonosító.ToUpper()
                                                   select a).FirstOrDefault();
                    if (rekordszer != null)
                        Soradat["Megnevezés"] = rekordszer.Megnevezés.Trim();
                    else
                        Soradat["Megnevezés"] = "";

                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Honnan"] = rekord.Honnan;
                    Soradat["Hova"] = rekord.Hova;
                    Soradat["Mennyiség"] = rekord.Mennyiség;
                    Soradat["Ki vitte el"] = rekord.Mirehasznál;
                    Soradat["Rögzítő"] = rekord.Módosította;
                    Soradat["Rögzítés dátuma"] = rekord.Módosításidátum;

                    AdatTáblaNapló.Rows.Add(Soradat);

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

        private void Dátumtól_ValueChanged(object sender, EventArgs e)
        {
            AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
        }
        #endregion


    }
}