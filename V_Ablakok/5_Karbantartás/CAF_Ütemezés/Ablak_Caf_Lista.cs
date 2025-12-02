using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_Caf_Lista : Form
    {
        public DateTime Végdát { get; private set; }
        public DateTime Elsődát { get; private set; }

        readonly Kezelő_CAF_alap KézAlap = new Kezelő_CAF_alap();
        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        CAF_Segéd_Adat Posta_adat;
        int Kijelölt_Sor = -1;
        string KiÍrás;


        public Ablak_Caf_Lista(DateTime végdát, DateTime elsődát)
        {
            InitializeComponent();
            Végdát = végdát;
            Elsődát = elsődát;
            Start();
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this);
            else
                Jogosultságkiosztás();
        }

        public Ablak_Caf_Lista()
        {
            InitializeComponent();
        }

        void Start()
        {
            Lista_Dátumig.Value = Végdát;
            Lista_Dátumtól.Value = Elsődát;
            Lista_Pályaszámokfeltöltése();
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Archíválás.Enabled = false;

                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Archíválás.Visible = true;
                }
                else
                {
                    Archíválás.Visible = false;

                }


                melyikelem = 119;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Archíválás.Enabled = true;
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


        private void Ablak_Caf_Lista_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_Caf_Lista_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Alapadat?.Close();
            Új_Ablak_CAF_Részletes?.Close();
        }

        private void Lista_Pályaszámokfeltöltése()
        {
            List<Adat_CAF_alap> Adatok = KézAlap.Lista_Adatok(true);
            Lista_Pályaszám.Items.Clear();

            foreach (Adat_CAF_alap item in Adatok)
                Lista_Pályaszám.Items.Add(item.Azonosító);
        }

        private void Alapadatok_listázása()
        {
            try
            {
                List<Adat_CAF_alap> Adatok = KézAlap.Lista_Adatok();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                KiÍrás = "Alap";

                Tábla_lista.Rows.Clear();
                Tábla_lista.Columns.Clear();
                Tábla_lista.Refresh();
                Tábla_lista.Visible = false;
                Tábla_lista.ColumnCount = 21;

                // fejléc elkészítése
                Tábla_lista.Columns[0].HeaderText = "Pályaszám";
                Tábla_lista.Columns[0].Width = 100;
                Tábla_lista.Columns[1].HeaderText = "Típus";
                Tábla_lista.Columns[1].Width = 100;

                Tábla_lista.Columns[2].HeaderText = "Idő Ciklus";
                Tábla_lista.Columns[2].Width = 100;
                Tábla_lista.Columns[3].HeaderText = "Idő UV";
                Tábla_lista.Columns[3].Width = 100;
                Tábla_lista.Columns[4].HeaderText = "Idő UV Ssz";
                Tábla_lista.Columns[4].Width = 100;
                Tábla_lista.Columns[5].HeaderText = "Idő telephely";
                Tábla_lista.Columns[5].Width = 100;
                Tábla_lista.Columns[6].HeaderText = "Idő Dátum";
                Tábla_lista.Columns[6].Width = 100;

                Tábla_lista.Columns[7].HeaderText = "KM Ciklus";
                Tábla_lista.Columns[7].Width = 100;
                Tábla_lista.Columns[8].HeaderText = "KM UV";
                Tábla_lista.Columns[8].Width = 100;
                Tábla_lista.Columns[9].HeaderText = "KM UV Ssz";
                Tábla_lista.Columns[9].Width = 100;
                Tábla_lista.Columns[10].HeaderText = "KM telephely";
                Tábla_lista.Columns[10].Width = 100;
                Tábla_lista.Columns[11].HeaderText = "KM Dátum";
                Tábla_lista.Columns[11].Width = 100;
                Tábla_lista.Columns[12].HeaderText = "Számláló";
                Tábla_lista.Columns[12].Width = 100;

                Tábla_lista.Columns[13].HeaderText = "Havi km";
                Tábla_lista.Columns[13].Width = 100;
                Tábla_lista.Columns[14].HeaderText = "KMU km";
                Tábla_lista.Columns[14].Width = 100;
                Tábla_lista.Columns[15].HeaderText = "Frissítés Dátum";
                Tábla_lista.Columns[15].Width = 100;
                Tábla_lista.Columns[16].HeaderText = "Felúítás Dátum";
                Tábla_lista.Columns[16].Width = 100;
                Tábla_lista.Columns[17].HeaderText = "Össz km";
                Tábla_lista.Columns[17].Width = 100;
                Tábla_lista.Columns[18].HeaderText = "Garanciális";
                Tábla_lista.Columns[18].Width = 100;
                Tábla_lista.Columns[19].HeaderText = "Aktív";
                Tábla_lista.Columns[19].Width = 100;
                Tábla_lista.Columns[20].HeaderText = "Utolsó Vizsgálat óta futott";
                Tábla_lista.Columns[20].Width = 100;

                foreach (Adat_CAF_alap rekord in Adatok)
                {
                    Tábla_lista.RowCount++;
                    int i = Tábla_lista.RowCount - 1;
                    Tábla_lista.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Adat_Jármű Elem = AdatokJármű.FirstOrDefault(a => a.Azonosító == rekord.Azonosító);
                    if (Elem != null)
                        Tábla_lista.Rows[i].Cells[1].Value = Elem.Valóstípus;

                    Tábla_lista.Rows[i].Cells[2].Value = rekord.Ciklusnap;
                    Tábla_lista.Rows[i].Cells[3].Value = rekord.Utolsó_Nap;
                    Tábla_lista.Rows[i].Cells[4].Value = rekord.Utolsó_Nap_sorszám;
                    Tábla_lista.Rows[i].Cells[5].Value = rekord.Végezte_nap.Trim();
                    Tábla_lista.Rows[i].Cells[6].Value = rekord.Vizsgdátum_nap.ToString("yyyy.MM.dd");

                    Tábla_lista.Rows[i].Cells[7].Value = rekord.Cikluskm;
                    Tábla_lista.Rows[i].Cells[8].Value = rekord.Utolsó_Km;
                    Tábla_lista.Rows[i].Cells[9].Value = rekord.Utolsó_Km_sorszám;
                    Tábla_lista.Rows[i].Cells[10].Value = rekord.Végezte_km.Trim();
                    Tábla_lista.Rows[i].Cells[11].Value = rekord.Vizsgdátum_km.ToString("yyyy.MM.dd");
                    Tábla_lista.Rows[i].Cells[12].Value = rekord.Számláló;

                    Tábla_lista.Rows[i].Cells[13].Value = rekord.Havikm;
                    Tábla_lista.Rows[i].Cells[14].Value = rekord.KMUkm;
                    Tábla_lista.Rows[i].Cells[15].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                    Tábla_lista.Rows[i].Cells[16].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                    Tábla_lista.Rows[i].Cells[17].Value = rekord.Teljeskm;
                    if (rekord.Garancia)
                        Tábla_lista.Rows[i].Cells[18].Value = "Igen";
                    else
                        Tábla_lista.Rows[i].Cells[18].Value = "Nem";

                    if (!rekord.Törölt)
                        Tábla_lista.Rows[i].Cells[19].Value = "Igen";
                    else
                        Tábla_lista.Rows[i].Cells[19].Value = "Nem";

                    Tábla_lista.Rows[i].Cells[20].Value = rekord.KMUkm - rekord.Számláló;
                }

                Tábla_lista.Visible = true;
                Tábla_lista.Refresh();
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

        private void Ütem_frissít_Click(object sender, EventArgs e)
        {
            Alapadatok_listázása();
        }

        private void Lista_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_lista.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"CAF_ütemzés_Adatok_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla_lista);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Tábla_lista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Kijelölt_Sor = e.RowIndex;
        }

        private void Lista_Pályaszám_friss_Click(object sender, EventArgs e)
        {
            Pályaszám_lista_tábla();
        }

        private void Pályaszám_lista_tábla()
        {
            try
            {
                List<Adat_CAF_Adatok> AdatokÖ = KézAdatok.Lista_Adatok(Lista_Dátumtól.Value.Year);

                List<Adat_CAF_Adatok> Adatok = (from a in AdatokÖ
                                                where a.Dátum >= Lista_Dátumtól.Value
                                                && a.Dátum <= Lista_Dátumig.Value
                                                orderby a.Dátum
                                                select a).ToList();

                if (!Radio_mind.Checked && Radio_km.Checked)
                    Adatok = Adatok.Where(a => a.IDŐvKM == 2).ToList();
                if (!Radio_mind.Checked && Radio_idő.Checked)
                    Adatok = Adatok.Where(a => a.IDŐvKM == 1).ToList();
                if (Lista_Pályaszám.Text.Trim() != "")
                    Adatok = Adatok.Where(a => a.Azonosító == Lista_Pályaszám.Text.Trim()).ToList();

                KiÍrás = "Pályaszám";
                Holtart.Be(20);

                Tábla_lista.Rows.Clear();
                Tábla_lista.Columns.Clear();
                Tábla_lista.Refresh();
                Tábla_lista.Visible = false;
                Tábla_lista.ColumnCount = 11;

                // fejléc elkészítése
                Tábla_lista.Columns[0].HeaderText = "Sorszám";
                Tábla_lista.Columns[0].Width = 100;
                Tábla_lista.Columns[1].HeaderText = "Pályaszám";
                Tábla_lista.Columns[1].Width = 100;
                Tábla_lista.Columns[2].HeaderText = "Vizsgálat";
                Tábla_lista.Columns[2].Width = 100;
                Tábla_lista.Columns[3].HeaderText = "Dátum";
                Tábla_lista.Columns[3].Width = 100;
                Tábla_lista.Columns[4].HeaderText = "Számláló állás";
                Tábla_lista.Columns[4].Width = 100;
                Tábla_lista.Columns[5].HeaderText = "Státus";
                Tábla_lista.Columns[5].Width = 120;
                Tábla_lista.Columns[6].HeaderText = "KM_Sorszám";
                Tábla_lista.Columns[6].Width = 100;
                Tábla_lista.Columns[7].HeaderText = "IDŐ_Sorszám";
                Tábla_lista.Columns[7].Width = 100;
                Tábla_lista.Columns[8].HeaderText = "Vizsgálat fajta";
                Tábla_lista.Columns[8].Width = 100;
                Tábla_lista.Columns[9].HeaderText = "Megjegyzés";
                Tábla_lista.Columns[9].Width = 200;
                Tábla_lista.Columns[10].HeaderText = "Generált dátum";
                Tábla_lista.Columns[10].Width = 200;

                foreach (Adat_CAF_Adatok rekord in Adatok)
                {

                    Tábla_lista.RowCount++;
                    int i = Tábla_lista.RowCount - 1;
                    Tábla_lista.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla_lista.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                    Tábla_lista.Rows[i].Cells[2].Value = rekord.Vizsgálat.Trim();
                    Tábla_lista.Rows[i].Cells[3].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla_lista.Rows[i].Cells[4].Value = rekord.Számláló;
                    switch (rekord.Státus)
                    {
                        case 0:
                            {
                                Tábla_lista.Rows[i].Cells[5].Value = "0- Tervezési";
                                break;
                            }
                        case 2:
                            {
                                Tábla_lista.Rows[i].Cells[5].Value = "2- Ütemezett";
                                break;
                            }
                        case 4:
                            {
                                Tábla_lista.Rows[i].Cells[5].Value = "4- Előjegyzett";
                                break;
                            }
                        case 6:
                            {
                                Tábla_lista.Rows[i].Cells[5].Value = "6- Elvégzett";
                                break;
                            }
                        case 8:
                            {
                                Tábla_lista.Rows[i].Cells[5].Value = "8- Tervezésisegéd";
                                break;
                            }
                        case 9:
                            {
                                Tábla_lista.Rows[i].Cells[5].Value = "9- Törölt";
                                break;
                            }
                    }
                    Tábla_lista.Rows[i].Cells[6].Value = rekord.KM_Sorszám;
                    Tábla_lista.Rows[i].Cells[7].Value = rekord.IDŐ_Sorszám;
                    switch (rekord.IDŐvKM)
                    {
                        case 0:
                            {
                                Tábla_lista.Rows[i].Cells[8].Value = "?";
                                break;
                            }
                        case 1:
                            {
                                Tábla_lista.Rows[i].Cells[8].Value = "Idő";
                                break;
                            }
                        case 2:
                            {
                                Tábla_lista.Rows[i].Cells[8].Value = "Km";
                                break;
                            }
                    }
                    Tábla_lista.Rows[i].Cells[9].Value = rekord.Megjegyzés.Trim();
                    Tábla_lista.Rows[i].Cells[10].Value = rekord.Dátum_program.ToString("yyyy.MM.dd");

                    Holtart.Lép();
                }

                Tábla_lista.Visible = true;
                Tábla_lista.Refresh();
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

        private void Tábla_lista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (Tábla_lista.Rows.Count < 1) return;
            // egész sor színezése ha törölt
            foreach (DataGridViewRow row in Tábla_lista.Rows)
            {
                if (MyF.Szöveg_Tisztítás(row.Cells[5].Value.ToString(), 0, 1) == "9")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                }
            }
        }


        Ablak_CAF_Részletes Új_Ablak_CAF_Részletes;
        private void Átírja_Módosításhoz_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kijelölt_Sor == -1) throw new HibásBevittAdat("Nincs kijelölve érvényes adat.");
                Új_Ablak_CAF_Részletes?.Close();
                if (KiÍrás == "Pályaszám")
                {
                    int sorszám = int.Parse(Tábla_lista.Rows[Kijelölt_Sor].Cells[0].Value.ToString());
                    string pályaszám = Tábla_lista.Rows[Kijelölt_Sor].Cells[1].Value.ToString();
                    DateTime dátum = DateTime.Parse(Tábla_lista.Rows[Kijelölt_Sor].Cells[3].Value.ToString());
                    Posta_adat = new CAF_Segéd_Adat(pályaszám, dátum, sorszám);

                    Új_Ablak_CAF_Részletes = new Ablak_CAF_Részletes(Posta_adat, Lista_Dátumig.Value);
                    Új_Ablak_CAF_Részletes.FormClosed += Ablak_CAF_Részletes_Closed;
                    Új_Ablak_CAF_Részletes.StartPosition = FormStartPosition.CenterScreen;
                    Új_Ablak_CAF_Részletes.Show();
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

        private void Ablak_CAF_Részletes_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Részletes = null;
        }


        Ablak_CAF_Alapadat Új_Ablak_CAF_Alapadat;
        private void Alap_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                string azonosító = "";
                if (Kijelölt_Sor != -1)
                {
                    if (KiÍrás == "Alap")
                        azonosító = Tábla_lista.Rows[Kijelölt_Sor].Cells[0].Value.ToString().Trim();
                    else
                        azonosító = Tábla_lista.Rows[Kijelölt_Sor].Cells[1].Value.ToString().Trim();
                }
                Új_Ablak_CAF_Alapadat?.Close();

                Új_Ablak_CAF_Alapadat = new Ablak_CAF_Alapadat(azonosító);
                Új_Ablak_CAF_Alapadat.FormClosed += Ablak_CAF_Alapadat_Closed;
                Új_Ablak_CAF_Alapadat.StartPosition = FormStartPosition.CenterScreen;
                Új_Ablak_CAF_Alapadat.Show();
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

        private void Ablak_CAF_Alapadat_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_CAF_Alapadat = null;
        }

        private void Archíválás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista_Dátumtól.Value.Year >= DateTime.Today.Year - 1) throw new HibásBevittAdat("A kívánt időszakot még nem lehet archíválni.");
                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Év_elsőnapja(Lista_Dátumtól.Value)
                          && a.Dátum <= MyF.Év_utolsónapja(Lista_Dátumtól.Value)
                          select a).ToList();
                KézAdatok.Archíválás(Lista_Dátumtól.Value, Adatok);
                MessageBox.Show("Az adatok Archíválása elkészült. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
