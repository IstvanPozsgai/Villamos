using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Adatszerkezet;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_Szín : Form
    {
        Szín_kódolás Szín;

        readonly Kezelő_CAF_Szinezés KézSzín = new Kezelő_CAF_Szinezés();
        List<Adat_CAF_Szinezés> AdatokSzín = new List<Adat_CAF_Szinezés>();

        public Ablak_CAF_Szín()
        {
            InitializeComponent();
            Start();
        }


        private void Start()
        {
            Telephelyekfeltöltése();
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this);
            else
                Jogosultságkiosztás();
        }

        private void Ablak_CAF_Szín_Load(object sender, EventArgs e)
        {

        }


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Karb_rögzít.Enabled = false;
                Karb_töröl.Enabled = false;

                // csak Főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Karb_rögzít.Visible = true;
                    Karb_töröl.Visible = true;
                }
                else
                {
                    Karb_rögzít.Visible = false;
                    Karb_töröl.Visible = false;
                }

                melyikelem = 119;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {

                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Karb_rögzít.Enabled = true;
                    Karb_töröl.Enabled = true;
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Színtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Színtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Színtelephely.Text = Színtelephely.Items[0].ToString().Trim(); }
                else
                { Színtelephely.Text = Program.PostásTelephely; }

                Színtelephely.Enabled = Program.Postás_Vezér;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Szín_tábla_lista_Click(object sender, EventArgs e)
        {
            Szín_tábla_kiírás();
        }

        private void Szín_tábla_kiírás()
        {
            try
            {
                AdatokSzín = KézSzín.Lista_Adatok();

                Szín_Tábla.Rows.Clear();
                Szín_Tábla.Columns.Clear();
                Szín_Tábla.Refresh();
                Szín_Tábla.Visible = false;
                Szín_Tábla.ColumnCount = 14;

                // fejléc elkészítése
                Szín_Tábla.Columns[0].HeaderText = "Telephely";
                Szín_Tábla.Columns[0].Width = 100;
                Szín_Tábla.Columns[1].HeaderText = "Pályaszám";
                Szín_Tábla.Columns[1].Width = 90;
                Szín_Tábla.Columns[2].HeaderText = "Pályaszám gar.";
                Szín_Tábla.Columns[2].Width = 90;
                Szín_Tábla.Columns[3].HeaderText = "IS tűrés";
                Szín_Tábla.Columns[3].Width = 75;
                Szín_Tábla.Columns[4].HeaderText = "IS";
                Szín_Tábla.Columns[4].Width = 75;
                Szín_Tábla.Columns[5].HeaderText = "P";
                Szín_Tábla.Columns[5].Width = 75;
                Szín_Tábla.Columns[6].HeaderText = "Szombat";
                Szín_Tábla.Columns[6].Width = 80;
                Szín_Tábla.Columns[7].HeaderText = "Vasárnap";
                Szín_Tábla.Columns[7].Width = 80;

                Szín_Tábla.Columns[8].HeaderText = "Eszterga";
                Szín_Tábla.Columns[8].Width = 80;
                Szín_Tábla.Columns[9].HeaderText = "$ beírás";
                Szín_Tábla.Columns[9].Width = 75;
                Szín_Tábla.Columns[10].HeaderText = "@ beírás";
                Szín_Tábla.Columns[10].Width = 75;
                Szín_Tábla.Columns[11].HeaderText = "# beírás";
                Szín_Tábla.Columns[11].Width = 75;
                Szín_Tábla.Columns[12].HeaderText = "§ beírás";
                Szín_Tábla.Columns[12].Width = 75;
                Szín_Tábla.Columns[13].HeaderText = "> beírás";
                Szín_Tábla.Columns[13].Width = 75;

                foreach (Adat_CAF_Szinezés rekord in AdatokSzín)
                {
                    Szín_Tábla.RowCount++;
                    int i = Szín_Tábla.RowCount - 1;
                    Szín_Tábla.Rows[i].Cells[0].Value = rekord.Telephely;
                    Szín_Tábla.Rows[i].Cells[1].Value = rekord.SzínPsz;
                    Szín_Tábla.Rows[i].Cells[2].Value = rekord.SzínPSZgar;
                    Szín_Tábla.Rows[i].Cells[3].Value = rekord.SzínIStűrés;
                    Szín_Tábla.Rows[i].Cells[4].Value = rekord.SzínIS;
                    Szín_Tábla.Rows[i].Cells[5].Value = rekord.SzínP;
                    Szín_Tábla.Rows[i].Cells[6].Value = rekord.Színszombat;
                    Szín_Tábla.Rows[i].Cells[7].Value = rekord.SzínVasárnap;

                    Szín_Tábla.Rows[i].Cells[8].Value = rekord.Szín_E;
                    Szín_Tábla.Rows[i].Cells[9].Value = rekord.Szín_dollár;
                    Szín_Tábla.Rows[i].Cells[10].Value = rekord.Szín_Kukac;
                    Szín_Tábla.Rows[i].Cells[11].Value = rekord.Szín_Hasteg;
                    Szín_Tábla.Rows[i].Cells[12].Value = rekord.Szín_jog;
                    Szín_Tábla.Rows[i].Cells[13].Value = rekord.Szín_nagyobb;

                    Szín = MyColor.Szín_váltó((long)rekord.SzínPsz);
                    Szín_Tábla.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.SzínPSZgar);
                    Szín_Tábla.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.SzínIStűrés);
                    Szín_Tábla.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.SzínIS);
                    Szín_Tábla.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.SzínP);
                    Szín_Tábla.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.Színszombat);
                    Szín_Tábla.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.SzínVasárnap);
                    Szín_Tábla.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

                    Szín = MyColor.Szín_váltó((long)rekord.Szín_E);
                    Szín_Tábla.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.Szín_dollár);
                    Szín_Tábla.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.Szín_Kukac);
                    Szín_Tábla.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.Szín_Hasteg);
                    Szín_Tábla.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.Szín_jog);
                    Szín_Tábla.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó((long)rekord.Szín_nagyobb);
                    Szín_Tábla.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                }

                Szín_Tábla.Visible = true;
                Szín_Tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Command17_Click(object sender, EventArgs e)
        {
            try
            {
                double piros;
                double zöld;
                double kék;
                Színe.Text = 0.ToString();
                ColorDialog ColorDialog1 = new ColorDialog();
                if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    piros = ColorDialog1.Color.R;
                    zöld = ColorDialog1.Color.G;
                    kék = ColorDialog1.Color.B;
                    Színe.Text = (piros + zöld * 256d + kék * 65536d).ToString();
                    Színe.BackColor = ColorDialog1.Color;

                    switch (int.Parse(Színmező.Text))
                    {
                        case 1:
                            {
                                SzínPsz.Text = Színe.Text;
                                SzínPsz.BackColor = Színe.BackColor;
                                break;
                            }
                        case 2:
                            {
                                SzínPszGar.Text = Színe.Text;
                                SzínPszGar.BackColor = Színe.BackColor;
                                break;
                            }
                        case 3:
                            {
                                SzínISTűrés.Text = Színe.Text;
                                SzínISTűrés.BackColor = Színe.BackColor;
                                break;
                            }
                        case 4:
                            {
                                SzínIS.Text = Színe.Text;
                                SzínIS.BackColor = Színe.BackColor;
                                break;
                            }
                        case 5:
                            {
                                SzínP.Text = Színe.Text;
                                SzínP.BackColor = Színe.BackColor;
                                break;
                            }
                        case 6:
                            {
                                SzínSzombat.Text = Színe.Text;
                                SzínSzombat.BackColor = Színe.BackColor;
                                break;
                            }
                        case 7:
                            {
                                SzínVasárnap.Text = Színe.Text;
                                SzínVasárnap.BackColor = Színe.BackColor;
                                break;
                            }

                        case 8:
                            {
                                Szín_E.Text = Színe.Text;
                                Szín_E.BackColor = Színe.BackColor;
                                break;
                            }
                        case 9:
                            {
                                Szín_dollár.Text = Színe.Text;
                                Szín_dollár.BackColor = Színe.BackColor;
                                break;
                            }
                        case 10:
                            {
                                Szín_Kukac.Text = Színe.Text;
                                Szín_Kukac.BackColor = Színe.BackColor;
                                break;
                            }
                        case 11:
                            {
                                Szín_Hasteg.Text = Színe.Text;
                                Szín_Hasteg.BackColor = Színe.BackColor;
                                break;
                            }
                        case 12:
                            {
                                Szín_jog.Text = Színe.Text;
                                Szín_jog.BackColor = Színe.BackColor;
                                break;
                            }
                        case 13:
                            {
                                Szín_nagyobb.Text = Színe.Text;
                                Szín_nagyobb.BackColor = Színe.BackColor;
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

        private void Karb_új_Click(object sender, EventArgs e)
        {
            SzínPsz.Text = "";
            SzínPszGar.Text = "";
            SzínISTűrés.Text = "";
            SzínIS.Text = "";
            SzínP.Text = "";
            SzínSzombat.Text = "";
            SzínVasárnap.Text = "";

            Szín_E.Text = "";
            Szín_dollár.Text = "";
            Szín_Kukac.Text = "";
            Szín_Hasteg.Text = "";
            Szín_jog.Text = "";
            Szín_nagyobb.Text = "";
        }

        private void Karb_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (SzínPsz.Text.Trim() == "") throw new HibásBevittAdat("Pályaszám szín mező nem lehet üres");
                if (SzínPszGar.Text.Trim() == "") throw new HibásBevittAdat("Pályaszám gar szín mező nem lehet üres");
                if (SzínISTűrés.Text.Trim() == "") throw new HibásBevittAdat("IS tűrés szín mező nem lehet üres");
                if (SzínIS.Text.Trim() == "") throw new HibásBevittAdat("IS szín mező nem lehet üres");
                if (SzínP.Text.Trim() == "") throw new HibásBevittAdat("P szín mező nem lehet üres");
                if (SzínSzombat.Text.Trim() == "") throw new HibásBevittAdat("Szombat szín mező nem lehet üres");
                if (SzínVasárnap.Text.Trim() == "") throw new HibásBevittAdat("Vasárnap szín mező nem lehet üres");
                if (Szín_E.Text.Trim() == "") throw new HibásBevittAdat("E szín mező nem lehet üres");
                if (Szín_dollár.Text.Trim() == "") throw new HibásBevittAdat("$ szín mező nem lehet üres");
                if (Szín_Kukac.Text.Trim() == "") throw new HibásBevittAdat("@ szín mező nem lehet üres");
                if (Szín_Hasteg.Text.Trim() == "") throw new HibásBevittAdat("# szín mező nem lehet üres");
                if (Szín_jog.Text.Trim() == "") throw new HibásBevittAdat("§ szín mező nem lehet üres");
                if (Szín_nagyobb.Text.Trim() == "") throw new HibásBevittAdat("> szín mező nem lehet üres");

                if (!int.TryParse(SzínPsz.Text, out int színpsz)) throw new HibásBevittAdat("Pályaszám szín mezőnek számnak kell lennie.");
                if (!int.TryParse(SzínPszGar.Text, out int színpszgar)) throw new HibásBevittAdat("Pályaszám gar szín mezőnek számnak kell lennie.");
                if (!int.TryParse(SzínISTűrés.Text, out int színistűrés)) throw new HibásBevittAdat("IS tűrés szín mezőnek számnak kell lennie.");
                if (!int.TryParse(SzínIS.Text, out int színis)) throw new HibásBevittAdat("Is szín mezőnek számnak kell lennie.");
                if (!int.TryParse(SzínP.Text, out int színp)) throw new HibásBevittAdat("P szín mezőnek számnak kell lennie.");
                if (!int.TryParse(SzínSzombat.Text, out int színszombat)) throw new HibásBevittAdat("Szombat szín mezőnek számnak kell lennie.");
                if (!int.TryParse(SzínVasárnap.Text, out int színvasárnap)) throw new HibásBevittAdat("Vasárnap szín mezőnek számnak kell lennie.");
                if (!int.TryParse(Szín_E.Text, out int szín_e)) throw new HibásBevittAdat("Eszterga szín mezőnek számnak kell lennie.");
                if (!int.TryParse(Szín_dollár.Text, out int szín_dollár)) throw new HibásBevittAdat("$ szín mezőnek számnak kell lennie.");
                if (!int.TryParse(Szín_Kukac.Text, out int szín_kukac)) throw new HibásBevittAdat("@ szín mezőnek számnak kell lennie.");
                if (!int.TryParse(Szín_Hasteg.Text, out int szín_hasteg)) throw new HibásBevittAdat("# szín mezőnek számnak kell lennie.");
                if (!int.TryParse(Szín_jog.Text, out int szín_jog)) throw new HibásBevittAdat("§ szín mezőnek számnak kell lennie.");
                if (!int.TryParse(Szín_nagyobb.Text, out int szín_nagyobb)) throw new HibásBevittAdat("> szín mezőnek számnak kell lennie.");

                if (Színtelephely.Text.Trim() == "") throw new HibásBevittAdat("Telephely mező nem lehet üres.");

                AdatokSzín = KézSzín.Lista_Adatok();
                Adat_CAF_Szinezés Elem = (from a in AdatokSzín
                                          where a.Telephely == Színtelephely.Text.Trim()
                                          select a).FirstOrDefault();

                Adat_CAF_Szinezés ADAT = new Adat_CAF_Szinezés(
                                    Színtelephely.Text.Trim(),
                                    színpsz,
                                    színpszgar,
                                    színistűrés,
                                    színis,
                                    színp,
                                    színszombat,
                                    színvasárnap,
                                    szín_e, szín_dollár, szín_kukac, szín_hasteg, szín_jog, szín_nagyobb);
                if (Elem == null)
                    KézSzín.Rögzítés(ADAT);
                else
                    KézSzín.Módosítás(ADAT);
                Szín_tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Karb_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Színtelephely.Text.Trim() == "") return;

                AdatokSzín = KézSzín.Lista_Adatok();
                Adat_CAF_Szinezés Elem = (from a in AdatokSzín
                                          where a.Telephely == Színtelephely.Text.Trim()
                                          select a).FirstOrDefault();

                if (Elem != null) KézSzín.Törlés(Színtelephely.Text.Trim());

                Szín_tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Szín_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Színtelephely.Text = Szín_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();

            SzínPsz.Text = Szín_Tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[1].Value.ToString()));
            SzínPsz.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            SzínPszGar.Text = Szín_Tábla.Rows[e.RowIndex].Cells[2].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[2].Value.ToString()));
            SzínPszGar.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            SzínISTűrés.Text = Szín_Tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[3].Value.ToString()));
            SzínISTűrés.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            SzínIS.Text = Szín_Tábla.Rows[e.RowIndex].Cells[4].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[4].Value.ToString()));
            SzínIS.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            SzínP.Text = Szín_Tábla.Rows[e.RowIndex].Cells[5].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[5].Value.ToString()));
            SzínP.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            SzínSzombat.Text = Szín_Tábla.Rows[e.RowIndex].Cells[6].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[6].Value.ToString()));
            SzínSzombat.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            SzínVasárnap.Text = Szín_Tábla.Rows[e.RowIndex].Cells[7].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[7].Value.ToString()));
            SzínVasárnap.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            Szín_E.Text = Szín_Tábla.Rows[e.RowIndex].Cells[8].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[8].Value.ToString()));
            Szín_E.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            Szín_dollár.Text = Szín_Tábla.Rows[e.RowIndex].Cells[9].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[9].Value.ToString()));
            Szín_dollár.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            Szín_Kukac.Text = Szín_Tábla.Rows[e.RowIndex].Cells[10].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[10].Value.ToString()));
            Szín_Kukac.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            Szín_Hasteg.Text = Szín_Tábla.Rows[e.RowIndex].Cells[11].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[11].Value.ToString()));
            Szín_Hasteg.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            Szín_jog.Text = Szín_Tábla.Rows[e.RowIndex].Cells[12].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[12].Value.ToString()));
            Szín_jog.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);

            Szín_nagyobb.Text = Szín_Tábla.Rows[e.RowIndex].Cells[13].Value.ToString();
            Szín = MyColor.Szín_váltó(long.Parse(Szín_Tábla.Rows[e.RowIndex].Cells[13].Value.ToString()));
            Szín_nagyobb.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
        }

        private void SzínPsz_Click(object sender, EventArgs e)
        {
            Színmező.Text = 1.ToString();
        }

        private void SzínPszgar_Click(object sender, EventArgs e)
        {
            Színmező.Text = 2.ToString();
        }

        private void SzínIStűrés_Click(object sender, EventArgs e)
        {
            Színmező.Text = 3.ToString();
        }

        private void SzínIS_Click(object sender, EventArgs e)
        {
            Színmező.Text = 4.ToString();
        }

        private void SzínP_Click(object sender, EventArgs e)
        {
            Színmező.Text = 5.ToString();
        }

        private void SzínSzombat_Click(object sender, EventArgs e)
        {
            Színmező.Text = 6.ToString();
        }

        private void SzínVasárnap_Click(object sender, EventArgs e)
        {
            Színmező.Text = 7.ToString();
        }

        private void Szín_E_Click(object sender, EventArgs e)
        {
            Színmező.Text = 8.ToString();
        }

        private void Szín_dollár_Click(object sender, EventArgs e)
        {
            Színmező.Text = 9.ToString();
        }

        private void Szín_Kukac_Click(object sender, EventArgs e)
        {
            Színmező.Text = 10.ToString();
        }

        private void Szín_Hasteg_Click(object sender, EventArgs e)
        {
            Színmező.Text = 11.ToString();
        }

        private void Szín_jog_Click(object sender, EventArgs e)
        {
            Színmező.Text = 12.ToString();
        }

        private void Szín_nagyobb_Click(object sender, EventArgs e)
        {
            Színmező.Text = 13.ToString();
        }
    }
}
