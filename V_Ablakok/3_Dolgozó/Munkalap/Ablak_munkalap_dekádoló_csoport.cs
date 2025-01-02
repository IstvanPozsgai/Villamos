using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_munkalap_dekádoló_csoport : Form
    {
        public DateTime Dátum { get; private set; }
        public string Cmbtelephely { get; private set; }

        public Ablak_munkalap_dekádoló_csoport(DateTime dátum, string cmbtelephely)
        {
            InitializeComponent();
            Dátum = dátum;
            Cmbtelephely = cmbtelephely;
        }



        private void Ablak_munkalap_dekádoló_csoport_Load(object sender, EventArgs e)
        {

        }

        private void Command21_Click(object sender, EventArgs e)
        {
            string helynap = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

            if (File.Exists(helynap))
                Csoportlétszám();
            else
            {

                helynap = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\adatok\Beosztás\{Dátum.Year}\{Dátum.Year}{Dátum.Month}beosztás.mdb";
                if (File.Exists(helynap))
                    Csoportlétszám1();
            }

        }

        private void Csoportlétszám()
        {
            try
            {
                Holtart.Be(20);

                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                if (!File.Exists(hely) )
                    return;
                string helynap = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";
                string jelszónap = "kiskakas";
                if (!File.Exists(helynap))
                    return;


                CsoportTábla.Rows.Clear();
                CsoportTábla.Columns.Clear();
                CsoportTábla.Refresh();
                CsoportTábla.Visible = false;
                CsoportTábla.ColumnCount = 3;

                // fejléc elkészítése
                CsoportTábla.Columns[0].HeaderText = "Csoport";
                CsoportTábla.Columns[0].Width = 200;
                CsoportTábla.Columns[1].HeaderText = "8 órás létszám:";
                CsoportTábla.Columns[1].Width = 80;
                CsoportTábla.Columns[2].HeaderText = "12 órás létszám:";
                CsoportTábla.Columns[2].Width = 80;

                //Beolvassuk a nyolc és a tizenkét órása kódokat
                Kezelő_Kiegészítő_Beosztáskódok KézKód = new Kezelő_Kiegészítő_Beosztáskódok();

                string szöveg = $"SELECT * FROM Beosztáskódok WHERE számoló=true AND Munkarend=8 ORDER BY beosztáskód";
                List<string> Kód8 = KézKód.Lista_AdatBeoKód(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM Beosztáskódok WHERE számoló=true AND Munkarend=12 ORDER BY beosztáskód";
                List<string> Kód12 = KézKód.Lista_AdatBeoKód(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM csoportbeosztás order by sorszám";
                Kezelő_Kiegészítő_Csoportbeosztás kéz = new Kezelő_Kiegészítő_Csoportbeosztás();
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                int i;

                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                {
                    CsoportTábla.RowCount++;
                    i = CsoportTábla.RowCount - 1;
                    CsoportTábla.Rows[i].Cells[0].Value = rekord.Csoportbeosztás.Trim();
                    CsoportTábla.Rows[i].Cells[1].Value = 0;
                    CsoportTábla.Rows[i].Cells[2].Value = 0;
                }


                CsoportTábla.Visible = true;
                CsoportTábla.Refresh();


                hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\Adatok\Dolgozók.mdb";
                jelszó = "forgalmiutasítás";

                //CsoportTábla.Visible = false;

                Kezelő_Dolgozó_Alap kézdolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolg = null;



                Kezelő_Dolgozó_Beosztás_Új KézBeoLista = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> BeoLista;

                for (i = 0; i < CsoportTábla.Rows.Count; i++)
                {
                    int Fő8 = 0;
                    int Fő12 = 0;

                    // beolvassuk a csoport dolgozóit
                    szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900# AND [csoport]='" + CsoportTábla.Rows[i].Cells[0].Value.ToString().Trim() + "' order by DolgozóNév";
                    AdatokDolg = kézdolg.Lista_Adatok(hely, jelszó, szöveg);

                    //havi adatokban a dolgozó azonosító lista
                    szöveg = $"SELECT * FROM beosztás WHERE NAP=#{Dátum:MM-dd-yyyy}# ORDER BY dolgozószám";
                    BeoLista = KézBeoLista.Lista_Adatok(helynap, jelszónap, szöveg);

                    foreach (Adat_Dolgozó_Alap rekord in AdatokDolg)
                    {
                        // ha van adattáblában olyan dolgozó akkor megnézzük, hogy dolgozott-e
                        string BeosztásKód = (from a in BeoLista
                                              where a.Dolgozószám.Trim() == rekord.Dolgozószám.Trim()
                                              select a.Beosztáskód.Trim()).FirstOrDefault();

                        if (BeosztásKód != null)
                        {
                            if (Kód8.Contains(BeosztásKód.Trim()))
                            {
                                Fő8++;
                            }
                            else if (Kód12.Contains(BeosztásKód.Trim()))
                            {
                                Fő12++;
                            }
                        }

                    }
                    CsoportTábla.Rows[i].Cells[1].Value = Fő8.ToString();
                    CsoportTábla.Rows[i].Cells[2].Value = Fő12.ToString();
                    Holtart.Lép();
                }
                CsoportTábla.Visible = true;
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

        private void Csoportlétszám1()
        {
            try
            {
                Holtart.Be(20);

                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                if (!File.Exists(hely) )
                    return;
                string helynap = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\adatok\Beosztás\{Dátum.Year}\{Dátum.Year}{Dátum.Month}beosztás.mdb";
                string jelszónap = "kiskakas";
                if (!File.Exists(helynap))
                    return;


                CsoportTábla.Rows.Clear();
                CsoportTábla.Columns.Clear();
                CsoportTábla.Refresh();
                CsoportTábla.Visible = false;
                CsoportTábla.ColumnCount = 3;

                // fejléc elkészítése
                CsoportTábla.Columns[0].HeaderText = "Csoport";
                CsoportTábla.Columns[0].Width = 200;
                CsoportTábla.Columns[1].HeaderText = "8 órás létszám:";
                CsoportTábla.Columns[1].Width = 80;
                CsoportTábla.Columns[2].HeaderText = "12 órás létszám:";
                CsoportTábla.Columns[2].Width = 80;

                //Beolvassuk a nyolc és a tizenkét órása kódokat
                Kezelő_Kiegészítő_Beosztáskódok KézKód = new Kezelő_Kiegészítő_Beosztáskódok();

                string szöveg = $"SELECT * FROM Beosztáskódok WHERE számoló=true AND Munkarend=8 ORDER BY beosztáskód";
                List<string> Kód8 = KézKód.Lista_AdatBeoKód(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM Beosztáskódok WHERE számoló=true AND Munkarend=12 ORDER BY beosztáskód";
                List<string> Kód12 = KézKód.Lista_AdatBeoKód(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM csoportbeosztás order by sorszám";
                Kezelő_Kiegészítő_Csoportbeosztás kéz = new Kezelő_Kiegészítő_Csoportbeosztás();
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                int i;

                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                {
                    CsoportTábla.RowCount++;
                    i = CsoportTábla.RowCount - 1;
                    CsoportTábla.Rows[i].Cells[0].Value = rekord.Csoportbeosztás.Trim();
                    CsoportTábla.Rows[i].Cells[1].Value = 0;
                    CsoportTábla.Rows[i].Cells[2].Value = 0;
                }


                CsoportTábla.Visible = true;
                CsoportTábla.Refresh();


                hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\Adatok\Dolgozók.mdb";
                jelszó = "forgalmiutasítás";

                //CsoportTábla.Visible = false;

                Kezelő_Dolgozó_Alap kézdolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolg = null;

                Kezelő_Dolgozó_Beosztás kézBeo = new Kezelő_Dolgozó_Beosztás();
                Adat_Dolgozó_Beosztás AdatokBeo = null;

                Kezelő_Dolgozó_Beosztás_lista KézBeoLista = new Kezelő_Dolgozó_Beosztás_lista();
                List<string> BeoLista;

                for (i = 0; i < CsoportTábla.Rows.Count; i++)
                {
                    int Fő8 = 0;
                    int Fő12 = 0;

                    // beolvassuk a csoport dolgozóit
                    szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900# AND [csoport]='" + CsoportTábla.Rows[i].Cells[0].Value.ToString().Trim() + "' order by DolgozóNév";
                    AdatokDolg = kézdolg.Lista_Adatok(hely, jelszó, szöveg);

                    //havi adatokban a dolgozó azonosító lista
                    szöveg = "SELECT * FROM dolgozólista ORDER BY dolgozólista";
                    BeoLista = KézBeoLista.Lista_Adatok(helynap, jelszónap, szöveg);

                    foreach (Adat_Dolgozó_Alap rekord in AdatokDolg)
                    {
                        // ha van adattáblában olyan dolgozó akkor megnézzük, hogy dolgozott-e
                        if (BeoLista.Contains(rekord.Dolgozószám.Trim()))
                        {
                            szöveg = $"SELECT * FROM {rekord.Dolgozószám.Trim()}  WHERE nap={Dátum.Day}";
                            AdatokBeo = kézBeo.Egy_Adat(helynap, jelszónap, szöveg);
                            if (AdatokBeo != null)
                            {
                                if (Kód8.Contains(AdatokBeo.Beosztáskód.Trim()))
                                {
                                    Fő8++;
                                }
                                else if (Kód12.Contains(AdatokBeo.Beosztáskód.Trim()))
                                {
                                    Fő12++;
                                }
                            }
                        }
                    }
                    CsoportTábla.Rows[i].Cells[1].Value = Fő8.ToString();
                    CsoportTábla.Rows[i].Cells[2].Value = Fő12.ToString();
                    Holtart.Lép();
                }
                CsoportTábla.Visible = true;
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

        private void Ablak_munkalap_dekádoló_csoport_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
