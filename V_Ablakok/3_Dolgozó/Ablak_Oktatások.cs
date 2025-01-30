using PdfiumViewer;
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
    public partial class Ablak_Oktatások
    {
        readonly Kezelő_Oktatás_Napló Kéz_Okt_Nap = new Kezelő_Oktatás_Napló();
        readonly Kezelő_Oktatásrajelöltek Kéz_OktJelölt = new Kezelő_Oktatásrajelöltek();

        List<Adat_Oktatás_Napló> Adatok_Okt_Nap = new List<Adat_Oktatás_Napló>();
        List<Adat_Oktatásrajelöltek> Adatok_OktJelölt = new List<Adat_Oktatásrajelöltek>();
        string VálasztottOktatás = "";

        public Ablak_Oktatások()
        {
            InitializeComponent();
        }
        string ListaNév = "";

        private void AblakOktatások_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Csoportfeltöltés();
            Névfeltöltés();
            Oktatásistátusok();
            TáblaOktatáslistázás();
            PDF_néző.Width = TPOktatásRögz.Width;
            Okatatófeltöltés();
            Tárgyfeltöltés();

            BizDátum.Value = DateTime.Now;
            OktDátum.Value = DateTime.Now;
            Dátumig.Value = DateTime.Now;
            Átütemezés.Value = DateTime.Now;
            Dátumtól.Value = new DateTime(DateTime.Now.Year, 1, 1);
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Oktatás_ALAP(hely);
        }


        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Fülekkitöltése()
        {
            Lecsukja();

            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        TáblaOktatáslistázás();
                        Oktatásistátusok();
                        break;
                    }
                case 1:
                    {
                        Tárgyfeltöltés();
                        Oktataandó_Választó.Checked = true;
                        break;
                    }
                case 2:
                    {
                        Okatatófeltöltés();
                        break;
                    }
                case 3:
                    {
                        Okatatófeltöltés();
                        break;
                    }
                case 4:
                    {
                        Tárgyfeltöltés();
                        break;
                    }
                case 5:
                    {

                        Felcsukja();
                        break;
                    }
            }
        }


        #region Alap
        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Személy(true));
                if (Program.PostásTelephely == "Főmérnökség")
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


        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            BtnElrendelés.Enabled = false;
            Kötelezésmód.Enabled = false;
            TörölKötelezés.Enabled = false;
            BtnJelenléti.Enabled = false;
            BtnAdminMentés.Enabled = false;
            BtnEmailKüldés.Enabled = false;
            BtnPDFsave.Enabled = false;
            BtnNaplózásEredményTöröl.Enabled = false;

            melyikelem = 64;
            // módosítás 1 Dolgozók oktatásainak elrendelése
            if (MyF.Vanjoga(melyikelem, 1))
                BtnElrendelés.Enabled = true;
            // módosítás 2 dolgozó oktatás elrendelésének törlése átütemezése
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Kötelezésmód.Enabled = true;
                TörölKötelezés.Enabled = true;
            }
            // módosítás 3 adminisztráció mentés, jelenléti ív készítés, e-mail küldés
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BtnJelenléti.Enabled = true;
                BtnAdminMentés.Enabled = true;
                BtnEmailKüldés.Enabled = true;
            }

            melyikelem = 65;
            // módosítás 1 Oktatás tényének rögzítése és módosítása
            if (MyF.Vanjoga(melyikelem, 1))
                BtnPDFsave.Enabled = true;
            // módosítás 2 Oktatás tényének törlése
            if (MyF.Vanjoga(melyikelem, 2))
                BtnNaplózásEredményTöröl.Enabled = true;
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
                if (Program.Postás_Vezér)
                {
                    // Rögzítteljes.Visible = true;
                }
                else
                {
                    // Rögzítteljes.Visible = false;
                }
            }
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
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }
        #endregion


        #region Gombok
        private void BtnKijelölcsop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkCsoport.Items.Count; i++)
                ChkCsoport.SetItemChecked(i, true);
            Jelöltcsoport();
        }


        private void Btnkijelöltörlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkCsoport.Items.Count; i++)

                ChkCsoport.SetItemChecked(i, false);
            Jelöltcsoport();
        }


        private void Btnmindkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                ChkDolgozónév.SetItemChecked(i, true);
        }


        private void Btnkijelöléstöröl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                ChkDolgozónév.SetItemChecked(i, false);
        }


        private void BtnKijelölésátjelöl_Click(object sender, EventArgs e)
        {
            Jelöltcsoport();
        }


        private void Jelöltcsoport()
        {
            try
            {
                ChkDolgozónév.Items.Clear();
                ChkDolgozónév.BeginUpdate();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";

                Kezelő_Dolgozó_Alap kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok;
                for (int j = 0; j < ChkCsoport.CheckedItems.Count; j++)
                {
                    string szöveg;
                    //csoporttagokat kiválogatja
                    if (ChkCsoport.CheckedItems[j].ToString().Trim() == "Összes")
                        szöveg = "SELECT * FROM Dolgozóadatok WHERE kilépésiidő=#1-1-1900# ORDER BY DolgozóNév asc";
                    else
                        szöveg = $"SELECT * FROM Dolgozóadatok WHERE kilépésiidő=#1-1-1900# AND [csoport]='{ChkCsoport.CheckedItems[j].ToString().Trim()}' order by DolgozóNév";

                    Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                    foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    {
                        ChkDolgozónév.Items.Add($"{rekord.DolgozóNév.Trim()} = {rekord.Dolgozószám.Trim()}");
                    }
                }
                ChkDolgozónév.EndUpdate();
            }
            catch (HibásBevittAdat ex)
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


        private void Csoportfeltöltés()
        {
            ChkCsoport.Items.Clear();
            ChkCsoport.BeginUpdate();
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Segéd\kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM csoportbeosztás order by sorszám";
            Kezelő_Kiegészítő_Csoportbeosztás kéz = new Kezelő_Kiegészítő_Csoportbeosztás();
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                ChkCsoport.Items.Add(rekord.Csoportbeosztás);
            ChkCsoport.EndUpdate();
        }


        private void Névfeltöltés()
        {
            try
            {
                ChkDolgozónév.Items.Clear();
                ChkDolgozónév.BeginUpdate();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";
                string szöveg = "SELECT * FROM Dolgozóadatok WHERE Kilépésiidő=#1/1/1900# order by DolgozóNév asc";

                Kezelő_Dolgozó_Alap kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    ChkDolgozónév.Items.Add($"{rekord.DolgozóNév.Trim()}={rekord.Dolgozószám.Trim()}");

                ChkDolgozónév.EndUpdate();
            }
            catch (HibásBevittAdat ex)
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
            string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\oktatás.html";
            MyE.Megnyitás(hely);
        }





        private void TáblaOktatás_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (TáblaOktatás.RowCount > 0)
                {
                    // egész sor színezése ha törölt
                    if (Chkelrendelés.Checked)
                    {
                        foreach (DataGridViewRow row in TáblaOktatás.Rows)
                        {
                            if (row.Cells[5].Value.ToString().Trim() == "Törölt")
                            {
                                row.DefaultCellStyle.ForeColor = Color.White;
                                row.DefaultCellStyle.BackColor = Color.IndianRed;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                            }
                        }
                    }
                    // egész sor színezése ha törölt
                    if (CHkNapló.Checked)
                    {
                        if (TáblaOktatás.ColumnCount - 1 > 12)
                        {
                            foreach (DataGridViewRow row in TáblaOktatás.Rows)
                            {
                                if ((row.Cells[12].Value.ToString().Trim() == "Törölt").ToÉrt_Bool())
                                {
                                    row.DefaultCellStyle.ForeColor = Color.White;
                                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                                }
                            }
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


        private void Oktatásistátusok()
        {
            try
            {
                // töröljük az előzményeket
                CMBStátus.Items.Clear();
                CmbKategória.Items.Clear();
                CmbGyakoriság.Items.Clear();

                // üres 
                CMBStátus.Items.Add("");
                CmbKategória.Items.Add("");
                CmbGyakoriság.Items.Add("");
                // feltöltjök amit kell
                // kategória
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                string szöveg = "SELECT * FROM Oktatástábla WHERE státus='Érvényes' ORDER BY Kategória";
                string előző = "";

                Kezelő_OktatásTábla kéz = new Kezelő_OktatásTábla();
                List<Adat_OktatásTábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_OktatásTábla rekord in Adatok)
                {
                    // csak azokat teszi bele a listába akiknél vannak dolgozók
                    if (előző != rekord.Kategória.Trim())
                    {
                        előző = rekord.Kategória.Trim();
                        CmbKategória.Items.Add(előző);
                    }
                }
                // gyakoriság
                szöveg = "SELECT * FROM Oktatástábla WHERE státus='Érvényes' ORDER BY gyakoriság";
                előző = "";

                Kezelő_OktatásTábla kéz2 = new Kezelő_OktatásTábla();
                List<Adat_OktatásTábla> Adatok2 = kéz2.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_OktatásTábla rekord2 in Adatok2)
                {
                    // csak azokat teszi bele a listába akiknél vannak dolgozók
                    if (előző != rekord2.Gyakoriság.Trim())
                    {
                        előző = rekord2.Gyakoriság.Trim();
                        CmbGyakoriság.Items.Add(előző);
                    }
                }
                // státusok 
                CMBStátus.Items.Add("Érvényes");
                CMBStátus.Items.Add("Törölt");
                CMBStátus.Text = "Érvényes";
                // számonkérés
                CMBszámon.Items.Clear();
                CMBszámon.Items.Add("0 -nem volt");
                CMBszámon.Items.Add("1 -megfelelt");
                CMBszámon.Items.Add("2 -nem felelt meg");
            }
            catch (HibásBevittAdat ex)
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
            Csoportfeltöltés();
            Névfeltöltés();
            TáblaOktatáslistázás();
            Fülek.SelectedIndex = 0;
        }


        private void Felcsukja()
        {
            Fülek.Top = 10;
            Fülek.Height = Size.Height - 60;
            BtnPdfNyit.Visible = false;
        }


        private void Lecsukja()
        {
            Fülek.Top = TáblaOktatás.Height + TáblaOktatás.Top + 10;
            Fülek.Height = 220;
            BtnPdfNyit.Visible = true;
            Button10.Visible = true;
            Button9.Visible = false;
        }


        private void BtnPdfMegnyitás_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
                Txtmegnyitott.Text = "";
                Txtmentett.Text = "";
                OpenFileDialog1.Filter = "PDF Files |*.pdf";
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Kezelő_Pdf.PdfMegnyitás(PDF_néző, OpenFileDialog1.FileName);

                    Txtmegnyitott.Text = OpenFileDialog1.FileName;
                    CHKpdfvan.Checked = false;
                    Fülek.SelectedIndex = 5;
                    Felcsukja();
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


        private void BtnPDFsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txtmegnyitott.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva a feltölteni kívánt fájl.");
                if (Txtmentett.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva a feltölteni kívánt fájl átnevezve, ellenőrizze le hogy van-e kiválasztva dolgozó.");
                if (CMBszámon.Text.Trim() == "")
                {
                    CMBszámon.Focus();
                    CMBszámon.BackColor = Color.Yellow;
                    return;
                }
                if (LSToktató.Text.Trim() == "")
                {
                    LSToktató.Focus();
                    LSToktató.BackColor = Color.Yellow;
                    return;
                }
                if (Megjegyzés.Text.Trim() == "") Megjegyzés.Text = "_";
                // megnézzük, hogy létezik-e a könyvtár
                string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás";
                // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
                hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}";
                // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
                hely += $@"\{Txtmentett.Text.Trim()}";
                // ha fel kell tölteni
                if (!CHKpdfvan.Checked)
                {
                    // PDF fájl-t feltöljük.
                    if (File.Exists(hely))
                    {
                        if (MessageBox.Show("Ezen a néven már létezik fájl, felülírjuk?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            return;
                        else
                            File.Delete(hely);
                    }
                    // ha nem létezik akkor odamásoljuk
                    File.Copy(Txtmegnyitott.Text, hely);
                }
                // Naplófájl ellenőrzése
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{DateTime.Now:yyyy}";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{DateTime.Now:yyyy}\Oktatásnapló_{Cmbtelephely.Text}.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Oktatás_Napló(hely);

                string jelszó = "pázmányt";
                // adatbázis adatokat rögzítünk.
                if (TáblaOktatás.SelectedRows.Count == 1)
                {
                    // ha egy dolgozót töltünk fel.
                    // módosítjuk a tábla adatait
                    hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                    string szöveg;
                    if (int.Parse(TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[7].Value.ToString()) == 0)
                    {
                        // ha csak egyszer kell feltölteni, akkor a státust átállítjuk 1-re
                        szöveg = "UPDATE oktatásrajelöltek SET státus=1 ";
                        szöveg += $" Where hrazonosító='{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value}'";
                        szöveg += $" AND IDoktatás={TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}";
                        szöveg += $" AND telephely='{Cmbtelephely.Text}'";
                    }
                    else
                    {
                        // ha valamilyen rendszereséggel kell oktatni, akkor hozzáadjuk a dátumhoz
                        szöveg = "UPDATE oktatásrajelöltek SET mikortól= '" + TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[4].Value.ToÉrt_DaTeTime()
                                                    .AddMonths(int.Parse(TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[7].Value.ToString())).ToString() + "'";
                        szöveg += $" Where hrazonosító='{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value}'";
                        szöveg += $" AND IDoktatás={TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}";
                        szöveg += $" AND telephely='{Cmbtelephely.Text}'";
                        szöveg += $" AND státus=0 ";
                    }
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    // Naplózás
                    // Melyik az utolsó ID
                    hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{DateTime.Now:yyyy}\Oktatásnapló_{Cmbtelephely.Text}.mdb";
                    szöveg = "SELECT * FROM oktatásnapló";
                    Adatok_Okt_Nap = Kéz_Okt_Nap.Lista_Adatok(hely, jelszó, szöveg);
                    long i = 1;
                    if (Adatok_Okt_Nap.Count > 0) i = Adatok_Okt_Nap.Max(a => a.ID) + 1;

                    szöveg = "INSERT INTO Oktatásnapló";
                    szöveg += "( Id, Hrazonosító, IDoktatás, oktatásdátuma, kioktatta, rögzítésdátuma, telephely, PDFfájlneve, Számonkérés, státus, rögzítő, megjegyzés)";
                    szöveg += " VALUES (";
                    szöveg += $"{i}, "; //id
                    szöveg += $"'{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value}', "; //Hrazonosító
                    szöveg += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}, "; //IDoktatás
                    szöveg += $"'{BizDátum.Value}', ";
                    szöveg += $"'{LSToktató.Text.Trim()}', ";
                    szöveg += $"'{DateTime.Now}', ";
                    szöveg += $"'{Cmbtelephely.Text.Trim()}', ";
                    szöveg += $"'{Txtmentett.Text.Trim()}', ";
                    szöveg += $"{CMBszámon.Text.Substring(0, 1)}, 0, ";
                    szöveg += $"'{Program.PostásNév.Trim()}', ";
                    szöveg += $"'{Megjegyzés.Text.Trim()}'  )";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                else
                {
                    // ha csoportosan töljük fel az adatokat
                    List<string> szövegGy = new List<string>();
                    List<string> szövegGyN = new List<string>();

                    hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                    string helynapló = $@"{Application.StartupPath}\Főmérnökség\Adatok\{DateTime.Now:yyyy}\Oktatásnapló_{Cmbtelephely.Text}.mdb";
                    if (!File.Exists(helynapló)) Adatbázis_Létrehozás.Oktatás_Napló(hely);

                    for (int i = 0; i < TáblaOktatás.SelectedRows.Count; i++)
                    {
                        string szöveg;
                        if (int.Parse(TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[7].Value.ToString()) == 0)
                        {
                            // ha csak egyszer kell feltölteni, akkor a státust átállítjuk 1-re
                            szöveg = "UPDATE oktatásrajelöltek SET státus=1 ";
                            szöveg += $" WHERE hrazonosító='{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[0].Value}'";
                            szöveg += $" AND IDoktatás={TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[2].Value}";
                            szöveg += $" AND telephely='{Cmbtelephely.Text}'";
                        }
                        else
                        {
                            // ha valamilyen rendszereséggel kell oktatni, akkor hozzáadjuk a dátumhoz
                            DateTime ideigDátum = DateTime.Parse(TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[4].Value.ToString());
                            int ideigHónap = int.Parse(TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[7].Value.ToString());

                            szöveg = $"UPDATE oktatásrajelöltek SET mikortól='{ideigDátum.AddMonths(ideigHónap):yyyy.MM.dd}'";
                            szöveg += $" Where hrazonosító='{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[0].Value}'";
                            szöveg += $" AND IDoktatás={TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[2].Value}";
                            szöveg += $" AND telephely='{Cmbtelephely.Text}'";
                        }
                        szövegGy.Add(szöveg);

                        // Naplózás
                        // Melyik az utolsó ID
                        szöveg = "SELECT * FROM oktatásnapló";
                        Adatok_Okt_Nap = Kéz_Okt_Nap.Lista_Adatok(helynapló, jelszó, szöveg);
                        long j = 1;
                        if (Adatok_Okt_Nap.Count > 0) j = Adatok_Okt_Nap.Max(a => a.ID) + 1;

                        szöveg = "INSERT INTO Oktatásnapló";
                        szöveg += "( Id, Hrazonosító, IDoktatás, oktatásdátuma, kioktatta, rögzítésdátuma, telephely, PDFfájlneve, Számonkérés, státus, rögzítő, megjegyzés)";
                        szöveg += " VALUES (";
                        szöveg += $"{j}, ";//id
                        szöveg += $"'{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[0].Value}', ";//Hrazonosító
                        szöveg += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[2].Value}, ";//IDoktatás
                        szöveg += $"'{BizDátum.Value}', ";
                        szöveg += $"'{LSToktató.Text.Trim()}', ";
                        szöveg += $"'{DateTime.Now}', ";
                        szöveg += $"'{Cmbtelephely.Text.Trim()}', ";
                        szöveg += $"'{Txtmentett.Text.Trim()}', ";
                        szöveg += $"{CMBszámon.Text.Substring(0, 1)}, 0,";
                        szöveg += $"'{Program.PostásNév.Trim()} ', ";
                        szöveg += $"'{Megjegyzés.Text.Trim()} '  )";

                        szövegGyN.Add(szöveg);
                    }
                    MyA.ABMódosítás(hely, jelszó, szövegGy);
                    MyA.ABMódosítás(helynapló, jelszó, szövegGyN);
                }


                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CMBszámon.BackColor = Color.White;
                LSToktató.BackColor = Color.White;
                Txtmegnyitott.Text = "";
                Txtmentett.Text = "";
                PDF_néző.Visible = false;
                CHKpdfvan.Checked = false;
                Oktatáslistázáskötelezés(1);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Okatatófeltöltés()
        {
            Holtart.Be();
            LSToktató.Items.Clear();
            LSToktató.Items.Add("");
            AdminOktató.Items.Clear();
            AdminOktató.Items.Add("");
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";
            string jelszó = "forgalmiutasítás";
            string szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900# order by DolgozóNév asc";

            Kezelő_Dolgozó_Alap kéz = new Kezelő_Dolgozó_Alap();
            List<Adat_Dolgozó_Alap> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_Dolgozó_Alap rekord in Adatok)
            {
                LSToktató.Items.Add(rekord.DolgozóNév.Trim());
                AdminOktató.Items.Add(rekord.DolgozóNév.Trim());
                Holtart.Lép();
            }
            AdminOktató.Refresh();
            LSToktató.Refresh();
            Holtart.Ki();
        }


        private void Tárgyfeltöltés()
        {
            Holtart.Be();
            Cmboktatásrögz.Items.Clear();
            Cmboktatásrögz.Items.Add("");
            CMBoktatástárgya.Items.Clear();
            CMBoktatástárgya.Items.Add("");
            Adminoktatástárgya.Items.Clear();
            Adminoktatástárgya.Items.Add("");
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
            string jelszó = "pázmányt";
            string szöveg = "SELECT * FROM Oktatástábla";
            szöveg += $" WHERE telephely='{Cmbtelephely.Text}'";
            szöveg += " ORDER BY listázásisorrend";

            Kezelő_OktatásTábla kéz = new Kezelő_OktatásTábla();
            List<Adat_OktatásTábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_OktatásTábla rekord in Adatok)
            {

                Cmboktatásrögz.Items.Add($"{rekord.IDoktatás} - {rekord.Téma.Trim()}");
                Adminoktatástárgya.Items.Add($"{rekord.IDoktatás} - {rekord.Téma.Trim()}");
                Holtart.Lép();
            }
            List<Adat_OktatásTábla> SzűrtAdatok = (from a in Adatok
                                                   where a.Státus == "Érvényes"
                                                   select a).ToList();

            foreach (Adat_OktatásTábla rekord in SzűrtAdatok)
                CMBoktatástárgya.Items.Add($"{rekord.IDoktatás} - {rekord.Téma.Trim()}");

            Cmboktatásrögz.Refresh();
            CMBoktatástárgya.Refresh();
            Adminoktatástárgya.Refresh();
            if (VálasztottOktatás.Trim() != "") CMBoktatástárgya.Text = VálasztottOktatás;
            Holtart.Ki();

        }


        private void BtnPdfÚjHasználClick(object sender, EventArgs e)
        {
            try
            {
                string Könyvtár = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}";

                // feltöltött fájlok ismételt felhasználása
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "PDF Files |*.pdf",
                    InitialDirectory = Könyvtár
                };



                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!OpenFileDialog1.FileName.Contains(Könyvtár)) throw new HibásBevittAdat("A program által beállított könyvtárból lehet csak fájlt választani.");
                    PDF_néző.Document = PdfDocument.Load(OpenFileDialog1.FileName);
                    Txtmegnyitott.Text = OpenFileDialog1.FileName;
                    Txtmentett.Text = OpenFileDialog1.SafeFileName;
                    CHKpdfvan.Checked = true;
                    PDF_néző.Visible = true;
                    Fülek.SelectedIndex = 5;
                    Felcsukja();
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


        private void TáblaOktatás_SelectionChanged(object sender, EventArgs e)
        {
            if (!CHKpdfvan.Checked)
            {
                if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                {
                    // ha oktatandóra kattint akkor összeállítja a fájl nevét
                    if (TáblaOktatás.SelectedRows.Count == 1)
                    {
                        // ha egy van kijelölve
                        Txtmentett.Text = $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value}_";
                        Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                        Txtmentett.Text += $"{Cmbtelephely.Text}_";
                        Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                    }
                    else
                    {
                        // ha több sor van kijelölve
                        int volt = 0;
                        int i = 1;
                        while (volt == 0)
                        {
                            Txtmentett.Text = $"Csop{i}_";
                            Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                            Txtmentett.Text += $"{Cmbtelephely.Text}_";
                            Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                            string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{Txtmentett.Text.Trim()}";
                            if (File.Exists(hely))
                                i++;
                            else
                                volt = 1;
                        }
                    }
                }
            }
            if (TáblaOktatás.SelectedRows.Count == 1 && CHkNapló.Checked)
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[8].Value}";
                Pdf_Megjelenítés(hely);
            }
        }



        void Pdf_Megjelenítés(string hely)
        {
            if (!File.Exists(hely)) return;
            Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);
        }


        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaOktatás.Rows.Count <= 0)
                    return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Oktatás_{Program.PostásNév.Trim()} - {DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, TáblaOktatás, true);
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


        private void BtnNaplózásEredményTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Chkelrendelés.Checked) return;
                // ha nincs kijelölve sor akkor kilép

                DateTime dátumoktatás = new DateTime(1900, 1, 1);
                if (TáblaOktatás.SelectedRows.Count == 0) return;
                string helynapló = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátumtól.Value:yyyy}\Oktatásnapló_{Cmbtelephely.Text}.mdb";
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";

                List<string> szövegGy = new List<string>();
                List<string> szövegGyN = new List<string>();

                string szöveg = "SELECT * FROM oktatástábla";
                Kezelő_OktatásTábla kéz = new Kezelő_OktatásTábla();
                List<Adat_OktatásTábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM oktatásrajelöltek ";
                Adatok_OktJelölt = Kéz_OktJelölt.Lista_Adatok(hely, jelszó, szöveg);


                foreach (DataGridViewRow row in TáblaOktatás.SelectedRows)
                {
                    // a naplófájl módosítása törlésre 
                    szöveg = $"UPDATE oktatásnapló SET státus= 1, rögzítő='{Program.PostásNév.Trim()}', ";
                    szöveg += $" rögzítésdátuma='{DateTime.Now}'";
                    szöveg += $" Where id={row.Cells[0].Value}";
                    szövegGyN.Add(szöveg);

                    int oktatásism = 0;
                    int idoktatás = row.Cells[3].Value.ToÉrt_Int();

                    Adat_OktatásTábla ElemOkt = (from a in Adatok
                                                 where a.IDoktatás == idoktatás
                                                 select a).FirstOrDefault();
                    if (ElemOkt != null) oktatásism = (int)ElemOkt.Ismétlődés;

                    Adat_Oktatásrajelöltek ElemOktJel = (from a in Adatok_OktJelölt
                                                         where a.IDoktatás == (long)row.Cells[3].Value
                                                         && a.HRazonosító == row.Cells[1].Value.ToString()
                                                         && a.Telephely == Cmbtelephely.Text
                                                         select a).FirstOrDefault();
                    if (ElemOktJel != null) dátumoktatás = ElemOktJel.Mikortól;

                    if (oktatásism == 0)
                    {
                        // ha csak egyszer kell feltölteni, akkor a státust átállítjuk 0-re, hisz töröltük az oktatást
                        szöveg = "UPDATE oktatásrajelöltek SET státus=0 ";
                        szöveg += $" WHERE hrazonosító='{row.Cells[1].Value}'";
                        szöveg += $" AND IDoktatás={row.Cells[3].Value}";
                        szöveg += $" AND telephely='{Cmbtelephely.Text}'";
                    }
                    else
                    {
                        // ha valamilyen rendszereséggel kell oktatni, akkor levonjuk a dátumból hisz töröljük
                        szöveg = $"UPDATE oktatásrajelöltek SET mikortól='{dátumoktatás.AddMonths(-1 * oktatásism)}'";
                        szöveg += $" WHERE hrazonosító='{row.Cells[1].Value}'";
                        szöveg += $" AND IDoktatás={row.Cells[3].Value}";
                        szöveg += $" AND telephely='{Cmbtelephely.Text}'";
                    }
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
                MyA.ABMódosítás(helynapló, jelszó, szövegGyN);
                MessageBox.Show("Az adatok törlése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnPdfNyit_Click(object sender, EventArgs e)
        {
            Felcsukja();
        }


        private void BtnPdfCsuk_Click(object sender, EventArgs e)
        {
            Lecsukja();
        }


        private void BizDátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHKpdfvan.Checked)
                {
                    if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                    {
                        // ha oktatandóra kattint akkor összeállítja a fájl nevét
                        if (TáblaOktatás.SelectedRows.Count == 1)
                        {
                            // ha egy van kijelölve
                            Txtmentett.Text = $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value}_";
                            Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                            Txtmentett.Text += $"{Cmbtelephely.Text}_";
                            Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                        }
                        else
                        {
                            // ha több sor van kijelölve
                            int volt = 0;
                            int i = 1;
                            while (volt != 1)
                            {
                                Txtmentett.Text = $"Csop{i}_";
                                Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                                Txtmentett.Text += $"{Cmbtelephely.Text}_";
                                Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                                string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{Txtmentett.Text.Trim()}";
                                if (!File.Exists(hely))
                                    i++;
                                else
                                    volt = 1;
                            }
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


        #region Elrendelés

        private void Btnfrissít_Click_1(object sender, EventArgs e)
        {
            CHkNapló.Checked = false;
            Chkoktat.Checked = false;
            Chkelrendelés.Checked = false;
            TáblaOktatáslistázás();
        }


        private void TáblaOktatáslistázás()
        {
            try
            {

                Chkoktat.Checked = false;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                Holtart.Be();
                TáblaOktatás.Rows.Clear();
                TáblaOktatás.Columns.Clear();
                ListaNév = "Elrendelés";
                TáblaOktatás.Refresh();
                TáblaOktatás.Visible = false;
                TáblaOktatás.ColumnCount = 9;
                TáblaOktatás.RowCount = 0;
                // ' fejléc elkészítése
                TáblaOktatás.Columns[0].HeaderText = "Sor- szám";
                TáblaOktatás.Columns[0].Width = 50;
                TáblaOktatás.Columns[1].HeaderText = "Oktatás témája";
                TáblaOktatás.Columns[1].Width = 520;
                TáblaOktatás.Columns[2].HeaderText = "Kategória";
                TáblaOktatás.Columns[2].Width = 120;
                TáblaOktatás.Columns[3].HeaderText = "Gyakoriság";
                TáblaOktatás.Columns[3].Width = 110;
                TáblaOktatás.Columns[4].HeaderText = "Gyakoriság hónap";
                TáblaOktatás.Columns[4].Width = 100;
                TáblaOktatás.Columns[5].HeaderText = "Státus";
                TáblaOktatás.Columns[5].Width = 100;
                TáblaOktatás.Columns[6].HeaderText = "Dátum";
                TáblaOktatás.Columns[6].Width = 110;
                TáblaOktatás.Columns[7].HeaderText = "Telephely";
                TáblaOktatás.Columns[7].Width = 120;
                TáblaOktatás.Columns[8].HeaderText = "Listázási sorrend";
                TáblaOktatás.Columns[8].Width = 70;

                string szöveg = "SELECT * FROM Oktatástábla";
                szöveg += $@" WHERE telephely='{Cmbtelephely.Text}'";

                if (CmbGyakoriság.Text.Trim() != "")
                    szöveg += $" AND gyakoriság='{CmbGyakoriság.Text}'";
                if (CMBStátus.Text.Trim() != "")
                    szöveg += $" AND státus='{CMBStátus.Text}'";
                if (CmbKategória.Text.Trim() != "")
                    szöveg += $" AND kategória='{CmbKategória.Text}'";
                szöveg += " ORDER BY listázásisorrend";

                Kezelő_OktatásTábla kéz = new Kezelő_OktatásTábla();
                List<Adat_OktatásTábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_OktatásTábla rekord in Adatok)
                {
                    TáblaOktatás.RowCount++;
                    int i = TáblaOktatás.RowCount - 1;
                    TáblaOktatás.Rows[i].Cells[0].Value = rekord.IDoktatás;
                    TáblaOktatás.Rows[i].Cells[1].Value = rekord.Téma.Trim();
                    TáblaOktatás.Rows[i].Cells[2].Value = rekord.Kategória.Trim();
                    TáblaOktatás.Rows[i].Cells[3].Value = rekord.Gyakoriság.Trim();
                    TáblaOktatás.Rows[i].Cells[4].Value = rekord.Ismétlődés;
                    TáblaOktatás.Rows[i].Cells[5].Value = rekord.Státus.Trim();
                    TáblaOktatás.Rows[i].Cells[6].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    TáblaOktatás.Rows[i].Cells[7].Value = rekord.Telephely.Trim();
                    TáblaOktatás.Rows[i].Cells[8].Value = rekord.Listázásisorrend;
                    Holtart.Lép();
                }
                Chkelrendelés.Checked = true;
                TáblaOktatás.Visible = true;
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


        private void BtnElrendelés_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();

                if (ListaNév != "Elrendelés") throw new HibásBevittAdat("Nem megfelelő listázott tartalom.");    //Ha nem az van kilistázva, akkor kilép
                if (TáblaOktatás.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve a táblázatban sor!");        // ha nincs kijelölve egy sor sem akkor kilép
                if (ChkDolgozónév.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve dolgozó!");       // ha nincs kijelölve egy dolgozó sem akkor kilép 

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                bool volt = false;
                // dolgozóneveket pörgetjük végig

                string szöveg = "SELECT * FROM oktatásrajelöltek";
                Adatok_OktJelölt = Kéz_OktJelölt.Lista_Adatok(hely, jelszó, szöveg);

                List<string> szövegGy = new List<string>();
                for (int i = 0; i < ChkDolgozónév.CheckedItems.Count; i++)
                {
                    // a tábla adatait pörgetjük végig
                    for (int j = 0; j < TáblaOktatás.SelectedRows.Count; j++)
                    {
                        string[] darabol = ChkDolgozónév.CheckedItems[i].ToString().Split('=');
                        // megnézzük, hogy van-e már rögzítve
                        Adat_Oktatásrajelöltek rekord = (from r in Adatok_OktJelölt
                                                         where r.HRazonosító == darabol[1].Trim() &&
                                                               r.Telephely == Cmbtelephely.Text.Trim() &&
                                                               r.IDoktatás == TáblaOktatás.SelectedRows[j].Cells[0].Value.ToÉrt_Int() &&
                                                               r.Státus == 0
                                                         select r).FirstOrDefault();
                        if (rekord == null)
                        {
                            // rögzítjük az adatokat
                            szöveg = "INSERT INTO oktatásrajelöltek (HRazonosító, IDoktatás, Mikortól,  státus,  telephely)";
                            szöveg += $" VALUES ('{darabol[1].Trim()}', ";
                            szöveg += TáblaOktatás.SelectedRows[j].Cells[0].Value.ToString() + ", ";
                            szöveg += $"'{OktDátum.Value:yyyy.MM.dd}', 0,";
                            szöveg += $"'{Cmbtelephely.Text.Trim()}') ";
                            szövegGy.Add(szöveg);
                            volt = true;
                        }
                        Holtart.Lép();
                    }
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
                Holtart.Ki();
                if (volt)
                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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



        #region Oktatandó
        private void BtnOktatásFrissít_Click(object sender, EventArgs e)
        {
            FrissítOktatás();
        }

        private void FrissítOktatás()
        {
            CHkNapló.Checked = false;
            Chkoktat.Checked = false;
            Chkelrendelés.Checked = false;
            if (Oktataandó_Választó.Checked)
                Oktatáslistázáskötelezés(1);
            else
                Oktatáslistázáskötelezés(2);

        }


        private void Oktatáslistázáskötelezés(int Melyik)
        {
            try
            {
                if (CMBoktatástárgya.Text.Trim() == "") return;

                // ha nincs kijelölve egy dolgozó sem akkor kilép
                if (ChkDolgozónév.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve dolgozó!");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";
                string szöveg = "SELECT * FROM Dolgozóadatok";
                Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolgozó = KézDolgozó.Lista_Adatok(hely, jelszó, szöveg);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                jelszó = "pázmányt";
                szöveg = "SELECT * FROM Oktatástábla ";
                Kezelő_OktatásTábla KézOktatás = new Kezelő_OktatásTábla();
                List<Adat_OktatásTábla> AdatokOktatás = KézOktatás.Lista_Adatok(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM Oktatásrajelöltek ";
                Kezelő_Oktatásrajelöltek KézJelölt = new Kezelő_Oktatásrajelöltek();
                List<Adat_Oktatásrajelöltek> AdatokJelölt = KézJelölt.Lista_Adatok(hely, jelszó, szöveg);

                Chkoktat.Checked = true;

                TáblaOktatás.Rows.Clear();
                TáblaOktatás.Columns.Clear();

                ListaNév = "Kötelezés";
                TáblaOktatás.Refresh();
                TáblaOktatás.Visible = false;
                TáblaOktatás.ColumnCount = 8;
                TáblaOktatás.RowCount = 0;
                // ' fejléc elkészítése
                TáblaOktatás.Columns[0].HeaderText = "HR azonosító";
                TáblaOktatás.Columns[0].Width = 115;
                TáblaOktatás.Columns[1].HeaderText = "Név";
                TáblaOktatás.Columns[1].Width = 300;
                TáblaOktatás.Columns[2].HeaderText = "IDoktatás";
                TáblaOktatás.Columns[2].Width = 80;
                TáblaOktatás.Columns[3].HeaderText = "Oktatás Témája";
                TáblaOktatás.Columns[3].Width = 300;
                TáblaOktatás.Columns[4].HeaderText = "Elrendelés dátuma";
                TáblaOktatás.Columns[4].Width = 110;
                TáblaOktatás.Columns[5].HeaderText = "Telephely";
                TáblaOktatás.Columns[5].Width = 120;
                TáblaOktatás.Columns[6].HeaderText = "Státus";
                TáblaOktatás.Columns[6].Width = 100;
                TáblaOktatás.Columns[7].HeaderText = "ismétlődés";
                TáblaOktatás.Columns[7].Width = 100;

                List<Adat_Oktatásrajelöltek> Adatok;
                long oktatásid = CMBoktatástárgya.Text.Substring(0, CMBoktatástárgya.Text.IndexOf("-")).ToÉrt_Long();
                switch (Melyik)
                {
                    case 1:

                        if (oktatásid != 0)
                            Adatok = (from a in AdatokJelölt
                                      where a.Mikortól < Lejáródátum.Value && a.Telephely == Cmbtelephely.Text.Trim()
                                      && a.IDoktatás == oktatásid
                                      && a.Státus == 0
                                      orderby a.HRazonosító
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokJelölt
                                      where a.Mikortól < Lejáródátum.Value && a.Telephely == Cmbtelephely.Text.Trim()
                                      && a.Státus == 0
                                      orderby a.HRazonosító
                                      select a).ToList();

                        break;
                    case 2:
                        if (oktatásid != 0)
                            Adatok = (from a in AdatokJelölt
                                      where a.Telephely == Cmbtelephely.Text.Trim()
                                      && a.IDoktatás == oktatásid && a.Státus < 2
                                      orderby a.HRazonosító
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokJelölt
                                      where a.Telephely == Cmbtelephely.Text.Trim() && a.Státus < 2
                                      orderby a.HRazonosító
                                      select a).ToList();
                        break;
                    default:
                        Adatok = AdatokJelölt;
                        break;
                }

                Holtart.Be(ChkDolgozónév.CheckedItems.Count + 1);
                for (int i = 0; i < ChkDolgozónév.CheckedItems.Count; i++)
                {
                    string[] tomb = ChkDolgozónév.CheckedItems[i].ToString().Split('=');
                    List<Adat_Oktatásrajelöltek> Rekordok = (from ab in Adatok
                                                             where ab.HRazonosító.Trim() == tomb[1].Trim()
                                                             select ab).ToList();
                    if (Rekordok != null)
                    {
                        foreach (Adat_Oktatásrajelöltek Elem in Rekordok)
                        {
                            int ii = TáblaOktatás.Rows.Add();
                            TáblaOktatás.Rows[ii].Cells[0].Value = Elem.HRazonosító;
                            TáblaOktatás.Rows[ii].Cells[2].Value = Elem.IDoktatás;
                            TáblaOktatás.Rows[ii].Cells[4].Value = Elem.Mikortól.ToString("yyyy.MM.dd");
                            TáblaOktatás.Rows[ii].Cells[5].Value = Elem.Telephely;
                            TáblaOktatás.Rows[ii].Cells[6].Value = Elem.Státus;

                            string DolgozóNév = (from a in AdatokDolgozó
                                                 where a.Dolgozószám == Elem.HRazonosító
                                                 select a.DolgozóNév).FirstOrDefault() ?? "";
                            TáblaOktatás.Rows[ii].Cells[1].Value = DolgozóNév;

                            Adat_OktatásTábla rekordszer = (from a in AdatokOktatás
                                                            where a.IDoktatás == Elem.IDoktatás
                                                            select a).FirstOrDefault();
                            if (rekordszer != null)
                            {
                                TáblaOktatás.Rows[ii].Cells[3].Value = rekordszer.Téma;
                                TáblaOktatás.Rows[ii].Cells[7].Value = rekordszer.Ismétlődés;
                            }
                            Holtart.Lép();
                        }
                    }
                }
                TáblaOktatás.Visible = true;
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


        private void Kötelezésmód_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaNév != "Kötelezés")
                    throw new HibásBevittAdat("Hibás listázott tartalom.");
                if (TáblaOktatás.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve érvényes sor a táblázatban.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                {
                    List<string> SzövegGy = new List<string>();
                    foreach (DataGridViewRow row in TáblaOktatás.SelectedRows)
                    {
                        string szöveg = $"UPDATE oktatásrajelöltek SET mikortól='{Átütemezés.Value:yyyy.MM.dd}'";
                        szöveg += $" WHERE idoktatás={row.Cells[2].Value}";
                        szöveg += $" and hrazonosító='{row.Cells[0].Value.ToString().Trim()}'";
                        szöveg += $" AND telephely='{Cmbtelephely.Text.Trim()}'";
                        SzövegGy.Add(szöveg);
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }
                Oktatáslistázáskötelezés(1);
                MessageBox.Show("Az adatok módosítása megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnOktatásEredményTöröl_Click(object sender, EventArgs e)
        {
            TáblaOktatás.ClearSelection();
            Txtmentett.Text = "";
        }


        private void TörölKötelezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaNév != "Kötelezés")
                    throw new HibásBevittAdat("Hibás listázott tartalom.");
                if (TáblaOktatás.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve érvényes sor a táblázatban.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                {
                    List<string> SzövegGy = new List<string>();
                    foreach (DataGridViewRow row in TáblaOktatás.SelectedRows)
                    {
                        string szöveg = "UPDATE oktatásrajelöltek SET státus=2 ";
                        szöveg += $" WHERE idoktatás={row.Cells[2].Value}";
                        szöveg += $" and hrazonosító='{row.Cells[0].Value.ToString().Trim()}'";
                        szöveg += $" AND telephely='{Cmbtelephely.Text.Trim()}'";
                        SzövegGy.Add(szöveg);
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }
                Oktatáslistázáskötelezés(1);
                MessageBox.Show("Az adatok törlése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CMBoktatástárgya_SelectedIndexChanged(object sender, EventArgs e)
        {
            Adminoktatástárgya.Text = CMBoktatástárgya.Text;
            VálasztottOktatás = CMBoktatástárgya.Text;
        }


        private void Oktataandó_Választó_CheckedChanged(object sender, EventArgs e)
        {
            Oktatás_Panel.Visible = Oktataandó_Választó.Checked;
            FrissítOktatás();
        }
        #endregion



        #region adminisztráció
        private void Button10_Click(object sender, EventArgs e)
        {
            Lecsukja();
            Button10.Visible = true;
            Button9.Visible = false;
        }


        private void BtnLapFül_Click(object sender, EventArgs e)
        {
            Felcsukja();
            Button9.Visible = true;
            Button10.Visible = false;
        }


        private void ADMINürítés()
        {
            Adminoktatásdátuma.Value = DateTime.Now;
            AdminOktatásoka.Text = "";
            AdminOktató.Text = "";
            AdminOktatómunkaköre.Text = "";
            Adminhelyszín.Text = "";
            Admintartam.Text = "";
            Admintematika.Text = "";
            Egyébszöveg.Text = "";
            Txtemail.Text = "";
        }


        private void Adminoktatástárgya_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kiírjaadmin();
        }


        private void Kiírjaadmin()
        {
            try
            {
                if (Adminoktatástárgya.Text.Trim() == "")
                    return;
                Holtart.Be();
                ADMINürítés();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                string szöveg = "SELECT * FROM Oktatásisegéd";
                szöveg += $" WHERE telephely='{Cmbtelephely.Text}'";
                szöveg += $" AND IDoktatás={Adminoktatástárgya.Text.Substring(0, Adminoktatástárgya.Text.IndexOf("-"))}";
                Holtart.Lép();
                Kezelő_OktatásiSegéd kéz = new Kezelő_OktatásiSegéd();
                Adat_OktatásiSegéd rekord = kéz.Egy_Adat(hely, jelszó, szöveg);
                Holtart.Lép();
                if (rekord != null)
                {
                    AdminOktatásoka.Text = rekord.Oktatásoka.Trim();
                    AdminOktató.Text = rekord.Oktató.Trim();
                    AdminOktatómunkaköre.Text = rekord.Oktatóbeosztása.Trim();
                    Adminhelyszín.Text = rekord.Oktatáshelye.Trim();
                    Admintartam.Text = rekord.Oktatásidőtartama.ToString().Trim();
                    Admintematika.Text = rekord.Oktatástárgya.Replace('°', '"'); ;  // visszacseréli a °-t "-ra
                    Egyébszöveg.Text = rekord.Egyébszöveg.Replace('~', '"'); // visszacseréli a ~-t "-ra
                    Txtemail.Text = rekord.Email.Trim();
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


        private void BtnEmailKüldés_Click(object sender, EventArgs e)
        {
            EmailKüldés();
        }


        private void JelenlétiÍVKészítés()
        {
            try
            {
                Holtart.Be();
                if (Chkelrendelés.Checked)
                    return;
                // E-mail küldés előkészítése
                TextBox1.Text = "";
                TextBox2.Text = "";
                BtnEmailKüldés.Visible = false;
                // ha nincs kijelölve senki akkor kilép
                if (TáblaOktatás.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                int hányember = TáblaOktatás.SelectedRows.Count;
                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Jelenléti_Oktatáshoz-{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}";

                // formázáshoz
                MyE.ExcelLétrehozás();
                string munkalap = "Munka1";
                MyE.Munkalap_betű("Calibri", 14);
                MyE.Oszlopszélesség(munkalap, "a:a", 6);
                MyE.Oszlopszélesség(munkalap, "b:b", 15);
                MyE.Oszlopszélesség(munkalap, "c:c", 26);
                MyE.Oszlopszélesség(munkalap, "d:d", 36);
                MyE.Oszlopszélesség(munkalap, "e:e", 13);
                MyE.Oszlopszélesség(munkalap, "f:f", 20);
                MyE.Oszlopszélesség(munkalap, "g:g", 15);

                for (int j = 1; j <= 10; j++)
                {
                    MyE.Egyesít(munkalap, "A" + j.ToString() + ":b" + j.ToString());
                    MyE.Egyesít(munkalap, "c" + j.ToString() + ":g" + j.ToString());
                    Holtart.Lép();
                }

                MyE.Kiir("Szervezet:", "a1");
                string eredmény = "";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM jelenlétiív ";

                Kezelő_Kiegészítő_Jelenlétiív kéz = new Kezelő_Kiegészítő_Jelenlétiív();
                List<Adat_Kiegészítő_Jelenlétiív> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                if (Adatok != null)
                {
                    string Szervezet = (from a in Adatok
                                        where a.Id == 2
                                        select a.Szervezet).FirstOrDefault();
                    if (Szervezet != null)
                        eredmény += $"{Szervezet.Trim()}\n\r";
                    Szervezet = (from a in Adatok
                                 where a.Id == 3
                                 select a.Szervezet).FirstOrDefault();
                    if (Szervezet != null)
                        eredmény += $"{Szervezet.Trim()}\n\r";
                    Szervezet = (from a in Adatok
                                 where a.Id == 4
                                 select a.Szervezet).FirstOrDefault();
                    if (Szervezet != null)
                        eredmény += $"{Szervezet.Trim()}\n\r";
                }
                Holtart.Lép();
                MyE.Kiir(eredmény, "c1");
                MyE.Sormagasság("c1", 55);

                MyE.Kiir("Hely, helyszíne:", "a2");
                MyE.Kiir(Adminhelyszín.Text, "c2");
                MyE.Sormagasság("2:2", 20);

                MyE.Kiir("Tárgy:", "a3");
                MyE.Kiir(Adminoktatástárgya.Text.Substring(Adminoktatástárgya.Text.IndexOf("-") + 1), "c3");
                int sormagasság = Hánysor(Adminoktatástárgya.Text) * 20;
                if (sormagasság > 408) sormagasság = 408;
                MyE.Sormagasság("3:3", sormagasság);

                MyE.Kiir("Időpont:", "a4");
                MyE.Kiir(Adminoktatásdátuma.Value.ToString("yyyy.MM.dd"), "c4");
                MyE.Sormagasság("4:4", 20);

                MyE.Kiir("Időtartam:", "a5");
                MyE.Kiir(Admintartam.Text + " óra", "c5");
                MyE.Sormagasság("5:5", 20);

                MyE.Kiir("Ok:", "a6");
                MyE.Kiir(AdminOktatásoka.Text, "c6");
                sormagasság = Hánysor(AdminOktatásoka.Text) * 20;
                if (sormagasság > 408) sormagasság = 408;
                MyE.Sormagasság("6:6", sormagasság);

                MyE.Kiir("Leírás:", "a7");
                MyE.Kiir(Admintematika.Text, "c7");
                sormagasság = Hánysor(Admintematika.Text) * 19;
                if (sormagasság > 408) sormagasság = 408;
                MyE.Sormagasság("c7", sormagasság);

                MyE.Kiir("Előadó:", "a8");
                MyE.Kiir(AdminOktató.Text, "c8");
                MyE.Sormagasság("8:8", 20);

                MyE.Kiir("Munkaköre:", "a9");
                MyE.Kiir(AdminOktatómunkaköre.Text, "c9");
                MyE.Sormagasság("9:9", 20);

                MyE.Kiir("Aláírása:", "a10");
                MyE.Sormagasság("10:10", 40);


                // E-MAIL SZÖVEGÉT előkészítjük
                TextBox1.Text = $"A felnőttképzés adatszolgáltatási rendszeréhez (FAR) szükséges adatok\n\r";
                TextBox1.Text += $"Képzés megnevezése: {Adminoktatástárgya.Text.Substring(Adminoktatástárgya.Text.IndexOf("-") + 1)}\n\r";
                TextBox1.Text += $"Képzés jellege: {AdminOktatásoka.Text.Trim()}\n\r";
                TextBox1.Text += $"Képzés helye: {eredmény}\n\r";
                TextBox1.Text += $"Képzés helyének címe: {Adminhelyszín.Text.Trim()}\n\r";
                TextBox1.Text += $"Képzés óraszáma: {Admintartam.Text.Trim()}\n\r";
                TextBox1.Text += $"Első Képzési nap: {Adminoktatásdátuma.Value:yyyy.MM.dd}\n\r";
                TextBox1.Text += $"Befejezés tervezett időpontja: {Adminoktatásdátuma.Value:yyyy.MM.dd}\n\r\n\r";
                Holtart.Lép();
                // aláírás vonal

                MyE.Egyesít(munkalap, "A11:g11");
                MyE.Aláírásvonal("C11:G11");
                MyE.Kiir(Egyébszöveg.Text, "a11");
                MyE.Sormagasság("a11", 50);

                for (int ik = 1; ik < 12; ik++)
                {
                    MyE.Igazít_függőleges($"A{ik}", "közép");
                    MyE.Igazít_vízszintes($"A{ik}", "bal");
                    MyE.Igazít_függőleges($"C{ik}", "közép");
                    MyE.Igazít_vízszintes($"C{ik}", "bal");
                }

                // táblázat fejléc
                MyE.Sormagasság("12:12", 55);
                MyE.Kiir("Sor-szám", "a12");
                MyE.Aktív_Cella(munkalap, "A12");
                MyE.Kiir("HR azonosító", "b12");
                MyE.Kiir("Dolgozó neve", "c12");
                MyE.Kiir("Munkaköre", "d12");
                MyE.Kiir("Dátum", "e12");
                MyE.Kiir("Aláírás", "f12");
                MyE.Kiir($"Megfelelt\n\r(igen/nem/-)", "g12");
                // E-mail
                TextBox1.Text += $"HR azonosító - Dolgozó neve\n\r";
                MyE.Aktív_Cella(munkalap, "g12");
                int sor = 13;
                int i = 1;
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";
                jelszó = "forgalmiutasítás";
                szöveg = $"SELECT * FROM dolgozóadatok ";
                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(hely, jelszó, szöveg);

                for (int j = 0; j < TáblaOktatás.Rows.Count; j++)
                {
                    if (TáblaOktatás.Rows[j].Selected)
                    {
                        MyE.Sormagasság($"{sor}:{sor}", 35);
                        MyE.Kiir($"{i}", $"a{sor}");
                        string HR = TáblaOktatás.Rows[j].Cells[0].Value.ToStrTrim();
                        MyE.Kiir(HR, $"b{sor}");
                        // E-mail
                        TextBox1.Text += $"{TáblaOktatás.Rows[j].Cells[0].Value} - ";
                        MyE.Kiir($"{TáblaOktatás.Rows[j].Cells[1].Value}", $"c{sor}");
                        // E-mail
                        TextBox1.Text += $"{TáblaOktatás.Rows[j].Cells[1].Value}\n\r";
                        MyE.Aktív_Cella(munkalap, $"c{sor}");
                        if (AdatokDolg != null)
                        {
                            string munkakör = (from a in AdatokDolg
                                               where a.Dolgozószám == HR
                                               select a.Munkakör).FirstOrDefault();
                            if (munkakör != null)
                            {
                                MyE.Kiir(munkakör, $"d{sor}");
                                if (munkakör.Length > 30)
                                    MyE.Sortörésseltöbbsorba($"d{sor}");
                            }
                        }
                        MyE.Aktív_Cella(munkalap, $"d{sor}");
                        i++;
                        sor++;
                    }
                    Holtart.Lép();
                }
                i--;
                sor--;
                MyE.Rácsoz($"a12:g{sor}");
                MyE.Vastagkeret("a12:g12");
                MyE.Vastagkeret($"a12:g{sor}");

                string fénykép = $@"{Application.StartupPath}\Főmérnökség\adatok\BKV.png";
                // '**********************************************
                // '**Nyomtatási beállítások                    **
                // '**********************************************
                MyE.NyomtatásiTerület_részletes(munkalap,
                                                $"a1:g{sor}",
                                                "",
                                                "",
                                                "&G",
                                                "",
                                                $"Budapesti Közlekedési Zártkörűen Működő Részvénytársaság\nJELENLÉTI ÍV",
                                                "Hatálybalépés dátuma: 2020.10.15",
                                                "&P/&N",
                                                "",
                                                fénykép,
                                                0.590551181102362d, 0.590551181102362d,
                                                0.748031496062992d, 0.748031496062992d,
                                                0.393700787401575d, 0.511811023622047d,
                                                true, false,
                                                "Álló");


                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc + ".xlsx");

                if (TextBox1.Text?.Trim() != "")
                    BtnEmailKüldés.Visible = true;
                if (Txtemail.Text.Trim() != "_")
                    BtnEmailKüldés.Enabled = true;
                else
                    BtnEmailKüldés.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("A nyomtatvány elkészült. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int Hánysor(string szöveg)
        {
            int válasz = 0;
            string[] darabol = szöveg.Split('\n');
            for (int i = 0; i < darabol.Length; i++)
            {
                //97 karakter fér el mezőben
                válasz += darabol[i].Length / 97;
                if (darabol[i].Length / 97 != darabol[i].Length % 97)
                    válasz++;
            }
            return válasz;
        }

        private void BtnAdminMentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Adminoktatástárgya.Text?.Trim() == "") return;
                Adminoktatástárgya.Text = Adminoktatástárgya.Text.Replace("\"", "°").Replace("'", "°");
                Egyébszöveg.Text = Egyébszöveg.Text.Replace("\"", "°").Replace("'", "°");
                if (Admintartam.Text?.Trim() == "") Admintartam.Text = "0";
                if (!double.TryParse(Admintartam.Text, out _)) Admintartam.Text = "0";
                if (Txtemail.Text?.Trim() == "") Txtemail.Text = "_";

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                string szöveg = "SELECT * FROM Oktatásisegéd";

                Kezelő_OktatásiSegéd kéz = new Kezelő_OktatásiSegéd();
                List<Adat_OktatásiSegéd> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                Adat_OktatásiSegéd rekord = (from a in Adatok
                                             where a.Telephely == Cmbtelephely.Text &&
                                                   a.IDoktatás == long.Parse(Adminoktatástárgya.Text.Substring(0, Adminoktatástárgya.Text.IndexOf("-")))
                                             select a).FirstOrDefault();

                if (rekord != null)
                {
                    // módosít
                    szöveg = "Update oktatásisegéd SET ";
                    szöveg += $"oktatásoka='{AdminOktatásoka.Text.Trim()}', ";
                    szöveg += $"oktatástárgya='{Admintematika.Text.Trim()}', ";
                    szöveg += $"oktatáshelye='{Adminhelyszín.Text.Trim()}', ";
                    szöveg += $"oktatásidőtartama={Admintartam.Text.Trim()}, ";
                    szöveg += $"oktató='{AdminOktató.Text.Trim()}', ";
                    szöveg += $"oktatóbeosztása='{AdminOktatómunkaköre.Text.Trim()}', ";
                    szöveg += $"egyébszöveg='{Egyébszöveg.Text.Trim()}', ";
                    szöveg += $"email='{Txtemail.Text.Trim()}' ";
                    szöveg += $" WHERE Idoktatás={Adminoktatástárgya.Text.Substring(0, Adminoktatástárgya.Text.IndexOf("-")).Trim()}";
                    szöveg += $" and telephely='{Cmbtelephely.Text.Trim()}'";
                }
                else
                {
                    // újat rögzít
                    szöveg = "INSERT INTO oktatásisegéd (IDoktatás,  telephely, oktatásoka, oktatástárgya, oktatáshelye, oktatásidőtartama, oktató, oktatóbeosztása, egyébszöveg, email )";
                    szöveg += $" VALUES ({Adminoktatástárgya.Text.Substring(0, Adminoktatástárgya.Text.IndexOf("-")).Trim()}, ";
                    szöveg += $"'{Cmbtelephely.Text.Trim()}', ";
                    szöveg += $"'{AdminOktatásoka.Text.Trim()}', ";
                    szöveg += $"'{Admintematika.Text.Trim()}', ";
                    szöveg += $"'{Adminhelyszín.Text.Trim()}', ";
                    szöveg += $"{Admintartam.Text.Trim()}, ";
                    szöveg += $"'{AdminOktató.Text.Trim()}', ";
                    szöveg += $"'{AdminOktatómunkaköre.Text.Trim()}', ";
                    szöveg += $"'{Egyébszöveg.Text.Trim()}', ";
                    szöveg += $"'{Txtemail.Text.Trim()}') ";
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Kiírjaadmin();
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnJelenléti_Click(object sender, EventArgs e)
        {
            JelenlétiÍVKészítés();
        }

        private void EmailKüldés()
        {
            // e-mail küldés
            try
            {
                if (Txtemail.Text.Trim() == "_" || Txtemail.Text.Trim() == "")
                    return;
                Microsoft.Office.Interop.Outlook.Application _app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)_app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                // címzett
                mail.To = Txtemail.Text.Trim();
                // mail.To = "pozsgaii@bkv.hu"
                // üzent szövege
                TextBox1.Text += $"\n\r\n\r Ezt az e-mailt a Villamos program generálta.";
                mail.Body = TextBox1.Text;
                // üzenet tárgya
                mail.Subject = $"FAR rendszerbe adatszolgáltatás {Cmbtelephely.Text} - {DateTime.Now.AddDays(-1):yyyyMMdd}";
                mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceNormal;
                mail.Attachments.Add(TextBox2.Text);
                mail.Send();
                MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show("Nem lett elküldve az e-mail!", "Üzenet küldési hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void TáblaOktatás_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (CHkNapló.Checked && e.RowIndex >= 0)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{TáblaOktatás.Rows[e.RowIndex].Cells[8].Value}";
                    if (File.Exists(hely))
                        Pdf_Megjelenítés(hely);

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


        #region Oktatás Rögzítés

        #endregion


        #region Rögzítés Napló
        private void BtnRögzítFrissít_Click(object sender, EventArgs e)
        {
            if (Dátumtól.Value.Year != Dátumig.Value.Year)
                throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");

            Listanapló();
        }


        private void Listanapló()
        {
            try
            {
                Holtart.Be();
                CHkNapló.Checked = false;
                Chkoktat.Checked = false;
                Chkelrendelés.Checked = false;

                // Naplófájl ellenőrzése
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátumtól.Value:yyyy}";
                if (hely == null)
                    Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátumtól.Value:yyyy}\Oktatásnapló_{Cmbtelephely.Text}.mdb";
                if (!File.Exists(hely))
                    throw new HibásBevittAdat("Nincs az időszakban naplózott események adatbázisa.");

                string jelszó = "pázmányt";

                TáblaOktatás.Rows.Clear();
                TáblaOktatás.Columns.Clear();
                ListaNév = "Napló";

                TáblaOktatás.Refresh();
                TáblaOktatás.Visible = true;
                TáblaOktatás.ColumnCount = 14;
                TáblaOktatás.RowCount = 0;
                // ' fejléc elkészítése
                TáblaOktatás.Columns[0].HeaderText = "Sorszám";
                TáblaOktatás.Columns[0].Width = 80;
                TáblaOktatás.Columns[1].HeaderText = "HR azonosító";
                TáblaOktatás.Columns[1].Width = 115;
                TáblaOktatás.Columns[2].HeaderText = "Név";
                TáblaOktatás.Columns[2].Width = 300;
                TáblaOktatás.Columns[3].HeaderText = "IDoktatás";
                TáblaOktatás.Columns[3].Width = 80;
                TáblaOktatás.Columns[4].HeaderText = "Oktatás Témája";
                TáblaOktatás.Columns[4].Width = 300;
                TáblaOktatás.Columns[5].HeaderText = "Oktatás dátuma";
                TáblaOktatás.Columns[5].Width = 110;
                TáblaOktatás.Columns[6].HeaderText = "Telephely";
                TáblaOktatás.Columns[6].Width = 120;
                TáblaOktatás.Columns[7].HeaderText = "Oktató";
                TáblaOktatás.Columns[7].Width = 150;
                TáblaOktatás.Columns[8].HeaderText = "PDF név";
                TáblaOktatás.Columns[8].Width = 300;
                TáblaOktatás.Columns[9].HeaderText = "Számonkérés";
                TáblaOktatás.Columns[9].Width = 100;
                TáblaOktatás.Columns[10].HeaderText = "Rögzítő";
                TáblaOktatás.Columns[10].Width = 100;
                TáblaOktatás.Columns[11].HeaderText = "Rögzítés ideje";
                TáblaOktatás.Columns[11].Width = 170;
                TáblaOktatás.Columns[12].HeaderText = "Státus";
                TáblaOktatás.Columns[12].Width = 100;
                TáblaOktatás.Columns[13].HeaderText = "Megjegyzés/ Tárolási hely";
                TáblaOktatás.Columns[13].Width = 300;
                CHkNapló.Checked = true;

                string szöveg = "SELECT * FROM Oktatásnapló ";
                szöveg += $@" WHERE telephely='{Cmbtelephely.Text}'";
                szöveg += $" AND rögzítésdátuma >#{Dátumtól.Value:yyyy-MM-dd} 00:00:00#";
                szöveg += $" AND rögzítésdátuma <#{Dátumig.Value:yyyy-MM-dd} 23:59:59#";
                if (Cmboktatásrögz.Text.Trim() != "")
                    szöveg += $" AND Idoktatás={Cmboktatásrögz.Text.Substring(0, Cmboktatásrögz.Text.IndexOf("-"))}";
                szöveg += " ORDER BY Hrazonosító";

                string helyalap = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";
                string jelszóalap = "forgalmiutasítás";
                string helyoktatás = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";

                Kezelő_Oktatás_Napló kéz = new Kezelő_Oktatás_Napló();
                List<Adat_Oktatás_Napló> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                if (ChkDolgozónév.CheckedItems.Count < 1)
                {
                    foreach (Adat_Oktatás_Napló rekord in Adatok)
                    {
                        NaplóElem(rekord, helyalap, jelszóalap, helyoktatás, jelszó);
                    }
                }
                else
                {
                    for (int j = 0; j < ChkDolgozónév.CheckedItems.Count; j++)
                    {
                        string[] darabol = ChkDolgozónév.CheckedItems[j].ToString().Split('=');
                        List<Adat_Oktatás_Napló> AdatokSzűrt = (from ab in Adatok
                                                                where ab.HRazonosító.Trim() == darabol[1].Trim()
                                                                select ab).ToList();
                        if (AdatokSzűrt != null)
                        {
                            foreach (Adat_Oktatás_Napló Rekordok in AdatokSzűrt)
                                NaplóElem(Rekordok, helyalap, jelszóalap, helyoktatás, jelszó);

                        }
                    }
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

        void NaplóElem(Adat_Oktatás_Napló rekord, string helyalap, string jelszóalap, string helyoktatás, string jelszó)
        {
            try
            {
                int i = TáblaOktatás.Rows.Add();
                TáblaOktatás.Rows[i].Cells[0].Value = rekord.ID;
                TáblaOktatás.Rows[i].Cells[1].Value = rekord.HRazonosító.Trim();

                string szöveg = $"SELECT * FROM Dolgozóadatok";
                Kezelő_Dolgozó_Alap kézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolg = kézDolg.Lista_Adatok(helyalap, jelszóalap, szöveg);

                Adat_Dolgozó_Alap Elem = (from a in AdatokDolg
                                          where a.Dolgozószám == rekord.HRazonosító
                                          select a).FirstOrDefault();

                if (Elem != null) TáblaOktatás.Rows[i].Cells[2].Value = Elem.DolgozóNév;

                TáblaOktatás.Rows[i].Cells[3].Value = rekord.IDoktatás;

                szöveg = "SELECT * FROM Oktatástábla ";
                Kezelő_OktatásTábla kézOkt = new Kezelő_OktatásTábla();
                List<Adat_OktatásTábla> AdatokOkt = kézOkt.Lista_Adatok(helyoktatás, jelszó, szöveg);
                Adat_OktatásTábla ElemT = (from a in AdatokOkt
                                           where a.IDoktatás == rekord.IDoktatás
                                           select a).FirstOrDefault();

                if (Elem != null) TáblaOktatás.Rows[i].Cells[4].Value = ElemT.Téma;
                TáblaOktatás.Rows[i].Cells[5].Value = rekord.Oktatásdátuma.ToString("yyyy.MM.dd").Trim();
                TáblaOktatás.Rows[i].Cells[6].Value = rekord.Telephely.Trim();
                TáblaOktatás.Rows[i].Cells[7].Value = rekord.Kioktatta.Trim();
                TáblaOktatás.Rows[i].Cells[8].Value = rekord.PDFFájlneve.Trim();
                string válasz = "";
                switch (rekord.Számonkérés)
                {
                    case 0:
                        {
                            válasz = "nem volt";
                            break;
                        }
                    case 1:
                        {
                            válasz = "megfelelt";
                            break;
                        }
                    case 2:
                        {
                            válasz = "nem felelt meg";
                            break;
                        }
                }

                TáblaOktatás.Rows[i].Cells[9].Value = válasz.Trim();
                TáblaOktatás.Rows[i].Cells[10].Value = rekord.Rögzítő.Trim();
                TáblaOktatás.Rows[i].Cells[11].Value = rekord.Rögzítésdátuma;
                switch (rekord.Státus)
                {
                    case 0:
                        {
                            válasz = "Érvényes";
                            break;
                        }
                    case 1:
                        {
                            válasz = "Törölt";
                            break;
                        }
                }
                TáblaOktatás.Rows[i].Cells[12].Value = válasz.Trim();
                TáblaOktatás.Rows[i].Cells[13].Value = rekord.Megjegyzés.Trim();
            }
            catch (HibásBevittAdat ex)
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
    }
}