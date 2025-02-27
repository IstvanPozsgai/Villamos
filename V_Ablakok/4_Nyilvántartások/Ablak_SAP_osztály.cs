using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;


namespace Villamos
{

    public partial class Ablak_SAP_osztály
    {

        readonly Kezelő_Osztály_Adat KézOsztály = new Kezelő_Osztály_Adat();
        readonly Kezelő_Osztály_Név KézNév = new Kezelő_Osztály_Név();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();


        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Osztály_Adat> AdatokOsztály = new List<Adat_Osztály_Adat>();
        List<Adat_Osztály_Név> AdatokNév = new List<Adat_Osztály_Név>();
        public Ablak_SAP_osztály()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Jogosultságkiosztás();
            Fülekkitöltése();
            Pályaszámfeltöltés();

            Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            ListákFeltöltése();
            Osztályfeltöltés();
        }

        private void Ablak_SAP_osztály_Load(object sender, EventArgs e)
        {

        }

        #region Alap
        private void Jogosultságkiosztás()
        {
            try
            {
                SAP_Betölt.Visible = false;
                Telepadatok.Visible = false;
                int melyikelem = 189;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))

                {
                    SAP_Betölt.Visible = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Telepadatok.Visible = true;
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


        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Osztály.html";
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


        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Fülekkitöltése()
        {
            switch (Lapfülek.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        break;
                    }

                case 2:
                    {
                        break;
                    }

                case 3:
                    {
                        break;
                    }
            }
        }


        private void Lapfülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Lapfülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Lapfülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                Font BoldFont = new Font(Lapfülek.Font.Name, Lapfülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }
        #endregion



        #region SAP osztály adatok
        private void Pályaszámfeltöltés()
        {
            try
            {
                string hely, jelszó, szöveg;
                hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                jelszó = "pozsgaii";
                szöveg = "SELECT * FROM állománytábla where törölt=0 order by  azonosító";

                PályaszámCombo1.Items.Clear();
                PályaszámCombo1.BeginUpdate();
                PályaszámCombo1.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
                PályaszámCombo1.EndUpdate();
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


        private void Frissít_Click(object sender, EventArgs e)
        {
            Tábla_író();
        }


        private void Tábla_író()
        {
            try
            {
                if (PályaszámCombo1.Text.Trim() == "") return;
                ListákFeltöltése();

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 2;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Osztály név";
                Tábla.Columns[0].Width = 400;
                Tábla.Columns[1].HeaderText = "Osztály érték";
                Tábla.Columns[1].Width = 400;


                Adat_Osztály_Adat Elem = (from a in AdatokOsztály
                                          where a.Azonosító == PályaszámCombo1.Text.Trim()
                                          select a).FirstOrDefault();

                if (Elem == null)
                {
                    Tábla.Visible = true;
                    return;
                }

                Tábla.RowCount = Elem.Adatok.Count;
                for (int i = 0; i < Elem.Adatok.Count; i++)
                {
                    string Név = (from a in AdatokNév
                                  where a.Osztálymező.Trim() == Elem.Mezőnév[i]
                                  select a.Osztálynév.Trim()).FirstOrDefault();
                    if (Név != null)
                        Tábla.Rows[i].Cells[0].Value = Név;
                    else
                        Tábla.Rows[i].Cells[0].Value = Elem.Mezőnév[i];

                    Tábla.Rows[i].Cells[1].Value = Elem.Adatok[i];

                }
                Tábla.Refresh();
                Tábla.Visible = true;

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


        private void SAP_Betölt_Click(object sender, EventArgs e)
        {
            SAP_Betöltés();
        }

        private void SAP_Betöltés()
        {
            try
            {
                string pályaszám = "";
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Szövegfájlok |*.txt"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                string hely = Application.StartupPath + @"\Főmérnökség\adatok\osztály.mdb";
                string jelszó = "kéménybe";

                string szöveg = "SELECT * FROM osztálytábla ORDER BY id";
                Kezelő_Osztály_Név KézONév = new Kezelő_Osztály_Név();
                List<Adat_Osztály_Név> AdatokNév = KézONév.Lista_Adat();


                Kezelő_Osztály_Adat KézAdat = new Kezelő_Osztály_Adat();
                List<Adat_Osztály_Adat> AdatokAdat = KézAdat.Lista_Adat();

                Holtart.Be();
                // beolvassuk a szövegfájlt
                string[] lines = ReadAllLines(fájlexc);
                // soronként elemezzük

                //Fejléc adatok
                string[] Soradatok = lines[3].ToString().Split('\t');
                string[] Fejléc = new string[Soradatok.Length + 1];
                int fejléchossz = Soradatok.Length + 1;
                int[] Sorszám = new int[Soradatok.Length + 1];
                Fejléc[0] = "Megnevezés"; //Első elem

                for (int i = 0; i < Soradatok.Length; i++)
                {
                    if (Soradatok[i].Trim() == "Megnevezés")
                        Sorszám[0] = i;

                    int Elem = (from a in AdatokNév
                                where a.Osztálynév.Trim() == Soradatok[i].Trim()
                                select a.Id).FirstOrDefault();
                    if (Elem != 0)
                    {

                        Fejléc[Elem] = Soradatok[i].Trim();
                        Sorszám[i] = Elem; //megadja , hogy az elemet hova rakjuk
                    }
                }
                List<string> SzövegGy = new List<string>();
                for (int i = 5; i < lines.Length; i++)  // lines
                {

                    Soradatok = lines[i].ToString().Split('\t');

                    string[] Ideig = new string[fejléchossz];

                    // Feldaraboljuk a sort elemekre és beletesszük a megfelelő helyre
                    for (int j = 0; j < Soradatok.Length; j++)
                    {
                        if (Sorszám[j] != 0)
                            Ideig[Sorszám[j]] = Soradatok[j].ToStrTrim();
                    }
                    pályaszám = MyF.Szöveg_Tisztítás(Soradatok[Sorszám[0]], 1, 4);

                    // az új azonosító
                    szöveg = "SELECT * FROM osztályadatok where azonosító='" + pályaszám.ToString().Trim() + "'";
                    Adat_Osztály_Adat Elem = (from a in AdatokAdat
                                              where a.Azonosító == pályaszám.ToStrTrim()
                                              select a).FirstOrDefault();
                    if (Elem == null)
                    {
                        szöveg = "INSERT INTO osztályadatok ( azonosító, típus, altípus, telephely, szolgálat ";
                        for (int k = 1; k <= 40; k++)
                            szöveg += ", adat" + k.ToString();
                        szöveg += ") VALUES (";
                        szöveg += "'" + pályaszám + "', '?', '?', '?', '?'";
                        for (int k = 1; k <= 40; k++)
                            szöveg += ", '?'";
                        szöveg += ")";
                    }
                    else
                    {
                        szöveg = "UPDATE osztályadatok SET ";
                        for (int ki = 1; ki < AdatokNév.Count; ki++)
                        {
                            if (Ideig[ki] != null)
                            {
                                if (Ideig[ki].Trim() != "")
                                    szöveg += $"adat{ki}='{Ideig[ki].Trim()}', ";
                            }
                        }
                        szöveg = szöveg.Substring(0, szöveg.Length - 2); //az utolsó vesszőt eldobjuk
                        szöveg += $" WHERE azonosító='{pályaszám.Trim()}'";
                    }
                    SzövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                Delete(fájlexc);
                ListákFeltöltése();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void PályaszámCombo1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if ((int)e.KeyCode == 13)
                {
                    this.AcceptButton = Frissít;
                    Tábla_író();
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



        #region Lekérdezések
        private void Osztályfeltöltés()
        {
            try
            {
                Osztálylista.Items.Clear();
                Osztálylista.BeginUpdate();
                List<Adat_Osztály_Név> Adatok = (from a in AdatokNév
                                                 where a.Használatban == true
                                                 select a).ToList();

                foreach (Adat_Osztály_Név rekord in Adatok)
                    Osztálylista.Items.Add(rekord.Osztálynév);

                Osztálylista.EndUpdate();
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


        private void LekérdezTelep_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálylista.SelectedItems.Count < 1) return;
                if (Osztálylista.Items.Count < 0) return;
                ListákFeltöltése();


                string honnan = Osztálylista.SelectedItem.ToStrTrim();
                string helyhiba = (from a in AdatokNév
                                   where a.Osztálynév == honnan
                                   select a.Osztálymező).FirstOrDefault() ?? "";
                if (helyhiba.Trim() == "") return;

                string szöveg = $"SELECT telephely, altípus, {helyhiba}, Count(osztályadatok.altípus) AS Összeg";
                szöveg += "  From osztályadatok";
                szöveg += " GROUP BY  telephely, típus, altípus, " + helyhiba;
                szöveg += " Having ((altípus <> '?' )";
                szöveg += " And (" + helyhiba + " <> '?' ))";
                szöveg += " order by altípus";
                List<Adat_Osztály_Adat> AdatokÖ = KézOsztály.Lista_Adat();

                List<Adat_Osztály_Adat> Adatok = (AdatokÖ);

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 4;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Telephely";
                Tábla1.Columns[0].Width = 140;
                Tábla1.Columns[1].HeaderText = "Típus";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = honnan;
                Tábla1.Columns[2].Width = 400;
                Tábla1.Columns[3].HeaderText = "Darabszám";
                Tábla1.Columns[3].Width = 100;

                foreach (Adat_Osztály_Adat rekord in Adatok)
                {
                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    //Tábla1.Rows[i].Cells[0].Value = rekord.Telephely;
                    //Tábla1.Rows[i].Cells[1].Value = rekord.AlTípus;
                    //Tábla1.Rows[i].Cells[2].Value = rekord.Adat;
                    //Tábla1.Rows[i].Cells[3].Value = rekord.Összeg;
                }

                Tábla1.Refresh();
                Tábla1.Visible = true;
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



        private void LekérdezFajta_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálylista.SelectedItems.Count < 1) return;
                ListákFeltöltése();

                string honnan = Osztálylista.SelectedItem.ToStrTrim();
                string helyhiba = (from a in AdatokNév
                                   where a.Osztálynév == honnan
                                   select a.Osztálymező).FirstOrDefault() ?? "";
                if (helyhiba.Trim() == "") return;

                string szöveg = $"SELECT  {helyhiba}, Count(osztályadatok.altípus) AS Összeg";
                szöveg += "  From osztályadatok";
                szöveg += " GROUP BY  " + helyhiba;
                szöveg += " Having " + helyhiba + " <> '?'";
                szöveg += " order by " + helyhiba;



                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 2;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = honnan;
                Tábla1.Columns[0].Width = 400;
                Tábla1.Columns[1].HeaderText = "Darabszám";
                Tábla1.Columns[1].Width = 140;

                //Kezelő_Osztály_Adat_Szum KézSzum = new Kezelő_Osztály_Adat_Szum();
                //List<Adat_Osztály_Adat_Szum> Adatok = KézSzum.Lista_Adat1(hely, jelszó, szöveg, helyhiba);

                //foreach (Adat_Osztály_Adat_Szum rekord in Adatok)
                //{
                //    Tábla1.RowCount++;
                //    int i = Tábla1.RowCount - 1;
                //    Tábla1.Rows[i].Cells[0].Value = rekord.Adat;
                //    Tábla1.Rows[i].Cells[1].Value = rekord.Összeg;
                //}
                Tábla1.Refresh();
                Tábla1.Visible = true;
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


        private void LekérdezRészletes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálylista.SelectedItems.Count < 1) return;
                ListákFeltöltése();


                string hely = Application.StartupPath + @"\Főmérnökség\adatok\osztály.mdb";
                if (!System.IO.File.Exists(hely)) return;


                string honnan = Osztálylista.SelectedItem.ToStrTrim();
                string helyhiba = (from a in AdatokNév
                                   where a.Osztálynév == honnan
                                   select a.Osztálymező).FirstOrDefault() ?? "";
                if (helyhiba.Trim() == "") return;

                List<Adat_Osztály_Adat> Adatok = (from a in AdatokOsztály
                                                  where a.GetType().GetProperty(helyhiba).GetValue(a).ToStrTrim() != "?"
                                                  select a).ToList();

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 4;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Pályaszám";
                Tábla1.Columns[0].Width = 140;
                Tábla1.Columns[1].HeaderText = "Telephely";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = "Típus";
                Tábla1.Columns[2].Width = 240;
                Tábla1.Columns[3].HeaderText = honnan;
                Tábla1.Columns[3].Width = 400;
                foreach (Adat_Osztály_Adat rekord in Adatok)
                {
                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.Azonosító;
                    //Tábla1.Rows[i].Cells[1].Value = rekord.Telephely;
                    //Tábla1.Rows[i].Cells[2].Value = rekord.AlTípus;
                    Tábla1.Rows[i].Cells[3].Value = rekord.GetType().GetProperty(helyhiba).GetValue(rekord);
                }

                Tábla1.Refresh();
                Tábla1.Visible = true;
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



        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Osztály_" + Program.PostásNév.ToString().Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla1, true);

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

        #region Listafeltöltések
        private void ListákFeltöltése()
        {
            OsztályAdatFeltöltés();
            AdatokNév = KézNév.Lista_Adat();
        }

        private void OsztályAdatFeltöltés()
        {
            try
            {
                AdatokOsztály.Clear();
                AdatokOsztály = KézOsztály.Lista_Adat();
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



        private void AdatokJárműLista()
        {
            try
            {
                AdatokJármű.Clear();
                string hely = Application.StartupPath + @"\főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
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