using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok.Közös;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_Utasítás
    {

        double UtolsóUtas;

        readonly Kezelő_Utasítás KézUtas = new Kezelő_Utasítás();
        readonly Kezelő_utasítás_Olvasás KézOlvas = new Kezelő_utasítás_Olvasás();

        List<Adat_Utasítás> AdatokUtas = new List<Adat_Utasítás>();
        List<Adat_utasítás_olvasás> AdatokOlvas = new List<Adat_utasítás_olvasás>();

        public Ablak_Utasítás()
        {
            InitializeComponent();
            Start();
        }

        private void Ablaküzenet_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_Utasítás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Utasítás_Generálás?.Close();
        }

        #region Alapadatok

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

                Szűrésalaphelyzetbe();
                Írokfeltöltése();
                Dátumig.MaxDate = new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59);
                Dátumtól.MaxDate = DateTime.Today;


                Utasítás_feltöltés();

                Táblalistázás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString(); }
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


        private void Szűrésalaphelyzetbe()
        {
            // alaphelyzetbe állítja a szűrő mezőket
            cmbNév.Text = "";
            txtszövegrészlet.Text = "";
            Radioolvas.Checked = true;
            Dátumig.Value = new DateTime(DateTime.Now.Year, 12, 31);
            Dátumtól.Value = new DateTime(DateTime.Now.Year, 1, 1);
        }

        private void Írokfeltöltése()
        {
            try
            {
                Utasítás_feltöltés();
                List<string> Adatok = AdatokUtas.Select(a => a.Írta).Distinct().ToList();

                cmbNév.Items.Clear();
                cmbNév.Items.Add("");

                foreach (string elem in Adatok)
                    cmbNév.Items.Add(elem);

                cmbNév.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {

                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\utasítások.html";
                MyF.Megnyitás(hely);
            }
            catch (HibásBevittAdat ex)
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
            btnVisszavon.Enabled = false;
            btnOlvasva.Enabled = false;

            melyikelem = 202;
            // módosítás 1

            if (MyF.Vanjoga(melyikelem, 1))
            {
                btnOlvasva.Enabled = true;
            }
            // módosítás 2 Főmérnökségi belépés és mindenhova tud írni
            if (MyF.Vanjoga(melyikelem, 2))
            {
                if (Program.PostásTelephely == "Főmérnökség") Cmbtelephely.Enabled = true;
            }
            // módosítás 3 szakszolgálati belépés és sajátjaiba tud írni
            if (MyF.Vanjoga(melyikelem, 3))
            {
                if (Program.Postás_Vezér == true) Cmbtelephely.Enabled = true;
            }
            melyikelem = 203;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {

            }
            // módosítás 2 Főmérnökségi belépés és mindenhova tud írni

            if (MyF.Vanjoga(melyikelem, 2))
            {
                if (Program.PostásTelephely == "Főmérnökség") Cmbtelephely.Enabled = true;

                btnVisszavon.Enabled = true;
            }
            // módosítás 3 szakszolgálati belépés és sajátjaiba tud írni

            if (MyF.Vanjoga(melyikelem, 3))
            {
                if (Program.Postás_Vezér == true) Cmbtelephely.Enabled = true;
            }
        }
        #endregion


        #region Lapozás Olvasás
        private void Btnolvasás_Click(object sender, EventArgs e)
        {
            try
            {
                int sorszám = 1;
                if (txtsorszám.Text.Trim() == "") txtsorszám.Text = sorszám.ToString();
                if (int.TryParse(txtsorszám.Text, out sorszám)) txtsorszám.Text = sorszám.ToString();

                Üzenetrészletes(sorszám);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Első_Click(object sender, EventArgs e)
        {
            txtsorszám.Text = "1";
            Üzenetrészletes(1);
        }

        private void Előző_Click(object sender, EventArgs e)
        {
            try
            {
                int sorszám = 1;
                if (txtsorszám.Text.Trim() == "") txtsorszám.Text = sorszám.ToString();
                if (int.TryParse(txtsorszám.Text, out sorszám)) txtsorszám.Text = sorszám.ToString();
                if (sorszám > 1) sorszám--;
                txtsorszám.Text = sorszám.ToString();
                Üzenetrészletes(sorszám);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Következő_Click(object sender, EventArgs e)
        {
            try
            {
                double sorszám = 1;
                if (txtsorszám.Text.Trim() == "") txtsorszám.Text = sorszám.ToString();
                if (double.TryParse(txtsorszám.Text, out sorszám)) txtsorszám.Text = sorszám.ToString();
                sorszám++;
                if (sorszám > UtolsóUtas) sorszám = UtolsóUtas;
                txtsorszám.Text = sorszám.ToString();
                Üzenetrészletes(sorszám);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Utolsó_Click(object sender, EventArgs e)
        {
            try
            {
                txtsorszám.Text = UtolsóUtas.ToString();
                Üzenetrészletes(UtolsóUtas);
            }
            catch (HibásBevittAdat ex)
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

        #region Utasítás Írás

        Ablak_Utasítás_Generálás Új_Ablak_Utasítás_Generálás;
        private void Btnújüzenet_Click(object sender, EventArgs e)
        {
            Új_Ablak_Utasítás_Generálás?.Close();

            Új_Ablak_Utasítás_Generálás = new Ablak_Utasítás_Generálás(Cmbtelephely.Text.Trim(), "");
            Új_Ablak_Utasítás_Generálás.FormClosed += Ablak_Utasítás_Generálás_FormClosed;
            Új_Ablak_Utasítás_Generálás.Változás += Változások;
            Új_Ablak_Utasítás_Generálás.Show();
        }

        private void Ablak_Utasítás_Generálás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Utasítás_Generálás = null;
        }

        private void Változások()
        {
            try
            {

                btnOlvasva.Visible = false;
                Utasítás_feltöltés();
                Olvas_feltöltés();
                Táblalistázás();
            }
            catch (HibásBevittAdat ex)
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


        #region Vezérlő gombok

        private void BtnOlvasva_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtsorszám.Text.Trim(), out double sorszám)) return;

                Adat_utasítás_olvasás Olvasta = (from a in AdatokOlvas
                                                 where a.Üzenetid == sorszám && a.Ki == Program.PostásNév.Trim()
                                                 select a).FirstOrDefault();
                if (Olvasta == null)
                    KézOlvas.Rögzítés(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year, new Adat_utasítás_olvasás(0, Program.PostásNév.Trim(), sorszám, DateTime.Now, false));

                btnOlvasva.Visible = false;
                Olvas_feltöltés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVisszavon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtsorszám.Text, out int sorszám)) return;

                // módosít
                Adat_Utasítás Elem = (from a in AdatokUtas
                                      where a.Sorszám == sorszám
                                      select a).FirstOrDefault();
                string ideig = $"\r\n\r\n Visszavonta : {Program.PostásNév.Trim()} Dátum: {DateTime.Now:yyyy.MM.dd hh:mm}";

                if (Elem != null)
                {
                    Adat_Utasítás ADAT = new Adat_Utasítás(sorszám, Elem.Szöveg + ideig, 1);
                    KézUtas.Módosítás(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year, ADAT);
                    txtírásimező.Text = Elem.Szöveg + ideig;
                }
                btnVisszavon.Visible = false;
                Utasítás_feltöltés();
                Táblalistázás();
            }
            catch (HibásBevittAdat ex)
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



        private void Frissít_Click(object sender, EventArgs e)
        {
            Táblalistázás();
            txtsorszám.Text = "";
            txtírásimező.Text = "";
            tábla.ClearSelection();
        }

        private void Táblalistázás()
        {
            try
            {
                Utasítás_feltöltés();
                Olvas_feltöltés();
                if (Dátumig.Value < Dátumtól.Value) Dátumig.Value = Dátumtól.Value;

                List<Adat_Utasítás> Adatok = (from a in AdatokUtas
                                              where a.Mikor >= Dátumtól.Value && a.Mikor <= Dátumig.Value.AddDays(1)
                                              select a).ToList();
                if (cmbNév.Text.Trim() != "")
                    Adatok = (from a in Adatok
                              where a.Írta == cmbNév.Text.Trim()
                              select a).ToList();

                if (Radioolvas.Checked)
                    Adatok = (from a in Adatok
                              where a.Érvényes == 0
                              select a).ToList();
                else if (Radioolvastan.Checked)
                    Adatok = (from a in Adatok
                              where a.Érvényes == 1
                              select a).ToList();

                if (txtszövegrészlet.Text.Trim() != "")
                    Adatok = (from a in Adatok
                              where a.Szöveg.ToUpper().Contains(txtszövegrészlet.Text.ToUpper().Trim())
                              select a).ToList();


                tábla.Visible = false;
                tábla.RowCount = 0;
                foreach (Adat_Utasítás rekord in Adatok)
                {
                    tábla.RowCount += 1;
                    int i = tábla.RowCount - 1;
                    tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    tábla.Rows[i].Cells[1].Value = rekord.Írta.Trim();
                    tábla.Rows[i].Cells[2].Value = rekord.Mikor.ToString("yyyy.MM.dd. HH:mm");
                    tábla.Rows[i].Cells[3].Value = rekord.Szöveg.Replace('°', '"');
                    if (rekord.Érvényes == 0)
                        tábla.Rows[i].Cells[4].Value = CheckState.Checked;
                    else
                        tábla.Rows[i].Cells[4].Value = CheckState.Unchecked;
                }
                tábla.Visible = true;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Alaphelyzet_Click(object sender, EventArgs e)
        {
            Szűrésalaphelyzetbe();
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (tábla.RowCount == 0) return;
                if (e.RowIndex < 0) return;

                txtsorszám.Text = tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                Üzenetrészletes(txtsorszám.Text.ToÉrt_Int());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Üzenetrészletes(double sorszám)
        {
            try
            {
                Adat_Utasítás Adat = (from a in AdatokUtas
                                      where a.Sorszám == sorszám
                                      select a).FirstOrDefault();
                if (Adat == null) return;

                List<Adat_utasítás_olvasás> Olvasta = (from a in AdatokOlvas
                                                       where a.Üzenetid == sorszám
                                                       select a).ToList();

                txtírásimező.Text = "Dátum: " + Adat.Mikor + "\n";
                txtírásimező.Text += $"Írta:{Adat.Írta.Trim()}\n";
                txtírásimező.Text += $"Utasítás tartalma:\n\n{Adat.Szöveg.Replace('°', '"')}";
                // Érvényesség gomb
                if (Adat.Érvényes == 0)
                    btnVisszavon.Visible = true;
                else
                    btnVisszavon.Visible = false;

                // olvasók kiírása

                string szöveg0 = "Üzenetet olvasta: ";
                foreach (Adat_utasítás_olvasás rekord in Olvasta)
                    szöveg0 += rekord.Ki.Trim() + ", ";

                txtírásimező.Text += "\r\n\r\n" + szöveg0;

                // gombok kezelése

                btnOlvasva.Visible = false;
                //Ha olvasta akkor mégegyszer nem kell 
                if (Olvasta != null)
                {
                    Adat_utasítás_olvasás olvasók = (from Olvas in Olvasta
                                                     where Olvas.Ki == Program.PostásNév
                                                     select Olvas).FirstOrDefault();
                    if (olvasók == null) btnOlvasva.Visible = true;
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

        private void Excel_kimenet_Click(object sender, EventArgs e)
        {
            try
            {
                //ha üres a táblázat akkor kilép
                if (tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Utasítás_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.ExcelLétrehozás();

                string munkalap = "Utasítás";
                MyX.Munkalap_átnevezés("Munka1", "Utasítás");
                Holtart.Be(tábla.Rows.Count + 2);

                MyX.Kiir("Sorszám", "a1");
                MyX.Kiir("Írta", "b1");
                MyX.Kiir("Mikor", "c1");
                MyX.Kiir("Üzenet", "d1");

                MyX.Oszlopszélesség(munkalap, "A:A", 8);
                MyX.Oszlopszélesség(munkalap, "B:B", 15);
                MyX.Oszlopszélesség(munkalap, "C:C", 18);
                MyX.Oszlopszélesség(munkalap, "D:D", 100);

                for (int i = 0; i < tábla.Rows.Count; i++)
                {
                    MyX.Kiir(tábla.Rows[i].Cells[0].Value.ToString(), $"a{i + 2}");
                    MyX.Kiir(tábla.Rows[i].Cells[1].Value.ToString(), $"b{i + 2}");
                    MyX.Kiir(tábla.Rows[i].Cells[2].Value.ToString(), $"c{i + 2}");
                    MyX.Kiir(tábla.Rows[i].Cells[3].Value.ToString(), $"d{i + 2}");
                    MyX.Sortörésseltöbbsorba(munkalap, $"d{i + 2}");
                    Holtart.Lép();
                }
                MyX.Rácsoz(munkalap, $"A1:D{tábla.Rows.Count + 1}");
                Beállítás_Nyomtatás beállítás = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:D{tábla.Rows.Count + 1}",
                    Álló = false,
                    IsmétlődőSorok = "$1:$1"
                };
                MyX.Szűrés(munkalap, "A", "D", tábla.Rows.Count + 1);
                MyX.NyomtatásiTerület_részletes(munkalap, beállítás);
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                Holtart.Ki();

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


        #region feltöltések
        private void Utasítás_feltöltés()
        {
            try
            {
                AdatokUtas.Clear();
                UtolsóUtas = 0;
                AdatokUtas = KézUtas.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
                if (AdatokUtas.Count == 0) return;
                UtolsóUtas = AdatokUtas.Max(a => a.Sorszám);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Olvas_feltöltés()
        {
            try
            {
                AdatokOlvas.Clear();
                AdatokOlvas = KézOlvas.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
            }
            catch (HibásBevittAdat ex)
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
            Utasítás_feltöltés();
            Olvas_feltöltés();
            Írokfeltöltése();
            Táblalistázás();
            txtsorszám.Text = "";
            txtírásimező.Text = "";
        }
        #endregion


        #region Képernyőátméretezés
        private void Lefelé_Click(object sender, EventArgs e)
        {
            try
            {
                int lépés = 20;
                if ((txtírásimező.Height - lépés) > 100)
                {
                    tábla.Height += lépés;
                    panel1.Top += lépés;
                    txtírásimező.Top += lépés;
                    txtírásimező.Height -= lépés;
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

        private void Felfelé_Click(object sender, EventArgs e)
        {
            try
            {     //Felfelé mozgat
                int lépés = 20;
                if ((tábla.Height - lépés) > 100)
                {
                    tábla.Height -= lépés;
                    panel1.Top -= lépés;
                    txtírásimező.Top -= lépés;
                    txtírásimező.Height += lépés;
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