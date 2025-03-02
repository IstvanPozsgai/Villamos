using System;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Jármű_Takarítás
{
    public partial class Jármű_Takarítás_Ütemezés_Segéd2 : Form
    {
        private Ablak_Jármű_takarítás_új AblakTakFő { get; set; }

        readonly Kezelő_Jármű_Takarítás_Vezénylés KézVezénylés = new Kezelő_Jármű_Takarítás_Vezénylés();

        public Esemény_Delegált Esemény;
        long Ütem_szerelvényszám_2 = 0;

        public Jármű_Takarítás_Ütemezés_Segéd2(Form főTakAblak)
        {
            AblakTakFő = főTakAblak as Ablak_Jármű_takarítás_új;
            InitializeComponent();
        }

        public void Kiírja_Kocsi_Másik(int sor)
        {
            try
            {
                T5C5_1.Visible = false;
                T5C5_2.Visible = false;
                T5C5_3.Visible = false;
                T5C5_4.Visible = false;
                T5C5_5.Visible = false;
                T5C5_6.Visible = false;

                Segéd_tábla.Rows.Clear();
                Segéd_tábla.Refresh();
                Segéd_tábla.Visible = false;
                Segéd_tábla.RowCount = 5;
                Segéd_tábla.ColumnCount = 8;

                Segéd_tábla.Rows[0].Cells[1].Value = "J2";
                Segéd_tábla.Rows[1].Cells[1].Value = "J3";
                Segéd_tábla.Rows[2].Cells[1].Value = "J4";
                Segéd_tábla.Rows[3].Cells[1].Value = "J5";
                Segéd_tábla.Rows[4].Cells[1].Value = "J6";

                Segéd_tábla.Columns[2].Visible = false;
                Segéd_tábla.Columns[3].Visible = false;
                Segéd_tábla.Columns[4].Visible = false;
                Segéd_tábla.Columns[5].Visible = false;
                Segéd_tábla.Columns[6].Visible = false;
                Segéd_tábla.Columns[7].Visible = false;

                Segéd_tábla.Visible = true;

                string T5C5Előterv = AblakTakFő.Tábla.Rows[sor].Cells[29].Value?.ToString();
                long SzerelvénySzám = AblakTakFő.Tábla.Rows[sor].Cells[21].Value.ToÉrt_Long();
                string Szerelvény = AblakTakFő.Tábla.Rows[sor].Cells[22].Value?.ToString();
                string Pályaszám = AblakTakFő.Tábla.Rows[sor].Cells[0].Value?.ToString();
                string[] psz = new string[6];

                Kocsi_PSZ_2.Text = AblakTakFő.Tábla.Rows[sor].Cells[0].Value.ToString();
                Ütem_szerelvényszám_2 = SzerelvénySzám;
                Ütem_szerelvény_text2.Text = Szerelvény ?? Pályaszám;
                psz = Ütem_szerelvény_text2.Text.Split('-');

                for (int i = 0; i < psz.Length; i++)
                {
                    if (psz[i].Trim() == "_" || psz[i].Trim() == "")
                        Segéd_tábla.Columns[i + 2].Visible = false;
                    else
                    {
                        Segéd_tábla.Columns[i + 2].Visible = true;
                        // megjelenítjük a T5C5 vezénylést E3,V1
                        switch (i)
                        {
                            case 0: T5C5_1.Visible = true; break;

                            case 1: T5C5_2.Visible = true; break;

                            case 2: T5C5_3.Visible = true; break;

                            case 3: T5C5_4.Visible = true; break;

                            case 4: T5C5_5.Visible = true; break;

                            case 5: T5C5_6.Visible = true; break;
                        }
                    }
                    Segéd_tábla.Columns[i + 2].HeaderText = psz[i].Trim();
                }
                // megkeressük a pályaszámot a táblázatban
                for (int i = 0; i < psz.Length; i++)
                {
                    // ha nincs pályaszám akkor kilépünk
                    if (psz[i].Trim() == "_" || psz[i].Trim() == "")
                        break;

                    for (int j = 0; j < AblakTakFő.Tábla.RowCount; j++)
                    {
                        if (AblakTakFő.Tábla.Rows[j].Cells[0].Value.ToStrTrim() == psz[i].Trim())
                        {
                            Segéd_tábla.Rows[0].Cells[i + 2].Value = AblakTakFő.Tábla.Rows[j].Cells[4].Value;
                            Segéd_tábla.Rows[1].Cells[i + 2].Value = AblakTakFő.Tábla.Rows[j].Cells[7].Value;
                            Segéd_tábla.Rows[2].Cells[i + 2].Value = AblakTakFő.Tábla.Rows[j].Cells[10].Value;
                            Segéd_tábla.Rows[3].Cells[i + 2].Value = AblakTakFő.Tábla.Rows[j].Cells[13].Value;
                            Segéd_tábla.Rows[4].Cells[i + 2].Value = AblakTakFő.Tábla.Rows[j].Cells[16].Value;

                            T5C5Előterv = AblakTakFő.Tábla.Rows[j].Cells[29].Value.ToString();

                            switch (i)
                            {
                                case 0: T5C5_1.Text = T5C5Előterv ?? ""; break;

                                case 1: T5C5_2.Text = T5C5Előterv ?? ""; break;

                                case 2: T5C5_3.Text = T5C5Előterv ?? ""; break;

                                case 3: T5C5_4.Text = T5C5Előterv ?? ""; break;

                                case 4: T5C5_5.Text = T5C5Előterv ?? ""; break;

                                case 5: T5C5_6.Text = T5C5Előterv ?? ""; break;
                            }
                        }
                    }
                    // kiírjuk a pipákat
                    //Hiba

                    if (AblakTakFő.Tábla.Rows[sor].Cells[5].Value != null)
                    {
                        if (AblakTakFő.Tábla.Rows[sor].Cells[5].Value.ToStrTrim() == "")
                            Segéd_tábla.Rows[0].Cells[0].Value = false;
                        else
                            Segéd_tábla.Rows[0].Cells[0].Value = true;
                    }
                    if (AblakTakFő.Tábla.Rows[sor].Cells[8].Value != null)
                    {
                        if (AblakTakFő.Tábla.Rows[sor].Cells[8].Value.ToStrTrim() == "")
                            Segéd_tábla.Rows[1].Cells[0].Value = false;
                        else
                            Segéd_tábla.Rows[1].Cells[0].Value = true;
                    }
                    if (AblakTakFő.Tábla.Rows[sor].Cells[11].Value != null)
                    {
                        if (AblakTakFő.Tábla.Rows[sor].Cells[11].Value.ToStrTrim() == "")
                            Segéd_tábla.Rows[2].Cells[0].Value = false;
                        else
                            Segéd_tábla.Rows[2].Cells[0].Value = true;
                    }
                    if (AblakTakFő.Tábla.Rows[sor].Cells[14].Value != null)
                    {
                        if (AblakTakFő.Tábla.Rows[sor].Cells[14].Value.ToStrTrim() == "")
                            Segéd_tábla.Rows[3].Cells[0].Value = false;
                        else
                            Segéd_tábla.Rows[3].Cells[0].Value = true;
                    }
                    if (AblakTakFő.Tábla.Rows[sor].Cells[17].Value != null)
                    {
                        if (AblakTakFő.Tábla.Rows[sor].Cells[17].Value.ToStrTrim() == "")
                            Segéd_tábla.Rows[4].Cells[0].Value = false;
                        else
                            Segéd_tábla.Rows[4].Cells[0].Value = true;
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

        private void Ütem_töröl_segéd2(string pályaszám, string takarításfajta)
        {
            try
            {
                Adat_Jármű_Takarítás_Vezénylés ADAT = new Adat_Jármű_Takarítás_Vezénylés(
                                           0,
                                           pályaszám,
                                           AblakTakFő.Dátum.Value,
                                           takarításfajta,
                                           0,
                                           0);
                KézVezénylés.Törlés(AblakTakFő.Cmbtelephely.Text.Trim(), AblakTakFő.Dátum.Value.Year, ADAT);
                AblakTakFő.Ütem_Tábla_törlés(pályaszám.Trim(), takarításfajta);
            }
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

        private void Ütem_Töröl2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] pályaszám = new string[6];
                for (int i = 0; i < 6; i++)
                    pályaszám[i] = "";

                pályaszám = Ütem_szerelvény_text2.Text.Split('-');

                for (int i = 0; i < pályaszám.Length; i++)
                {
                    Ütem_töröl_segéd2(pályaszám[i], "J2");
                    Ütem_töröl_segéd2(pályaszám[i], "J3");
                    Ütem_töröl_segéd2(pályaszám[i], "J4");
                    Ütem_töröl_segéd2(pályaszám[i], "J5");
                    Ütem_töröl_segéd2(pályaszám[i], "J6");
                }
                AblakTakFő.Ütemezettkocsik();
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

        private void Kedvenc_2_Click(object sender, EventArgs e)
        {
            Esemény?.Invoke();
        }

        private void Ütem_rögzít_segéd(string pályaszám, string takarításfajta, long szerelvényszám)
        {
            try
            {
                Adat_Jármű_Takarítás_Vezénylés ADAT = new Adat_Jármű_Takarítás_Vezénylés(
                                                   0,
                                                   pályaszám,
                                                   AblakTakFő.Dátum.Value,
                                                   takarításfajta,
                                                   szerelvényszám,
                                                   0);
                KézVezénylés.Döntés(AblakTakFő.Cmbtelephely.Text.Trim(), AblakTakFő.Dátum.Value.Year, ADAT);
                AblakTakFő.Ütem_tábla_Rögzítés(pályaszám.Trim(), takarításfajta);
            }
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

        private void Ütem_Rögzít2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] pályaszám = new string[6];
                for (int i = 0; i < 6; i++)
                    pályaszám[i] = "";

                if (Ütem_szerelvény2.Checked)
                    pályaszám = Ütem_szerelvény_text2.Text.Split('-');
                else
                    pályaszám[0] = Kocsi_PSZ_2.Text.Trim();

                for (int i = 0; i < pályaszám.Count(); i++)
                {

                    if (pályaszám[i].ToStrTrim() == "") break;
                    if (Segéd_tábla.Rows[0].Cells[0].Value != null && Segéd_tábla.Rows[0].Cells[0].Value.ToÉrt_Bool()) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J2", Ütem_szerelvényszám_2);
                    if (Segéd_tábla.Rows[1].Cells[0].Value != null && Segéd_tábla.Rows[1].Cells[0].Value.ToÉrt_Bool()) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J3", Ütem_szerelvényszám_2);
                    if (Segéd_tábla.Rows[2].Cells[0].Value != null && Segéd_tábla.Rows[2].Cells[0].Value.ToÉrt_Bool()) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J4", Ütem_szerelvényszám_2);
                    if (Segéd_tábla.Rows[3].Cells[0].Value != null && Segéd_tábla.Rows[3].Cells[0].Value.ToÉrt_Bool()) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J5", Ütem_szerelvényszám_2);
                    if (Segéd_tábla.Rows[4].Cells[0].Value != null && Segéd_tábla.Rows[4].Cells[0].Value.ToÉrt_Bool()) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J6", Ütem_szerelvényszám_2);
                    AblakTakFő.Ütemezettkocsik();
                }
                AblakTakFő.Ütemezettkocsik();

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

        private void Jármű_Takarítás_Ütemezés_Segéd2_Load(object sender, EventArgs e)
        {

        }
    }
}
