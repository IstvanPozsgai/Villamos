using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok._5_Karbantartás.CAF_Ütemezés;
using Villamos.Villamos_Ablakok.CAF_Ütemezés;
using Villamos.Villamos_Adatszerkezet;
using MyCaf = Villamos.Villamos_Ablakok.CAF_Ütemezés.CAF_Közös_Eljárások;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Tulajdonságok_CAF
    {
        #region Kezelők
        readonly Kezelő_CAF_Szinezés KézSzín = new Kezelő_CAF_Szinezés();
        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        readonly Kezelő_CAF_alap KézAlap = new Kezelő_CAF_alap();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Váltós_Naptár KézVáltós = new Kezelő_Váltós_Naptár();
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Jármű_Hiba_Napló KézJárműHibaNapló = new Kezelő_Jármű_Hiba_Napló();
        #endregion


        CAF_Segéd_Adat Posta_Segéd = null;



        List<Adat_CAF_Adatok> AdatokCaf = new List<Adat_CAF_Adatok>();

        int SOR = -1;
        int OSZLOP = -1;

        #region Alap
        public Ablak_Tulajdonságok_CAF()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            Telephelyekfeltöltése();
        }

        private void Ablak_Tulajdonságok_CAF_Load(object sender, EventArgs e)
        {
            try
            {
                // Ezt kell futtatni az első alkalommal amikor felkerül ez a verzió.
                //KézAdatok.StatustVizsgal(KézAdatok.Lista_Adatok());

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
            uj_ablak_Caf_Km_Mod?.Close();
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
                km_modosit_btn.Enabled = false;
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
                    km_modosit_btn.Enabled = true;
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
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\CAF.html";
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                List<Adat_Jármű> AdatokÖ = KézJármű.Lista_Adatok("Főmérnökség");

                List<string> Adatok = (from a in AdatokÖ
                                       where a.Típus.Contains("CAF")
                                       select a.Üzem).Distinct().ToList();
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
            try
            {
                List<Adat_CAF_alap> Adatok = KézAlap.Lista_Adatok(true);
                Elő_pályaszám.Items.Clear();
                foreach (Adat_CAF_alap item in Adatok)
                    Elő_pályaszám.Items.Add(item.Azonosító);
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


        #region Táblázat Listázás    
        private void Tábla_frissítés_Click(object sender, EventArgs e)
        {
            ListázzaElőtervet();
        }

        private void ListázzaElőtervet()
        {
            try
            {
                Holtart.Be();
                Előterv_listázás();

                if (Elő_Dátumtól.Value > Elő_Dátumig.Value) throw new HibásBevittAdat("A dátum intervallum beállítás hibás.");
                if (Elő_pályaszám.CheckedItems.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve egy pályaszám sem.");

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
                int oszlop = 3;
                Tábla_elő.Rows.Clear();
                Tábla_elő.Columns.Clear();
                Tábla_elő.Refresh();
                Tábla_elő.Visible = false;
                Tábla_elő.ColumnCount = oszlop;

                // fejléc elkészítése
                Tábla_elő.Columns[0].HeaderText = "Dátum";
                Tábla_elő.Columns[0].Width = 80;
                Tábla_elő.Columns[1].HeaderText = "IS".Trim();
                Tábla_elő.Columns[1].Width = 30;
                Tábla_elő.Columns[2].HeaderText = "P".Trim();
                Tábla_elő.Columns[2].Width = 30;
                Tábla_elő.Columns[2].Frozen = true;

                Munkaidő_naptár();
                List<Adat_CAF_Adatok> Adatok = Szűrés();
                //pályaszámok kiírása
                for (int o = 0; o < Elő_pályaszám.CheckedItems.Count; o++)
                {
                    string pályaszám = Elő_pályaszám.CheckedItems[o].ToStrTrim();

                    //Fejléc kiírása
                    Tábla_elő.ColumnCount = oszlop + o + 1;
                    Tábla_elő.Columns[oszlop + o].HeaderText = pályaszám;
                    Tábla_elő.Columns[oszlop + o].Width = 45;

                    //Szűrés pályaszámra
                    List<Adat_CAF_Adatok> Szűrt = Adatok.Where(x => x.Azonosító == pályaszám).ToList();
                    EgyKocsiKiírás(Szűrt, oszlop + o);
                    //Napi szűrés
                    Holtart.Lép();
                }

                // összesítjük
                IS_P_összesítés();
                Tábla_elő.Refresh();
                Tábla_elő.Visible = true;
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

        private List<Adat_CAF_Adatok> Szűrés()
        {
            List<Adat_CAF_Adatok> Válasz = new List<Adat_CAF_Adatok>();
            try
            {
                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();

                Adatok = (from a in Adatok
                          where a.Dátum >= Elő_Dátumtól.Value
                          && a.Dátum <= Elő_Dátumig.Value
                          orderby a.Azonosító, a.Dátum, a.Státus
                          select a).ToList();
                if (!Elő_Mind.Checked)
                {
                    if (Elő_Km.Checked)
                        Adatok = Adatok.Where(x => x.IDŐvKM == 2).ToList();
                    if (Elő_Idő.Checked)
                        Adatok = Adatok.Where(x => x.IDŐvKM == 1).ToList();
                }
                if (!Elő_törölt.Checked)
                    Adatok = Adatok.Where(x => x.Státus < 9).ToList();
                Válasz.AddRange(Adatok);
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
            return Válasz;
        }

        private void EgyKocsiKiírás(List<Adat_CAF_Adatok> Adatok, int Oszlop)
        {
            try
            {
                for (int sor = 0; sor < Tábla_elő.Rows.Count; sor++)
                {
                    Tábla_elő.Rows[sor].Cells[Oszlop].Value = "";
                    DateTime Dátum = DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString());
                    List<Adat_CAF_Adatok> Szűrt1 = Adatok.Where(x => x.Dátum == Dátum).ToList();
                    if (Szűrt1 != null && Szűrt1.Count > 0)
                    {
                        string ideig = "";
                        foreach (Adat_CAF_Adatok item in Szűrt1)
                        {
                            if (item.IDŐvKM == 1)
                                ideig += string.Join("-", item.Vizsgálat, item.IDŐ_Sorszám);
                            else
                                ideig += string.Join("-", item.Vizsgálat, item.KM_Sorszám);
                        }

                        Tábla_elő.Rows[sor].Cells[Oszlop].Value = ideig;
                        Cella_formátum(sor, Oszlop, Szűrt1[0].Státus);
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

        private void Oszlop_újra()
        {

            string pályaszám = Tábla_elő.Columns[OSZLOP].HeaderText;
            List<Adat_CAF_Adatok> Adatok = Szűrés();
            Adatok = Adatok.Where(x => x.Azonosító == pályaszám).ToList();
            EgyKocsiKiírás(Adatok, OSZLOP);

            // összesítjük
            IS_P_összesítés();
            Tábla_elő.Refresh();
        }

        private void Munkaidő_naptár()
        {
            try
            {
                //Feltöltjük a munkanap táblát
                List<Adat_Váltós_Naptár> AdatokNaptár = new List<Adat_Váltós_Naptár>();
                for (int év = Elő_Dátumtól.Value.Year; év <= Elő_Dátumig.Value.Year; év++)
                {
                    List<Adat_Váltós_Naptár> IdeigÉv = KézVáltós.Lista_Adatok(év, "");
                    AdatokNaptár.AddRange(IdeigÉv);
                }

                // elkészítjük a dátumokat
                DateTime ideigdát = Elő_Dátumtól.Value;
                int i = 0;

                while (Elő_Dátumig.Value >= ideigdát)
                {
                    Tábla_elő.RowCount++;
                    i = Tábla_elő.RowCount - 1;
                    Tábla_elő.Rows[i].Cells[0].Value = ideigdát.ToString("yyyy.MM.dd");
                    //kiszínezzük
                    Adat_Váltós_Naptár Nap = AdatokNaptár.FirstOrDefault(x => x.Dátum == ideigdát);
                    if (Nap != null)
                    {
                        switch (Nap.Nap)
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
                    ideigdát = ideigdát.AddDays(1);
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

        private void IS_P_összesítés()
        {
            try
            {
                {
                    for (int j = 0; j < Tábla_elő.Rows.Count; j++)
                    {
                        int isdb = 0;
                        int pdb = 0;
                        for (int i = 3; i < Tábla_elő.Columns.Count; i++)
                        {
                            if (Tábla_elő.Rows[j].Cells[i].Value != null)
                            {
                                if (Tábla_elő.Rows[j].Cells[i].Value.ToStrTrim().Contains("IS") && !Tábla_elő.Rows[j].Cells[i].Value.ToStrTrim().Contains("X"))
                                {
                                    isdb += 1;
                                }
                                if (Tábla_elő.Rows[j].Cells[i].Value.ToStrTrim().Contains("P") && !Tábla_elő.Rows[j].Cells[i].Value.ToStrTrim().Contains("X"))
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

        private void Cella_formátum(int sor_a, int oszlop_a, int státus_a)
        {
            try
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
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 3) return;

                SOR = e.RowIndex;
                OSZLOP = e.ColumnIndex;
                DateTime dátum = DateTime.TryParse(Tábla_elő.Rows[e.RowIndex].Cells[0].Value.ToString(), out dátum) ? dátum : new DateTime(1900, 1, 1);
                string pályaszám = Tábla_elő.Columns[e.ColumnIndex].HeaderText.Trim();

                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                Adat_CAF_Adatok rekord = KézAdatok.Egy_Adat_Spec(pályaszám, dátum, 9);
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

        private void SegédAblakKezelés()
        {
            Új_Ablak_CAF_Segéd?.Close();

            Új_Ablak_CAF_Segéd = new Ablak_CAF_Segéd(Posta_Segéd, Elő_Dátumig.Value);
            Új_Ablak_CAF_Segéd.FormClosed += Ablak_CAF_Segéd_Closed;
            Új_Ablak_CAF_Segéd.Változás += Oszlop_újra;
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
                if (Cmbtelephely.Text.Trim() == "") return;

                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség").Where(a => a.Üzem == Cmbtelephely.Text.Trim()).ToList();

                for (int i = 0; i < Elő_pályaszám.Items.Count; i++)
                {
                    Adat_Jármű Adat = Adatok.FirstOrDefault(a => a.Azonosító == Elő_pályaszám.Items[i].ToStrTrim());
                    if (Adat != null) Elő_pályaszám.SetItemChecked(i, true);
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
            if (Tábla_elő.Rows.Count <= 0) return;
            string fájlexc;

            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Listázott tartalom mentése Excel fájlba",
                FileName = $"CAF_ütemzés_Adatok{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;

            MyE.DataGridViewToExcel(fájlexc, Tábla_elő);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Module_Excel.Megnyitás(fájlexc + ".xlsx");
        }
        #endregion


        #region Előtervet készít
        private void Előtervet_készít_Click(object sender, EventArgs e)
        {
            try
            {
                Elő_pályaszám.Height = 25;
                Holtart.Be();
                Eltervező_IDŐ_gyűjtő();
                Eltervező_KM_gyűjtő();
                Előterv_listázás();
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

        private void Eltervező_IDŐ_gyűjtő()
        {
            try
            {
                Holtart.Be();
                List<Adat_CAF_Adatok> Adatok = new List<Adat_CAF_Adatok>();
                foreach (string Elem in Elő_pályaszám.CheckedItems)
                {
                    List<Adat_CAF_Adatok> AdatokEgy = MyCaf.IDŐ_EgyKocsi(Elem.ToStrTrim(), Elő_Dátumig.Value, new DateTime(1900, 1, 1));
                    Adatok.AddRange(AdatokEgy);
                    Holtart.Lép();
                }
                KézAdatok.Rögzítés(Adatok);
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

        private void Eltervező_KM_gyűjtő()
        {
            try
            {
                Holtart.Be();
                List<Adat_CAF_Adatok> Adatok = new List<Adat_CAF_Adatok>();
                foreach (string Elem in Elő_pályaszám.CheckedItems)
                {
                    List<Adat_CAF_Adatok> AdatokEgy = MyCaf.KM_EgyKocsi(Elem.ToStrTrim(), Elő_Dátumig.Value);
                    Adatok.AddRange(AdatokEgy);
                    Holtart.Lép();
                }
                KézAdatok.Rögzítés(Adatok);
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


        #region Előterv törlés- Ütemez- 
        private void ELő_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Elő_pályaszám.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");

                AdatokCaf = KézAdatok.Lista_Adatok();
                Holtart.Be();
                List<Adat_CAF_Adatok_Pót> ADATOK = new List<Adat_CAF_Adatok_Pót>();
                foreach (string elem in Elő_pályaszám.CheckedItems)
                {
                    Adat_CAF_Adatok AdatCaf = (from a in AdatokCaf
                                               where a.Azonosító == elem.ToStrTrim()
                                               && a.Dátum >= Elő_Dátumtól.Value
                                               && a.Státus == 0
                                               select a).FirstOrDefault();

                    if (AdatCaf != null)
                    {
                        Adat_CAF_Adatok_Pót ADAT = new Adat_CAF_Adatok_Pót(
                                                    elem,
                                                    Elő_Dátumtól.Value,
                                                    0);
                        ADATOK.Add(ADAT);
                    }

                    AdatCaf = (from a in AdatokCaf
                               where a.Azonosító == elem.ToStrTrim()
                               && a.Dátum >= Elő_Dátumtól.Value
                               && a.Státus >= 8
                               select a).FirstOrDefault();

                    if (AdatCaf != null)
                    {
                        Adat_CAF_Adatok_Pót ADAT = new Adat_CAF_Adatok_Pót(
                                            elem,
                                            Elő_Dátumtól.Value,
                                            8);
                        ADATOK.Add(ADAT);

                        ADAT = new Adat_CAF_Adatok_Pót(
                                            elem,
                                            Elő_Dátumtól.Value,
                                            9);
                        ADATOK.Add(ADAT);
                    }
                    Holtart.Lép();
                }
                KézAdatok.Törlés(ADATOK);
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
                if (Elő_pályaszám.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");

                Holtart.Be();
                AdatokCaf = KézAdatok.Lista_Adatok();

                List<Adat_CAF_Adatok_Pót> ADATOK = new List<Adat_CAF_Adatok_Pót>();
                foreach (string elem in Elő_pályaszám.CheckedItems)
                {
                    Adat_CAF_Adatok AdatCaf = (from a in AdatokCaf
                                               where a.Azonosító == elem.ToStrTrim()
                                               && a.Dátum >= Elő_Dátumtól.Value
                                               && a.Dátum <= Elő_Dátumig.Value
                                               && a.Státus == 0
                                               select a).FirstOrDefault();

                    if (AdatCaf != null)
                    {
                        Adat_CAF_Adatok_Pót ADAT = new Adat_CAF_Adatok_Pót(
                                                    elem,
                                                    Elő_Dátumtól.Value,
                                                    Elő_Dátumig.Value,
                                                    0
                                                    );
                    }
                    Holtart.Lép();
                }
                KézAdatok.Ütemez(ADATOK);
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

                DateTime Dátum_ütem = Elő_Dátumtól.Value;

                List<Adat_CAF_Adatok> AdatokCaf = KézAdatok.Lista_Adatok();
                AdatokCaf = (from a in AdatokCaf
                             where a.Státus == 2
                             && a.Dátum == Dátum_ütem
                             orderby a.Azonosító
                             select a).ToList();
                if (AdatokCaf.Count == 0) throw new HibásBevittAdat($"Nincs a {Dátum_ütem:yyyy.MM.dd} napra egy jármű sem amit ütemezni kell.");

                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Jármű_hiba> AdatokJárműHiba = KézJárműHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

                Holtart.Be();
                // ha van ütemezett kocsi
                foreach (Adat_CAF_Adatok rekordütemez in AdatokCaf)
                {
                    Holtart.Lép();

                    // megnézzük, hogy a telephelyen van-e a kocsi
                    Adat_Jármű Kocsi = (from a in AdatokJármű
                                        where a.Azonosító == rekordütemez.Azonosító.Trim()
                                        select a).FirstOrDefault();

                    if (Kocsi != null)
                    {
                        // ha telephelyen van a kocsi
                        // hiba leírása
                        string szöveg1 = $"{rekordütemez.Vizsgálat.Trim()}-{rekordütemez.Id}-{rekordütemez.Dátum:yyyy.MM.dd}";

                        // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                        // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                        Adat_Jármű_hiba AdatHiba = (from a in AdatokJárműHiba
                                                    where a.Azonosító == rekordütemez.Azonosító.Trim()
                                                    && a.Hibaleírása.Contains(szöveg1)
                                                    select a).FirstOrDefault();

                        if (AdatHiba == null)
                        {
                            // hibák számát emeljük és státus állítjuk ha kell
                            Kocsi = (from a in AdatokJármű
                                     where a.Azonosító == rekordütemez.Azonosító.Trim()
                                     select a).FirstOrDefault();

                            if (Kocsi != null)
                            {
                                Adat_Jármű ADAT_Jármű = new Adat_Jármű(
                                                    rekordütemez.Azonosító.Trim(),
                                                    Kocsi.Hibák + 1,
                                                    3);
                                KézJármű.Módosítás_Hiba_Státus(Cmbtelephely.Text.Trim(), ADAT_Jármű);
                            }

                            // beírjuk a hibákat
                            Adat_Jármű_hiba ADATHIBA = new Adat_Jármű_hiba(
                                                   Program.PostásNév,
                                                   3,
                                                   szöveg1,
                                                   DateTime.Now,
                                                   false,
                                                   Kocsi.Típus.Trim(),
                                                   rekordütemez.Azonosító.Trim(),
                                                   Kocsi.Hibák + 1);
                            KézJárműHiba.Rögzítés(Cmbtelephely.Text.Trim(), ADATHIBA);

                            // naplózzuk a hibákat
                            KézJárműHibaNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today, ADATHIBA);

                            // módosítjuk az ütemezett adatokat is
                            KézAdatok.Módosítás_Státus(rekordütemez.Id, 4);
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
            Új_Ablak_CAF_Részletes.Változás += Oszlop_újra;
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
                if (Elő_pályaszám.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kocsi sem.");
                Elő_pályaszám.Height = 25;
                Előterv_listázás_excelhez();

                string munkalap = "Munka1";

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

                List<Adat_CAF_Szinezés> AdatokSzín = KézSzín.Lista_Adatok();
                Adat_CAF_Szinezés Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[3].Value.ToStrTrim()).FirstOrDefault();
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
                }

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


                // kiírjuk  a pályaszámokat
                int sor = 3;
                int sormax;
                string pályaszám = "";
                int k = 0;

                MyE.Oszlopszélesség(munkalap, "a:a", 9);

                for (int ii = 3; ii < Tábla_elő.ColumnCount; ii++)
                {
                    MyE.Kiir((Tábla_elő.Columns[ii].HeaderText.Trim()), $"a{sor}");
                    double PSZszín = 255;
                    double PSZgarszín = 255;

                    Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[ii].Value.ToStrTrim()).FirstOrDefault();
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

                            Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[j].Value.ToStrTrim()).FirstOrDefault();
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
                                    string szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim();
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

                            Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 1].Cells[j].Value.ToStrTrim()).FirstOrDefault();
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
                                        string szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim();
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
                Munkaidő_naptár();

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
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");

                Tábla_elő.RowCount += 1;
                int vége = Tábla_elő.RowCount - 1;
                for (int i = 3; i < Tábla_elő.Columns.Count; i++)
                {
                    Adat_Jármű Adat = (from a in Adatok
                                       where a.Azonosító == Tábla_elő.Columns[i].HeaderText.Trim()
                                       select a).FirstOrDefault();
                    if (Adat != null)
                    {
                        Tábla_elő.Rows[vége].Cells[i].Value = Adat.Üzem.Trim();
                    }
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
                    if (Elő_pályaszám.GetItemChecked(i))
                    {
                        Tábla_elő.ColumnCount += 1;
                        Tábla_elő.Columns[oszlop].HeaderText = Elő_pályaszám.Items[i].ToStrTrim();
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
                List<Adat_CAF_alap> AdatokCafAlap = KézAlap.Lista_Adatok();

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
                List<Adat_Ciklus> Adatok = KézCiklus.Lista_Adatok(true);

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
                        Adat_Ciklus Elem = (from a in Adatok
                                            where a.Típus == Tábla_elő.Rows[vége - 2].Cells[oszlop].Value.ToStrTrim()
                                            && a.Sorszám == 1
                                            && a.Törölt == "0"
                                            select a).FirstOrDefault();
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
                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Dátum >= Elő_Dátumtól.Value
                          && a.Dátum <= Elő_Dátumig.Value
                          && a.Státus < 8
                          && a.IDŐvKM == 1
                          orderby a.Azonosító, a.Dátum ascending, a.IDŐvKM descending
                          select a).ToList();
                // előterv kiírása csak az IS listázása

                Holtart.Be();
                for (int oszlop = 3; oszlop < Tábla_elő.Columns.Count; oszlop++)
                {
                    List<Adat_CAF_Adatok> AdatokSzűrt = (from a in Adatok
                                                         where a.Azonosító == Tábla_elő.Columns[oszlop].HeaderText.Trim()
                                                         select a).ToList();
                    foreach (Adat_CAF_Adatok rekord in AdatokSzűrt)
                    {
                        for (int sor = 0; sor < Tábla_elő.Rows.Count - 5; sor++)
                        {
                            if (DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) == rekord.Dátum)
                            {   // névleges
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
                        }
                    }
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
                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Dátum >= Elő_Dátumtól.Value
                          && a.Dátum <= Elő_Dátumig.Value
                          && a.Státus < 9
                          orderby a.Azonosító, a.Dátum ascending
                          select a).ToList();
                // előterv kiírása csak az IS listázása

                Holtart.Be();
                for (int oszlop = 3; oszlop < Tábla_elő.Columns.Count; oszlop++)
                {
                    List<Adat_CAF_Adatok> AdatokSzűrt = (from a in Adatok
                                                         where a.Azonosító == Tábla_elő.Columns[oszlop].HeaderText.Trim()
                                                         select a).ToList();
                    foreach (Adat_CAF_Adatok rekord in AdatokSzűrt)
                    {
                        for (int sor = 0; sor < Tábla_elő.Rows.Count - 5; sor++)
                        {
                            if (DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) == rekord.Dátum)
                                Tábla_elő.Rows[sor].Cells[oszlop].Value = rekord.Vizsgálat.Trim();
                        }
                    }
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
                Munkaidő_naptár();
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
                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Dátum >= Elő_Dátumtól.Value
                          && a.Dátum <= Elő_Dátumig.Value
                          && a.Státus == 8
                          orderby a.Azonosító, a.Dátum ascending
                          select a).ToList();
                // előterv kiírása csak az IS listázása

                Holtart.Be();
                for (int oszlop = 3; oszlop < Tábla_elő.Columns.Count; oszlop++)
                {
                    List<Adat_CAF_Adatok> AdatokSzűrt = (from a in Adatok
                                                         where a.Azonosító == Tábla_elő.Columns[oszlop].HeaderText.Trim()
                                                         select a).ToList();
                    foreach (Adat_CAF_Adatok rekord in AdatokSzűrt)
                    {
                        for (int sor = 0; sor < Tábla_elő.Rows.Count - 5; sor++)
                        {
                            if (DateTime.Parse(Tábla_elő.Rows[sor].Cells[0].Value.ToString()) == rekord.Dátum)
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
                    }
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

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Elő_pályaszám.CheckedItems.Count < 1) return;
            Elő_pályaszám.Height = 25;
            Előterv_listázás_excelhez();

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Elő_pályaszám.CheckedItems.Count < 1) return;
            Elő_pályaszám.Height = 25;
            Előterv_listázás_excelhez_negát();
        }
        #endregion


        Ablak_CAF_KM uj_ablak_Caf_Km_Mod;
        private void Km_modosit_btn_Click(object sender, EventArgs e)
        {
            uj_ablak_Caf_Km_Mod?.Close();

            uj_ablak_Caf_Km_Mod = new Ablak_CAF_KM
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            uj_ablak_Caf_Km_Mod.Show();
        }

        private void Ablak_CAF_KM_Closed(object sender, FormClosedEventArgs e)
        {
            uj_ablak_Caf_Km_Mod = null;
        }
    }
}