using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok.Közös;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;



namespace Villamos
{

    public partial class Ablak_üzenet
    {
        readonly Kezelő_Üzenet KézÜzenet = new Kezelő_Üzenet();
        readonly Kezelő_Üzenet_Olvas KézOlvas = new Kezelő_Üzenet_Olvas();
        readonly Kezelő_Kiegészítő_Könyvtár KézKiegKönyvtár = new Kezelő_Kiegészítő_Könyvtár();

        List<Adat_Üzenet> Adatok_Üzenet = new List<Adat_Üzenet>();
        List<Adat_Üzenet_Olvasás> Adatok_Olvas = new List<Adat_Üzenet_Olvasás>();

        #region Alap
        public Ablak_üzenet()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_üzenet_Load(object sender, EventArgs e)
        { }

        private void Listák_Feltöltése()
        {
            Adatok_Üzenet = KézÜzenet.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
            Olvasás_listázás();
        }

        private void CMBtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Szűrésalaphelyzetbe();
            Írokfeltöltése();
        }

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

                Dátumig.MaxDate = DateTime.Today;
                Dátumtól.MaxDate = DateTime.Today;

                Radioolvastan.Checked = true;
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
                if (Program.PostásTelephely == "Főmérnökség")
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


        private void Button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\üzenetek.html";
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


        private void Szűrésalaphelyzetbe()
        {
            try
            {
                // alaphelyzetbe állítja a szűrő mezőket
                cmbNév.Text = "";
                txtszövegrészlet.Text = "";
                Radioolvastan.Checked = true;

                Dátumig.Value = DateTime.Today;

                // ha az előző évre esik a 30 nappal korábbi dátum, akkor 01.01.
                if (DateTime.Today.AddDays(-30).Year == Dátumig.Value.Year)
                    Dátumtól.Value = DateTime.Today.AddDays(-30);
                else
                    Dátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);
                Írokfeltöltése();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Szűrésalaphelyzetbe();
        }

        private void Írokfeltöltése()
        {
            try
            {
                Adatok_Üzenet = KézÜzenet.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
                List<string> Írók = Adatok_Üzenet.Select(x => x.Írta).Distinct().ToList();
                if (Írók == null) return;

                cmbNév.Items.Clear();
                cmbNév.Items.Add("");
                cmbNév.BeginUpdate();
                foreach (string elem in Írók)
                    cmbNév.Items.Add(elem);

                cmbNév.EndUpdate();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Táblalistázás();
            Txtírásimező.Text = "";
        }

        private void Táblalistázás()
        {
            try
            {
                Listák_Feltöltése();
                if (Adatok_Üzenet == null) return;

                List<Adat_Üzenet> Adatok = (from a in Adatok_Üzenet
                                            where a.Mikor >= Dátumtól.Value && a.Mikor < Dátumig.Value.AddDays(1)
                                            orderby a.Sorszám descending
                                            select a).ToList();
                // Író szűrő
                if (cmbNév.Text.Trim() != "")
                    Adatok = (from a in Adatok
                              where a.Írta == cmbNév.Text.Trim()
                              select a).ToList();

                if (txtszövegrészlet.Text.Trim() != "")
                    Adatok = (from a in Adatok
                              where a.Szöveg.Contains(txtszövegrészlet.Text.Trim())
                              select a).ToList();

                Tábla.Visible = false;
                Tábla.RowCount = 0;

                foreach (Adat_Üzenet rekord in Adatok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[1].Value = rekord.Írta.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Mikor.ToString("yyyy.MM.dd HH:mm");
                    Tábla.Rows[i].Cells[3].Value = rekord.Szöveg.Replace('°', '"').Trim();
                    Adat_Üzenet_Olvasás Elem = (from a in Adatok_Olvas
                                                where a.Üzenetid == rekord.Sorszám && a.Ki == Program.PostásNév
                                                select a).FirstOrDefault();
                    if (Elem != null)
                        Tábla.Rows[i].Cells[4].Value = CheckState.Checked;
                    else
                        Tábla.Rows[i].Cells[4].Value = CheckState.Unchecked;
                }
                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
                // radió gombok szerint listázunk tovább
                if (!RadioMinden.Checked)
                {
                    Tábla.Visible = false;
                    int utolsó = Tábla.RowCount - 1;
                    for (int ii = utolsó; ii >= 0; ii -= 1)
                    {
                        if (Tábla.Rows[ii].Cells[4].Value.ToString() == "Unchecked")
                        {
                            if (!Radioolvastan.Checked)
                                Tábla.Rows.Remove(Tábla.Rows[ii]);
                        }
                        else
                        {
                            if (Radioolvastan.Checked)
                                Tábla.Rows.Remove(Tábla.Rows[ii]);

                        }
                    }
                    Tábla.Refresh();
                    Tábla.ClearSelection();
                    Tábla.Visible = true;
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tábla.RowCount == 0) return;
            if (e.RowIndex >= 0)
            {
                if (!int.TryParse(Tábla.Rows[e.RowIndex].Cells[0].Value.ToString(), out int sorszám))
                    return;
                txtsorszám.Text = sorszám.ToString();
                Kiválasztott_üzenet(sorszám);
            }
        }

        private void Tábla_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla.RowCount == 0) return;
            if (Tábla.SelectedRows.Count == 0) return;

            if (!int.TryParse(Tábla.Rows[Tábla.SelectedRows[0].Index].Cells[0].Value.ToString(), out int sorszám))
                return;

            txtsorszám.Text = sorszám.ToString();
            Kiválasztott_üzenet(sorszám);
        }

        private void Kiválasztott_üzenet(double sorszám)
        {
            try
            {
                Adat_Üzenet Adat = (from a in Adatok_Üzenet
                                    where a.Sorszám == sorszám
                                    select a).FirstOrDefault() ?? throw new HibásBevittAdat($"Nincs {sorszám} számú üzenet.");

                Txtírásimező.Text = "";
                txtválasz.Text = Adat.Válaszsorszám.ToString();

                if (Adat.Válaszsorszám != 0) Txtírásimező.Text = $"Válasz a {Adat.Válaszsorszám} számú üzenetre:\n\r";

                Txtírásimező.Text += $"Dátum: {Adat.Mikor}\r";
                Txtírásimező.Text += $"Írta: {Adat.Írta.Trim()}\n\r";
                Txtírásimező.Text += $"Üzenet tartalma:\n\r{Adat.Szöveg.Replace('°', '"')}";

                // válaszok kiírása
                List<Adat_Üzenet> Adatok = (from a in Adatok_Üzenet
                                            where a.Válaszsorszám == sorszám
                                            select a).ToList();

                if (Adatok != null)
                {
                    foreach (Adat_Üzenet rekord in Adatok)
                    {
                        Txtírásimező.Text += $"\r\n\r\n Választ írta: {rekord.Írta.Trim()} Dátum: {rekord.Mikor} Sorszám: {rekord.Sorszám}";
                        Txtírásimező.Text += $"\r\n\r\n{rekord.Szöveg.Trim().Replace('°', '"')}";
                    }
                }

                // olvasók kiírása
                List<Adat_Üzenet_Olvasás> AdatokO = (from a in Adatok_Olvas
                                                     where a.Üzenetid == sorszám
                                                     select a).ToList();

                string szöveg0 = "Üzenetet olvasta: ";
                foreach (Adat_Üzenet_Olvasás rekord in AdatokO)
                    szöveg0 += rekord.Ki.Trim() + ", ";

                Txtírásimező.Text += "\r\n\r\n" + szöveg0;
                Txtírásimező.Refresh();

                Adat_Üzenet_Olvasás Olvasó = (from Elem in Adatok_Olvas
                                              where Elem.Ki.Trim() == Program.PostásNév.Trim()
                                              && Elem.Üzenetid == sorszám
                                              select Elem).FirstOrDefault();

                // gombok kezelése
                if (Olvasó == null)
                    BtnOlvasva.Visible = true;
                else
                    BtnOlvasva.Visible = false;

                btnválaszol.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btnolvasás_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtsorszám.Text, out int sorszám)) return;
                Kiválasztott_üzenet(sorszám);
            }
            catch (HibásBevittAdat ex)
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
            try
            {
                txtsorszám.Text = 1.ToString();
                Kiválasztott_üzenet(1);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Előző_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtsorszám.Text, out double sorszám)) sorszám = 1;
                txtsorszám.Text = sorszám.ToString();
                if (sorszám > 1)
                {
                    sorszám--;
                    txtsorszám.Text = sorszám.ToString();
                }
                Kiválasztott_üzenet(sorszám);
            }
            catch (HibásBevittAdat ex)
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
                if (!double.TryParse(txtsorszám.Text, out double sorszám)) sorszám = 1;

                txtsorszám.Text = sorszám.ToString();
                sorszám++;
                txtsorszám.Text = sorszám.ToString();
                double utolsó = Utolsóüzenet();
                if (sorszám > utolsó)
                {
                    txtsorszám.Text = utolsó.ToString();
                    sorszám = utolsó;
                }
                Kiválasztott_üzenet(sorszám);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int Utolsóüzenet()
        {
            int válasz = 1;
            if (Adatok_Üzenet != null)
            {
                válasz = (int)(from a in Adatok_Üzenet
                               orderby a.Sorszám descending
                               select a.Sorszám).FirstOrDefault();
            }

            return válasz;
        }

        private void Utolsó_Click(object sender, EventArgs e)
        {
            try
            {
                double utolsó = Utolsóüzenet();
                txtsorszám.Text = utolsó.ToString();
                Kiválasztott_üzenet(utolsó);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btnválaszol_Click(object sender, EventArgs e)
        {
            try
            {
                // válasz előkészítése
                if (txtsorszám.Text.Trim() == "") return;
                if (!int.TryParse(txtsorszám.Text, out int sorszám)) return;

                Új_Ablak_Üzenet_Generálás?.Close();

                Új_Ablak_Üzenet_Generálás = new Ablak_Üzenet_Generálás(Cmbtelephely.Text.Trim(), "", sorszám);
                Új_Ablak_Üzenet_Generálás.FormClosed += Ablak_Üzenet_Generálás_FormClosed;
                Új_Ablak_Üzenet_Generálás.Változás += Változások;
                Új_Ablak_Üzenet_Generálás.Show();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Üzenet Írás

        Ablak_Üzenet_Generálás Új_Ablak_Üzenet_Generálás;
        private void Btnújüzenet_Click(object sender, EventArgs e)
        {
            Új_Ablak_Üzenet_Generálás?.Close();

            Új_Ablak_Üzenet_Generálás = new Ablak_Üzenet_Generálás(Cmbtelephely.Text.Trim(), "", 0);
            Új_Ablak_Üzenet_Generálás.FormClosed += Ablak_Üzenet_Generálás_FormClosed;
            Új_Ablak_Üzenet_Generálás.Változás += Változások;
            Új_Ablak_Üzenet_Generálás.Show();
        }

        private void Ablak_Üzenet_Generálás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Üzenet_Generálás = null;
        }

        private void Változások()
        {
            try
            {
                BtnOlvasva.Visible = false;
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

        private void BtnOlvasva_Click(object sender, EventArgs e)
        {
            try
            {
                // ha nincs kijelölve egy sor sem és a sorszám mező üres, akkor kilépünk
                if (Tábla.SelectedRows.Count == 0 || !int.TryParse(txtsorszám.Text, out int sorszám)) return;

                // ha nincs kijelölve, de a sorszám mező nem üres
                if (Tábla.SelectedRows.Count == 0)
                    Olvasottátesz(sorszám);
                else
                {
                    List<double> Sorok = new List<double>();
                    for (int sor = 0; sor < Tábla.SelectedRows.Count; sor++)
                        if (double.TryParse(Tábla.SelectedRows[sor].Cells[0].Value.ToString(), out double sora)) Sorok.Add(sora);

                    Olvasottátesz(Sorok);
                }
                Táblalistázás();
                Txtírásimező.Text = "";

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Olvasottátesz(double sorszám)
        {
            try
            {
                Adat_Üzenet_Olvasás ADAT = new Adat_Üzenet_Olvasás(0,
                                                               Program.PostásNév.Trim(),
                                                               sorszám,
                                                               DateTime.Now,
                                                               false);
                KézOlvas.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today.Year, ADAT);

                BtnOlvasva.Visible = false;
                Olvasás_listázás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Olvasottátesz(List<double> Sorszámok)
        {
            try
            {

                List<Adat_Üzenet_Olvasás> ADATOK = new List<Adat_Üzenet_Olvasás>();
                foreach (double item in Sorszámok)
                {
                    Adat_Üzenet_Olvasás ADAT = new Adat_Üzenet_Olvasás(0,
                                                          Program.PostásNév.Trim(),
                                                          item,
                                                          DateTime.Now,
                                                          false);
                    ADATOK.Add(ADAT);
                }


                KézOlvas.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today.Year, ADATOK);

                BtnOlvasva.Visible = false;
                Olvasás_listázás();
            }
            catch (HibásBevittAdat ex)
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

                BtnOlvasva.Visible = false;
                melyikelem = 200;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    BtnOlvasva.Visible = true;
                }
                // módosítás 2 főmérnökségi belépés és mindenhova tud írni
                if (MyF.Vanjoga(melyikelem, 2))
                {
                }
                // módosítás 3 szakszolgálati belépés és sajátjaiba tud írni
                if (MyF.Vanjoga(melyikelem, 3))
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

        private void Tábla_MultiSelectChanged(object sender, EventArgs e)
        {
            if (Tábla.SelectedRows.Count > 0)
                BtnOlvasva.Visible = true;
            else
                BtnOlvasva.Visible = false;
        }




        #region Excel
        private void Excel_kimenet_Click(object sender, EventArgs e)
        {
            try
            {
                //ha üres a táblázat akkor kilép
                if (Tábla.Rows.Count <= 0) return;

                string fájlexc = MyF.Mentés_Fájlnév
                    (
                    "Listázott tartalom mentése Excel fájlba",
                    $"Üzenetek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}"
                    );
                if (fájlexc.Trim() == "") return;


                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.ExcelLétrehozás();


                string munkalap = "Üzenetek";
                MyE.Munkalap_átnevezés("Munka1", "Üzenetek");
                Holtart.Be(Tábla.Rows.Count + 2);

                MyE.Kiir("Sorszám", "a1");
                MyE.Kiir("Írta", "b1");
                MyE.Kiir("Mikor", "c1");
                MyE.Kiir("Üzenet", "d1");

                MyE.Oszlopszélesség(munkalap, "A:A", 8);
                MyE.Oszlopszélesség(munkalap, "B:B", 15);
                MyE.Oszlopszélesség(munkalap, "C:C", 18);
                MyE.Oszlopszélesség(munkalap, "D:D", 100);

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    MyE.Kiir(Tábla.Rows[i].Cells[0].Value.ToString(), "a" + (i + 2).ToString());
                    MyE.Kiir(Tábla.Rows[i].Cells[1].Value.ToString(), "b" + (i + 2).ToString());
                    MyE.Kiir(Tábla.Rows[i].Cells[2].Value.ToString(), "c" + (i + 2).ToString());
                    MyE.Kiir(Tábla.Rows[i].Cells[3].Value.ToString(), "d" + (i + 2).ToString());
                    MyE.Sortörésseltöbbsorba("d" + (i + 2).ToString());
                    Holtart.Lép();
                }
                MyE.Rácsoz("A1:D" + (Tábla.Rows.Count + 2).ToString());
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:d" + (Tábla.Rows.Count + 2).ToString(), "1:1", "", false);
                MyE.Szűrés(munkalap, "A", "D", Tábla.Rows.Count + 2);
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                Holtart.Ki();

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
        #endregion


        private void Button7_Click(object sender, EventArgs e)
        {
            //Felfelé mozgat
            int lépés = 20;
            if ((Tábla.Height - lépés) > 100)
            {
                Tábla.Height -= lépés;
                panel2.Top -= lépés;
                Txtírásimező.Top -= lépés;
                Txtírásimező.Height += lépés;
            }
        }

        private void Button7_DoubleClick(object sender, EventArgs e)
        {
            //Felfelé mozgat
            int lépés = Tábla.Height + 100;

            Tábla.Height -= lépés;
            panel2.Top -= lépés;
            Txtírásimező.Top -= lépés;
            Txtírásimező.Height += lépés;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            int lépés = 20;
            if ((Txtírásimező.Height - lépés) > 100)
            {
                Tábla.Height += lépés;
                panel2.Top += lépés;
                Txtírásimező.Top += lépés;
                Txtírásimező.Height -= lépés;
            }
        }

        private void Olvasás_listázás()
        {
            try
            {
                Adatok_Olvas.Clear();
                Adatok_Olvas = KézOlvas.Lista_Adatok(Cmbtelephely.Text.Trim(), DateTime.Today.Year);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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