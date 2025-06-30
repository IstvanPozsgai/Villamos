using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_Caf_km_mod : Form
    {

        readonly Kezelő_CAF_alap KézAlap = new Kezelő_CAF_alap();
        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        CAF_Segéd_Adat Posta_adat;
        int Kijelölt_Sor = -1;
        string KiÍrás;


        public Ablak_Caf_km_mod()
        {
            InitializeComponent();
            Start();
            Jogosultságkiosztás();
        }

        void Start()
        {
            
        }

        //Van szükség erre a metódusra?
        // JAVÍTANDÓ: most igen itt kellene szabályozni, hogy ki módosíthatja az adatokat és ki nem.
        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false


                melyikelem = 119;
                // módosítás 1 
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

        private void Ablak_Caf_km_mod_Load(object sender, EventArgs e)
        {

        }

        // JAVÍTANDÓ:nem kell azt szeretnénk látni a táblázatban, hogy melyikek azok a km adatok amiket módosítani kellene:
        // KÉSZ✔       

        private void Alapadatok_listázása()
        {
            try
            {
                KiÍrás = "Alap";

                List<Adat_CAF_Adatok> CAF_Adatok_Tabla = KézAdatok.Lista_Adatok();

                Tábla_lista.Rows.Clear();
                Tábla_lista.Columns.Clear();
                Tábla_lista.Refresh();
                Tábla_lista.Visible = false;
                Tábla_lista.ColumnCount = 21;

                // fejléc elkészítése
                Tábla_lista.Columns[0].HeaderText = "Pályaszám";
                Tábla_lista.Columns[0].Width = 100;
                Tábla_lista.Columns[1].HeaderText = "Vizsgálat";
                Tábla_lista.Columns[1].Width = 100;

                Tábla_lista.Columns[2].HeaderText = "Dátum";
                Tábla_lista.Columns[2].Width = 100;
                Tábla_lista.Columns[3].HeaderText = "Dátum_prog";
                Tábla_lista.Columns[3].Width = 100;
                Tábla_lista.Columns[4].HeaderText = "Számláló";
                Tábla_lista.Columns[4].Width = 100;
                Tábla_lista.Columns[5].HeaderText = "Státusz";
                Tábla_lista.Columns[5].Width = 100;
                Tábla_lista.Columns[6].HeaderText = "KM_Sor";
                Tábla_lista.Columns[6].Width = 100;

                Tábla_lista.Columns[7].HeaderText = "Idő_Sor";
                Tábla_lista.Columns[7].Width = 100;

                Tábla_lista.Columns[8].HeaderText = "Idő v KM";
                Tábla_lista.Columns[8].Width = 100;

                foreach (Adat_CAF_Adatok rekord in CAF_Adatok_Tabla)
                {
                    if (rekord.KmRogzitett_e)
                    {
                        Tábla_lista.RowCount++;
                        int i = Tábla_lista.RowCount - 1;
                        Tábla_lista.Rows[i].Cells[0].Value = rekord.Azonosító;
                        Tábla_lista.Rows[i].Cells[1].Value = rekord.Vizsgálat;
                        Tábla_lista.Rows[i].Cells[2].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Tábla_lista.Rows[i].Cells[3].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Tábla_lista.Rows[i].Cells[4].Value = rekord.Dátum_program.ToString("yyyy.MM.dd");
                        Tábla_lista.Rows[i].Cells[5].Value = rekord.Státus;
                        Tábla_lista.Rows[i].Cells[6].Value = rekord.KM_Sorszám;
                        Tábla_lista.Rows[i].Cells[7].Value = rekord.IDŐ_Sorszám;
                        Tábla_lista.Rows[i].Cells[8].Value = rekord.IDŐvKM;
                    }
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

        private void Tábla_lista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Kijelölt_Sor = e.RowIndex;
            //Lekéri a táblázatban lévő villamosokat, majd a kattintott sor index-e alapján lekéri a villamos ID-ját.
            double CAF_ID = KézAdatok.Lista_Adatok()[e.RowIndex].Id;
            uj_ablak_CAF_Km_Mod_Seged = new Ablak_CAF_km_mod_seged(CAF_ID);
            uj_ablak_CAF_Km_Mod_Seged.StartPosition = FormStartPosition.CenterScreen;
            uj_ablak_CAF_Km_Mod_Seged.Show();

        }

        private void Lista_Pályaszám_friss_Click(object sender, EventArgs e)
        {
            Pályaszám_lista_tábla();
        }

        private void Pályaszám_lista_tábla()
        {
            try
            {

                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok().Where(a => a.KmRogzitett_e == true).ToList();

                KiÍrás = "Pályaszám";
                Holtart.Be(20);

                Tábla_lista.Rows.Clear();
                Tábla_lista.Columns.Clear();
                Tábla_lista.Refresh();
                Tábla_lista.Visible = false;
                Tábla_lista.ColumnCount = 10;

                // fejléc elkészítése
                Tábla_lista.Columns[0].HeaderText = "Pályaszám";
                Tábla_lista.Columns[0].Width = 100;
                Tábla_lista.Columns[1].HeaderText = "Vizsgálat";
                Tábla_lista.Columns[1].Width = 100;
                Tábla_lista.Columns[2].HeaderText = "Dátum";
                Tábla_lista.Columns[2].Width = 100;
                Tábla_lista.Columns[3].HeaderText = "Számláló állás";
                Tábla_lista.Columns[3].Width = 100;
                Tábla_lista.Columns[4].HeaderText = "Státus";
                Tábla_lista.Columns[4].Width = 120;
                Tábla_lista.Columns[5].HeaderText = "KM_Sorszám";
                Tábla_lista.Columns[5].Width = 100;
                Tábla_lista.Columns[6].HeaderText = "IDŐ_Sorszám";
                Tábla_lista.Columns[6].Width = 100;
                Tábla_lista.Columns[7].HeaderText = "Vizsgálat fajta";
                Tábla_lista.Columns[7].Width = 100;
                Tábla_lista.Columns[8].HeaderText = "Tervezési KM állás";
                Tábla_lista.Columns[8].Width = 200;
                // JAVÍTANDÓ:
                Tábla_lista.Columns[9].HeaderText = "Valós KM állás";
                Tábla_lista.Columns[9].Width = 200;

                foreach (Adat_CAF_Adatok rekord in Adatok)
                {
                    if (rekord.KmRogzitett_e)
                    {
                        Tábla_lista.RowCount++;
                        int i = Tábla_lista.RowCount - 1;
                        Tábla_lista.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                        Tábla_lista.Rows[i].Cells[1].Value = rekord.Vizsgálat.Trim();
                        Tábla_lista.Rows[i].Cells[2].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Tábla_lista.Rows[i].Cells[3].Value = rekord.Számláló;
                        switch (rekord.Státus)
                        {
                            case 0:
                                {
                                    Tábla_lista.Rows[i].Cells[4].Value = "0- Tervezési";
                                    break;
                                }
                            case 2:
                                {
                                    Tábla_lista.Rows[i].Cells[4].Value = "2- Ütemezett";
                                    break;
                                }
                            case 4:
                                {
                                    Tábla_lista.Rows[i].Cells[4].Value = "4- Előjegyzett";
                                    break;
                                }
                            case 6:
                                {
                                    Tábla_lista.Rows[i].Cells[4].Value = "6- Elvégzett";
                                    break;
                                }
                            case 8:
                                {
                                    Tábla_lista.Rows[i].Cells[4].Value = "8- Tervezésisegéd";
                                    break;
                                }
                            case 9:
                                {
                                    Tábla_lista.Rows[i].Cells[4].Value = "9- Törölt";
                                    break;
                                }
                        }
                        Tábla_lista.Rows[i].Cells[5].Value = rekord.KM_Sorszám;
                        Tábla_lista.Rows[i].Cells[6].Value = rekord.IDŐ_Sorszám;
                        switch (rekord.IDŐvKM)
                        {
                            case 0:
                                {
                                    Tábla_lista.Rows[i].Cells[7].Value = "?";
                                    break;
                                }
                            case 1:
                                {
                                    Tábla_lista.Rows[i].Cells[7].Value = "Idő";
                                    break;
                                }
                            case 2:
                                {
                                    Tábla_lista.Rows[i].Cells[7].Value = "Km";
                                    break;
                                }
                        }
                        Tábla_lista.Rows[i].Cells[8].Value = rekord.Számláló;
                        Tábla_lista.Rows[i].Cells[9].Value = "0";

                        Holtart.Lép();
                    }
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

        Ablak_CAF_km_mod_seged uj_ablak_CAF_Km_Mod_Seged;
        // JAVÍTANDÓ:Nem bonyolítanám tovább a rögzítést
        // KÉSZ✔
    }
}
