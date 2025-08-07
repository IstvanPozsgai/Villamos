using System;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Jármű_Takarítás
{
    public partial class Jármű_Takarítás_Ütemezés_Segéd1 : Form
    {
        private Ablak_Jármű_takarítás_új AblakTakFő { get; set; }

        readonly Kezelő_Jármű_Takarítás_Vezénylés KézVezénylés = new Kezelő_Jármű_Takarítás_Vezénylés();

        public Esemény_Delegált Esemény;
        long Ütem_szerelvényszám = 0;

        public Jármű_Takarítás_Ütemezés_Segéd1(Form főTakAblak)
        {
            AblakTakFő = főTakAblak as Ablak_Jármű_takarítás_új;
            InitializeComponent();
            Start();
        }

        public Jármű_Takarítás_Ütemezés_Segéd1()
        {
            InitializeComponent();
        }

        private void Start()
        {
            GombLathatosagKezelo.Beallit(this);
            //Jogosultságkiosztás();
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            Ütem_Rögzít.Enabled = false;
            Ütem_Töröl.Enabled = false;

            melyikelem = 181;
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Ütem_Rögzít.Enabled = true;
                Ütem_Töröl.Enabled = true;
            }
        }


        private void Jármű_Takarítás_Ütemezés_Segéd1_Load(object sender, EventArgs e)
        {

        }

        public void Kiírja_Kocsi(int sor)
        {
            try
            {
                Kocsi_PSZ_1.Text = AblakTakFő.Tábla.Rows[sor].Cells[0].Value.ToStrTrim();
                Ütem_szerelvényszám = AblakTakFő.Tábla.Rows[sor].Cells[21].Value == null ? 0 : AblakTakFő.Tábla.Rows[sor].Cells[21].Value.ToÉrt_Long();

                Ütem_szerelvény_text.Text = AblakTakFő.Tábla.Rows[sor].Cells[22].Value == null ? "0" : AblakTakFő.Tábla.Rows[sor].Cells[22].Value.ToStrTrim();
                Ütem_szerelvény.Checked = false;
                Ütem_J2_nap.Text = AblakTakFő.Tábla.Rows[sor].Cells[4].Value == null ? "" : AblakTakFő.Tábla.Rows[sor].Cells[4].Value.ToStrTrim();
                Ütem_J3_nap.Text = AblakTakFő.Tábla.Rows[sor].Cells[7].Value == null ? "" : AblakTakFő.Tábla.Rows[sor].Cells[7].Value.ToStrTrim();
                Ütem_J4_nap.Text = AblakTakFő.Tábla.Rows[sor].Cells[10].Value == null ? "" : AblakTakFő.Tábla.Rows[sor].Cells[10].Value.ToStrTrim();
                Ütem_J5_nap.Text = AblakTakFő.Tábla.Rows[sor].Cells[13].Value == null ? "" : AblakTakFő.Tábla.Rows[sor].Cells[13].Value.ToStrTrim();
                Ütem_J6_nap.Text = AblakTakFő.Tábla.Rows[sor].Cells[16].Value == null ? "" : AblakTakFő.Tábla.Rows[sor].Cells[16].Value.ToStrTrim();
                if (AblakTakFő.Tábla.Rows[sor].Cells[5].Value != null)
                {
                    if (AblakTakFő.Tábla.Rows[sor].Cells[5].Value.ToStrTrim() == "") Ütem_J2_kell.Checked = false;
                    else Ütem_J2_kell.Checked = true;
                }
                if (AblakTakFő.Tábla.Rows[sor].Cells[8].Value != null)
                {
                    if (AblakTakFő.Tábla.Rows[sor].Cells[8].Value.ToStrTrim() == "") Ütem_J3_kell.Checked = false;
                    else Ütem_J3_kell.Checked = true;
                }
                if (AblakTakFő.Tábla.Rows[sor].Cells[11].Value != null)
                {
                    if (AblakTakFő.Tábla.Rows[sor].Cells[11].Value.ToStrTrim() == "") Ütem_J4_kell.Checked = false;
                    else Ütem_J4_kell.Checked = true;
                }
                if (AblakTakFő.Tábla.Rows[sor].Cells[14].Value != null)
                {
                    if (AblakTakFő.Tábla.Rows[sor].Cells[14].Value.ToStrTrim() == "") Ütem_J5_kell.Checked = false;
                    else Ütem_J5_kell.Checked = true;
                }
                if (AblakTakFő.Tábla.Rows[sor].Cells[17].Value != null)
                {
                    if (AblakTakFő.Tábla.Rows[sor].Cells[17].Value.ToStrTrim() == "") Ütem_J6_kell.Checked = false;
                    else Ütem_J6_kell.Checked = true;
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

        private void Ütem_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                string[] pályaszám = new string[6];
                for (int i = 0; i < 6; i++)
                    pályaszám[i] = "";

                if (Ütem_szerelvény.Checked == true)
                    pályaszám = Ütem_szerelvény_text.Text.Split('-');
                else
                    pályaszám[0] = Kocsi_PSZ_1.Text.Trim();

                for (int i = 0; i < 6; i++)
                {
                    if (i >= pályaszám.Length) break;
                    if (pályaszám[i].ToStrTrim() == "" | pályaszám[i].ToStrTrim() == "_") break;

                    Ütem_töröl_segéd(pályaszám[i], "J2");
                    Ütem_töröl_segéd(pályaszám[i], "J3");
                    Ütem_töröl_segéd(pályaszám[i], "J4");
                    Ütem_töröl_segéd(pályaszám[i], "J5");
                    Ütem_töröl_segéd(pályaszám[i], "J6");
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

        private void Ütem_töröl_segéd(string pályaszám, string takarításfajta)
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

        private void Kedvenc_1_Click(object sender, EventArgs e)
        {
            Esemény?.Invoke();
        }

        private void Ütem_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{AblakTakFő.Cmbtelephely.Text.Trim()}\Adatok\Takarítás\Takarítás_{AblakTakFő.Dátum.Value:yyyy}.mdb";
                if (!Exists(hely)) return;

                string[] pályaszám = new string[6];
                for (int i = 0; i < 6; i++)
                    pályaszám[i] = "";

                if (Ütem_szerelvény.Checked)
                    pályaszám = Ütem_szerelvény_text.Text.Split('-');
                else
                    pályaszám[0] = Kocsi_PSZ_1.Text.Trim();

                for (int i = 0; i < pályaszám.Length; i++)
                {
                    if (pályaszám[i].ToStrTrim() == "" | pályaszám[i].ToStrTrim() == "_") break;

                    if (Ütem_J2_kell.Checked) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J2", Ütem_szerelvényszám);
                    if (Ütem_J3_kell.Checked) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J3", Ütem_szerelvényszám);
                    if (Ütem_J4_kell.Checked) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J4", Ütem_szerelvényszám);
                    if (Ütem_J5_kell.Checked) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J5", Ütem_szerelvényszám);
                    if (Ütem_J6_kell.Checked) Ütem_rögzít_segéd(pályaszám[i].ToStrTrim(), "J6", Ütem_szerelvényszám);
                    AblakTakFő.Ütemezettkocsik();
                    MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                AblakTakFő.Ütemezettkocsik();


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
    }
}
