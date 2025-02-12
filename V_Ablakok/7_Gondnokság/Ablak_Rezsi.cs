using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.Közös;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Rezsi
    {
        readonly Kezelő_Rezsi KézRezsi = new Kezelő_Rezsi();
        readonly Kezelő_Rezsi_Törzs KézTörzs = new Kezelő_Rezsi_Törzs();

        List<Adat_Rezsi_Törzs> AdatokTörzs = new List<Adat_Rezsi_Törzs>();
        List<Adat_Rezsi_Hely> AdatokHely = new List<Adat_Rezsi_Hely>();
        List<Adat_Rezsi_Lista> AdatokLista = new List<Adat_Rezsi_Lista>();
        List<Adat_Rezsi_Listanapló> AdatokNapló = new List<Adat_Rezsi_Listanapló>();


        #region Alap
        public Ablak_Rezsi()
        {
            InitializeComponent();
        }

        private void Ablak_Rezsi_könyvelés_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\rezsiképek";
            if (!Exists(hely)) Directory.Exists(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi";

            if (!Exists(hely)) Directory.Exists(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsihely.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.Rezsihely(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsinapló" + DateTime.Now.Year.ToString() + ".mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.Rezsilistanapló(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsikönyv.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.Rezsilista(hely);



            Dátumtól.Value = new DateTime(DateTime.Now.Year, 1, 1);
            Dátumig.Value = DateTime.Today;

            AdatokTörzs = KézTörzs.Lista_Adatok();
            AdatokHely = RezsiHelyFeltöltés();
            AdatokLista = RezsiKészletFeltöltés();
            AdatokNapló = RezsiNaplóFeltöltés();

            Lapfülek.SelectedIndex = 3;
            Fülekkitöltése();

            Jogosultságkiosztás();
            Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Ablak_Rezsi_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fénykép_Betöltés?.Close();
            Új_Ablak_Kereső?.Close();
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
                                        Azonosító.Text.Trim(),
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
                MyE.EXCELtábla(fájlexc, _Törzs_tábla, false);
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
                Törzs_tábla.Rows.Clear();
                Törzs_tábla.Columns.Clear();
                Törzs_tábla.Refresh();
                Törzs_tábla.Visible = false;
                Törzs_tábla.ColumnCount = 5;

                // fejléc elkészítése
                Törzs_tábla.Columns[0].HeaderText = "Azonosító";
                Törzs_tábla.Columns[0].Width = 150;
                Törzs_tábla.Columns[1].HeaderText = "Megnevezés";
                Törzs_tábla.Columns[1].Width = 400;
                Törzs_tábla.Columns[2].HeaderText = "Méret";
                Törzs_tábla.Columns[2].Width = 200;
                Törzs_tábla.Columns[3].HeaderText = "Csoport";
                Törzs_tábla.Columns[3].Width = 200;
                Törzs_tábla.Columns[4].HeaderText = "Aktív";
                Törzs_tábla.Columns[4].Width = 100;
                List<Adat_Rezsi_Törzs> AdatokTörzs = KézTörzs.Lista_Adatok();
                foreach (Adat_Rezsi_Törzs rekord in AdatokTörzs)
                {
                    Törzs_tábla.RowCount++;
                    int i = Törzs_tábla.RowCount - 1;
                    Törzs_tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Törzs_tábla.Rows[i].Cells[1].Value = rekord.Megnevezés.Trim();
                    Törzs_tábla.Rows[i].Cells[2].Value = rekord.Méret.Trim();
                    Törzs_tábla.Rows[i].Cells[3].Value = rekord.Csoport.Trim();
                    if (rekord.Státusz == 0)
                        Törzs_tábla.Rows[i].Cells[4].Value = "Aktív";
                    else
                        Törzs_tábla.Rows[i].Cells[4].Value = "Törölt";
                }

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

        private void Törzs_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) throw new HibásBevittAdat("A táblázatban megjelölt hely nem listázható.");
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
                MyE.EXCELtábla(fájlexc, _Tár_tábla, false);
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
                Tár_tábla.Rows.Clear();
                Tár_tábla.Columns.Clear();
                Tár_tábla.Refresh();
                Tár_tábla.Visible = false;
                Tár_tábla.ColumnCount = 6;

                // fejléc elkészítése
                Tár_tábla.Columns[0].HeaderText = "Azonosító";
                Tár_tábla.Columns[0].Width = 150;
                Tár_tábla.Columns[1].HeaderText = "Megnevezés";
                Tár_tábla.Columns[1].Width = 400;
                Tár_tábla.Columns[2].HeaderText = "Helység";
                Tár_tábla.Columns[2].Width = 200;
                Tár_tábla.Columns[3].HeaderText = "Állvány";
                Tár_tábla.Columns[3].Width = 200;
                Tár_tábla.Columns[4].HeaderText = "Polc";
                Tár_tábla.Columns[4].Width = 100;
                Tár_tábla.Columns[5].HeaderText = "Megjegyzés";
                Tár_tábla.Columns[5].Width = 300;

                foreach (Adat_Rezsi_Törzs rekord in AdatokTörzs)
                {
                    Tár_tábla.RowCount++;
                    int i = Tár_tábla.RowCount - 1;
                    Tár_tábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Tár_tábla.Rows[i].Cells[1].Value = rekord.Megnevezés;

                    Adat_Rezsi_Hely rekordszer = (from a in AdatokHely
                                                  where a.Azonosító == rekord.Azonosító
                                                  select a).FirstOrDefault();
                    if (rekordszer != null)
                    {
                        Tár_tábla.Rows[i].Cells[2].Value = rekordszer.Helyiség;
                        Tár_tábla.Rows[i].Cells[3].Value = rekordszer.Állvány;
                        Tár_tábla.Rows[i].Cells[4].Value = rekordszer.Polc;
                        Tár_tábla.Rows[i].Cells[5].Value = rekordszer.Megjegyzés;
                    }
                }
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
                string szöveg;
                if (Elem != null)
                {
                    szöveg = "UPDATE tábla  SET ";
                    szöveg += $"Helyiség='{Helyiség.Text.Trim()}', ";
                    szöveg += $"Állvány='{Állvány.Text.Trim()} ', ";
                    szöveg += $"polc='{Polc.Text.Trim()}', ";
                    szöveg += $"Megjegyzés='{Megjegyzés.Text.Trim()}' ";
                    szöveg += $" WHERE azonosító='{TárAzonosító.Text.Trim()}'";
                }
                else
                {
                    szöveg = "INSERT INTO tábla (azonosító, helyiség, állvány, polc, megjegyzés) VALUES (";
                    szöveg += $"'{TárAzonosító.Text.Trim()}', ";
                    szöveg += $"'{Helyiség.Text.Trim()}', ";
                    szöveg += $"'{Állvány.Text.Trim()}', ";
                    szöveg += $"'{Polc.Text.Trim()}', ";
                    szöveg += $"'{Megjegyzés.Text.Trim()}') ";
                }

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsihely.mdb";
                string jelszó = "csavarhúzó";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                AdatokHely = RezsiHelyFeltöltés();

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
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Rezsiképek";
                if (!System.IO.Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);

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
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Rezsiképek\" + FényképLista.SelectedItems[0].ToString();
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
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Rezsiképek";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);


                Új_Ablak_Fénykép_Betöltés?.Close();

                Új_Ablak_Fénykép_Betöltés = new Ablak_Fénykép_Betöltés(hely, Fényazonosító.Text.Trim(), SorszámMax(hely, Fényazonosító.Text.Trim()) + 1);
                Új_Ablak_Fénykép_Betöltés.FormClosed += Új_Ablak_Fénykép_Betöltés_Closed;
                Új_Ablak_Fénykép_Betöltés.Top = 50;
                Új_Ablak_Fénykép_Betöltés.Left = 50;
                Új_Ablak_Fénykép_Betöltés.Változás += ÚjraListáz;
                Új_Ablak_Fénykép_Betöltés.Show();

                // 'képek másolása átnevezése
                if (FénySorszám.Text.Trim() == "")
                    return;
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
                string honnan = Application.StartupPath + @"\Főmérnökség\adatok\Rezsiképek\";
                KépKeret.Visible = false;
                if (FényképLista.SelectedItems.Count == 0)
                    return;
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
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\Rezsi\rezsitörzs.mdb";
            string jelszó = "csavarhúzó";
            string szöveg = "SELECT DISTINCT csoport FROM törzs WHERE státus= 0 ORDER BY csoport ";

            ListaCsoportCombo.Items.Clear();
            ListaCsoportCombo.BeginUpdate();
            ListaCsoportCombo.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "csoport"));
            ListaCsoportCombo.EndUpdate();
            ListaCsoportCombo.Refresh();
        }


        private void Command20_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }


        int Képekszáma(string azonosító)
        {
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\Rezsiképek";
            DirectoryInfo di = new System.IO.DirectoryInfo(hely);
            System.IO.FileInfo[] aryFi = di.GetFiles($"*{azonosító}*");
            return aryFi.Length;
        }


        private void Táblaíró()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Rezsi\rezsitörzs.mdb";
                string szöveg = "SELECT * FROM törzs where státus=0 ";
                if (ListaCsoportCombo.Text.Trim() != "")
                    szöveg += $" and csoport='{ListaCsoportCombo.Text.Trim()}'";
                if (Lista_megnevezés_szűrő.Text.Trim() != "")
                    szöveg += $" AND megnevezés LIKE '%{Lista_megnevezés_szűrő.Text.Trim()}%'";

                szöveg += " order by azonosító";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 3;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Azonosító";
                Tábla.Columns[0].Width = 200;
                Tábla.Columns[1].HeaderText = "Megnevezés";
                Tábla.Columns[1].Width = 400;
                Tábla.Columns[2].HeaderText = "Fénykép";
                Tábla.Columns[2].Width = 100;

                Kezelő_Rezsi kéz = new Kezelő_Rezsi();


                foreach (Adat_Rezsi_Törzs rekord in AdatokTörzs)
                {
                    // ha nincs kitöltve a mező , vagy  ha azt a szöveget tartalmazza
                    string ideigmegnevezés = rekord.Megnevezés.Trim().ToUpper();

                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Tábla.Rows[i].Cells[1].Value = rekord.Megnevezés;
                    Tábla.Rows[i].Cells[2].Value = Képekszáma(rekord.Azonosító);

                }

                if (Tábla.Rows.Count > 0)
                {
                    Tábla_kiírás_folyt();
                }
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


        private void Tábla_kiírás_folyt()
        {
            try
            {
                string jelszó = "csavarhúzó";
                string szöveg = "select * from könyv ORDER BY azonosító";
                // telephelyi készletek felirat

                if (Program.Postás_Vezér)
                {
                    for (int k = 0; k < Cmbtelephely.Items.Count; k++)
                    {
                        string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Items[k].ToString().Trim() + @"\Adatok\Rezsi\rezsikönyv.mdb";

                        if (Exists(hely))
                        {
                            Tábla.ColumnCount += 1;
                            Tábla.Columns[Tábla.ColumnCount - 1].HeaderText = Cmbtelephely.Items[k].ToString().Trim();
                            Tábla.Columns[Tábla.ColumnCount - 1].Width = 100;

                            Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

                            for (int ij = 0; ij < Tábla.Rows.Count; ij++)
                            {
                                Tábla.Rows[ij].Cells[Tábla.ColumnCount - 1].Value = 0;
                            }


                            Kezelő_Rezsi kéz = new Kezelő_Rezsi();
                            List<Adat_Rezsi_Lista> Adatok = kéz.Lista_Adatok_Lista(hely, jelszó, szöveg);

                            int i = 0;
                            int hiba = 0;

                            foreach (Adat_Rezsi_Lista rekordszer in Adatok)
                            {
                                if (String.Compare(Tábla.Rows[i].Cells[0].Value.ToString().Trim(), rekordszer.Azonosító.ToString().Trim()) <= 0)
                                {
                                    // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                                    while (String.Compare(Tábla.Rows[i].Cells[0].Value.ToString().Trim(), rekordszer.Azonosító.ToString().Trim()) < 0)
                                    {
                                        Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value = 0;
                                        i += 1;
                                        if (i == Tábla.Rows.Count)
                                        {
                                            hiba = 1;
                                            break;
                                        }
                                    }

                                    if (hiba == 1)
                                        break;
                                    while (String.Compare(Tábla.Rows[i].Cells[0].Value.ToString().Trim(), rekordszer.Azonosító.ToString().Trim()) <= 0)
                                    {
                                        if (Tábla.Rows[i].Cells[0].Value.ToString().Trim() == rekordszer.Azonosító.Trim())
                                        {
                                            // ha egyforma akkor kiírjuk
                                            Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value = rekordszer.Mennyiség;
                                        }
                                        i += 1;
                                        if (i == Tábla.Rows.Count)
                                        {
                                            hiba = 1;
                                            break;
                                        }
                                    }
                                    if (hiba == 1)
                                        break;
                                }
                            }
                        }
                    }

                    // összesítő sorok
                    int összeg;
                    Tábla.ColumnCount += 1;
                    Tábla.Columns[Tábla.ColumnCount - 1].HeaderText = "Összesen";
                    Tábla.Columns[Tábla.ColumnCount - 1].Width = 100;

                    for (int i = 0; i < Tábla.Rows.Count; i++)
                    {
                        összeg = 0;
                        for (int kk = 3; kk < Tábla.Columns.Count - 1; kk++)
                        {
                            if (!double.TryParse(Tábla.Rows[i].Cells[kk].Value.ToString(), out double ideig))
                                ideig = 0;

                            összeg = (int)(összeg + ideig);
                        }
                        Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value = összeg;
                    }
                }

                else
                {
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsikönyv.mdb";
                    // sorbarendezzük a táblát azonosító szerint

                    Tábla.ColumnCount += 1;
                    Tábla.Columns[3].HeaderText = "Készlet";
                    Tábla.Columns[3].Width = 100;

                    Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

                    Kezelő_Rezsi kéz = new Kezelő_Rezsi();
                    List<Adat_Rezsi_Lista> Adatok = kéz.Lista_Adatok_Lista(hely, jelszó, szöveg);

                    int i = 0;
                    int hiba = 0;

                    foreach (Adat_Rezsi_Lista rekordszer in Adatok)
                    {
                        if (String.Compare(Tábla.Rows[i].Cells[0].Value.ToString().Trim(), rekordszer.Azonosító.ToString().Trim()) <= 0)
                        {
                            // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                            while (String.Compare(Tábla.Rows[i].Cells[0].Value.ToString().Trim(), rekordszer.Azonosító.ToString().Trim()) < 0)
                            {
                                Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value = 0;
                                i += 1;
                                if (i >= Tábla.Rows.Count)
                                {
                                    hiba = 1;
                                    break;
                                }
                            }

                            if (hiba == 1)
                                break;
                            while (String.Compare(Tábla.Rows[i].Cells[0].Value.ToString().Trim(), rekordszer.Azonosító.ToString().Trim()) <= 0)
                            {
                                if (Tábla.Rows[i].Cells[0].Value.ToString().Trim() == rekordszer.Azonosító.ToString().Trim())
                                {
                                    // ha egyforma akkor kiírjuk
                                    Tábla.Rows[i].Cells[3].Value = (object)rekordszer.Mennyiség;
                                }
                                i += 1;
                                if (i >= Tábla.Rows.Count)
                                {
                                    hiba = 1;
                                    break;
                                }
                            }
                            if (hiba == 1)
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


        Ablak_Kereső Új_Ablak_Kereső;
        private void Keresés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Új_Ablak_Kereső == null)
                {
                    Új_Ablak_Kereső = new Ablak_Kereső();
                    Új_Ablak_Kereső.FormClosed += Új_Ablak_Kereső_Closed;
                    Új_Ablak_Kereső.Top = 50;
                    Új_Ablak_Kereső.Left = 50;
                    Új_Ablak_Kereső.Show();
                    Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés;
                }
                else
                {
                    Új_Ablak_Kereső.Activate();
                    Új_Ablak_Kereső.WindowState = FormWindowState.Normal;
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

        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }

        private void Szövegkeresés()
        {
            // megkeressük a szöveget a táblázatban
            if (Új_Ablak_Kereső.Keresendő == null)
                return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "")
                return;

            if (Tábla.Rows.Count < 0)
                return;


            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                if (Tábla.Rows[i].Cells[1].Value != null)
                {
                    if (Tábla.Rows[i].Cells[1].Value.ToStrTrim().Contains(Új_Ablak_Kereső.Keresendő.Trim()))
                    {
                        Tábla.Rows[i].Cells[1].Style.BackColor = Color.Orange;
                        Tábla.FirstDisplayedScrollingRowIndex = i;
                        Tábla.CurrentCell = Tábla.Rows[i].Cells[1];
                    }
                }
            }
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


        private void BeRögzít_Click(object sender, EventArgs e)
        {
            try
            {

                if (BeMegnevezés.Text.Trim() == "") throw new HibásBevittAdat("Az Megnevezés mező kitöltése kötelező.");
                if (BeMennyiség.Text.Trim() == "") throw new HibásBevittAdat("Az Mennyiség mező kitöltése kötelező.");
                if (!double.TryParse(BeMennyiség.Text, out double mennyiségbe)) throw new HibásBevittAdat("A Mennyiség mezőnek számnak kell lennie.");
                if (!double.TryParse(Bekészlet.Text, out double készletbe)) throw new HibásBevittAdat("A Készlet mezőnek számnak kell lennie.");
                if (BeHonnanraktár.Text.Trim() == "Raktár" && készletbe + mennyiségbe < 0) throw new HibásBevittAdat("Negatív készlet nem lehet könyvelni!");
                if (BeHonnanraktár.Text.Trim() == "Rezsi Raktár" && készletbe + mennyiségbe > 0) throw new HibásBevittAdat("Negatív készlet nem lehet könyvelni!");

                BeRögzítés();
                BeNaplózás();

                AdatokLista = RezsiKészletFeltöltés();
                BeMennyiség.Text = "";

                Beazonosítókiírás();
                AdatokNapló = RezsiNaplóFeltöltés();
            }
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


        private void BeRögzítés()
        {
            try
            {
                if (!double.TryParse(Bekészlet.Text, out double készletbe)) throw new HibásBevittAdat("A Készlet mezőnek számnak kell lennie.");
                if (!double.TryParse(BeMennyiség.Text, out double mennyiségbe)) throw new HibásBevittAdat("A Mennyiség mezőnek számnak kell lennie.");

                string jelszó = "csavarhúzó";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsikönyv.mdb";

                Adat_Rezsi_Lista Elem = (from a in AdatokLista
                                         where a.Azonosító == BeAzonosító.Text.Trim()
                                         select a).FirstOrDefault();
                double Mennyiség;
                string szöveg;
                if (Elem != null)
                {
                    // ha van
                    if (BeHonnanraktár.Text.Trim() == "Raktár")
                        Mennyiség = készletbe + mennyiségbe;
                    else
                        Mennyiség = készletbe - mennyiségbe;

                    szöveg = "UPDATE könyv  SET ";
                    szöveg += $"Mennyiség={Mennyiség.ToString().Replace(',', '.')}, ";
                    szöveg += "státus=false, ";
                    szöveg += $"dátum ='{DateTime.Today}' ";
                    szöveg += $" WHERE [azonosító]='{BeAzonosító.Text.Trim()}'";
                }
                else
                {
                    // ha nincs
                    szöveg = "INSERT INTO könyv (azonosító, Mennyiség, dátum, státus ) VALUES (";
                    szöveg += $"'{MyF.Szöveg_Tisztítás(BeAzonosító.Text.Trim(), 0, 18)}', ";
                    szöveg += $"{mennyiségbe.ToString().Replace(',', '.')}, ";
                    szöveg += $"'{DateTime.Today}' , false)";
                }

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


        private void BeNaplózás()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsinapló{DateTime.Today.Year}.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.Rezsilistanapló(hely);
            string jelszó = "csavarhúzó";

            string szöveg = "INSERT INTO napló (Azonosító, honnan, hova, mennyiség, státus, módosította, mirehasznál, módosításidátum) VALUES (";
            szöveg += $"'{MyF.Szöveg_Tisztítás(BeAzonosító.Text.Trim(), 0, 18)}', ";
            szöveg += $"'{BeHonnanraktár.Text.Trim()}', ";
            szöveg += $"'{BehovaRaktár.Text.Trim()}', ";
            szöveg += $"{BeMennyiség.Text.Trim().Replace(',', '.')}, false, ";
            szöveg += $"'{Program.PostásNév.Trim()}', '_', ";
            szöveg += $"'{DateTime.Now}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsikönyv.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Rezsilista(hely);

                if (Kiazonosító.Text.Trim() == "") throw new HibásBevittAdat("Az azonosító mező kitöltése kötelező.");
                if (KiMegnevezés.Text.Trim() == "") throw new HibásBevittAdat("Az Megnevezés mező kitöltése kötelező.");
                if (KiMennyiség.Text.Trim() == "") throw new HibásBevittAdat("Adja meg a mennyiséget.");
                if (KiFelhasználás.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a felhasználás mezőt.");
                if (!double.TryParse(KiMennyiség.Text, out double mennyiségki)) throw new HibásBevittAdat("A mennyiség mezőnek számnak kell lennie.");
                if (!double.TryParse(KiKészlet.Text, out double készletki)) throw new HibásBevittAdat("A Készlet mezőnek számnak kell lennie.");
                if (készletki - mennyiségki < 0) throw new HibásBevittAdat("Negatív készlet nem lehet könyvelni!");

                KiRögzítés();
                KiNaplózás();
                AdatokLista = RezsiKészletFeltöltés();
                KiAzonosítókiírás();
                AdatokNapló = RezsiNaplóFeltöltés();
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


        private void KiRögzítés()
        {
            try
            {
                if (!double.TryParse(KiMennyiség.Text, out double mennyiségki)) throw new HibásBevittAdat("A mennyiség mezőnek számnak kell lennie.");
                if (!double.TryParse(KiKészlet.Text, out double készletki)) throw new HibásBevittAdat("A Készlet mezőnek számnak kell lennie.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsikönyv.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Rezsilista(hely);

                Adat_Rezsi_Lista Készlet = (from a in AdatokLista
                                            where a.Azonosító == Kiazonosító.Text.Trim()
                                            select a).FirstOrDefault();
                string szöveg;
                double Mennyiség;

                if (Készlet != null)
                {
                    // ha van
                    if (KiHovaRaktár.Text.Trim() == "Kiadás")
                        Mennyiség = készletki - mennyiségki;
                    else
                        Mennyiség = készletki + mennyiségki;

                    szöveg = "UPDATE könyv  SET ";
                    szöveg += $" Mennyiség={Mennyiség.ToString().Replace(',', '.')}, ";
                    szöveg += $" státus=false, ";
                    szöveg += $" dátum ='{DateTime.Today}'";
                    szöveg += $" WHERE [azonosító]='{Kiazonosító.Text.Trim()}'";
                }
                else
                {
                    // ha nincs
                    szöveg = "INSERT INTO könyv (azonosító, Mennyiség, dátum, státus ) VALUES (";
                    szöveg += $"'{MyF.Szöveg_Tisztítás(Kiazonosító.Text.Trim(), 0, 18)}', ";
                    szöveg += $"{mennyiségki.ToString().Replace(',', '.')}, ";
                    szöveg += $"'{DateTime.Today}' , false)";
                }
                string jelszó = "csavarhúzó";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                AdatokLista = RezsiKészletFeltöltés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void KiNaplózás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsinapló{DateTime.Today.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Rezsilistanapló(hely);

                string jelszó = "csavarhúzó";
                string szöveg = "INSERT INTO napló (Azonosító, honnan, hova, mennyiség, státus, módosította, Mirehasznál, módosításidátum) VALUES (";
                szöveg += $"'{MyF.Szöveg_Tisztítás(Kiazonosító.Text, 0, 18)}', ";
                szöveg += $"'{KiHonnanRaktár.Text.Trim()}', ";
                szöveg += $"'{KiHovaRaktár.Text.Trim()}', ";
                szöveg += $"{KiMennyiség.Text.ToString().Replace(',', '.')}, false, ";
                szöveg += $"'{Program.PostásNév.Trim()}', ";
                szöveg += $"'{KiFelhasználás.Text.Trim()}', ";
                szöveg += $"'{DateTime.Now}')";
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
                AdatokNapló = RezsiNaplóFeltöltés();

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

                Napló_tábla.Rows.Clear();
                Napló_tábla.Columns.Clear();
                Napló_tábla.Refresh();
                Napló_tábla.Visible = false;
                Napló_tábla.ColumnCount = 8;

                // fejléc elkészítése
                Napló_tábla.Columns[0].HeaderText = "Azonosító";
                Napló_tábla.Columns[0].Width = 150;
                Napló_tábla.Columns[1].HeaderText = "Megnevezés";
                Napló_tábla.Columns[1].Width = 400;
                Napló_tábla.Columns[2].HeaderText = "Honnan";
                Napló_tábla.Columns[2].Width = 150;
                Napló_tábla.Columns[3].HeaderText = "Hova";
                Napló_tábla.Columns[3].Width = 150;
                Napló_tábla.Columns[4].HeaderText = "Mennyiség";
                Napló_tábla.Columns[4].Width = 100;
                Napló_tábla.Columns[5].HeaderText = "Ki vitte el";
                Napló_tábla.Columns[5].Width = 200;
                Napló_tábla.Columns[6].HeaderText = "Rögzítő";
                Napló_tábla.Columns[6].Width = 200;
                Napló_tábla.Columns[7].HeaderText = "Rögzítés dátuma";
                Napló_tábla.Columns[7].Width = 170;

                Holtart.Be(Adatok.Count + 1);

                foreach (Adat_Rezsi_Listanapló rekord in Adatok)
                {
                    Napló_tábla.RowCount++;
                    int i = Napló_tábla.RowCount - 1;
                    Napló_tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Napló_tábla.Rows[i].Cells[2].Value = rekord.Honnan.Trim();
                    Napló_tábla.Rows[i].Cells[3].Value = rekord.Hova.Trim();
                    Napló_tábla.Rows[i].Cells[4].Value = rekord.Mennyiség;
                    Napló_tábla.Rows[i].Cells[5].Value = rekord.Mirehasznál.Trim();
                    Napló_tábla.Rows[i].Cells[6].Value = rekord.Módosította.Trim();
                    Napló_tábla.Rows[i].Cells[7].Value = rekord.Módosításidátum.ToString();

                    Adat_Rezsi_Törzs rekordszer = (from a in AdatokTörzs
                                                   where a.Azonosító == rekord.Azonosító
                                                   select a).FirstOrDefault();
                    if (rekordszer != null) Napló_tábla.Rows[i].Cells[1].Value = rekordszer.Megnevezés.Trim();

                    Holtart.Lép();
                }

                Holtart.Ki();
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


        private void Dátumtól_ValueChanged(object sender, EventArgs e)
        {
            AdatokNapló = RezsiNaplóFeltöltés();
        }
        #endregion


        #region Listák



        private List<Adat_Rezsi_Hely> RezsiHelyFeltöltés()
        {
            List<Adat_Rezsi_Hely> Adatok = new List<Adat_Rezsi_Hely>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsihely.mdb";
                string jelszó = "csavarhúzó";
                string szöveg = "SELECT * FROM tábla";
                Adatok = KézRezsi.Lista_Adatok_Hely(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;

        }

        private List<Adat_Rezsi_Lista> RezsiKészletFeltöltés()
        {
            List<Adat_Rezsi_Lista> Adatok = new List<Adat_Rezsi_Lista>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsikönyv.mdb";
                string jelszó = "csavarhúzó";
                string szöveg = "SELECT * FROM könyv";
                Adatok = KézRezsi.Lista_Adatok_Lista(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;

        }

        private List<Adat_Rezsi_Listanapló> RezsiNaplóFeltöltés()
        {
            List<Adat_Rezsi_Listanapló> Adatok = new List<Adat_Rezsi_Listanapló>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Rezsi\rezsinapló{Dátumtól.Value.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Rezsilistanapló(hely);
                string jelszó = "csavarhúzó";
                string szöveg = "SELECT * FROM napló";
                Adatok = KézRezsi.Lista_Adatok_Listanapló(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;

        }
        #endregion


    }
}