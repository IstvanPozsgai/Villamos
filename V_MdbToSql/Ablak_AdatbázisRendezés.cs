using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_AdatbázisRendezés : Form
    {
        string Mdbfájl = "";
        string Mdbjelszó = "";
        string Mdbtábla = "";
        string Mdbkönyvtár = "";

        string SqLitekönyvtár = "";
        string SqLitefájl = "";
        string SqLitejelszó = "";
        string SqLitetábla = "";

        readonly Sql_Kezelő_Működés Kéz = new Sql_Kezelő_Működés();
        readonly Sql_Kezelő_Áttöltés KézFile = new Sql_Kezelő_Áttöltés();
        List<Sql_Működés> FileLista = new List<Sql_Működés>();


        public Ablak_AdatbázisRendezés()
        {
            InitializeComponent();
            Start();
        }

        #region Alap
        private void Ablak_AdatbázisRendezés_Load(object sender, EventArgs e)
        {
            // kapcsoljuk a gombokat      Program.Postás_Felhasználó.GlobalAdmin;

        }

        private void Start()
        {
            SqlTáblaFrissítés();
            txtCélKönyvtár.Text = $@"{Application.StartupPath}\Főmérnökség\SQL\";
            TxtCélTábla.Text = "Tbl_";
            FileLista = KézFile.Lista_Adatok();

            GetKezeloOsztalyok();
            Telephelyekfeltöltése();
            AdatszerkezetekListázása();

        }

        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\alapegyéb.html";
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
        #endregion


        #region Fájlok
        private void BtnHozzaad_Click(object sender, EventArgs e)
        {
            try
            {
                FileLista = KézFile.Lista_Adatok();
                DvgFájlok.Rows.Clear();
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "MDB fájl (*.mdb)|*.mdb";
                    ofd.Multiselect = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in ofd.FileNames)
                        {
                            string Könyvtár = System.IO.Path.GetDirectoryName(file);
                            string Fájlnév = System.IO.Path.GetFileName(file);
                            string jelszo = MyF.GetPassword(file);

                            // 1. Sor hozzáadása és az index eltárolása
                            int rowIndex = DvgFájlok.Rows.Add(Könyvtár, Fájlnév, jelszo);

                            // 2. Ellenőrzés: benne van-e a FileLista-ban?

                            bool marLetezik = FileLista.Any(x => x.Fájl == file);

                            if (marLetezik)
                            {
                                // 3. Színezés (pl. világospiros vagy sárga)
                                DvgFájlok.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
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

        private void DvgFájlok_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DvgFájlok.Rows[e.RowIndex].Selected = true;
            FájlAdatTáblái();
        }
        #endregion


        #region Táblák
        private void FájlAdatTáblái()
        {
            try
            {
                ChkTáblák.Items.Clear();
                LstMezők.Items.Clear();
                DgvAdatok.DataSource = null; // Kapcsolat bontása az adatforrással
                DgvAdatok.Rows.Clear();
                DgvAdatok.Columns.Clear();
                if (DvgFájlok.SelectedRows.Count < 1) return;

                Mdbkönyvtár = DvgFájlok.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                Mdbfájl = DvgFájlok.SelectedRows[0].Cells[1].Value?.ToString() ?? "";
                Mdbjelszó = DvgFájlok.SelectedRows[0].Cells[2].Value?.ToString() ?? "";
                if (Mdbkönyvtár == string.Empty || Mdbfájl == string.Empty || Mdbjelszó == string.Empty) return;
                ChkTáblák.Items.AddRange(MyA.Mdb_ABTáblák($@"{Mdbkönyvtár}\{Mdbfájl}", Mdbjelszó).ToArray());
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

        private void ChkTáblák_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChkTáblák.SelectedItem == null) return;
            Mdbtábla = ChkTáblák.SelectedItem.ToString();
            MezőkFeltöltése();
            AdatokMegjelenítése();
        }

        private void MezőkFeltöltése()
        {
            try
            {
                LstMezők.Items.Clear();
                if (Mdbkönyvtár == string.Empty || Mdbfájl == string.Empty || Mdbjelszó == string.Empty || Mdbtábla == string.Empty) return;
                LstMezők.Items.AddRange(MyA.Mdb_ABMezők($@"{Mdbkönyvtár}\{Mdbfájl}", Mdbjelszó, Mdbtábla).ToArray());

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

        private void AdatokMegjelenítése()
        {
            try
            {
                // Ellenőrizzük az alapvető adatokat, mielőtt nekifutunk
                if (string.IsNullOrEmpty(Mdbkönyvtár) || string.IsNullOrEmpty(Mdbfájl) || string.IsNullOrEmpty(Mdbtábla)) return;

                string elérésiÚt = Path.Combine(Mdbkönyvtár, Mdbfájl);

                // Itt hívjuk meg az adatbázis-kezelő osztályodat
                // Feltételezve, hogy van egy Mdb_TáblaLekérése metódusod, ami DataTable-t ad vissza
                DataTable dt = MyA.Mdb_TáblaLekérése(elérésiÚt, Mdbjelszó, Mdbtábla);

                if (dt != null)
                {
                    DgvAdatok.DataSource = dt;
                    DgvAdatok.AutoGenerateColumns = true; // Automatikusan létrehozza az oszlopokat
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show("Nem sikerült betölteni az adatokat: " + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIndit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCelFajl.Text) || string.IsNullOrWhiteSpace(TxtCélJelszó.Text) || string.IsNullOrWhiteSpace(txtCélKönyvtár.Text))
                    throw new HibásBevittAdat("Add meg a cél adatbázist és jelszót!");

                if (!txtCelFajl.Text.Contains(".db"))
                    txtCelFajl.Text += ".db";

                if (DvgFájlok.SelectedRows.Count < 1) return;

                SqLitefájl = $@"{txtCélKönyvtár.Text.Trim()}\{txtCelFajl.Text.Trim()}".KönyvSzerk();
                SqLitejelszó = TxtCélJelszó.Text.Trim();
                SqLitetábla = TxtCélTábla.Text.Trim();

                Sql_Működés MdbAdat = new Sql_Működés { Fájl = $@"{Mdbkönyvtár}\{Mdbfájl}", Jelszó = Mdbjelszó, Tábla = Mdbtábla };
                Sql_Működés SqLiteAdat = new Sql_Működés { Fájl = SqLitefájl, Jelszó = SqLitejelszó, Tábla = SqLitetábla };


                Cursor = Cursors.WaitCursor;
                MdbToSqliteMigrator.EgyTáblaMigrálása(MdbAdat, SqLiteAdat);
                Cursor = Cursors.Default;
                SqlTáblaFrissítés();

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

        private void Bejáró(string utvonal, string minta, List<string> konyvtarLista, List<string> fajlLista)
        {
            try
            {
                // Csak a megadott mintának megfelelő fájlokat adjuk hozzá
                foreach (string fajl in Directory.GetFiles(utvonal, minta))
                {
                    fajlLista.Add(fajl);
                }

                // Almappák bejárása
                foreach (string konyvtar in Directory.GetDirectories(utvonal))
                {
                    konyvtarLista.Add(konyvtar);
                    Bejáró(konyvtar, minta, konyvtarLista, fajlLista);
                }
            }
            catch (UnauthorizedAccessException) { /* Jogosultsági hiba esetén átugorjuk */ }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Bejaro", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        /// <summary>
        /// Minde beviteli lista tartalmát törli, hogy újra lehessen kezdeni a fájlok és táblák kiválasztását.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAlaphelyzet_Click(object sender, EventArgs e)
        {
            DvgFájlok.Rows.Clear();
            ChkTáblák.Items.Clear();
            LstMezők.Items.Clear();
            DgvAdatok.DataSource = null; // Kapcsolat bontása az adatforrással
            DgvAdatok.Rows.Clear();
            DgvAdatok.Columns.Clear();
            SqlTábla.DataSource = null;
            SqlTábla.Rows.Clear();
            SqlTábla.Columns.Clear();

            SqlTáblaAdatok.DataSource = null;
            SqlTáblaAdatok.Rows.Clear();
            SqlTáblaAdatok.Columns.Clear();

            LstSqlMezők.Items.Clear();
        }


        #region SqlTábla
        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            SqlTáblaFrissítés();
        }

        private void SqlTáblaFrissítés()
        {
            try
            {
                //  Lekéred a listát
                List<Sql_Működés> Adatok = Kéz.Lista_Adatok();

                // 3. Hozzárendeled a DataGridView-hoz
                SqlTábla.DataSource = null; // Kényszerített frissítéshez néha kell
                SqlTábla.DataSource = Adatok;

                // --- 1. MINDENT ELREJTÜNK ELŐSZÖR ---
                foreach (DataGridViewColumn col in SqlTábla.Columns)
                {
                    col.Visible = false;
                }

                // Fájlnév (fix vagy tartalom szerinti szélesség)
                if (SqlTábla.Columns["Könyvtár"] != null)
                {
                    SqlTábla.Columns["Könyvtár"].Visible = true;
                    SqlTábla.Columns["Könyvtár"].HeaderText = "Könyvtár";
                    SqlTábla.Columns["Könyvtár"].DisplayIndex = 0;
                    SqlTábla.Columns["Könyvtár"].Width = 150;
                }

                // Fájlnév (fix vagy tartalom szerinti szélesség)
                if (SqlTábla.Columns["Fájlnév"] != null)
                {
                    SqlTábla.Columns["Fájlnév"].Visible = true;
                    SqlTábla.Columns["Fájlnév"].HeaderText = "Adatbázis fájl";
                    SqlTábla.Columns["Fájlnév"].DisplayIndex = 1;
                    SqlTábla.Columns["Fájlnév"].Width = 150;
                }
                // Tábla (az osztályodban "Tábla" néven szerepel)
                if (SqlTábla.Columns["Tábla"] != null)
                {
                    SqlTábla.Columns["Tábla"].Visible = true;
                    SqlTábla.Columns["Tábla"].HeaderText = "Tábla név";
                    SqlTábla.Columns["Tábla"].Width = 120;
                    SqlTábla.Columns["Tábla"].DisplayIndex = 2;
                }

                // Jelszó
                if (SqlTábla.Columns["Jelszó"] != null)
                {
                    SqlTábla.Columns["Jelszó"].Visible = true;
                    SqlTábla.Columns["Jelszó"].HeaderText = "Jelszó";
                    SqlTábla.Columns["Jelszó"].Width = 100;
                    SqlTábla.Columns["Jelszó"].DisplayIndex = 3;

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

        private void SqlTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                SqlTábla.Rows[e.RowIndex].Selected = true;
                SqlTáblaAdatai();
                SqlTáblaSzerkezet();
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

        private void SqlTáblaSzerkezet()
        {
            try
            {
                LstSqlMezők.Items.Clear();
                if (SqLitekönyvtár == string.Empty || SqLitefájl == string.Empty || SqLitejelszó == string.Empty || SqLitetábla == string.Empty) return;
                LstSqlMezők.Items.AddRange(MyA.SqLite_ABMezők($@"{SqLitekönyvtár}\{SqLitefájl}", SqLitejelszó, SqLitetábla).ToArray());

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

        private void SqlAdatokMezőbeírása()
        {
            txtCélKönyvtár.Text = "";
            txtCelFajl.Text = "";
            TxtCélJelszó.Text = "";
            TxtCélTábla.Text = "";

            txtCélKönyvtár.Text = SqLitekönyvtár;
            txtCelFajl.Text = SqLitefájl;
            TxtCélJelszó.Text = SqLitejelszó;
            TxtCélTábla.Text = SqLitetábla;
        }

        private void SqlTáblaAdatai()
        {
            try
            {
                if (SqlTábla.SelectedRows.Count < 1) return;

                SqLitekönyvtár = SqlTábla.SelectedRows[0].Cells[5].Value?.ToString() ?? "";
                SqLitefájl = SqlTábla.SelectedRows[0].Cells[6].Value?.ToString() ?? "";
                SqLitejelszó = SqlTábla.SelectedRows[0].Cells[2].Value?.ToString() ?? "";
                SqLitetábla = SqlTábla.SelectedRows[0].Cells[3].Value?.ToString() ?? "";
                SqlAdatokMezőbeírása();
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

        private void BtnSqlTáblaLista_Click(object sender, EventArgs e)
        {
            try
            {
                // Ellenőrizzük az alapvető adatokat, mielőtt nekifutunk
                if (string.IsNullOrEmpty(SqLitekönyvtár) || string.IsNullOrEmpty(SqLitefájl) || string.IsNullOrEmpty(SqLitetábla)) return;

                string elérésiÚt = Path.Combine(SqLitekönyvtár, SqLitefájl);

                // Itt hívjuk meg az adatbázis-kezelő osztályodat
                // Feltételezve, hogy van egy Mdb_TáblaLekérése metódusod, ami DataTable-t ad vissza
                DataTable dt = MyA.SqLite_TáblaLekérése(elérésiÚt, SqLitejelszó, SqLitetábla);

                if (dt != null)
                {
                    SqlTáblaAdatok.DataSource = dt;
                    SqlTáblaAdatok.AutoGenerateColumns = true; // Automatikusan létrehozza az oszlopokat
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


        #region Osztálykezelés
        public void GetKezeloOsztalyok()
        {
            string nevter = "Villamos.Kezelők";

            // Lekérjük az aktuális futó programban lévő összes típust
            List<string> tipusok = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == nevter && t.Name.StartsWith("SQL_Kezelő")) // Csak azokat gyűjtsd ki, amik "SQL_Kezelő"-vel kezdődnek
                .Select(t => t.Name) // Csak a nevük kell
               .OrderBy(t => t)
                .ToList();
            foreach (string tipus in tipusok)
            {
                CmbOsztályok.Items.Add(tipus);
            }

        }

        public void MetodusokListazasa(string osztalyNev)
        {
            List<string> metodusLista = new List<string>();

            // Teljes név a névtérrel
            string teljesNev = "Villamos.Kezelők." + osztalyNev;
            Type tipus = Type.GetType(teljesNev);

            if (tipus != null)
            {
                // Csak a saját (nem örökölt) és publikus metódusokat kérjük le
                var metodusok = tipus.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (var m in metodusok)
                {
                    // Paraméterek összegyűjtése (típus és név)
                    var parancs = m.GetParameters();
                    string paramString = string.Join(", ", parancs.Select(p => $"{p.ParameterType.Name} {p.Name}"));

                    metodusLista.Add($"{m.Name}({paramString})");
                }
            }
            foreach (string metodus in metodusLista)
            {
                CmbMetódusok.Items.Add(metodus);
            }
        }

        private void CmbOsztályok_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbMetódusok.Items.Clear();
            CmbOsztályok.Text = CmbOsztályok.Items[CmbOsztályok.SelectedIndex].ToStrTrim();
            MetodusokListazasa(CmbOsztályok.Text);
        }

        private void CmbMetódusok_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbMetódusok.Text = CmbMetódusok.Items[CmbMetódusok.SelectedIndex].ToStrTrim();
            TxtMetódus.Text = CmbMetódusok.Text.Split('(')[0]; // Csak a metódus neve, paraméterek nélkül
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.Add("");
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
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
            Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
        }

        private void BtnAdatbázis_Click(object sender, EventArgs e)
        {

            if (Cmbtelephely.Text.Trim() == "" && Évek.Value.ToÉrt_Int() == 0)
            {
                EgyszerűMetódusHívás(CmbOsztályok.Text.Trim(), TxtMetódus.Text.Trim());
            }
            else
            {
                object[] paraméterek = new object[2];

                if (CmbMetódusok.Text.Contains("Telephely")) paraméterek[0] = Cmbtelephely.Text.Trim();
                if (CmbMetódusok.Text.Contains("Év")) paraméterek[1] = Évek.Value.ToÉrt_Int();
                DinamikusMetódusHívás(CmbOsztályok.Text.Trim(), TxtMetódus.Text.Trim(), paraméterek);
            }

        }

        public void EgyszerűMetódusHívás(string osztályNév, string metódusNév)
        {
            try
            {
                // 1. Osztály megkeresése (Namespace-szel együtt!)
                string teljesNév = $"Villamos.Kezelők.{osztályNév}";
                Type típus = Type.GetType(teljesNév);

                if (típus == null) return; // Ha nincs ilyen osztály, csendben kilépünk

                // 2. Példányosítás
                object példány = Activator.CreateInstance(típus);

                // Feltételezve, hogy a 'példány' az Activator.CreateInstance-szel készült objektum

                FieldInfo FájlHelye = példány.GetType().GetField("hely", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo FájlJelszó = példány.GetType().GetField("jelszó", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo Fájltábla = példány.GetType().GetField("táblanév", BindingFlags.NonPublic | BindingFlags.Instance);
                string elérésiÚt = "";
                if (FájlHelye != null)
                {
                    elérésiÚt = FájlHelye.GetValue(példány)?.ToString();
                    SqLitekönyvtár = System.IO.Path.GetDirectoryName(elérésiÚt);
                    SqLitefájl = System.IO.Path.GetFileName(elérésiÚt);
                }
                TxtHely.Text = elérésiÚt;
                if (FájlJelszó != null) SqLitejelszó = FájlJelszó.GetValue(példány)?.ToString();
                if (Fájltábla != null) SqLitetábla = Fájltábla.GetValue(példány)?.ToString();
                SqlAdatokMezőbeírása();
                Sql_Működés SqLiteAdat = new Sql_Működés { Fájl = elérésiÚt, Jelszó = SqLitejelszó, Tábla = SqLitetábla };
                Kéz.Döntés(SqLiteAdat);
                SqlTáblaFrissítés();
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
        public void DinamikusMetódusHívás(string osztályNév, string metódusNév, object[] paraméterek)
        {
            try
            {
                // 1. Osztály megkeresése (Namespace-szel együtt!)
                string teljesNév = $"Villamos.Kezelők.{osztályNév}";
                Type típus = Type.GetType(teljesNév);

                if (típus == null) return; // Ha nincs ilyen osztály, csendben kilépünk

                // 2. Példányosítás
                object példány = Activator.CreateInstance(típus);

                // 3. Metódus megkeresése név alapján
                MethodInfo metódus = típus.GetMethod(metódusNév);

                if (metódus != null)
                {
                    // 4. Ellenőrizzük, hogy a paraméterek száma egyezik-e
                    var vártParaméterek = metódus.GetParameters();
                    if (vártParaméterek.Length == paraméterek.Length)
                    {
                        metódus.Invoke(példány, paraméterek);

                        // Feltételezve, hogy a 'példány' az Activator.CreateInstance-szel készült objektum

                        FieldInfo FájlHelye = példány.GetType().GetField("hely", BindingFlags.NonPublic | BindingFlags.Instance);
                        FieldInfo FájlJelszó = példány.GetType().GetField("jelszó", BindingFlags.NonPublic | BindingFlags.Instance);
                        FieldInfo Fájltábla = példány.GetType().GetField("táblanév", BindingFlags.NonPublic | BindingFlags.Instance);
                        string elérésiÚt = "";
                        if (FájlHelye != null)
                        {
                            elérésiÚt = FájlHelye.GetValue(példány)?.ToString();
                            SqLitekönyvtár = System.IO.Path.GetDirectoryName(elérésiÚt);
                            SqLitefájl = System.IO.Path.GetFileName(elérésiÚt);
                        }
                        TxtHely.Text = elérésiÚt;
                        if (FájlJelszó != null) SqLitejelszó = FájlJelszó.GetValue(példány)?.ToString();
                        if (Fájltábla != null) SqLitetábla = Fájltábla.GetValue(példány)?.ToString();
                        SqlAdatokMezőbeírása();
                        Sql_Működés SqLiteAdat = new Sql_Működés { Fájl = elérésiÚt, Jelszó = SqLitejelszó, Tábla = SqLitetábla };
                        Kéz.Döntés(SqLiteAdat);
                        SqlTáblaFrissítés();
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


        #region Adatszerkezet
        private void AdatszerkezetekListázása()
        {
            string nevter = "Villamos.Adatszerkezet";

            // Lekérjük az aktuális futó programban lévő összes típust
            List<string> tipusok = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == nevter && t.Name.StartsWith("")) // Csak azokat gyűjtsd ki, amik "SQL_Kezelő"-vel kezdődnek
                .Select(t => t.Name) // Csak a nevük kell
               .OrderBy(t => t)
                .ToList();
            foreach (string tipus in tipusok)
            {
                CmBAdatszerkezetek.Items.Add(tipus);
            }

        }


        private void CmBAdatszerkezetek_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmBAdatszerkezetek.Text = CmBAdatszerkezetek.Items[CmBAdatszerkezetek.SelectedIndex].ToStrTrim();
        }

        /// <summary>
        /// Megírja a rögzítés és a módosítást és kimenti txtbe
        /// </summary>
        private void KódotÍr()
        {
            if (CmBAdatszerkezetek.Text.Trim() == "") return;
            string fájlexc;

            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Kód generálás",
                FileName = $"{CmBAdatszerkezetek.Text.Trim()} Osztály",
                Filter = "Jegyzettömb |*.txt"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                fájlexc = SaveFileDialog1.FileName;
            }
            else
            {
                return;
            }
            OsztályKészítő OV = new OsztályKészítő(fájlexc, CmBAdatszerkezetek.Text.Trim());

            Type tipus = Type.GetType($"Villamos.Adatszerkezet.{CmBAdatszerkezetek.Text.Trim()}");
            if (tipus != null) OV.OsztályKészítés();


        }

        private void BtnKódol_Click(object sender, EventArgs e)
        {
            KódotÍr();
        }

        #endregion
    }
}