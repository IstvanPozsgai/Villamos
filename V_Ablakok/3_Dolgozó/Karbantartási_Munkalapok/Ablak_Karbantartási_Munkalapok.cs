using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok;
using Villamos.Villamos_Adatszerkezet;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyIO = System.IO;
using MyLista = Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok.Karbantartási_ListaFeltöltés;


namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Karbantartási_Munkalapok : Form
    {

        readonly Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeosztás = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();

        List<Adat_Technológia_Rendelés> AdatokRendelés = new List<Adat_Technológia_Rendelés>();
        List<Adat_technológia_Ciklus> AdatokCiklus = new List<Adat_technológia_Ciklus>();
        List<Adat_Technológia_Kivételek> AdatokKivétel = new List<Adat_Technológia_Kivételek>();
        List<Adat_Dolgozó_Alap> AdatokDolgozó = new List<Adat_Dolgozó_Alap>();
        List<Adat_Technológia_TípusT> AdatokTípusT = new List<Adat_Technológia_TípusT>();
        List<Adat_Technológia_Változat> AdatokVáltozat = new List<Adat_Technológia_Változat>();
        List<Adat_Technológia> AdatokTechnológia = new List<Adat_Technológia>();
        List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsoport = new List<Adat_Kiegészítő_Csoportbeosztás>();

        List<string> PályaszámLista = new List<string>();
        List<string> Pályaszám_TáblaAdatok = new List<string>();
        Dictionary<string, string> Személy = new Dictionary<string, string>();

        readonly int sormagagasság = 30;
        readonly string munkalap = "Munka1";

        List<string> Munka_végzi = new List<string>();

        string kiv_tartalom = "";
        int kijelölt_sor = -1;
        public bool csoportos = false;
        string elérés = "";
        int sor = 0;
        string munkafejléchelye = "";
        long KM_korr = 0;



        #region Alap
        public Ablak_Karbantartási_Munkalapok()
        {
            InitializeComponent();
        }
        private void Ablak_Karbantartási_Munkalapok_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();
            Dátum.Value = DateTime.Today;
            Típus_feltöltés();
            Irányítófeltöltés();
            Csoportfeltöltés();
            Dolgozók_feltöltése();
            Jogosultságkiosztás();
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Munkalap";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
            CHKMinta.Checked = false;
        }

        private void Ablak_Karbantartási_Munkalapok_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartási_Rendelés?.Close();
            Új_Ablak_Karbantartás_Csoport?.Close();
        }

        private void Jogosultságkiosztás()
        {
            if (Program.PostásTelephely == "Főmérnökség")
                CHKMinta.Visible = true;
            else
                CHKMinta.Visible = false;


            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            melyikelem = 170;
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

            }
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
                Cmbtelephely.Items.Clear();
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);

                Cmbtelephely.Refresh();

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Karbantartási_Munkalap.html";
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

        private void Dolgozók_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    if (Dolgozók.Text.Trim() != "" && kijelölt_sor != -1)
                    {
                        Dolgozó_hozzárendelés_elj();
                        if (Tábla_Beosztás.Rows.Count - 1 > kijelölt_sor) kijelölt_sor++;
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
        #endregion


        #region Feltöltések
        private void Irányítófeltöltés()
        {
            try
            {
                Kiadta.Items.Clear();
                AdatokDolgozó = MyLista.DolgozóLista(Cmbtelephely.Text.Trim());
                List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokDolgozó
                                                  where a.Főkönyvtitulus != "" && a.Főkönyvtitulus != "_"
                                                  select a).ToList();
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Kiadta.Items.Add(rekord.DolgozóNév + "-" + rekord.Főkönyvtitulus);
                Kiadta.EndUpdate();
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

        private void Típus_feltöltés()
        {
            try
            {
                Járműtípus.Items.Clear();
                AdatokTípusT = MyLista.TípustáblaLista();

                foreach (Adat_Technológia_TípusT rekord in AdatokTípusT)
                    Járműtípus.Items.Add(rekord.Típus);

                Járműtípus.Refresh();
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

        private void Járműtípus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Járműtípus.Text.Trim() == "") return;

                Pályaszám.Items.Clear();
                Tábla_psz.Rows.Clear();
                Tábla_psz.Columns.Clear();
                AdatokTechnológia = MyLista.TechnológiaLista(Járműtípus.Text.Trim());
                AdatokCiklus = MyLista.KarbCiklusLista(Járműtípus.Text.Trim());
                AdatokVáltozat = MyLista.VáltozatLista(Járműtípus.Text.Trim(), Cmbtelephely.Text.Trim());
                Ciklus_feltöltés();

                AdatokTípusT = MyLista.AlTípustáblaLista(Járműtípus.Text.Trim());
                PályaszámLista = MyLista.T5C5_minden(Cmbtelephely.Text.Trim(), AdatokTípusT);
                Pályaszám_feltöltés();

                elérés = "Üres";
                Pályaszám_Variáció();
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

        private void Ciklus_feltöltés()
        {

            Combo_KarbCiklus.Text = "";
            Combo_KarbCiklus.Items.Clear();

            foreach (Adat_technológia_Ciklus rekord in AdatokCiklus)
                Combo_KarbCiklus.Items.Add(rekord.Fokozat);
            Combo_KarbCiklus.Refresh();
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Járműtípus.Text = "";
                Combo_KarbCiklus.Items.Clear();
                Combo_KarbCiklus.Text = "";
                Pályaszám.Items.Clear();
                Tábla_psz.Rows.Clear();
                Tábla_psz.Columns.Clear();

                Pályaszám_TáblaAdatok.Clear();

                if (Járműtípus.Text.Trim() != "")
                {
                    AdatokTechnológia = MyLista.TechnológiaLista(Járműtípus.Text.Trim());
                    AdatokTechnológia = (from a in AdatokTechnológia
                                         where a.Érv_kezdete >= Dátum.Value && a.Érv_vége <= Dátum.Value
                                         select a).ToList();
                }
                AdatokRendelés = MyLista.RendelésLista(Cmbtelephely.Text.Trim(), Dátum.Value);
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

        private void Hiba_sor_ValueChanged(object sender, EventArgs e)
        {
            Chk_hibássorok.Checked = true;
        }

        private void Szerszám_sor_ValueChanged(object sender, EventArgs e)
        {
            Chk_szerszám.Checked = true;
        }

        private void Combo_KarbCiklus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Adat_technológia_Ciklus Tech_Adat = (from a in AdatokCiklus
                                                 where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                 select a).FirstOrDefault();
            if (Tech_Adat != null)
            {
                if (Tech_Adat.Csoportos == 1)
                    csoportos = true;
                else csoportos = false;
                elérés = Tech_Adat.Elérés.Trim();
            }

            Pályaszám_Variáció();
            Munkalap_Változatnév_Feltöltlés();
            Tábla_Beosztás.Rows.Clear();
            Tábla_Beosztás.Columns.Clear();
            Tábla_Beosztás.ClearSelection();
            Személy.Clear();
        }
        #endregion


        #region Pályaszám kezelés
        private void Pályaszám_feltöltés()
        {
            try
            {
                Pályaszám.Items.Clear();
                if (Járműtípus.Text.Trim() == "") return;

                for (int i = 0; i < PályaszámLista.Count; i++)
                    Pályaszám.Items.Add(PályaszámLista[i].Trim());
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

        private void Pályaszám_Variáció()
        {
            try
            {
                List<Adat_Technológia_TípusT> AdatokTípus = MyLista.AlTípustáblaLista(Járműtípus.Text.Trim());
                if (elérés == "Üres") return;
                switch (elérés)
                {
                    case "Alap":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_minden(Cmbtelephely.Text.Trim(), AdatokTípus);
                            break;
                        }
                    case "T5C5_E2":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_E2(Cmbtelephely.Text.Trim(), Dátum.Value, PályaszámLista);
                            break;
                        }
                    case "T5C5_E3":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "E3", PályaszámLista);
                            break;
                        }
                    case "T5C5_V1":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "V1", PályaszámLista);
                            break;
                        }
                    case "T5C5_V2":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "V2", PályaszámLista);
                            break;
                        }
                    case "T5C5_V3":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "V3", PályaszámLista);
                            break;
                        }
                }
                Táblázat_kitöltés();
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

        private void Táblázat_kitöltés()
        {
            try
            {
                Tábla_psz.Rows.Clear();
                Tábla_psz.Columns.Clear();
                Tábla_psz.ColumnCount = 10;
                Tábla_psz.RowCount = 1;
                if (Pályaszám_TáblaAdatok.Count < 1) return;

                Tábla_psz.Columns[0].HeaderText = "";
                Tábla_psz.Columns[0].Width = 70;

                Tábla_psz.Columns[1].HeaderText = "";
                Tábla_psz.Columns[1].Width = 70;

                Tábla_psz.Columns[2].HeaderText = "";
                Tábla_psz.Columns[2].Width = 70;

                Tábla_psz.Columns[3].HeaderText = "";
                Tábla_psz.Columns[3].Width = 70;

                Tábla_psz.Columns[4].HeaderText = "";
                Tábla_psz.Columns[4].Width = 70;

                Tábla_psz.Columns[5].HeaderText = "";
                Tábla_psz.Columns[5].Width = 70;

                Tábla_psz.Columns[6].HeaderText = "";
                Tábla_psz.Columns[6].Width = 70;

                Tábla_psz.Columns[7].HeaderText = "";
                Tábla_psz.Columns[7].Width = 70;

                Tábla_psz.Columns[8].HeaderText = "";
                Tábla_psz.Columns[8].Width = 70;

                Tábla_psz.Columns[9].HeaderText = "";
                Tábla_psz.Columns[9].Width = 70;


                int sor = 0;
                int oszlop = 0;
                for (int i = 0; i < Pályaszám_TáblaAdatok.Count; i++)
                {
                    Tábla_psz.Rows[sor].Cells[oszlop].Value = Pályaszám_TáblaAdatok[i].Trim();
                    oszlop++;
                    if (oszlop == 10)
                    {
                        sor++;
                        oszlop = 0;
                        Tábla_psz.RowCount++;
                    }
                }
                Tábla_psz.ClearSelection();

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

        private void Minden_Pályaszám_Click(object sender, EventArgs e)
        {
            Pályaszám_TáblaAdatok = PályaszámLista;
            Táblázat_kitöltés();
        }

        private void Tábla_ürítés_Click(object sender, EventArgs e)
        {
            Pályaszám_TáblaAdatok.Clear();
            Táblázat_kitöltés();
        }

        private void Hozzá_ad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva egy kocsi sem.");
                bool volt = false;
                for (int i = 0; i < Pályaszám.Items.Count; i++)
                {
                    if (Pályaszám.Items[i].ToString().Contains(Pályaszám.Text.Trim()))
                    {
                        volt = true;
                    }
                }
                if (!volt)
                    throw new HibásBevittAdat($"Ez a {Pályaszám.Text.Trim()} pályaszám nem eleme ennek a típusnak.");

                if (Pályaszám_TáblaAdatok.Contains(Pályaszám.Text.Trim()))
                    throw new HibásBevittAdat($"Ezt a {Pályaszám.Text.Trim()} pályaszámot már tartalmazza a táblázat.");

                Pályaszám_TáblaAdatok.Add(Pályaszám.Text.Trim());
                Pályaszám_TáblaAdatok.Sort();
                Táblázat_kitöltés();
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

        private void Elem_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (kiv_tartalom.Trim() == "") throw new HibásBevittAdat("A táblázatban nincs kiválasztva érvényes elem.");
                Pályaszám_TáblaAdatok.Remove(kiv_tartalom.Trim());
                Táblázat_kitöltés();
                kiv_tartalom = "";
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

        private void Tábla_psz_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tábla_psz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

            kiv_tartalom = Tábla_psz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToStrTrim();
        }
        #endregion


        #region Dolgozói adatok
        private void Csoport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dolgozók_feltöltése();
        }

        private void Dolgozók_feltöltése()
        {
            try
            {
                Dolgozók.Items.Clear();

                List<Adat_Dolgozó_Alap> Adatok;
                if (Csoport.Text.Trim() == "")
                    Adatok = AdatokDolgozó;
                else
                    Adatok = (from a in AdatokDolgozó
                              where a.Csoport == Csoport.Text.Trim()
                              select a).ToList();

                if (Beosztás.Checked) Adatok = KézDolgozó.MunkaVégzőLista(Cmbtelephely.Text.Trim(), Dátum.Value, Adatok);

                foreach (Adat_Dolgozó_Alap A in Adatok)
                    Dolgozók.Items.Add(A.DolgozóNév.Trim() + "_" + A.Dolgozószám.Trim());
                Dolgozók.EndUpdate();
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

        private void Csoportfeltöltés()
        {
            Csoport.Items.Clear();
            AdatokCsoport = MyLista.CsoportLista(Cmbtelephely.Text.Trim());

            Csoport.Items.Add("");
            foreach (Adat_Kiegészítő_Csoportbeosztás rekord in AdatokCsoport)
                Csoport.Items.Add(rekord.Csoportbeosztás);
            Csoport.EndUpdate();
        }

        private void Beosztás_CheckedChanged(object sender, EventArgs e)
        {
            Dolgozók_feltöltése();
        }

        private void Dolgozó_Hozzárendelés_Click(object sender, EventArgs e)
        {
            Dolgozó_hozzárendelés_elj();
            kijelölt_sor = -1;
        }

        private void Dolgozó_hozzárendelés_elj()
        {
            try
            {
                if (Tábla_Beosztás.Rows.Count <= 0) throw new HibásBevittAdat("Előbb ki kell választani a változatot!");
                if (kijelölt_sor < 0) throw new HibásBevittAdat("A táblázatban nincs kijelölve a rögzítéshez sor.");
                if (Dolgozók.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                Tábla_Beosztás.Rows[kijelölt_sor].Cells[1].Value = Dolgozók.Text.Trim();

                string a = Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value == null ? "_" : Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value.ToStrTrim();
                string b = Tábla_Beosztás.Rows[kijelölt_sor].Cells[1].Value == null ? "_" : Tábla_Beosztás.Rows[kijelölt_sor].Cells[1].Value.ToStrTrim();
                if (Személy.ContainsKey(a))
                    Személy[a] = b;
                else
                    Személy.Add(a, b);
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

        private void Munkalap_Változatnév_SelectedIndexChanged(object sender, EventArgs e)
        {
            Személy.Clear();
            if (Munkalap_Változatnév.Text.Trim() == "Egyszerűsített")
                Tábla_Beosztás_feltöltés_Egyszerű();
            else
                Tábla_Beosztás_feltöltés();
        }

        private void Tábla_Beosztás_feltöltés_Egyszerű()
        {
            try
            {
                Tábla_Beosztás.Rows.Clear();
                Tábla_Beosztás.Columns.Clear();
                Tábla_Beosztás.ColumnCount = 2;

                // fejléc elkészítése
                Tábla_Beosztás.Columns[0].HeaderText = "Csoportosítási elnevezés";
                Tábla_Beosztás.Columns[0].Width = 250;
                Tábla_Beosztás.Columns[1].HeaderText = "Dolgozónév";
                Tábla_Beosztás.Columns[1].Width = 300;

                //Megkeressük, hogy melyik sorszám
                Adat_technológia_Ciklus AdatCikk = (from a in AdatokCiklus
                                                    where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                    select a).FirstOrDefault();

                Munka_végzi.Clear();
                Munka_végzi = (from a in AdatokTechnológia
                               where a.Karb_ciklus_eleje.Sorszám <= AdatCikk.Sorszám && a.Karb_ciklus_vége.Sorszám >= AdatCikk.Sorszám
                               orderby a.Szakmai_bontás
                               select a.Szakmai_bontás
                               ).Distinct().ToList();

                Tábla_Beosztás.RowCount = Munka_végzi.Count;
                for (int i = 0; i < Munka_végzi.Count; i++)
                    Tábla_Beosztás.Rows[i].Cells[0].Value = Munka_végzi[i].Trim();

                kijelölt_sor = -1;
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

        private void Tábla_Beosztás_feltöltés()
        {
            try
            {
                Tábla_Beosztás.Rows.Clear();
                Tábla_Beosztás.Columns.Clear();
                Tábla_Beosztás.ColumnCount = 2;

                // fejléc elkészítése
                Tábla_Beosztás.Columns[0].HeaderText = "Csoportosítási elnevezés";
                Tábla_Beosztás.Columns[0].Width = 200;
                Tábla_Beosztás.Columns[1].HeaderText = "Dolgozónév";
                Tábla_Beosztás.Columns[1].Width = 300;

                Munka_végzi.Clear();
                Munka_végzi = (from a in AdatokVáltozat
                               where a.Változatnév == Munkalap_Változatnév.Text.Trim()
                               orderby a.Végzi
                               select a.Végzi).Distinct().ToList();

                Tábla_Beosztás.RowCount = Munka_végzi.Count;
                for (int i = 0; i < Munka_végzi.Count; i++)
                {
                    Tábla_Beosztás.Rows[i].Cells[0].Value = Munka_végzi[i].Trim();
                }
                kijelölt_sor = -1;

                Tábla_Beosztás.ClearSelection();
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

        private void Tábla_Beosztás_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                kijelölt_sor = e.RowIndex;
                if (Tábla_Beosztás.Rows.Count <= 0) throw new HibásBevittAdat("Előbb ki kell választani a változatot!");
                if (Dolgozók.Text.Trim() != "")
                {

                    if (e.RowIndex < 0) return;
                    Tábla_Beosztás.Rows[e.RowIndex].Cells[1].Value = Dolgozók.Text;

                    string a = Tábla_Beosztás.Rows[e.RowIndex].Cells[0].Value == null ? "_" : Tábla_Beosztás.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    string b = Tábla_Beosztás.Rows[e.RowIndex].Cells[1].Value == null ? "_" : Tábla_Beosztás.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    if (Személy.ContainsKey(a))
                        Személy[a] = b;
                    else
                        Személy.Add(a, b);
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

        private void Új_sor_Click(object sender, EventArgs e)
        {
            try
            {
                Dolgozók.Text = "";
                if (kijelölt_sor < 0) throw new HibásBevittAdat("Nincs kijelölve egy sor sem.");

                string tartalom = Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value.ToStrTrim();
                string[] daraboló = tartalom.Split('_');

                if (!int.TryParse(daraboló[daraboló.Length - 1], out int sorszám))
                {
                    sorszám++;
                    Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value = daraboló[0] + "_" + sorszám;
                }
                bool folytat = true;
                while (folytat)
                {
                    if (Tábla_Beosztás.Rows.Count - 1 > kijelölt_sor)
                    {
                        if (Tábla_Beosztás.Rows[kijelölt_sor + 1].Cells[0].Value.ToStrTrim().Contains(daraboló[0]))
                        {
                            sorszám++;
                            kijelölt_sor++;
                        }
                        else
                            folytat = false;
                    }
                    else
                    {
                        folytat = false;
                    }

                }
                Tábla_Beosztás.Rows.Add(daraboló[0] + "_" + (sorszám + 1).ToString());
                Tábla_Beosztás.Sort(Tábla_Beosztás.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
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

        private void Munkalap_Változatnév_Feltöltlés()
        {

            try
            {
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus.");
                if (Combo_KarbCiklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva karbantartási ciklus.");

                List<string> ELemek = (from a in AdatokVáltozat
                                       where a.Karbantartási_fokozat == Combo_KarbCiklus.Text.Trim()
                                       orderby a.Végzi
                                       select a.Változatnév).Distinct().ToList();

                //  AdatokVáltozat.OrderBy(a => a.Változatnév).Select(a => a.Változatnév).Distinct().ToList();

                Munkalap_Változatnév.Items.Clear();
                if (Combo_KarbCiklus.Text.Trim() == "E1" || Combo_KarbCiklus.Text.Trim() == "E2")
                    Munkalap_Változatnév.Items.Add("Egyszerűsített");

                foreach (string elem in ELemek)
                    Munkalap_Változatnév.Items.Add(elem);

                Munkalap_Változatnév.Refresh();
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


        #region Rendelés adatok
        Ablak_Karbantartási_Rendelés Új_Ablak_Karbantartási_Rendelés;
        private void RendelésAdatok_Click(object sender, EventArgs e)
        {
            Új_Ablak_Karbantartási_Rendelés?.Close();
            Új_Ablak_Karbantartási_Rendelés = new Ablak_Karbantartási_Rendelés(Cmbtelephely.Text.Trim());
            Új_Ablak_Karbantartási_Rendelés.FormClosed += Új_Ablak_Karbantartási_Rendelés_Closed;
            Új_Ablak_Karbantartási_Rendelés.Show();
        }

        private void Új_Ablak_Karbantartási_Rendelés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartási_Rendelés = null;
        }
        #endregion


        #region Csoportosítás
        Ablak_Karbantartás_Csoport Új_Ablak_Karbantartás_Csoport;
        private void BtnCsoportosítás_Click(object sender, EventArgs e)
        {
            Új_Ablak_Karbantartás_Csoport?.Close();
            Új_Ablak_Karbantartás_Csoport = new Ablak_Karbantartás_Csoport(Cmbtelephely.Text.Trim());
            Új_Ablak_Karbantartás_Csoport.FormClosed += Új_Új_Ablak_Karbantartás_Csoport_Closed;
            Új_Ablak_Karbantartás_Csoport.Show();
        }

        private void Új_Új_Ablak_Karbantartás_Csoport_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartás_Csoport = null;
        }
        #endregion


        #region Excel kimenet
        private void Excel_mentés_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime Eleje = DateTime.Now;
                if (Combo_KarbCiklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy ciklus fokozat sem!");
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy járműtípus sem!");
                if (Pályaszám_TáblaAdatok.Count < 1) throw new HibásBevittAdat("Nincs a táblázatba felvéve egy pályaszám sem!");

                Excel_mentés.Visible = false;
                string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (csoportos)
                {
                    string fájlnév = $"Technológia_{Program.PostásNév}_{Járműtípus.Text.Trim()}_{Combo_KarbCiklus.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    string fájlexc = $@"{könyvtár}\{fájlnév}";
                    Excel_tábla(fájlexc);

                    //fájl törlése
                    if (Töröl_igen.Checked)
                        MyIO.File.Delete(fájlexc);
                    else
                        Module_Excel.Megnyitás(fájlexc);
                }
                else
                {
                    foreach (string psz in Pályaszám_TáblaAdatok)
                    {
                        string fájlnév = $"Technológia_{Program.PostásNév}_{psz}_{Járműtípus.Text.Trim()}_{Combo_KarbCiklus.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                        string fájlexc = $@"{könyvtár}\{fájlnév}";
                        Pályaszám.Text = psz;
                        Excel_tábla(fájlexc);

                        //fájl törlése
                        if (Töröl_igen.Checked)
                            MyIO.File.Delete(fájlexc);
                        else
                            Module_Excel.Megnyitás(fájlexc);
                    }
                }

                DateTime Vége = DateTime.Now;
                MessageBox.Show($"A nyomtatvány elkészült:{Vége - Eleje}", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Holtart.Ki();
                Excel_mentés.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Excel_mentés.Visible = true;
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excel_tábla(string fájlexc)
        {
            try
            {
                Adat_technológia_Ciklus AdatCikk = (from a in AdatokCiklus
                                                    where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                    select a).FirstOrDefault();

                Holtart.Be(25, MyColor.ColorToHex(Color.DeepSkyBlue));

                //Változatok
                List<Adat_Technológia_Változat> VÁLTAdatok = (from a in AdatokVáltozat
                                                              where a.Változatnév == Munkalap_Változatnév.Text.Trim()
                                                              select a).ToList();

                //pályaszám kivételei
                AdatokKivétel = MyLista.KivételekLista(Járműtípus.Text.Trim());

                List<Adat_Technológia> Adatok = (from a in AdatokTechnológia
                                                 where a.Karb_ciklus_eleje.Sorszám <= AdatCikk.Sorszám && a.Karb_ciklus_vége.Sorszám >= AdatCikk.Sorszám
                                                 && a.Érv_kezdete <= Dátum.Value && a.Érv_vége >= Dátum.Value
                                                 orderby a.Részegység, a.Munka_utasítás_szám, a.ID
                                                 select a).ToList();
                KM_korr = 0;
                if (CHKKMU.Checked && !csoportos)
                {
                    //KMU érték
                    string helykm = Application.StartupPath + @"\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
                    string jelszókm = "pocsaierzsi";
                    string szövegkm = $"SELECT * FROM KMtábla Where azonosító='{Pályaszám.Text.Trim()}' order by  vizsgdátumk  desc";
                    Kezelő_T5C5_Kmadatok KézKM = new Kezelő_T5C5_Kmadatok();
                    Adat_T5C5_Kmadatok EgyKmAdat = KézKM.Egy_Adat(helykm, jelszókm, szövegkm);
                    KM_korr = 0;
                    if (EgyKmAdat != null) KM_korr = EgyKmAdat.KMUkm;

                    //KMU korrekció
                    List<Adat_Főkönyv_Zser_Km> AdatokZSER = new List<Adat_Főkönyv_Zser_Km>();
                    helykm = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\Napi_km_Zser_{Dátum.Value.Year}.mdb";
                    if (File.Exists(helykm)) AdatokZSER = KézZser.Lista_adatok(helykm);
                    if (Dátum.Value.Month < 4)
                    {
                        helykm = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year - 1}\Napi_km_Zser_{Dátum.Value.Year - 1}.mdb";
                        List<Adat_Főkönyv_Zser_Km> AdatokZSERelőző = new List<Adat_Főkönyv_Zser_Km>();
                        if (File.Exists(helykm))
                        {
                            AdatokZSERelőző = KézZser.Lista_adatok(helykm);
                            AdatokZSER.AddRange(AdatokZSERelőző);
                        }
                    }


                    if (AdatokZSER != null)
                    {
                        List<Adat_Főkönyv_Zser_Km> KorNapikmLista = (from a in AdatokZSER
                                                                     where a.Azonosító == Pályaszám.Text.Trim() && a.Dátum > EgyKmAdat.KMUdátum
                                                                     select a).ToList();
                        long KorNapikm = 0;
                        if (KorNapikmLista != null)
                            KorNapikm = KorNapikmLista.Sum(a => a.Napikm);
                        KM_korr += KorNapikm;
                    }
                }


                //legkisebb dátum
                DateTime hatályos = Adatok.Min(a => a.Érv_kezdete);

                string munkalap = "Munka1";
                string hatályos_str = $"Hatálybalépés dátuma:{hatályos:yyyy.MM.dd}";
                string Verzió = $"{Járműtípus.Text.Trim()}_{Combo_KarbCiklus.Text.Trim()}_{AdatCikk.Verzió}";

                MyE.ExcelLétrehozás();
                ExcelMunkalap();
                int sormagagasság = 30;
                sor = 1;
                sor = Díszesblokk(sor, Verzió);
                sor = FejlécÁltalános(sor);
                sor = MunkaFejléc(sor);


                if (csoportos && Munkalap_Változatnév.Text.Trim() != "Egyszerűsített")
                {
                    foreach (string dolgnév in Személy.OrderBy(a => a.Value).Select(a => a.Value).Distinct())
                    {
                        sor = CsoportosPályaszámokÚj(sor, dolgnév);
                    }
                }

                sor = Fejlécspec(sor);

                //----------------
                //tartalmi változat
                //----------------
                if (Munkalap_Változatnév.Text.Trim() != "Egyszerűsített")
                    sor = Részletes(munkalap, Adatok, AdatokKivétel, sormagagasság, VÁLTAdatok, sor);
                else
                    sor = Egyszerűsített(munkalap, sor);

                //Karbantartó tevékenység
                if (Chk_hibássorok.Checked) sor = KarbantartóSorok(sor);
                Holtart.Lép();

                //Szerszámok
                if (Chk_szerszám.Checked == true) sor = SzerszámokSorok(sor);
                Holtart.Lép();

                //Pályaszámok
                if (csoportos && Munkalap_Változatnév.Text.Trim() == "Egyszerűsített") sor = CsoportosPályaszámok(sor);
                Holtart.Lép();

                //Megjegyzések
                sor = MegjegyzésSorok(sor);
                Holtart.Lép();

                //Nyomtatási beállítások
                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:Q{sor}", munkafejléchelye, "", "", "", "", hatályos_str, "&P / &N oldal",
                     Verzió, "", 0.236220472440945d, 0.236220472440945d, 0.551181102362205d, 0.354330708661417d, 0.31496062992126d, 0.31496062992126d
                    , true, false);
                Holtart.Lép();

                //nyomtatás
                if (Nyomtat_igen.Checked) MyE.Nyomtatás(munkalap, 1, 1);
                Holtart.Lép();

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
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

        private int MunkaFejléc(int sor)
        {
            //Munkalap fejléc
            sor++;
            MyE.Egyesít(munkalap, $"K{sor}:L{sor}");
            MyE.Kiir("Státusz** ", $"K{sor}");

            sor++;
            MyE.Egyesít(munkalap, $"A{sor - 1}:A{sor}");
            MyE.Kiir("Nr.", $"A{sor - 1}");

            MyE.Egyesít(munkalap, $"B{sor - 1}:I{sor}");
            MyE.Kiir("MUNKAUTASÍTÁS LEÍRÁSA", $"B{sor - 1}");

            MyE.Egyesít(munkalap, $"J{sor - 1}:J{sor}");
            MyE.Kiir("Karb. Cikl.", $"J{sor - 1}:J{sor}");
            MyE.Sortörésseltöbbsorba($"J{sor - 1}:J{sor}", true);

            MyE.Kiir("OK", $"K{sor}");
            MyE.Kiir("Jav.*", $"L{sor}");

            MyE.Egyesít(munkalap, $"M{sor - 1}:O{sor}");
            MyE.Kiir("Utasítást Végrehajtotta***", $"M{sor - 1}");
            MyE.Sortörésseltöbbsorba_egyesített($"M{sor - 1}:O{sor}");

            MyE.Egyesít(munkalap, $"P{sor - 1}:Q{sor}");
            MyE.Kiir("Aláírás", $"P{sor - 1}");

            MyE.Rácsoz($"A{sor - 1}:Q{sor}");
            MyE.Betű($"{sor - 1}:{sor}", false, false, true);
            MyE.Háttérszín($"A{sor - 1}:Q{sor}", System.Drawing.Color.Gainsboro);
            MyE.Sormagasság($"{sor - 1}:{sor}", 20);

            munkafejléchelye = $"${sor - 1}:${sor + 1}";
            return sor;
        }

        private int MegjegyzésSorok(int sor)
        {
            sor += 2;
            MyE.Egyesít(munkalap, $"A{sor}:Q{sor}");
            string szövegrész = "Megjegyzés: \n" +
                "(*) Nemmegfelelősségeket jelezd részletsesen írásban\n" +
                "(**) Státusz oszlopba pipálással jelezd a munkafolyamat eredeményét\n" +
                "(***) Aláírásommal igazolom, hogy a felsorolt járműveken, a típusra aktuálisan" +
                " érvényes Főtechnológia jelölt karbantartási ciklusban előírt feladatait elvégeztem.";
            MyE.Kiir(szövegrész, $"A{sor}");
            MyE.Igazít_vízszintes($"{sor}:{sor}", "bal");
            MyE.Sormagasság($"{sor}:{sor}", 83);

            sor += 2;
            MyE.Egyesít(munkalap, $"A{sor}" + ":J" + sor);
            MyE.Egyesít(munkalap, "K" + sor + ":M" + sor);
            MyE.Egyesít(munkalap, "N" + sor + ":Q" + sor);
            MyE.Kiir("Az ellenőrzések, javítások elvégzését követően a jármű forgalomképes.", $"A{sor}");
            MyE.Kiir("Ellenőrizte:", "K" + sor);
            MyE.Igazít_vízszintes($"{sor}:{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, "N" + sor + ":Q" + sor);
            MyE.Pontvonal("N" + sor + ":Q" + sor);
            if (Kiadta.Text.Trim() == "")
                MyE.Kiir("Irányító", "N" + sor);
            else
            {
                string ideig = Kiadta.Text.Trim().Replace("-", "\n");
                MyE.Kiir(ideig, "N" + sor);
                MyE.Sormagasság($"{sor}:{sor}", 52);
            }
            return sor;
        }

        private int CsoportosPályaszámok(int sor)
        {
            sor++;
            int soreleje = sor;
            int oszlop = 3;
            for (int i = 0; i < Pályaszám_TáblaAdatok.Count; i++)
            {
                MyE.Kiir(Pályaszám_TáblaAdatok[i].ToStrTrim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                oszlop++;
                if (oszlop == 18)
                {
                    oszlop = 3;
                    sor++;
                }
            }
            MyE.Egyesít(munkalap, $"A{soreleje}:B{sor}");
            MyE.Kiir("Pályaszám(ok):", $"A{soreleje}");

            MyE.Rácsoz($"A{soreleje}:Q{sor}");
            MyE.Sormagasság(soreleje.ToString() + ":" + $"{sor}", sormagagasság);
            return sor;
        }

        private int SzerszámokSorok(int sor)
        {
            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:Q{sor}");
            MyE.Kiir("A KARBANTARTÓ TEVÉKENYSÉG SORÁN HASZNÁLT KALIBRÁLT ESZKÖZÖK, SZERSZÁMOK LISTÁJA", $"A{sor}");
            MyE.Vastagkeret($"A{sor}:Q{sor}");
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            MyE.Háttérszín($"A{sor}:Q{sor}", System.Drawing.Color.Gainsboro);

            sor++;
            MyE.Egyesít(munkalap, $"A{sor}" + ":F" + sor);
            MyE.Egyesít(munkalap, "G" + sor + ":I" + sor);
            MyE.Egyesít(munkalap, "J" + sor + ":N" + sor);
            MyE.Egyesít(munkalap, "O" + sor + ":Q" + sor);

            MyE.Kiir("ESZKÖZ, SZERSZÁM TÍPUSA", $"A{sor}");
            MyE.Kiir("SOROZATSZÁMA", "G" + sor);
            MyE.Kiir("MUNKAUTASÍTÁS SORSZÁMA", "J" + sor);
            MyE.Kiir("ALÁÍRÁS", "O" + sor);
            MyE.Rácsoz($"A{sor}:Q{sor}");
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);

            int maximum = (int)Szerszám_sor.Value;
            int soreleje = sor + 1;
            for (int i = 0; i < maximum; i++)
            {
                sor++;
                MyE.Egyesít(munkalap, $"A{sor}" + ":F" + sor);
                MyE.Egyesít(munkalap, "G" + sor + ":I" + sor);
                MyE.Egyesít(munkalap, "J" + sor + ":N" + sor);
                MyE.Egyesít(munkalap, "O" + sor + ":Q" + sor);

            }
            MyE.Rácsoz($"A{soreleje}:Q{sor}");
            MyE.Sormagasság(soreleje.ToString() + ":" + $"{sor}", sormagagasság);
            return sor;
        }

        private int KarbantartóSorok(int sor)
        {
            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:Q{sor}");
            MyE.Kiir("A KARBANTARTÓ TEVÉKENYSÉG SORÁN FELMERÜLŐ ÉSZREVÉTELEK, JAVÍTÁSOK", $"A{sor}");
            MyE.Vastagkeret($"A{sor}:Q{sor}");
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            MyE.Háttérszín($"A{sor}:Q{sor}", System.Drawing.Color.Gainsboro);

            int maximum = (int)Hiba_sor.Value;
            int soreleje = sor + 1;
            for (int i = 0; i < maximum; i++)
            {
                sor++;
                MyE.Egyesít(munkalap, $"A{sor}:Q{sor}");
            }
            MyE.Rácsoz($"A{soreleje}:Q{sor}");
            MyE.Sormagasság($"{soreleje}:{sor}", sormagagasság);
            return sor;
        }

        private int Egyszerűsített(string munkalap, int sor)
        {
            //Munkalap fejléc
            sor++;
            MyE.Egyesít(munkalap, "B" + sor + ":I" + sor);
            MyE.Egyesít(munkalap, "M" + sor + ":O" + sor);
            MyE.Egyesít(munkalap, "P" + sor + ":Q" + sor);
            MyE.Kiir("Nr.", $"A{sor}");
            MyE.Kiir("Munkaköri feladatok elosztása", "B" + sor);
            MyE.Kiir("Karb. Cikl.", "J" + sor);
            MyE.Sortörésseltöbbsorba("J" + sor);
            MyE.Kiir("OK*", "K" + sor);
            MyE.Kiir("Jav.**", "L" + sor);
            MyE.Kiir("Utasítást Végrehajtotta***", "M" + sor);
            MyE.Sortörésseltöbbsorba_egyesített("M" + sor + ":O" + sor);
            MyE.Kiir("Aláírás", "P" + sor);

            MyE.Rácsoz($"A{sor}:Q{sor}");
            MyE.Betű($"{sor}:{sor}", false, false, true);
            MyE.Háttérszín($"A{sor}:Q{sor}", System.Drawing.Color.Gainsboro);
            MyE.Sormagasság($"{sor}:{sor}", 32);

            Holtart.Be(25, MyColor.ColorToHex(Color.DeepSkyBlue));
            string előzö = "";
            string tartalom;
            sor++;
            int elsősor = sor;

            for (int i = 0; i < Tábla_Beosztás.Rows.Count; i++)
            {
                Holtart.Lép();
                tartalom = Tábla_Beosztás.Rows[i].Cells[0].Value.ToStrTrim();
                string[] darabol = tartalom.Split('_');

                if (előzö.Trim() != "" && előzö.Trim() != darabol[0].Trim())
                {
                    MyE.Egyesít(munkalap, "B" + elsősor + ":I" + (sor - 1));
                    MyE.Egyesít(munkalap, "J" + elsősor + ":J" + (sor - 1));
                    MyE.Kiir(előzö, "B" + elsősor);
                    MyE.Betű("B" + elsősor, false, false, true);
                    MyE.Igazít_vízszintes("B" + elsősor, "bal");
                    MyE.Kiir(Combo_KarbCiklus.Text.Trim(), "J" + elsősor);
                    MyE.Igazít_vízszintes("J" + elsősor, "közép");
                    MyE.Rácsoz($"A{elsősor}:Q{(sor - 1)}");
                    MyE.Sormagasság($"{elsősor}:{sor - 1}", 30);

                    előzö = "";
                }

                if (darabol.Length == 1)
                {
                    MyE.Egyesít(munkalap, "B" + sor + ":I" + sor);
                    MyE.Egyesít(munkalap, "M" + sor + ":O" + sor);
                    MyE.Egyesít(munkalap, "P" + sor + ":Q" + sor);
                    MyE.Kiir((i + 1).ToString(), $"A{sor}");
                    MyE.Kiir(tartalom, "B" + sor);
                    MyE.Betű("B" + sor, false, false, true);
                    MyE.Igazít_vízszintes("B" + sor, "bal");
                    MyE.Kiir(Combo_KarbCiklus.Text.Trim(), "J" + sor);
                    MyE.Igazít_vízszintes("J" + sor, "közép");

                    tartalom = Tábla_Beosztás.Rows[i].Cells[1].Value == null ? "" : Tábla_Beosztás.Rows[i].Cells[1].Value.ToStrTrim();
                    MyE.Kiir(tartalom, "M" + sor);
                    MyE.Sortörésseltöbbsorba("M" + sor, true);

                    MyE.Rácsoz($"A{sor}:Q{sor}");
                    MyE.Sormagasság($"{sor}:{sor}", 30);

                    előzö = "";
                }
                else
                {
                    if (előzö == "")
                    {
                        elsősor = sor;
                        előzö = darabol[0];
                    }
                    tartalom = Tábla_Beosztás.Rows[i].Cells[1].Value == null ? "" : Tábla_Beosztás.Rows[i].Cells[1].Value.ToStrTrim();
                    MyE.Kiir((i + 1).ToString(), $"A{sor}");
                    MyE.Egyesít(munkalap, "M" + sor + ":O" + sor);
                    MyE.Egyesít(munkalap, "P" + sor + ":Q" + sor);
                    MyE.Kiir(tartalom, "M" + sor);
                    MyE.Sortörésseltöbbsorba("M" + sor, true);
                }
                sor++;
            }
            //Befejezzük ha nem lett befejezve
            if (előzö.Trim() != "")
            {
                MyE.Egyesít(munkalap, "B" + elsősor + ":I" + (sor - 1));
                MyE.Egyesít(munkalap, "J" + elsősor + ":J" + (sor - 1));
                MyE.Kiir(előzö, "B" + elsősor);
                MyE.Betű("B" + elsősor, false, false, true);
                MyE.Igazít_vízszintes("B" + elsősor, "bal");
                MyE.Kiir(Combo_KarbCiklus.Text.Trim(), "J" + elsősor);
                MyE.Igazít_vízszintes("J" + elsősor, "közép");
                MyE.Rácsoz("A" + elsősor + ":Q" + (sor - 1));
                MyE.Sormagasság($"{elsősor}:{sor - 1}", 30);
            }
            MyE.Egyesít(munkalap, $"A{sor}:Q{sor + 1}");
            MyE.Kiir("(***) Aláírásommal igazolom, hogy a felsorolt járműveken, a típusra aktuálisan" +
                " érvényes Főtechnológia jelölt karbantartási ciklusban előírt feladatait elvégeztem.", $"A{sor}".ToString());
            MyE.Sormagasság($"{sor}:{sor + 1}", 25);
            MyE.Betű($"A{sor}", false, true, false);
            MyE.Sortörésseltöbbsorba_egyesített($"A{sor}".ToString());
            MyE.Vastagkeret($"A{sor}:Q{sor + 1}");

            sor++;
            return sor;
        }

        private int Részletes(string munkalap, List<Adat_Technológia> Adatok, List<Adat_Technológia_Kivételek> KivételAdatok, int sormagagasság,
                  List<Adat_Technológia_Változat> VÁLTAdatok, int sor)
        {
            Holtart.Be(Adatok.Count + 2, MyColor.ColorToHex(Color.DeepSkyBlue));

            //munkalap érdemi része
            foreach (Adat_Technológia a in Adatok)
            {
                //Ha speciális, akkor kiírja különben kihagy
                if (Ki_kell_írni(a.Altípus, csoportos, KivételAdatok))
                {
                    sor++;
                    if (a.Munka_utasítás_szám.Trim() == "0")
                    {
                        //főcímsor
                        Főcím_kiírása(sor, sormagagasság, munkalap, a.Részegység, a.Utasítás_Cím);
                    }
                    else
                    {
                        MyE.Egyesít(munkalap, "B" + sor + ":I" + sor);
                        MyE.Egyesít(munkalap, "M" + sor + ":O" + sor);
                        MyE.Egyesít(munkalap, "P" + sor + ":Q" + sor);
                        MyE.Kiir(a.Részegység.Trim() + ". " + a.Munka_utasítás_szám.Trim(), $"A{sor}");
                        string szövegelem = a.Paraméter.Trim() == "_" ? "" : "\n" + a.Paraméter.Trim();
                        if (Chk_paraméter.Checked && Chk_utasítás.Checked)
                        {
                            //Minden kiírás
                            Minden_kiírása(sor, a.Utasítás_Cím, a.Utasítás_leírás, szövegelem, a.Paraméter);
                        }
                        else if (Chk_paraméter.Checked && !Chk_utasítás.Checked)
                        {

                            //Paraméter
                            MyE.Kiir(a.Utasítás_Cím.Trim() + szövegelem, "B" + sor);
                            //Vastag
                            MyE.Cella_Betű("B" + sor, false, false, true, 1, a.Utasítás_Cím.Trim().Length);
                            //dőlt
                            MyE.Cella_Betű("B" + sor, false, true, false, (a.Utasítás_Cím.Trim() + "\n").Length + 2, a.Paraméter.Trim().Length);

                            MyE.Kiir(a.Utasítás_Cím.Trim() + szövegelem + "\n", "AS" + sor);
                            MyE.Betű($"AS{sor}", false, false, true);
                        }
                        else if (!Chk_paraméter.Checked && Chk_utasítás.Checked)
                        {
                            //Utasítás szövege
                            MyE.Kiir(a.Utasítás_Cím.Trim() + "\n" + a.Utasítás_leírás.Trim(), "B" + sor);
                            //Vastag
                            MyE.Cella_Betű("B" + sor, false, false, true, 1, a.Utasítás_Cím.Trim().Length);

                            MyE.Kiir(a.Utasítás_Cím.Trim() + "\n" + a.Utasítás_leírás.Trim() + "\n", "AS" + sor);
                            MyE.Betű($"AS{sor}", false, false, true);

                        }
                        else
                        {
                            //csak utasítás cím
                            if (a.Utasítás_Cím.Trim().Length < 55)
                            {
                                MyE.Kiir(a.Utasítás_Cím.Trim(), "B" + sor);
                                MyE.Betű("B" + sor, false, false, true);
                            }
                            else
                            {
                                MyE.Kiir(a.Utasítás_Cím.Trim(), "B" + sor);
                                MyE.Betű("B" + sor, false, false, true);
                                MyE.Kiir(a.Utasítás_Cím.Trim() + "\n", "AS" + sor);
                                MyE.Betű($"AS{sor}", false, false, true);
                            }
                        }

                        if (VÁLTAdatok.Count > 0)
                        {
                            string ideignév = Dolgozónév_kiíratása(VÁLTAdatok, a.ID, Személy);
                            ideignév = ideignév.Trim() != "_" ? ideignév : "";
                            MyE.Kiir(ideignév.Replace("_", "\n"), "M" + sor);// kicseréljük a _-t sortörésre, hogy a cella magassága jó legyen.
                            MyE.Kiir(ideignév.Replace("_", "\n"), "AT" + sor);
                            MyE.Sortörésseltöbbsorba("M" + sor, true);
                        }
                        MyE.Sormagasság($"{sor}:{sor}");
                        MyE.Igazít_vízszintes("B" + sor, "bal");
                        MyE.Kiir(a.Karb_ciklus_eleje.Fokozat.Trim(), "J" + sor);
                        MyE.Igazít_vízszintes("J" + sor, "közép");
                        MyE.Rácsoz($"A{sor}:Q{sor}");
                    }
                }
                Holtart.Lép();
            }
            return sor;
        }

        private void Főcím_kiírása(int sor, int sormagasság, string munkalap, string Részegység, string Utasítás_Cím)
        {
            //főcímsor
            MyE.Egyesít(munkalap, "B" + sor + ":Q" + sor);
            MyE.Sormagasság($"{sor}:{sor}", sormagasság);
            MyE.Háttérszín($"A{sor}:Q{sor}", System.Drawing.Color.YellowGreen);
            MyE.Kiir(Részegység.Trim(), $"A{sor}");
            MyE.Igazít_vízszintes($"A{sor}", "bal");
            MyE.Kiir(Utasítás_Cím.Trim(), "B" + sor);
            MyE.Igazít_vízszintes("B" + sor, "bal");
            MyE.Rácsoz($"A{sor}:Q{sor}");
        }

        private void Minden_kiírása(int sor, string Utasítás_Cím, string Utasítás_leírás, string szövegelem, string Paraméter)
        {
            //Minden kiírás
            MyE.Kiir(Utasítás_Cím.Trim() + "\n" + Utasítás_leírás.Trim() + szövegelem, "B" + sor);
            //vastag
            MyE.Cella_Betű("B" + sor, false, false, true, 1, Utasítás_Cím.Trim().Length);
            //dőlt
            MyE.Cella_Betű("B" + sor, false, true, false, (Utasítás_Cím.Trim() + "\n" + Utasítás_leírás.Trim()).Length + 2, Paraméter.Trim().Length);
            MyE.Kiir(Utasítás_Cím.Trim() + "\n" + Utasítás_leírás.Trim() + szövegelem + "\n", "AS" + sor);
            MyE.Betű("AS" + sor, false, false, true);

            MyE.Sormagasság($"{sor}:{sor}");
        }

        private string Dolgozónév_kiíratása(List<Adat_Technológia_Változat> VÁLTAdatok, long ID, Dictionary<string, string> Személy)
        {
            string ideigdolgozó = "";
            string Ideignév = "";

            Ideignév = (from a in VÁLTAdatok
                        where a.Technológia_Id == ID
                        select a.Végzi).FirstOrDefault();
            //Kiírjuk a változatnevet

            if (Ideignév != null)
            {
                List<string> Elem = (from a in Személy
                                     where a.Key.Contains(Ideignév.Trim())
                                     select a.Value).ToList();
                foreach (string item in Elem)
                {
                    if (ideigdolgozó.Trim() != "") ideigdolgozó += "\n\n";
                    ideigdolgozó += item.Trim();
                }
            }
            return ideigdolgozó;
        }

        private bool Ki_kell_írni(string Altípus, bool csoportos, List<Adat_Technológia_Kivételek> KivételAdatok)
        {
            bool válasz = false;
            if (CHKMinta.Checked) return true;
            if (csoportos) return true;  // csoportos esetén nem vizsgálunk
            if (Altípus.Trim() == "" || Altípus.Trim() == "_") return true; //alap beállítást mindig kiírjuk
            if (Altípus.Trim() != "" && KivételAdatok.Count == 0) return válasz; //Ha nincs kivétel akkor ki kell írni

            //ha volt altípus a kivétel listában akkor kiírjuk 
            List<Adat_Technológia_Kivételek> Szűrt = (from a in KivételAdatok
                                                      where a.Altípus == Altípus.Trim()
                                                      && a.Azonosító == Pályaszám.Text.Trim()
                                                      select a).ToList();
            if (Szűrt != null && Szűrt.Count != 0) válasz = true;
            return válasz;
        }

        private void ExcelMunkalap()
        {
            MyE.Munkalap_betű("Arial", 12);
            MyE.Oszlopszélesség(munkalap, "A:A", 8);
            MyE.Oszlopszélesség(munkalap, "B:Q", 7);
            MyE.Oszlopszélesség(munkalap, "AS:AS", 70);
            MyE.Oszlopszélesség(munkalap, "AT:AT", 21);
        }

        private int Díszesblokk(int sor, string Verzió)
        {
            string Kép = $@"{Application.StartupPath}\Főmérnökség\Adatok\Ábrák\BKV.png";
            MyE.Kép_beillesztés(munkalap, "A1", Kép, 5, 5, 50, 125);

            sor++;
            MyE.Egyesít(munkalap, $"E{sor}:Q{sor}");
            MyE.Kiir("Budapesti Közlekedési Zártkörűen Működő Részvénytársaság", $"E{sor}");
            MyE.Betű($"E{sor}", 12);
            MyE.Betű($"E{sor}", false, false, true);
            MyE.Igazít_vízszintes($"E{sor}", "jobb");

            sor++;
            MyE.Egyesít(munkalap, $"E{sor}:Q{sor}");
            MyE.Kiir("MEGELŐZŐ KARBANTARTÁS MUNKACSOMAG", $"E{sor}");
            MyE.Betű($"E{sor}", 12);
            MyE.Betű($"E{sor}", false, false, true);
            MyE.Betű($"E{sor}", Color.Green);
            MyE.Igazít_vízszintes($"E{sor}", "jobb");
            sor++;
            MyE.Vastagkeret($"A1:Q{sor}");

            sor += 5;
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            MyE.Egyesít(munkalap, $"A{sor}:D{sor}");
            MyE.Kiir("Km óra állás:", $"A{sor}");
            MyE.Betű($"{sor}:{sor}", false, true, true);

            MyE.Egyesít(munkalap, $"N{sor}:Q{sor}");
            MyE.Kiir("Verzió:", $"N{sor}");
            MyE.Betű($"{sor}:{sor}", false, true, true);

            sor++;
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            MyE.Egyesít(munkalap, $"A{sor}:D{sor}");
            MyE.Rácsoz($"A{sor - 1}:D{sor}");
            if (csoportos)
                MyE.FerdeVonal($"A{sor}:D{sor}");
            else
                MyE.Kiir($"{KM_korr}", $"A{sor}");

            MyE.Egyesít(munkalap, $"N{sor}:Q{sor}");
            MyE.Kiir(Verzió, $"N{sor}");
            MyE.Betű($"{sor}:{sor}", false, true, true);
            MyE.Rácsoz($"N{sor - 1}:Q{sor}");

            sor++;
            Kép = $@"{Application.StartupPath}\Főmérnökség\Adatok\Ábrák\Villamos_{Járműtípus.Text.Trim()}.png";
            MyE.Kép_beillesztés(munkalap, "F5", Kép, 245, 70, 100, 225);
            MyE.Vastagkeret($"A5:Q{sor}");

            return sor;
        }

        public int FejlécÁltalános(int sor)
        {
            //Dátum fej
            MyE.Egyesít(munkalap, $"A{sor}:D{sor}");
            MyE.Egyesít(munkalap, $"E{sor}:H{sor}");
            MyE.Egyesít(munkalap, $"I{sor}:M{sor}");
            MyE.Egyesít(munkalap, $"N{sor}:Q{sor}");

            MyE.Kiir("Kezdő Dátum", $"A{sor}");
            MyE.Kiir("Befejező Dátum", $"E{sor}");
            MyE.Kiir("Rendelés Szám:", $"I{sor}");
            MyE.Kiir("Telephely", $"N{sor}");
            MyE.Betű($"{sor}:{sor}", false, true, true);
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:D{sor}");
            MyE.Egyesít(munkalap, $"E{sor}:H{sor}");
            MyE.Egyesít(munkalap, $"I{sor}:M{sor}");
            MyE.Egyesít(munkalap, $"N{sor}:Q{sor}");
            MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd"), $"A{sor}");

            //rendelési szám
            MyE.Kiir(Rendelés_Keresés(), $"I{sor}");
            MyE.Kiir(Cmbtelephely.Text.Trim(), $"N{sor}");
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            MyE.Rácsoz($"A{sor - 1}:Q{sor}");
            return sor;
        }

        private int CsoportosPályaszámokÚj(int sor, string DolgNeve)
        {
            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
            MyE.Egyesít(munkalap, $"I{sor}:Q{sor}");
            MyE.Kiir(DolgNeve, $"A{sor}");
            MyE.Kiir(" Pályaszám(ok) melyeken elvégezte a karbantartást:", $"I{sor}");
            MyE.Rácsoz($"A{sor}:Q{sor}");
            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            sor++;
            int soreleje = sor;
            int oszlop = 1;
            for (int i = 0; i < Pályaszám_TáblaAdatok.Count; i++)
            {
                MyE.Kiir(Pályaszám_TáblaAdatok[i].ToStrTrim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                oszlop++;
                if (oszlop == 18)
                {
                    oszlop = 1;
                    sor++;
                }
            }
            MyE.Rácsoz($"A{soreleje}:Q{sor}");
            MyE.Sormagasság($"{soreleje}:{sor}", sormagagasság);
            return sor;
        }

        private int Fejlécspec(int sor)
        {
            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:E{sor}");
            MyE.Egyesít(munkalap, $"F{sor}:L{sor}");
            MyE.Egyesít(munkalap, $"M{sor}:Q{sor}");

            //Feltétel mező
            if (!csoportos)
            {
                MyE.Kiir($"Pályaszám:{Pályaszám.Text.Trim()}", $"A{sor}");
                MyE.Betű($"A{sor}", false, false, true);
            }

            MyE.Kiir(Járműtípus.Text.Trim() + " - " + Combo_KarbCiklus.Text.Trim() + " Karbantartási munkalap", $"F{sor}");
            MyE.Betű($"F{sor}", false, false, true);

            MyE.Kiir($"Készítve: {DateTime.Now}", $"M{sor}");
            MyE.Betű($"M{sor}", false, true, false);

            MyE.Sormagasság($"{sor}:{sor}", sormagagasság);
            MyE.Rácsoz($"A{sor}:Q{sor}");
            return sor;
        }

        private string Rendelés_Keresés()
        {
            Adat_Technológia_Rendelés Elem = (from a in AdatokRendelés
                                              where a.Év == Dátum.Value.Year && a.Technológia_típus == Járműtípus.Text.Trim()
                                              && a.Karbantartási_fokozat == Combo_KarbCiklus.Text.Trim()
                                              select a).FirstOrDefault();
            string válasz = "";

            if (Elem != null)
                válasz = Elem.Rendelésiszám;
            switch (válasz)
            {
                case "T5C5":
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum.Value:yyyy}\Vezénylés{Dátum.Value:yyyy}.mdb";
                    string jelszó = "tápijános";
                    string szöveg = $"SELECT * FROM vezényléstábla";

                    Kezelő_Vezénylés Kéz = new Kezelő_Vezénylés();
                    List<Adat_Vezénylés> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                    Adat_Vezénylés Adat = (from a in Adatok
                                           where a.Dátum == Dátum.Value && a.Törlés == 0 && a.Vizsgálat == Combo_KarbCiklus.Text.Trim() && a.Azonosító == Pályaszám.Text.Trim()
                                           select a).FirstOrDefault();
                    if (Adat != null)
                        válasz = Adat.Rendelésiszám;
                    break;
                default:
                    break;
            }
            return válasz;
        }
        #endregion
    }
}
