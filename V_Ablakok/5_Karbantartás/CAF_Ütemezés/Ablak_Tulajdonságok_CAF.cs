using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok.CAF_Ütemezés;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyCaf = Villamos.Villamos_Ablakok.CAF_Ütemezés.CAF_Közös_Eljárások;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Tulajdonságok_CAF
    {
        CAF_Segéd_Adat Posta_Segéd = null;
        //Kezelő_Ciklus Kéz_ciklus = new Kezelő_Ciklus();
        //List<Adat_Ciklus> Adat_ciklus_km = null;
        //List<Adat_Ciklus> Adat_ciklus_idő = null;
        Kezelő_CAF_Adatok KézCAF = new Kezelő_CAF_Adatok();

        List<Adat_CAF_Adatok> AdatokCaf = new List<Adat_CAF_Adatok>();

        int SOR = -1;
        int OSZLOP = -1;

        public Ablak_Tulajdonságok_CAF()
        {
            InitializeComponent();
            Start();
        }




        #region Alap
        void Start()
        {
            // létrehozzuk a  könyvtárat
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\CAF";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.CAFtábla(hely);
            Telephelyekfeltöltése();

        }


        private void Ablak_Tulajdonságok_CAF_Load(object sender, EventArgs e)
        {
            try
            {
                ELő_Pályaszámokfeltöltése();

                Jogosultságkiosztás();

                DateTime elsődát = MyF.Hónap_elsőnapja(DateTime.Today).AddDays(-10);
                DateTime végdát = MyF.Hónap_elsőnapja(DateTime.Today).AddDays(40);


                Elő_Dátumig.Value = végdát;
                Elő_Dátumtól.Value = elsődát;
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


        private void Ablak_Tulajdonságok_CAF_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Szín?.Close();
            Új_Ablak_Caf_Lista?.Close();
            Új_Ablak_CAF_Segéd?.Close();
            Új_Ablak_CAF_Alapadat?.Close();
            Új_Ablak_CAF_Eszterga_Beállítás?.Close();
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false

                Előtervet_készít.Enabled = false;
                ELő_törlés.Enabled = false;
                Elő_ütemez.Enabled = false;

                Elő_Lehívás.Enabled = false;

                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Előtervet_készít.Visible = true;
                    ELő_törlés.Visible = true;

                    Elő_ütemez.Visible = true;
                    Elő_Lehívás.Visible = true;
                }
                else
                {
                    Előtervet_készít.Visible = false;
                    ELő_törlés.Visible = false;
                    Elő_ütemez.Visible = false;
                    Elő_Lehívás.Visible = false;

                }

                melyikelem = 116;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Előtervet_készít.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    ELő_törlés.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Elő_ütemez.Enabled = true;
                }

                melyikelem = 117;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Elő_Lehívás.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                }
                // módosítás 3 
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


        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\CAF.html";
            MyE.Megnyitás(hely);
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT DISTINCT Állománytábla.üzem FROM Állománytábla  WHERE Állománytábla.típus Like 'CAF%'";

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<string> Adatok = Kéz.List_Jármű_Telephely(hely, jelszó, szöveg);
                Cmbtelephely.Items.Clear();


                foreach (string rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Trim());

                if (Program.PostásTelephely == "Főmérnökség")
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
                else if (Cmbtelephely.Text.Contains(Program.PostásTelephely))
                { Cmbtelephely.Text = Program.PostásTelephely; }
                else { Cmbtelephely.Text = ""; }

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


        private void ELő_Pályaszámokfeltöltése()
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
            string jelszó = "CzabalayL";
            string szöveg = "SELECT * FROM alap WHERE törölt=false ORDER BY azonosító";
            Elő_pályaszám.Items.Clear();
            Elő_pályaszám.BeginUpdate();
            Elő_pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
            Elő_pályaszám.EndUpdate();
            Elő_pályaszám.Refresh();
        }


        #endregion


        #region Táblázat Listázás    


        private void Tábla_frissítés_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                Előterv_listázás();

                if (Elő_Dátumtól.Value > Elő_Dátumig.Value)
                    throw new HibásBevittAdat("A dátum intervallum beállítás hibás.");
                if (Elő_pályaszám.CheckedItems.Count <= 0)
                    throw new HibásBevittAdat("Nincs kijelölve egy pályaszám sem.");

                Elő_pályaszám.Height = 25;
                Holtart.Ki();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Holtart.Ki();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Előterv_listázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                if (!Exists(hely)) return;


                int oszlop = 3;
                string előző_pályaszám = "";

                {
                    Tábla_elő.Rows.Clear();
                    Tábla_elő.Columns.Clear();
                    Tábla_elő.Refresh();
                    Tábla_elő.Visible = false;
                    Tábla_elő.ColumnCount = 3;

                    // fejléc elkészítése
                    Tábla_elő.Columns[0].HeaderText = "Dátum";
                    Tábla_elő.Columns[0].Width = 80;
                    Tábla_elő.Columns[1].HeaderText = "IS".Trim();
                    Tábla_elő.Columns[1].Width = 30;
                    Tábla_elő.Columns[2].HeaderText = "P".Trim();
                    Tábla_elő.Columns[2].Width = 30;
                    Tábla_elő.Columns[2].Frozen = true;

                    // elkészítjük a dátumokat
                    DateTime ideigdát = Elő_Dátumtól.Value;
                    int i = 0;


                    while (Elő_Dátumig.Value >= ideigdát)
                    {
                        Tábla_elő.RowCount++;
                        i = Tábla_elő.RowCount - 1;

                        Tábla_elő.Rows[i].Cells[0].Value = ideigdát.ToString("yyyy.MM.dd");
                        ideigdát = ideigdát.AddDays(1);

                        Holtart.Lép();
                    }

                    // dátumok színezése
                    string jelszómunka = "katalin";
                    string helyelv = $@"{Application.StartupPath}\Főmérnökség\adatok\{Elő_Dátumtól.Value.Year}\munkaidőnaptár.mdb";
                    if (Exists(helyelv))
                        Munkaidő_naptár(helyelv, jelszómunka);

                    // ha átnyúlik a következő évre 
                    helyelv = $@"{Application.StartupPath}\Főmérnökség\adatok\{Elő_Dátumtól.Value.AddYears(1).Year}\munkaidőnaptár.mdb";
                    if (Exists(helyelv) == true)
                        Munkaidő_naptár(helyelv, jelszómunka);



                    string szöveg = "SELECT * FROM Adatok WHERE ";
                    if (Elő_Km.Checked == true)
                        szöveg += " IDŐvKM=2 AND ";
                    if (Elő_Idő.Checked == true)
                        szöveg += " IDŐvKM=1 AND ";
                    szöveg += $" ([Dátum]>=#{Elő_Dátumtól.Value:MM-dd-yyyy}#";
                    szöveg += $" AND [Dátum]<=#{Elő_Dátumig.Value:MM-dd-yyyy}#)";
                    if (Elő_törölt.Checked == false)
                        szöveg += " AND státus<9";
                    szöveg += "  ORDER BY azonosító,dátum asc, IDŐvKM desc  ";


                    i = 0;
                    int hiba = 0;
                    string ideig;
                    int j = 0;



                    Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                    List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                    foreach (Adat_CAF_Adatok rekord in Adatok)
                    {
                        // ha kisebb a listában lévő szám akkor léptetjük 
                        while (String.Compare(Elő_pályaszám.Items[j].ToString().Trim(), rekord.Azonosító.Trim()) < 0)
                        {
                            j += 1;
                            if (i == Tábla_elő.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }
                        }

                        if (Elő_pályaszám.GetItemChecked(j) && Elő_pályaszám.Items[j].ToString().Trim() == rekord.Azonosító.Trim())
                        {
                            if (előző_pályaszám.Trim() == "")
                            {
                                // első adat 
                                előző_pályaszám = rekord.Azonosító.Trim();
                                Tábla_elő.ColumnCount += 1;
                                Tábla_elő.Columns[oszlop].HeaderText = rekord.Azonosító.Trim();
                                Tábla_elő.Columns[oszlop].Width = 45;
                            }
                            if ((előző_pályaszám.Trim()) != rekord.Azonosító.Trim())
                            {
                                // ha új pályaszám van
                                i = 0;
                                oszlop += 1;
                                Tábla_elő.ColumnCount += 1;
                                Tábla_elő.Columns[oszlop].HeaderText = rekord.Azonosító.Trim();
                                Tábla_elő.Columns[oszlop].Width = 45;
                                előző_pályaszám = rekord.Azonosító.Trim();
                            }

                            // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                            while (DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString()) < rekord.Dátum)
                            {
                                i += 1;
                                if (i == Tábla_elő.Rows.Count)
                                {
                                    hiba = 1;
                                    break;
                                }
                            }

                            if (hiba == 1)
                                break;

                            if (DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString()) == rekord.Dátum)
                            {
                                // ha egyforma akkor kiírjuk
                                ideig = rekord.Vizsgálat.Trim();
                                switch (rekord.IDŐvKM)
                                {
                                    case 0:
                                        {
                                            break;
                                        }

                                    case 1:
                                        {
                                            ideig += "-" + rekord.IDŐ_Sorszám;
                                            break;
                                        }
                                    case 2:
                                        {
                                            ideig += "-" + rekord.IDŐ_Sorszám;
                                            break;
                                        }

                                }
                                if (Tábla_elő.Rows[i].Cells[oszlop].Value == null || Tábla_elő.Rows[i].Cells[oszlop].Value.ToString().Trim() == "")
                                {
                                    Tábla_elő.Rows[i].Cells[oszlop].Value = ideig;
                                }
                                else
                                {
                                    Tábla_elő.Rows[i].Cells[oszlop].Value = Tábla_elő.Rows[i].Cells[oszlop].Value.ToString().Trim() + "_" + ideig;
                                }

                                Cella_formátum(i, oszlop, rekord.Státus);
                            }

                            if (i == Tábla_elő.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }

                            Holtart.Lép();
                        }

                    }

                    // összesítjük
                    IS_P_összesítés();
                    Tábla_elő.Refresh();
                    Tábla_elő.Visible = true;

                }



            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Holtart.Ki();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Munkaidő_naptár(string hely_a, string jelszó_a)
        {
            try
            {
                string szöveg = " SELECT * from naptár ";
                szöveg += " WHERE (dátum>=#" + Elő_Dátumtól.Value.ToString("M-d-yy");
                szöveg += "# And dátum<=#" + Elő_Dátumig.Value.ToString("M-d-yy") + "#)";

                Kezelő_Váltós_Naptár kéz = new Kezelő_Váltós_Naptár();
                List<Adat_Váltós_Naptár> Adatok = kéz.Lista_Adatok(hely_a, jelszó_a, szöveg);

                Holtart.Be();
                int i = 0;
                foreach (Adat_Váltós_Naptár rekord in Adatok)
                {
                    bool exitDo = false;
                    while (rekord.Dátum > DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString()))
                    {
                        i += 1;
                        if (Tábla_elő.Rows.Count - 1 <= i)
                        {
                            exitDo = true;
                            break;
                        }
                    }

                    if (exitDo)
                    {
                        break;
                    }
                    if (rekord.Dátum == DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString()))
                    {
                        Tábla_elő.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        switch (rekord.Nap)
                        {
                            case "P":
                                {
                                    Tábla_elő.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                                    break;
                                }
                            case "V":
                                {
                                    Tábla_elő.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;
                                    break;
                                }
                            case "Ü":
                                {
                                    Tábla_elő.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                                    break;
                                }
                        }
                    }
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


        private void IS_P_összesítés()
        {
            try
            {
                int isdb;
                int pdb;
                {
                    for (int j = 0; j < Tábla_elő.Rows.Count; j++)
                    {
                        isdb = 0;
                        pdb = 0;
                        for (int i = 3; i < Tábla_elő.Columns.Count; i++)
                        {
                            if (Tábla_elő.Rows[j].Cells[i].Value != null)
                            {
                                if (Tábla_elő.Rows[j].Cells[i].Value.ToString().Trim().Contains("IS") && !Tábla_elő.Rows[j].Cells[i].Value.ToString().Trim().Contains("X"))
                                {
                                    isdb += 1;
                                }
                                if (Tábla_elő.Rows[j].Cells[i].Value.ToString().Trim().Contains("P") && !Tábla_elő.Rows[j].Cells[i].Value.ToString().Trim().Contains("X"))
                                {
                                    pdb += 1;
                                }
                            }
                        }
                        Tábla_elő.Rows[j].Cells[1].Value = isdb;
                        Tábla_elő.Rows[j].Cells[2].Value = pdb;
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


        private void IS_P_összesítésCSakEgySor()
        {
            try
            {
                int isdb = 0;
                int pdb = 0;

                for (int i = 3; i < Tábla_elő.Columns.Count; i++)
                {
                    if (Tábla_elő.Rows[SOR].Cells[i].Value != null)
                    {
                        if (Tábla_elő.Rows[SOR].Cells[i].Value.ToStrTrim().Contains("IS") && !Tábla_elő.Rows[SOR].Cells[i].Value.ToStrTrim().Contains("X"))
                        {
                            isdb += 1;
                        }
                        if (Tábla_elő.Rows[SOR].Cells[i].Value.ToStrTrim().Contains("P") && !Tábla_elő.Rows[SOR].Cells[i].Value.ToStrTrim().Contains("X"))
                        {
                            pdb += 1;
                        }
                    }
                }
                Tábla_elő.Rows[SOR].Cells[1].Value = isdb;
                Tábla_elő.Rows[SOR].Cells[2].Value = pdb;


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

        private void Cella_formátum(int sor_a, int oszlop_a, int státus_a)
        {
            try
            {
                {
                    switch (státus_a)
                    {
                        case 0:
                            {
                                Tábla_elő.Rows[sor_a].Cells[oszlop_a].Style.Font = new Font("Microsoft Sans Serif", 10f);
                                break;
                            }
                        case 2:
                            {
                                Tábla_elő.Rows[sor_a].Cells[oszlop_a].Style.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Italic);
                                break;
                            }
                        case 4:
                            {
                                Tábla_elő.Rows[sor_a].Cells[oszlop_a].Style.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Underline);
                                break;
                            }
                        case 6:
                            {
                                Tábla_elő.Rows[sor_a].Cells[oszlop_a].Style.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);
                                break;
                            }
                        case 9:
                            {
                                Tábla_elő.Rows[sor_a].Cells[oszlop_a].Style.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Strikeout);
                                Tábla_elő.Rows[sor_a].Cells[oszlop_a].Value += "X";
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



        private void Tábla_elő_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex < 3)
                    return;

                SOR = e.RowIndex;
                OSZLOP = e.ColumnIndex;
                DateTime dátum = DateTime.TryParse(Tábla_elő.Rows[e.RowIndex].Cells[0].Value.ToString(), out dátum) ? dátum : new DateTime(1900, 1, 1);
                string pályaszám = Tábla_elő.Columns[e.ColumnIndex].HeaderText.Trim();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = $"SELECT * FROM adatok where azonosító='{pályaszám}' AND dátum=#{dátum:MM-dd-yyyy}# AND Státus<9";
                Kezelő_CAF_Adatok Kéz = new Kezelő_CAF_Adatok();
                Adat_CAF_Adatok rekord = Kéz.Egy_Adat(hely, jelszó, szöveg);
                double sorszám = 0;
                if (rekord != null) sorszám = rekord.Id;

                Posta_Segéd = new CAF_Segéd_Adat(pályaszám, dátum, sorszám);
                if (Új_Ablak_CAF_Segéd != null)
                    SegédAblakKezelés();
                else
                    RészletesKezelés();
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


        #region Tábla Rögzítés Után újraírja

        void Tábla_Frissítés()
        {
            Tábla_oszlop_újraírás();
            Tábla_Sor_újraÍrás();
        }


        private void Tábla_Sor_újraÍrás()
        {
            // összesítjük
            if (SOR == -1 || OSZLOP == -1)
                return;

            IS_P_összesítésCSakEgySor();

        }


        private void Tábla_oszlop_újraírás()
        {
            try
            {
                if (SOR == -1 || OSZLOP == -1)
                    return;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";

                // kiürítjük az előzményt
                for (int sor = 0; sor < Tábla_elő.Rows.Count; sor++)
                    Tábla_elő.Rows[sor].Cells[OSZLOP].Value = "";

                string szöveg = "SELECT * FROM Adatok WHERE ";
                if (Elő_Km.Checked == true)
                    szöveg += " IDŐvKM=2 AND ";
                if (Elő_Idő.Checked == true)
                    szöveg += " IDŐvKM=1 AND ";
                szöveg += " (([Dátum]>=#" + Elő_Dátumtól.Value.ToString("MM-dd-yyyy") + "#)";
                szöveg += " AND ([Dátum]<=#" + Elő_Dátumig.Value.ToString("MM-dd-yyyy") + "#))";
                if (Elő_törölt.Checked == false)
                    szöveg += " AND státus<9 ";
                szöveg += "  AND azonosító='" + Tábla_elő.Columns[OSZLOP].HeaderText.Trim() + "'";
                szöveg += "  ORDER BY dátum ";

                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i = 0;

                foreach (Adat_CAF_Adatok rekord in Adatok)
                {

                    while (i < Tábla_elő.RowCount)
                    {
                        if (DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString()) >= rekord.Dátum)
                        {
                            break;
                        }
                        else
                        {
                            i += 1;
                        }
                    }
                    if (DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString()).ToString("yyyy.MM.dd") == rekord.Dátum.ToString("yyyy.MM.dd"))
                    {
                        // ha egyforma akkor kiírjuk
                        string ideig = rekord.Vizsgálat.Trim();
                        switch (rekord.IDŐvKM)
                        {
                            case 0:
                                {
                                    break;
                                }

                            case 1:
                                {
                                    ideig += "-" + rekord.IDŐ_Sorszám;
                                    break;
                                }
                            case 2:
                                {
                                    ideig += "-" + rekord.KM_Sorszám;
                                    break;
                                }

                        }
                        Tábla_elő.Rows[i].Cells[OSZLOP].Value = ideig;

                        Cella_formátum(i, OSZLOP, rekord.Státus);

                    }
                    if (i >= Tábla_elő.RowCount)
                        break;
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


        #region Alapadatok ablak

        Ablak_CAF_Alapadat Új_Ablak_CAF_Alapadat;
        private void Alap_adatok_Click(object sender, EventArgs e)
        {
            Új_Ablak_CAF_Alapadat?.Close();

            Új_Ablak_CAF_Alapadat = new Ablak_CAF_Alapadat("");
            Új_Ablak_CAF_Alapadat.FormClosed += Ablak_CAF_Alapadat_Closed;
            Új_Ablak_CAF_Alapadat.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_CAF_Alapadat.Show();
        }

        private void Ablak_CAF_Alapadat_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Alapadat = null;
        }

        #endregion


        #region Szín ablak

        Ablak_CAF_Szín Új_Ablak_CAF_Szín;
        private void Színbeállítás_Click(object sender, EventArgs e)
        {
            Új_Ablak_CAF_Szín?.Close();

            Új_Ablak_CAF_Szín = new Ablak_CAF_Szín();
            Új_Ablak_CAF_Szín.FormClosed += Ablak_CAF_Szín_Closed;
            //  Új_Ablak_CAF_Szín.Változás += Terv_lista_elj;
            Új_Ablak_CAF_Szín.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_CAF_Szín.Show();
        }

        private void Ablak_CAF_Szín_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Szín = null;
        }
        #endregion


        #region Listák ablak


        Ablak_Caf_Lista Új_Ablak_Caf_Lista;
        private void Caf_Listák_Click(object sender, EventArgs e)
        {
            Új_Ablak_Caf_Lista?.Close();

            Új_Ablak_Caf_Lista = new Ablak_Caf_Lista(Elő_Dátumig.Value, Elő_Dátumtól.Value);
            Új_Ablak_Caf_Lista.FormClosed += Ablak_Caf_Lista_Closed;

            Új_Ablak_Caf_Lista.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Caf_Lista.Show();
        }

        private void Ablak_Caf_Lista_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Caf_Lista = null;
        }
        #endregion


        #region Segéd ablak


        Ablak_CAF_Segéd Új_Ablak_CAF_Segéd;
        private void Segédablak_hívó_Click(object sender, EventArgs e)
        {
            Posta_Segéd = null;
            SegédAblakKezelés();
        }


        void SegédAblakKezelés()
        {
            Új_Ablak_CAF_Segéd?.Close();

            Új_Ablak_CAF_Segéd = new Ablak_CAF_Segéd(Posta_Segéd, Elő_Dátumig.Value);
            Új_Ablak_CAF_Segéd.FormClosed += Ablak_CAF_Segéd_Closed;
            Új_Ablak_CAF_Segéd.Változás += Tábla_Frissítés;
            Új_Ablak_CAF_Segéd.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_CAF_Segéd.Show();
        }


        private void Ablak_CAF_Segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Segéd = null;
        }
        #endregion


        #region Pályaszám választó Gombok
        private void Elő_Click(object sender, EventArgs e)
        {
            Elő_pályaszám.Height = 500;
        }

        private void Elő_Visszacsuk_Click(object sender, EventArgs e)
        {
            Elő_pályaszám.Height = 25;
        }


        private void Elő_Összeskijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Elő_pályaszám.Items.Count; i++)
                Elő_pályaszám.SetItemChecked(i, true);
            Elő_pályaszám.Height = 25;
        }


        private void Elő_Mindtöröl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Elő_pályaszám.Items.Count; i++)
                Elő_pályaszám.SetItemChecked(i, false);
            Elő_pályaszám.Height = 25;
        }


        private void Elő_tervező_telephely_Click(object sender, EventArgs e)
        {
            try
            {
                // ki jelöli a telephelyhez tartozó kocsikat
                if (Cmbtelephely.Text.Trim() == "")
                    return;

                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                // ha nem telephelyról kérdezzük le akkor minden kocsit kiír


                string szöveg = $"Select * FROM Állománytábla WHERE Üzem='{Cmbtelephely.Text.Trim()}' AND ";
                szöveg += " törölt=0 AND valóstípus Like  '%CAF%' ORDER BY azonosító";

                Kezelő_Jármű kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i = 0;
                int volt = 0;

                foreach (Adat_Jármű rekord in Adatok)
                {

                    // ha a beolvasott nagyobb mint a sorban lévő akkor léptetjük
                    while (String.Compare(Elő_pályaszám.Items[i].ToString().Trim(), rekord.Azonosító.Trim()) < 0)
                    {
                        i += 1;
                        if (Elő_pályaszám.Items.Count == i)
                        {
                            volt = 1;
                            break;
                        }
                    }
                    // ha egyforma akkor bejelöljük
                    if (Elő_pályaszám.Items[i].ToString().Trim() == rekord.Azonosító.Trim())
                    {
                        Elő_pályaszám.SetItemChecked(i, true);
                    }
                    if (volt == 1)
                        break;
                }
                Elő_pályaszám.Height = 25;

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


        #region Sima Excel

        private void Elő_Excel_Click(object sender, EventArgs e)
        {
            if (Tábla_elő.Rows.Count <= 0)
                return;
            string fájlexc;

            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Listázott tartalom mentése Excel fájlba",
                FileName = "CAF_ütemzés_Adatok" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;

            fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
            MyE.EXCELtábla(fájlexc, Tábla_elő, false);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Module_Excel.Megnyitás(fájlexc + ".xlsx");
        }
        #endregion


        #region Előtervet készít



        private void Előtervet_készít_Click(object sender, EventArgs e)
        {
            Elő_pályaszám.Height = 25;
            Holtart.Be();
            Eltervező_IDŐ_gyűjtő();
            Eltervező_KM_gyűjtő();
            Előterv_listázás();
            Holtart.Ki();
            MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void Eltervező_IDŐ_gyűjtő()
        {
            try
            {
                foreach (AdatCombohoz Elem in Elő_pályaszám.CheckedItems)
                {
                    MyCaf.IDŐ_Eltervező_EgyKocsi(Elem.ToString().Trim(), Elő_Dátumig.Value);
                    Holtart.Lép();
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

        private void Eltervező_KM_gyűjtő()
        {
            try
            {
                foreach (AdatCombohoz Elem in Elő_pályaszám.CheckedItems)
                {
                    MyCaf.KM_Eltervező_EgyKocsi(Elem.ToString().Trim(), Elő_Dátumig.Value);
                    Holtart.Lép();
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


        #region Előterv törlés- Ütemez- 
        private void ELő_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Elő_pályaszám.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");
                // táblázat minden elemén végig megyünk
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg;

                CafAdatokListázása();

                Holtart.Be();
                foreach (AdatCombohoz elem in Elő_pályaszám.CheckedItems)
                {
                    Adat_CAF_Adatok AdatCaf = (from a in AdatokCaf
                                               where a.Azonosító == elem.ToStrTrim()
                                               && a.Dátum >= Elő_Dátumtól.Value
                                               && a.Státus == 0
                                               select a).FirstOrDefault();

                    if (AdatCaf != null)
                    {
                        szöveg = "DELETE  FROM adatok ";
                        szöveg += $" WHERE azonosító='{elem}' AND ";
                        szöveg += $" dátum>=#{Elő_Dátumtól.Value:MM-dd-yyyy}# AND  státus=0";
                        MyA.ABtörlés(hely, jelszó, szöveg);
                    }

                    AdatCaf = (from a in AdatokCaf
                               where a.Azonosító == elem.ToStrTrim()
                               && a.Dátum >= Elő_Dátumtól.Value
                               && a.Státus >= 8
                               select a).FirstOrDefault();

                    if (AdatCaf != null)
                    {
                        szöveg = "DELETE  FROM adatok ";
                        szöveg += $" WHERE azonosító='{elem}' AND ";
                        szöveg += $" dátum>=#{Elő_Dátumtól.Value:MM-dd-yyyy}# AND státus>=8";
                        MyA.ABtörlés(hely, jelszó, szöveg);
                    }
                    Holtart.Lép();
                }

                Holtart.Ki();
                Előterv_listázás();
                Elő_pályaszám.Height = 25;
                MessageBox.Show("Az előterv adatok törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Elő_ütemez_Click(object sender, EventArgs e)
        {
            try
            {
                // a listázott vizsgálatok státusát átállítjuk ütemezettre
                if (Elő_pályaszám.SelectedItems.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg;
                Holtart.Be();

                foreach (AdatCombohoz elem in Elő_pályaszám.CheckedItems)
                {
                    Adat_CAF_Adatok AdatCaf = (from a in AdatokCaf
                                               where a.Azonosító == elem.ToStrTrim()
                                               && a.Dátum >= Elő_Dátumtól.Value
                                               && a.Dátum <= Elő_Dátumig.Value
                                               && a.Státus == 0
                                               select a).FirstOrDefault();

                    if (AdatCaf != null)
                    {
                        szöveg = "UPDATE adatok  SET Státus=2 ";
                        szöveg += $" WHERE azonosító='{elem}' AND dátum>=#{Elő_Dátumtól.Value:MM-dd-yyyy}# ";
                        szöveg += $" AND dátum<=#{Elő_Dátumig.Value:MM-dd-yyyy}# AND Státus=0";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
                    Holtart.Lép();
                }
                Holtart.Ki();
                Előterv_listázás();
                Elő_pályaszám.Height = 25;
                MessageBox.Show("Az listázott elemek átütemezése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Elő_Lehívás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return;

                DateTime Dátum_ütem = Elő_Dátumtól.Value.AddDays(0);

                // Módosítjuk a jármű státuszát
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                string helyütem = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";

                if (!Exists(hely)) return;
                string jelszóütem = "CzabalayL";
                string szöveg = $"SELECT * FROM adatok where STÁTUS=2 and [dátum]=#{Dátum_ütem:M-d-yy}# order by  azonosító";


                // megnyitjuk a hibákat
                string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\hiba.mdb";

                // naplózás
                string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló\{DateTime.Now:yyyyMM}hibanapló.mdb";
                if (Exists(helynapló) == false) Adatbázis_Létrehozás.Hibatáblalap(helynapló);

                Holtart.Be();
                CafAdatokListázása();

                Kezelő_Jármű KézJármű = new Kezelő_Jármű();

                string szöveg1;

                string szöveg0 = $"SELECT * FROM állománytábla";
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg0);


                string szöveg2 = $"SELECT * FROM hibatábla";

                Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
                List<Adat_Jármű_hiba> AdatokJárműHiba = KézJárműHiba.Lista_adatok(helyhiba, jelszó, szöveg2);


                // ha van ütemezett kocsi
                foreach (Adat_CAF_Adatok rekordütemez in AdatokCaf)
                {
                    Holtart.Lép();

                    // megnézzük, hogy a telephelyen van-e a kocsi
                    Adat_Jármű Kocsi = (from a in AdatokJármű
                             where a.Azonosító == rekordütemez.Azonosító.Trim()
                             && a.Státus == 2
                             select a).FirstOrDefault();

                    if (Kocsi != null)
                    {
                        // ha telephelyen van a kocsi
                        // hiba leírása
                        szöveg1 = rekordütemez.Vizsgálat.Trim() + "-" + rekordütemez.Id.ToString() + "-" + rekordütemez.Dátum.ToString();

                        // Megnézzük, hogy volt-e már rögzítve ilyen szöveg

                        // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer

                        Adat_Jármű_hiba AdatHiba = (from a in AdatokJárműHiba
                                                    where a.Azonosító == rekordütemez.Azonosító.Trim()
                                                    && a.Hibaleírása.Contains(szöveg1.Trim())
                                                    select a).FirstOrDefault();

                        if (AdatHiba == null)
                        {
                            // hibák számát emeljük és státus állítjuk ha kell
                            Kocsi = (from a in AdatokJármű
                                     where a.Azonosító == rekordütemez.Azonosító.Trim()
                                     select a).FirstOrDefault();

                            if (Kocsi != null)
                            {
                                // rögzítjük a villamos.mdb-be
                                szöveg = "UPDATE állománytábla SET ";
                                szöveg += $" hibák={Kocsi.Hibák + 1},  státus=3 ";
                                szöveg += $" WHERE  [azonosító]='{rekordütemez.Azonosító.Trim()}'";
                                MyA.ABMódosítás(hely, jelszó, szöveg);
                            }

                            // beírjuk a hibákat
                            szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                            szöveg += $"'{Program.PostásTelephely.Trim()}', 3, ";
                            szöveg += $"'{szöveg1.Trim()}', ";
                            szöveg += $"'{DateTime.Now}', false, ";
                            szöveg += $"'{Kocsi.Típus.Trim()}', ";
                            szöveg += $"'{rekordütemez.Azonosító.Trim()}', {Kocsi.Hibák + 1})";
                            MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                            // naplózzuk a hibákat
                            MyA.ABMódosítás(helynapló, jelszó, szöveg);

                            // módosítjuk az ütemezett adatokat is
                            szöveg = $"UPDATE adatok  SET Státus=4  WHERE id={rekordütemez.Id}";
                            MyA.ABMódosítás(helyütem, jelszóütem, szöveg);

                            // Ha lesz naplózás akkor ide kell írni

                        }
                    }
                    Holtart.Lép();
                }
                Holtart.Ki();

                Előterv_listázás();
                Elő_pályaszám.Height = 25;
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


        #region Részletes

        Ablak_CAF_Részletes Új_Ablak_CAF_Részletes;
        private void RészletesKezelés()
        {
            Új_Ablak_CAF_Részletes?.Close();
            Új_Ablak_CAF_Részletes = new Ablak_CAF_Részletes(Posta_Segéd, Elő_Dátumig.Value);
            Új_Ablak_CAF_Részletes.FormClosed += Ablak_CAF_Részletes_Closed;
            Új_Ablak_CAF_Részletes.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_CAF_Részletes.Változás += Tábla_Frissítés;
            Új_Ablak_CAF_Részletes.Show();
        }


        private void Ablak_CAF_Részletes_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Részletes = null;
        }
        #endregion


        #region Excel előterv


        private void Elő_havi_Click(object sender, EventArgs e)
        {
            try
            {
                if (Elő_pályaszám.CheckedItems.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy kocsi sem.");
                Elő_pályaszám.Height = 25;
                Előterv_listázás_excelhez();


                string munkalap = "Munka1";
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";

                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "CAF Ütemterv készítés",
                    FileName = "CAF_tábla_" + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excelt

                MyE.ExcelLétrehozás();
                // *********************************
                // * Tartalom kezdete              *
                // *********************************
                MyE.Munkalap_betű("Calibri", 11);

                DateTime ideigdátum;
                DateTime előzőHónap = new DateTime(1900, 1, 1);
                double szombat = 255;
                double vasárnap = 255;

                Kezelő_CAF_Szinezés kéz = new Kezelő_CAF_Szinezés();
                string szöveg = $"SELECT * FROM  szinezés WHERE Telephely='{Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[3].Value.ToString().Trim()}'";

                Adat_CAF_Szinezés Szín = kéz.Egy_Adat(hely, jelszó, szöveg);
                if (Szín != null)
                {
                    szombat = Szín.Színszombat;
                    vasárnap = Szín.SzínVasárnap;
                }

                // Kiírjuk a dátumokat
                Holtart.Be();
                for (int i = 0; i <= Tábla_elő.Rows.Count - 6; i++)
                {
                    ideigdátum = DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString());
                    MyE.Kiir(ideigdátum.ToString("dd"), MyE.Oszlopnév(i + 2) + "2");
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(i + 2) + ":" + MyE.Oszlopnév(i + 2), 3);

                    if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.Beige) // Pihenőnap
                        MyE.Háttérszín(MyE.Oszlopnév(i + 2) + "2:" + MyE.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), szombat);

                    if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.BurlyWood)                    // vasárnap
                        MyE.Háttérszín(MyE.Oszlopnév(i + 2) + "2:" + MyE.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), vasárnap);

                    if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.IndianRed)                  // ünnep
                        MyE.Háttérszín(MyE.Oszlopnév(i + 2) + "2:" + MyE.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), vasárnap);

                    Holtart.Lép();

                    előzőHónap = DateTime.Parse(Tábla_elő.Rows[0].Cells[0].Value.ToString());
                    int blokkeleje = 2;
                    // hónap nevek kiírása

                    for (int iii = 0; iii < Tábla_elő.Rows.Count - 6; iii++)
                    {
                        if (előzőHónap.ToString("yyyy MMM") != DateTime.Parse(Tábla_elő.Rows[iii].Cells[0].Value.ToString()).ToString("yyyy MMM"))
                        {
                            MyE.Egyesít(munkalap, MyE.Oszlopnév(blokkeleje) + "1:" + MyE.Oszlopnév(iii + 1) + "1");
                            MyE.Kiir(előzőHónap.ToString("yyyy MMM"), MyE.Oszlopnév(blokkeleje) + "1");
                            előzőHónap = DateTime.Parse(Tábla_elő.Rows[iii].Cells[0].Value.ToString());
                            blokkeleje = iii + 2;
                        }
                    }
                    // utolsó hónap
                    DateTime iidát = DateTime.Parse(Tábla_elő.Rows[Tábla_elő.Rows.Count - 6].Cells[0].Value.ToString());
                    if (előzőHónap.ToString("yyyy MMM") == DateTime.Parse(Tábla_elő.Rows[Tábla_elő.Rows.Count - 6].Cells[0].Value.ToString()).ToString("yyyy MMM"))
                    {
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(blokkeleje) + "1:" + MyE.Oszlopnév(Tábla_elő.Rows.Count - 4) + "1");
                        MyE.Kiir(előzőHónap.ToString("yyyy MMM"), MyE.Oszlopnév(blokkeleje) + "1");
                    }
                    Holtart.Lép();
                }

                // kiírjuk  a pályaszámokat
                int sor = 3;
                int sormax;
                string pályaszám = "";
                int k = 0;

                MyE.Oszlopszélesség(munkalap, "a:a", 9);

                for (int ii = 3; ii < Tábla_elő.ColumnCount; ii++)
                {
                    MyE.Kiir((Tábla_elő.Columns[ii].HeaderText.Trim()), "a" + sor.ToString());
                    double PSZszín = 255;
                    double PSZgarszín = 255;

                    szöveg = $"SELECT * FROM  szinezés WHERE Telephely='{Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[ii].Value.ToString().Trim()}'";
                    Szín = kéz.Egy_Adat(hely, jelszó, szöveg);
                    if (Szín != null)
                    {
                        PSZszín = Szín.SzínPsz;
                        PSZgarszín = Szín.SzínPSZgar;
                    }

                    if (Tábla_elő.Rows[Tábla_elő.RowCount - 4].Cells[ii].Value.ToString().Trim() == "1")
                        MyE.Háttérszín("a" + sor.ToString(), PSZgarszín);
                    else
                        MyE.Háttérszín("a" + sor.ToString(), PSZszín);

                    sor += 1;
                    Holtart.Lép();
                }
                sormax = sor;


                // feltöltjük a vizsgálatokat

                for (k = 3; k <= sormax; k++)
                {
                    pályaszám = MyE.Beolvas("a" + k.ToString()).Trim();
                    for (int j = 1; j < Tábla_elő.Columns.Count; j++)
                    {
                        // ha a két pályaszám egyezik
                        if (pályaszám.Trim() == Tábla_elő.Columns[j].HeaderText.Trim())
                        {
                            double isszín = 255d;
                            double istűrésszín = 255d;
                            double Pszín = 255d;

                            szöveg = $"SELECT * FROM  szinezés WHERE Telephely='{Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[j].Value.ToString().Trim()}'";
                            Szín = kéz.Egy_Adat(hely, jelszó, szöveg);
                            if (Szín != null)
                            {
                                isszín = Szín.SzínIS;
                                istűrésszín = Szín.SzínIStűrés;
                                Pszín = Szín.SzínP;

                            }
                            for (int i = 0; i < Tábla_elő.Rows.Count - 6; i++)
                            {
                                if (Tábla_elő.Rows[i].Cells[j].Value != null)
                                {
                                    szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim();
                                    // ha a napi adatok között van vizsgálat akkor kiírjuk
                                    if (szöveg != "")
                                    {
                                        // ************
                                        // IS előterv 
                                        // ************
                                        if (szöveg == "/")
                                            MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), istűrésszín);

                                        if (szöveg.Contains("IS") == true)
                                            MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), isszín);

                                        if (szöveg.Contains("P") == true)
                                            MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Pszín);
                                    }

                                    if (szöveg != "/")
                                        MyE.Kiir(szöveg, MyE.Oszlopnév(i + 2) + k.ToString());
                                }
                            }
                        }
                    }
                }
                Holtart.Lép();

                int előzővége = 3;
                // beírjuk a képleteket

                for (k = 3; k <= sormax; k++)
                {
                    pályaszám = MyE.Beolvas("a" + k.ToString()).Trim();
                    if (pályaszám.Trim() == "_")
                    {
                        for (int i = 0; i <= Tábla_elő.Rows.Count - 6; i++)
                            MyE.Kiir($"=COUNTIF(R[-{(k - előzővége)}]C:R[-1]C,\"*IS*\")+COUNTIF(R[-{(k - előzővége)}]C:R[-1]C,\"*P*\")", MyE.Oszlopnév(i + 2) + k.ToString());
                        k += 1;
                        előzővége = k + 1;
                    }
                    Holtart.Lép();
                }
                // Berácsozzuk
                MyE.Vastagkeret("a1:" + MyE.Oszlopnév(Tábla_elő.Rows.Count - 4) + k.ToString());
                MyE.Rácsoz("a1:" + MyE.Oszlopnév(Tábla_elő.Rows.Count - 4) + k.ToString());

                // *********************************
                // * Tartalom vége                 *
                // *********************************

                // * Kiegészítő adatok eleje       *
                // *********************************
                Előterv_listázás_excelhez_negát();


                // feltöltjük a vizsgálatokat
                for (k = 3; k < Tábla_elő.ColumnCount; k++)
                {
                    // beolvassuk az Excel táblából a pályaszámot

                    for (int j = 1; j < Tábla_elő.Columns.Count; j++)
                    {
                        pályaszám = MyE.Beolvas("a" + k.ToString()).Trim();
                        // ha a két pályaszám egyezik
                        if (pályaszám.Trim() == Tábla_elő.Columns[j].HeaderText.Trim())
                        {
                            // a színeket betöltjük
                            double Szín_E_v = 255d;
                            double Szín_dollár_v = 255d;
                            double Szín_Kukac_v = 255d;
                            double Szín_Hasteg_v = 255d;
                            double Szín_jog_v = 255d;
                            double Szín_nagyobb_v = 255d;

                            szöveg = $"SELECT * FROM  szinezés WHERE Telephely='{Tábla_elő.Rows[Tábla_elő.RowCount - 1].Cells[j].Value.ToString().Trim()}'";
                            Szín = kéz.Egy_Adat(hely, jelszó, szöveg);
                            if (Szín != null)
                            {
                                Szín_E_v = Szín.Szín_E;
                                Szín_dollár_v = Szín.Szín_dollár;
                                Szín_Kukac_v = Szín.Szín_Kukac;
                                Szín_Hasteg_v = Szín.Szín_Hasteg;
                                Szín_jog_v = Szín.Szín_jog;
                                Szín_nagyobb_v = Szín.Szín_nagyobb;
                            }

                            // végig megyünk cellánként és ha van tartalma akkor kiírjuk, illetve színezzük
                            for (int i = 0; i < Tábla_elő.Rows.Count; i++)
                            {
                                if (Tábla_elő.Rows[i].Cells[j].Value != null)
                                {
                                    if (Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim() != "")
                                    {

                                        switch (Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim().Substring(0, 1))
                                        {
                                            case "E":
                                                {
                                                    MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Szín_E_v);
                                                    break;
                                                }
                                            case "e":
                                                {
                                                    MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Szín_E_v);
                                                    break;
                                                }
                                            case "$":
                                                {
                                                    MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Szín_dollár_v);
                                                    break;
                                                }

                                            case "@":
                                                {
                                                    MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Szín_Kukac_v);
                                                    break;
                                                }

                                            case "#":
                                                {
                                                    MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Szín_Hasteg_v);
                                                    break;
                                                }

                                            case "§":
                                                {
                                                    MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Szín_jog_v);
                                                    break;
                                                }

                                            case ">":
                                                {
                                                    MyE.Háttérszín(MyE.Oszlopnév(i + 2) + k.ToString(), Szín_nagyobb_v);
                                                    break;
                                                }

                                        }
                                        szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim();
                                        MyE.Kiir(szöveg, MyE.Oszlopnév(i + 2) + k.ToString());
                                    }
                                }
                            }
                        }
                    }
                    Holtart.Lép();
                }

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtart.Ki();
                MyE.Megnyitás(fájlexc);
                {
                    Tábla_elő.Rows.Clear();
                    Tábla_elő.Columns.Clear();
                    Tábla_elő.Refresh();
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


        private void Előterv_listázás_excelhez()
        {
            try
            {
                //Kiírás.Text = "Pályaszám";
                Holtart.Be();

                Tábla_elő.Rows.Clear();
                Tábla_elő.Columns.Clear();
                Tábla_elő.Refresh();
                // .Visible = False
                Tábla_elő.ColumnCount = 3;

                // fejléc elkészítése
                Tábla_elő.Columns[0].HeaderText = "Dátum";
                Tábla_elő.Columns[0].Width = 80;
                Tábla_elő.Columns[1].HeaderText = "IS".Trim();
                Tábla_elő.Columns[1].Width = 30;
                Tábla_elő.Columns[2].HeaderText = "P".Trim();
                Tábla_elő.Columns[2].Width = 30;

                // elkészítjük a dátumokat
                DateTime ideigdát = Elő_Dátumtól.Value;
                int i = 0;

                while (Elő_Dátumig.Value >= ideigdát)
                {
                    Tábla_elő.RowCount++;
                    i = Tábla_elő.RowCount - 1;
                    Tábla_elő.Rows[i].Cells[0].Value = ideigdát.ToString("yyyy.MM.dd");
                    ideigdát = ideigdát.AddDays(1);
                    Holtart.Lép();
                }
                // elkészítjük a dátumokat
                // dátumok színezése

                string jelszómunka = "katalin";
                string helyelv = $@"{Application.StartupPath}\Főmérnökség\adatok\{Elő_Dátumtól.Value.Year}\munkaidőnaptár.mdb";
                if (Exists(helyelv) == true)
                    Munkaidő_naptár(helyelv, jelszómunka);

                // ha átnyúlik a következő évre 
                helyelv = Application.StartupPath + @"\Főmérnökség\adatok\" + (System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetYear(Elő_Dátumtól.Value) + 1).ToString() + @"\munkaidőnaptár.mdb";
                if (Exists(helyelv) == true)
                    Munkaidő_naptár(helyelv, jelszómunka);

                Excel_Psz_kiírás();
                CAF_telephelykiírása();
                Psz_tűrés();
                PSZ_Tűrés_nap();
                IS_tűréskiírása_táblába();
                Excel_előterv_kiírás();

                // összesítjük
                IS_P_összesítés();
                Tábla_elő.Visible = true;
                Tábla_elő.Refresh();

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


        private void CAF_telephelykiírása()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "Select * FROM Állománytábla order by azonosító";

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int oszlop = 3;

                Tábla_elő.RowCount += 1;
                int vége = Tábla_elő.RowCount - 1;

                int hiba = 0;

                foreach (Adat_Jármű rekord in Adatok)
                {
                    // ha kisebb a pályaszám akkor arább megyünk egy oszloppal
                    while (String.Compare(Tábla_elő.Columns[oszlop].HeaderText.Trim(), rekord.Azonosító.Trim()) < 0)
                    {
                        oszlop += 1;
                        if (oszlop >= Tábla_elő.ColumnCount - 1)
                        {
                            oszlop -= 1;
                            break;
                        }
                    }

                    // ha egyforma akkor kiírja a pszt-ot
                    if (Tábla_elő.Columns[oszlop].HeaderText.Trim() == rekord.Azonosító.Trim())
                    {
                        Tábla_elő.Rows[vége].Cells[oszlop].Value = rekord.Üzem.Trim();
                        oszlop += 1;
                        if (oszlop == Tábla_elő.ColumnCount)
                        {
                            hiba = 1;
                            break;
                        }
                    }
                    if (hiba == 1)
                        break;
                }

                Tábla_elő.Refresh();


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


        private void Excel_Psz_kiírás()
        {
            int oszlop = 3;
            {
                for (int i = 0; i < Elő_pályaszám.Items.Count; i++)
                {
                    if (Elő_pályaszám.GetItemChecked(i) == true)
                    {
                        Tábla_elő.ColumnCount += 1;
                        Tábla_elő.Columns[oszlop].HeaderText = Elő_pályaszám.Items[i].ToString().Trim();
                        Tábla_elő.Columns[oszlop].Width = 45;
                        oszlop += 1;
                    }
                }
                Tábla_elő.Refresh();
            }
        }


        private void Psz_tűrés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";

                string szöveg = "SELECT * FROM Alap";
                Kezelő_CAF_alap KézCafAlap = new Kezelő_CAF_alap();
                List<Adat_CAF_alap> AdatokCafAlap = KézCafAlap.Lista_Adatok(hely, jelszó, szöveg);


                // létrehozunk egy sor a ciklusnak
                Tábla_elő.RowCount += 2;
                int vége = Tábla_elő.RowCount - 1;
                Holtart.Be();
                // beolvassuk a pályaszámokat
                for (int oszlop = 3; oszlop < Tábla_elő.ColumnCount; oszlop++)
                {
                    Adat_CAF_alap AdatCafAlap = (from a in AdatokCafAlap
                                                 where a.Azonosító == Tábla_elő.Columns[oszlop].HeaderText.Trim()
                                                 select a).FirstOrDefault();

                    if (AdatCafAlap != null)
                    {
                        if (AdatCafAlap.Garancia)
                        {
                            Tábla_elő.Rows[vége - 1].Cells[oszlop].Value = "1";
                        }
                        else
                        {
                            Tábla_elő.Rows[vége - 1].Cells[oszlop].Value = "0";
                        }
                        Tábla_elő.Rows[vége].Cells[oszlop].Value = AdatCafAlap.Ciklusnap;
                    }
                    else
                    {
                        Tábla_elő.Rows[vége - 1].Cells[oszlop].Value = "0";
                        Tábla_elő.Rows[vége].Cells[oszlop].Value = "Nincs";
                    }
                    Holtart.Lép();
                }
                Tábla_elő.Refresh();
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


        private void PSZ_Tűrés_nap()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";
                Kezelő_Ciklus kéz = new Kezelő_Ciklus();
                Adat_Ciklus Elem;

                // létrehozunk egy sor a ciklusnak
                Tábla_elő.RowCount += 2;
                int vége = Tábla_elő.RowCount - 1;
                long min = 0;
                long max = 0;
                long névleg = 0;
                string előzőciklus = "";

                Holtart.Be();
                for (int oszlop = 3; oszlop <= Tábla_elő.ColumnCount - 1; oszlop++)
                {
                    // megnézzük a ciklustábla első elemének szélességét
                    if (előzőciklus.Trim() == Tábla_elő.Columns[oszlop].HeaderText.Trim())
                    {
                        Tábla_elő.Rows[vége - 1].Cells[oszlop].Value = névleg - min;
                        Tábla_elő.Rows[vége].Cells[oszlop].Value = max - névleg;
                    }
                    else
                    {
                        string szöveg = "SELECT * FROM ciklusrendtábla WHERE Típus='" + Tábla_elő.Rows[vége - 2].Cells[oszlop].Value.ToString().Trim() + "' AND sorszám=1 AND törölt='0'";
                        Elem = kéz.Egy_Adat(hely, jelszó, szöveg);
                        if (Elem != null)
                        {
                            min = Elem.Alsóérték;
                            max = Elem.Felsőérték;
                            névleg = Elem.Névleges;
                        }
                        Tábla_elő.Rows[vége - 1].Cells[oszlop].Value = névleg - min;
                        Tábla_elő.Rows[vége].Cells[oszlop].Value = max - névleg;
                    }
                    Holtart.Lép();
                }
                Tábla_elő.Refresh();
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


        private void IS_tűréskiírása_táblába()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";

                int hiba = 0;

                int oszlop = 3;
                int sor = 0;

                string szöveg = "SELECT * FROM Adatok WHERE ";
                szöveg += " IDŐvKM=1 AND ";
                szöveg += " (([Dátum]>=#" + Elő_Dátumtól.Value.ToString("MM-dd-yyyy") + "#)";
                szöveg += " AND ([Dátum]<=#" + Elő_Dátumig.Value.ToString("MM-dd-yyyy") + "#))";
                szöveg += " AND státus<8";
                szöveg += "  ORDER BY azonosító,dátum asc, IDŐvKM desc  ";


                Kezelő_CAF_Adatok Kéz = new Kezelő_CAF_Adatok();
                List<Adat_CAF_Adatok> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                // előterv kiírása csak az IS listázása

                Holtart.Be();
                foreach (Adat_CAF_Adatok rekord in Adatok)
                {
                    // ha kisebb a listában lévő szám akkor léptetjük 
                    string ideig = rekord.Azonosító.Trim();
                    DateTime ideigdát = rekord.Dátum_program;
                    if (ideigdát.ToString("yyyy.MM.dd") == "1900.01.01")
                        ideigdát = rekord.Dátum;
                    hiba = 0;

                    while (String.Compare(Tábla_elő.Columns[oszlop].HeaderText.Trim(), rekord.Azonosító.Trim()) < 0)
                    {
                        sor = 0;
                        oszlop += 1;
                        if (oszlop > Tábla_elő.ColumnCount - 1)
                        {
                            hiba = 1;
                            break;
                        }
                    }
                    if (hiba == 1)
                        break;
                    // ha a pályaszámnak megfelelő oszlopban vagyunk
                    if (Tábla_elő.Columns[oszlop].HeaderText.Trim() == rekord.Azonosító.Trim())
                    {
                        // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                        while (sor <= Tábla_elő.Rows.Count - 5 && DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) < ideigdát)
                        {
                            sor += 1;
                            if (sor == Tábla_elő.Rows.Count - 5)
                            {
                                hiba = 1;
                                break;
                            }
                        }

                        // ha egyforma akkor kiírjuk
                        if (sor <= Tábla_elő.Rows.Count - 5 && hiba != 1 && DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) == ideigdát)
                        {
                            // névleges
                            Tábla_elő.Rows[sor].Cells[oszlop].Value = "/";
                            // alsóérték 
                            int alsó = int.Parse(Tábla_elő.Rows[Tábla_elő.Rows.Count - 2].Cells[oszlop].Value.ToString());
                            for (int i = 1; i <= alsó; i++)
                            {
                                if (sor - i >= 0)
                                {
                                    Tábla_elő.Rows[sor - i].Cells[oszlop].Value = "/";
                                }
                            }
                            // felsőérték
                            int felső = int.Parse(Tábla_elő.Rows[Tábla_elő.Rows.Count - 1].Cells[oszlop].Value.ToString());
                            for (int i = 1; i <= felső; i++)
                            {
                                if (sor + i < Tábla_elő.Rows.Count - 5)
                                {
                                    Tábla_elő.Rows[sor + i].Cells[oszlop].Value = "/";
                                }
                            }

                        }
                        sor += 1;
                    }
                    if (sor >= Tábla_elő.Rows.Count - 5)
                    {
                        oszlop += 1;
                        if (oszlop == Tábla_elő.ColumnCount - 1)
                        {
                            hiba = 1;
                            break;
                        }
                        sor = 0;
                    }
                    Holtart.Lép();
                }
                Tábla_elő.Refresh();
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


        private void Excel_előterv_kiírás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                {

                    string szöveg = "SELECT * FROM Adatok WHERE ";
                    szöveg += " (([Dátum]>=#" + Elő_Dátumtól.Value.ToString("MM-dd-yyyy") + "#)";
                    szöveg += " AND ([Dátum]<=#" + Elő_Dátumig.Value.ToString("MM-dd-yyyy") + "#))";
                    szöveg += " AND státus<9";
                    szöveg += "  ORDER BY azonosító,dátum asc, IDŐvKM desc  ";

                    int oszlop = 3;
                    int sor = 0;
                    int hiba = 0;


                    Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                    List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                    Holtart.Be();

                    foreach (Adat_CAF_Adatok rekord in Adatok)
                    {


                        // ha kisebb a listában lévő szám akkor léptetjük 
                        string ideig = rekord.Azonosító.Trim();
                        DateTime ideigdát = rekord.Dátum;

                        while (String.Compare(Tábla_elő.Columns[oszlop].HeaderText.Trim(), rekord.Azonosító.Trim()) < 0)
                        {
                            sor = 0;
                            oszlop += 1;
                            if (oszlop > Tábla_elő.ColumnCount - 1)
                            {
                                oszlop -= 1;
                                break;
                            }

                        }

                        // ha a pályaszámnak megfelelő oszlopban vagyunk
                        if (Tábla_elő.Columns[oszlop].HeaderText.Trim() == rekord.Azonosító.Trim())
                        {
                            // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                            while (DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) < ideigdát)
                            {
                                sor += 1;
                                if (sor == Tábla_elő.Rows.Count - 5)
                                {
                                    hiba = 1;
                                    break;
                                }
                            }

                            // ha egyforma akkor kiírjuk
                            if (DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) == ideigdát)
                            {
                                // Vizsgálat
                                Tábla_elő.Rows[sor].Cells[oszlop].Value = rekord.Vizsgálat.Trim();
                            }
                            sor += 1;
                        }

                        if (sor == Tábla_elő.Rows.Count - 5)
                        {
                            oszlop += 1;
                            if (oszlop > Tábla_elő.ColumnCount - 1)
                            {
                                hiba = 1;
                                break;
                            }
                            sor = 0;
                        }
                        if (hiba == 1)
                            break;
                        Holtart.Lép();
                    }
                    Holtart.Ki();

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


        private void Előterv_listázás_excelhez_negát()
        {
            try
            {
                Holtart.Be();

                Tábla_elő.Rows.Clear();
                Tábla_elő.Columns.Clear();
                Tábla_elő.Refresh();
                // .Visible = False
                Tábla_elő.ColumnCount = 3;

                // fejléc elkészítése
                Tábla_elő.Columns[0].HeaderText = "Dátum";
                Tábla_elő.Columns[0].Width = 80;
                Tábla_elő.Columns[1].HeaderText = "IS".Trim();
                Tábla_elő.Columns[1].Width = 30;
                Tábla_elő.Columns[2].HeaderText = "P".Trim();
                Tábla_elő.Columns[2].Width = 30;

                // elkészítjük a dátumokat
                DateTime ideigdát = Elő_Dátumtól.Value;
                int i = 0;

                while (Elő_Dátumig.Value >= ideigdát)
                {
                    Tábla_elő.RowCount++;
                    i = Tábla_elő.RowCount - 1;
                    Tábla_elő.Rows[i].Cells[0].Value = ideigdát.ToString("yyyy.MM.dd");
                    ideigdát = ideigdát.AddDays(1);
                    Holtart.Lép();
                }

                // elkészítjük a dátumokat
                // dátumok színezése

                string jelszómunka = "katalin";
                string helyelv = $@"{Application.StartupPath}\Főmérnökség\adatok\{Elő_Dátumtól.Value.Year}\munkaidőnaptár.mdb";
                if (Exists(helyelv) == true)
                    Munkaidő_naptár(helyelv, jelszómunka);

                // ha átnyúlik a következő évre 
                helyelv = $@"{Application.StartupPath}\Főmérnökség\adatok\{Elő_Dátumtól.Value.AddYears(1).Year}\munkaidőnaptár.mdb";
                if (Exists(helyelv) == true)
                    Munkaidő_naptár(helyelv, jelszómunka);

                Excel_Psz_kiírás();
                Extra_kiírás();
                Tábla_elő.Visible = true;
                Tábla_elő.Refresh();

                CAF_telephelykiírása();
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


        private void Extra_kiírás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                Holtart.Be();

                int oszlop = 3;
                int sor = 0;

                string szöveg = "SELECT * FROM Adatok WHERE ";
                szöveg += " (([Dátum]>=#" + Elő_Dátumtól.Value.ToString("MM-dd-yyyy") + "#)";
                szöveg += " AND ([Dátum]<=#" + Elő_Dátumig.Value.ToString("MM-dd-yyyy") + "#))";
                szöveg += " AND státus=8";
                szöveg += "  ORDER BY azonosító,dátum asc ";

                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_CAF_Adatok rekord in Adatok)
                {
                    // ha kisebb a listában lévő szám akkor léptetjük 
                    string ideig = rekord.Azonosító.Trim();
                    DateTime ideigdát = rekord.Dátum;
                    // megkeressük, hogy melyik oszlopba kell írni.

                    while (String.Compare(Tábla_elő.Columns[oszlop].HeaderText.Trim(), rekord.Azonosító.Trim()) < 0)
                    {
                        sor = 0;
                        oszlop += 1; // melyik oszlopba kell majd kiírni
                        if (oszlop > Tábla_elő.ColumnCount - 1)
                        {
                            oszlop -= 1;
                            break;
                        }
                    }

                    // ha a pályaszámnak megfelelő oszlopban vagyunk
                    if (Tábla_elő.Columns[oszlop].HeaderText.Trim() == rekord.Azonosító.Trim())
                    {
                        // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                        while (DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) < ideigdát)
                        {
                            sor += 1;
                            if (sor == Tábla_elő.Rows.Count - 5)
                                break;
                        }

                        // ha egyforma akkor kiírjuk
                        if (DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) == ideigdát)
                        {
                            // tábláőzat tartalma
                            // csak azokat írjuk ki e, $, @, #, §, >
                            string miazeleje = rekord.Vizsgálat.Substring(0, 1);
                            if (miazeleje == "E" || miazeleje == "e" || miazeleje == "$" || miazeleje == "@" || miazeleje == "#" || miazeleje == "§" || miazeleje == ">")
                            {
                                Tábla_elő.Rows[sor].Cells[oszlop].Value = rekord.Vizsgálat.Trim();
                            }
                        }
                    }
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


        private void Button2_Click(object sender, EventArgs e)
        {
            if (Elő_pályaszám.CheckedItems.Count < 1)
                return;
            Elő_pályaszám.Height = 25;
            Előterv_listázás_excelhez();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            if (Elő_pályaszám.CheckedItems.Count < 1)
                return;
            Elő_pályaszám.Height = 25;
            Előterv_listázás_excelhez_negát();
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        #region Eszterga_beállítás
        Ablak_CAF_Eszterga_Beállítás Új_Ablak_CAF_Eszterga_Beállítás;

        private void Eszterga_Beállítás_Click(object sender, EventArgs e)
        {
            Új_Ablak_CAF_Eszterga_Beállítás?.Close();

            Új_Ablak_CAF_Eszterga_Beállítás = new Ablak_CAF_Eszterga_Beállítás();
            Új_Ablak_CAF_Eszterga_Beállítás.FormClosed += Új_Ablak_CAF_Eszterga_Beállítás_Closed;
            Új_Ablak_CAF_Eszterga_Beállítás.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_CAF_Eszterga_Beállítás.Show();
        }

        private void Új_Ablak_CAF_Eszterga_Beállítás_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Eszterga_Beállítás = null;
        }
        #endregion



        #region EsztergaÜtemezés

        private void Eszterga_Ütemez_Click(object sender, EventArgs e)
        {
            Eszterga_Ütemez_Eljárás();
        }


        void Eszterga_Ütemez_Eljárás()
        {
            try
            {
                // a listázott vizsgálatok státusát átállítjuk ütemezettre
                if (Elő_pályaszám.SelectedItems.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");

                string helykerék = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszókerék = "szabólászló";
                string szöveg;

                //Eszterga alapbeállítás
                Kezelő_Kerék_Eszterga_Beállítás KézEszt = new Kezelő_Kerék_Eszterga_Beállítás();
                Adat_Kerék_Eszterga_Beállítás Adat_Eszt;

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                Kezelő_CAF_alap KézCAF = new Kezelő_CAF_alap();
                Adat_CAF_alap AdatCAF;

                for (int i = 0; i < Elő_pályaszám.Items.Count; i++)
                {
                    // ki van a jelölve
                    if (Elő_pályaszám.GetItemChecked(i))
                    {  //aktuális CAF adatok
                        szöveg = $"SELECT * alap FROM WHERE azonosító='{Elő_pályaszám.GetItemChecked(i)}'";
                        AdatCAF = KézCAF.Egy_Adat(hely, jelszó, szöveg);

                        //Eszterga
                        szöveg = $"SELECT * FROM Eszterga_Beállítás WHERE azonosító='{Elő_pályaszám.GetItemChecked(i)}'";
                        Adat_Eszt = KézEszt.Egy_Adat(helykerék, jelszókerék, szöveg);

                        if (Adat_Eszt.KM_IDŐ)
                        {
                            // km alapú

                        }
                        else
                        {
                            //idő alapú

                            // mikor volt esztergálva
                            DateTime Eszterga = Mikorvolt(Elő_pályaszám.GetItemChecked(i).ToString());
                            if (Elő_Dátumig.Value <= Eszterga && Elő_Dátumtól.Value >= Eszterga)
                            {
                                //Ha intervallumon belül van akkor beütemezzük



                                //beírjuk a kerék adatokba is
                                szöveg = "UPDATE eszterga_beállítás SET ";
                                szöveg += $" Ütemezve='{Eszterga:yyyy.MM.dd}' ";
                                szöveg += $" WHERE azonosító='{Elő_pályaszám.GetItemChecked(i)}'";
                                MyA.ABMódosítás(helykerék, jelszókerék, szöveg);
                            }


                        }


                        { }
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


        /// <summary>
        /// Mikor volt utoljára esztergálva?
        /// </summary>
        /// <param name="azonosító"></param>
        /// <returns></returns>
        DateTime Mikorvolt(string azonosító)
        {
            DateTime válasz = new DateTime(1900, 1, 1);

            string jelszó = "szabólászló";
            string szöveg = $"SELECT * FROM esztergatábla WHERE azonosító='{azonosító.Trim()}'";

            Kezelő_Kerék_Eszterga kéz = new Kezelő_Kerék_Eszterga();
            Adat_Kerék_Eszterga Adat;

            for (int l = 1; l > -1; l--)
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.AddYears(-l).Year}\telepikerék.mdb";

                if (Exists(hely))
                {
                    Adat = kéz.Egy_Adat(hely, jelszó, szöveg);
                    if (Adat != null)
                        válasz = Adat.Eszterga;
                }

            }

            return válasz;
        }
        #endregion

        #region Listák
        private void CafAdatokListázása()
        {
            try
            {
                AdatokCaf.Clear();
                string szöveg = "SELECT * FROM adatok ";
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";

                AdatokCaf = KézCAF.Lista_Adatok(hely, jelszó, szöveg);
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