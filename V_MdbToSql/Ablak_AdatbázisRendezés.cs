using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        Sql_Kezelő_Működés Kéz = new Sql_Kezelő_Működés();
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
                            DvgFájlok.Rows.Add(Könyvtár, Fájlnév, MyF.GetPassword(file));
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
        #endregion



        #region Mezők
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
        #endregion

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
                MessageBox.Show("A tábla és az adatok másolása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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






        #region Mintafájlok
        private void BtnMintaKiválasztás_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "MDB fájl (*.mdb)|*.mdb";
                    ofd.Multiselect = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Mdbkönyvtár = System.IO.Path.GetDirectoryName(ofd.FileName);
                        Mdbfájl = System.IO.Path.GetFileName(ofd.FileName);
                        Mdbjelszó = MyF.GetPassword(ofd.FileName);
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

        private void MintaListázása_Click(object sender, EventArgs e)
        {
            try
            {
                DvgFájlok.Rows.Clear();
                string induloUtvonal = $@"{Application.StartupPath}";
                string kiterjesztes = "*.mdb";

                List<string> talaltKonyvtarok = new List<string>();
                List<string> szurtFajlok = new List<string>();

                Bejáró(induloUtvonal, kiterjesztes, talaltKonyvtarok, szurtFajlok);

                foreach (string file in szurtFajlok)
                {
                    if (MyF.GetPassword(file) == Mdbjelszó)
                    {
                        string Könyvtár = System.IO.Path.GetDirectoryName(file);
                        string Fájlnév = System.IO.Path.GetFileName(file);
                        DvgFájlok.Rows.Add(Könyvtár, Fájlnév, MyF.GetPassword(file));
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



        #region Gombok
        /// <summary>
        ///         táblanév másolása a listboxba, hogy onnan könnyen át lehessen másolni a cél adatbázisba, ha szükséges, akkor
        ///  lehet szerkeszteni is a név mezőben, de ha üresen hagyjuk akkor meghagyja az eredeti tábla nevét.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TáblanevekMásolása_Click(object sender, EventArgs e)
        {
            try
            {
                ÚjTáblanevek.Items.Clear();
                if (ChkTáblák.CheckedItems.Count < 1) return;
                for (int i = 0; i < ChkTáblák.CheckedItems.Count; i++)
                {
                    ÚjTáblanevek.Items.Add(ChkTáblák.CheckedItems[i].ToString());
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
        /// Minde beviteli lista tartalmát törli, hogy újra lehessen kezdeni a fájlok és táblák kiválasztását.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAlaphelyzet_Click(object sender, EventArgs e)
        {
            DvgFájlok.Rows.Clear();
            ChkTáblák.Items.Clear();
            LstMezők.Items.Clear();
            ÚjTáblanevek.Items.Clear();
            ÚjTáblaNév.Text = "";
        }


        #endregion



        #region Újtáblanevek
        /// <summary>
        /// A kijelölt táblaneveket kiegészíti újtáblanevekben rögzített értékkel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TáblaNévKieg_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÚjTáblaNév.Text.Trim() == string.Empty) return;
                if (ÚjTáblanevek.CheckedItems.Count < 1) return;
                // A CheckedIndices használata biztonságosabb, mert pontosan tudjuk, melyik sorszámú elemet kell átírni
                for (int i = 0; i < ÚjTáblanevek.CheckedIndices.Count; i++)
                {
                    int index = ÚjTáblanevek.CheckedIndices[i];
                    string regiSzoveg = ÚjTáblanevek.Items[index].ToString();
                    string ujSzoveg = ÚjTáblaNév.Text.Trim() + regiSzoveg;

                    // Itt frissítjük a tényleges elemet a listában
                    ÚjTáblanevek.Items[index] = ujSzoveg;

                    // Frissítés után újra be kell pipálni, mert az elem cseréje alaphelyzetbe állíthatja a pipát
                    ÚjTáblanevek.SetItemChecked(index, true);
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
        /// A kijelölt táblaneveket kiegészíti újtáblanevekben rögzített értékkel, de a név végére teszi, nem elejére, mint a TáblaNévKieg gomb.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TáblaNévMód_Click(object sender, EventArgs e)
        {
            try
            {     // Ellenőrizzük, hogy van-e kijelölt elem és a szövegmező nem üres-e
                if (ÚjTáblanevek.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(ÚjTáblaNév.Text))
                {
                    int index = ÚjTáblanevek.SelectedIndex;

                    // Megjegyezzük a jelenlegi pipa állapotát (opcionális, ha meg akarod tartani)
                    bool isChecked = ÚjTáblanevek.GetItemChecked(index);

                    // Az elem frissítése:
                    ÚjTáblanevek.Items[index] = ÚjTáblaNév.Text.Trim();

                    // Ha be volt pipálva, a csere után újra be kell pipálni
                    ÚjTáblanevek.SetItemChecked(index, isChecked);
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


        private void ÚjTáblanevek_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ellenőrizzük, hogy valóban van-e kijelölt elem
            if (ÚjTáblanevek.SelectedIndex != -1)
            {
                // A kijelölt elem szövegét a TextBoxba írjuk
                ÚjTáblaNév.Text = ÚjTáblanevek.SelectedItem.ToString();
            }
        }


        #endregion


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
                // Könyvtár (ez legyen a leghosszabb, kitöltve a maradék helyet)
                if (SqlTábla.Columns["Könyvtár"] != null)
                {
                    SqlTábla.Columns["Könyvtár"].Visible = true;
                    SqlTábla.Columns["Könyvtár"].HeaderText = "Mappa elérési útja";
                    SqlTábla.Columns["Könyvtár"].DisplayIndex = 0; // Első helyen
                    SqlTábla.Columns["Könyvtár"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void SqlTáblaAdatai()
        {
            try
            {
                if (SqlTábla.SelectedRows.Count < 1) return;

                txtCélKönyvtár.Text = "";
                txtCelFajl.Text = "";
                TxtCélJelszó.Text = "";
                TxtCélTábla.Text = "";

                SqLitekönyvtár = SqlTábla.SelectedRows[0].Cells[5].Value?.ToString() ?? "";
                SqLitefájl = SqlTábla.SelectedRows[0].Cells[6].Value?.ToString() ?? "";
                SqLitejelszó = SqlTábla.SelectedRows[0].Cells[2].Value?.ToString() ?? "";
                SqLitetábla = SqlTábla.SelectedRows[0].Cells[3].Value?.ToString() ?? "";

                txtCélKönyvtár.Text = SqLitekönyvtár;
                txtCelFajl.Text = SqLitefájl;
                TxtCélJelszó.Text = SqLitejelszó;
                TxtCélTábla.Text = SqLitetábla;
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


    }
}