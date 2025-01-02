using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    public partial class Ablak_Eszterga_Beosztás : Form
    {
        public DateTime Dátum { get; private set; }
        public string Gyökér { get; private set; }

        public Ablak_Eszterga_Beosztás(DateTime dátum, string gyökér)
        {
            InitializeComponent();
            Dátum = dátum;
            Gyökér = gyökér;
        }

        private void Ablak_Eszterga_Beosztás_Load(object sender, EventArgs e)
        {
            Beosztás();
        }

        void Beosztás()
        {
            try
            {

                DateTime Hételső = MyF.Hét_elsőnapja(Dátum);

                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum);

                Terv_Tábla.Rows.Clear();
                Terv_Tábla.Columns.Clear();
                Terv_Tábla.Refresh();
                Terv_Tábla.Visible = false;
                Terv_Tábla.ColumnCount = 9;
                Terv_Tábla.RowCount = 0;

                Terv_Tábla.Columns[0].HeaderText = "Név";
                Terv_Tábla.Columns[0].Width = 200;
                Terv_Tábla.Columns[1].HeaderText = "Azonosító";
                Terv_Tábla.Columns[1].Width = 100;
                Terv_Tábla.Columns[2].HeaderText = "H";
                Terv_Tábla.Columns[2].Width = 50;
                Terv_Tábla.Columns[3].HeaderText = "K";
                Terv_Tábla.Columns[3].Width = 50;
                Terv_Tábla.Columns[4].HeaderText = "Sze";
                Terv_Tábla.Columns[4].Width = 50;
                Terv_Tábla.Columns[5].HeaderText = "Cs";
                Terv_Tábla.Columns[5].Width = 50;
                Terv_Tábla.Columns[6].HeaderText = "P";
                Terv_Tábla.Columns[6].Width = 50;
                Terv_Tábla.Columns[7].HeaderText = "Szo";
                Terv_Tábla.Columns[7].Width = 50;
                Terv_Tábla.Columns[8].HeaderText = "V";
                Terv_Tábla.Columns[8].Width = 50;


                //Dolgozói beosztás kiírása

                Terv_Tábla.RowCount++;
                int sor = 0;
                for (int i = 0; i < 7; i++)
                {
                    Terv_Tábla.Rows[sor].Cells[i + 2].Value = Hételső.AddDays(i).ToString("MM.dd");
                }


                string előzőDolg = "";
                Kerékeszterga_Excel KerExc = new Kerékeszterga_Excel("", Application.StartupPath, Dátum);
                List<Adat_Dolgozó_Beosztás_Új> Adatok = KerExc.Adat_BEO_Csoport(Dátum);
                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok)
                {
                    if (előzőDolg.Trim() != rekord.Dolgozószám.Trim())
                    {
                        Terv_Tábla.RowCount++;
                        sor++;
                        előzőDolg = rekord.Dolgozószám.Trim();
                    }
                    TimeSpan ideig1 = rekord.Nap - Hételső;
                    int oszlopa = ideig1.Days + 2;
                    Terv_Tábla.Rows[sor].Cells[0].Value = KerExc.Dolgozó_név(rekord.Dolgozószám).ToString();
                    Terv_Tábla.Rows[sor].Cells[1].Value = rekord.Dolgozószám.Trim();
                    Terv_Tábla.Rows[sor].Cells[oszlopa].Value = rekord.Beosztáskód.Trim();
                }

                // hétvége és ünnepnap színezés
                string hely = Gyökér + @"\Főmérnökség\adatok\" + Dátum.Year.ToString() + @"\munkaidőnaptár.mdb";
                string jelszó = "katalin";
                string szöveg = $"SELECT * FROM naptár WHERE dátum>=#{Hételső:MM-dd-yyyy}# AND dátum<=#{Hétutolsó:MM-dd-yyyy}# ORDER BY Dátum";
                Kezelő_Váltós_Naptár KéZNaptár = new Kezelő_Váltós_Naptár();
                List<Adat_Váltós_Naptár> AdatNaptár = KéZNaptár.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Váltós_Naptár Elem in AdatNaptár)
                {
                    TimeSpan ideig1 = Elem.Dátum - Hételső;
                    int oszlopa = ideig1.Days + 2;
                    switch (Elem.Nap.Trim())
                    {
                        case "P":
                            for (int i = 0; i < Terv_Tábla.RowCount; i++)
                            {
                                Terv_Tábla.Rows[i].Cells[oszlopa].Style.BackColor = System.Drawing.Color.Green;
                            }

                            break;
                        case "V":
                            for (int i = 0; i < Terv_Tábla.RowCount; i++)
                            {
                                Terv_Tábla.Rows[i].Cells[oszlopa].Style.BackColor = System.Drawing.Color.Red;
                            }
                            break;
                        case "Ü":
                            for (int i = 0; i < Terv_Tábla.RowCount; i++)
                            {
                                Terv_Tábla.Rows[i].Cells[oszlopa].Style.BackColor = System.Drawing.Color.Red;
                            }
                            break;
                    }
                }


                Terv_Tábla.Refresh();
                Terv_Tábla.Visible = true;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Beosztás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Ablak_Eszterga_Beosztás_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }

            //Ctrl gomb nyomása
            if ((int)e.KeyCode == 17)



                //Ctrl+F
                if (e.Control && e.KeyCode == Keys.F)
                {

                }
        }
    }
}
