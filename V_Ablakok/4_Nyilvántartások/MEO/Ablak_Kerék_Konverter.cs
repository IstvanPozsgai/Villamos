using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Ablakok.MEO
{
    public partial class Ablak_Kerék_Konverter : Form
    {
        string könyvtár = null;
        List<Adat_KerékMérő> Adatok = new List<Adat_KerékMérő>();
        List<Adat_KerékmérőTengely> Nevek = new List<Adat_KerékmérőTengely>();
        string pályaszám;
        string típus;
        long km;

        public Ablak_Kerék_Konverter()
        {
            InitializeComponent();
        }

        private void Ablak_Kerék_Konverter_Load(object sender, EventArgs e)
        {

        }

        private void Könyvtár_Click(object sender, EventArgs e)
        {
            try
            {
                FileList.Items.Clear();
                FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
                if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    DirectoryInfo di = new DirectoryInfo(FolderBrowserDialog1.SelectedPath);
                    FileInfo[] aryFi = di.GetFiles("*.csv");
                    könyvtár = FolderBrowserDialog1.SelectedPath;
                    foreach (FileInfo fi in aryFi)
                        FileList.Items.Add(fi.Name);
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Végrehajt_Click(object sender, EventArgs e)
        {
            try
            {
                bool caf = false;
                Adatok = new List<Adat_KerékMérő>();
                if (könyvtár == null || könyvtár.Trim() == "") return;

                Holtart.Be(20);
                foreach (string elem in FileList.Items)
                {
                    string hely = könyvtár + @"\" + elem;
                    List<string> Lista = Beolvas_CSV(hely);
                    // Szétválasztjuk típusra
                    string[] darab = Lista[0].Split(';');
                    if (darab[1].Contains("CAF"))
                    {
                        caf = true;
                        TengelyAzonosítókCAF(Lista);
                        MértÉrtékekCAF(Lista);
                    }
                    else
                    {
                        TengelyAzonosítók(Lista);
                        MértÉrtékek(Lista);
                    }

                }
                if (caf)
                    Excel_KimenetCAF();
                else
                    Excel_Kimenet();

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

        private void Excel_KimenetCAF()
        {
            try
            {
                if (Adatok == null) return;
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Kerékmérési eredmények konvertálása CAF",
                    FileName = $"CAF_Kerékmérés{Program.PostásNév.ToStrTrim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.ExcelLétrehozás("Részletes");

                int sor = 1;
                int i = 1;
                //fejléc
                MyX.Kiir("Pályaszám", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Km", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Tengely", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("AGY_J", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("AGY_B", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Dátum", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Idő", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("h     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTJ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_J   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_J  ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("hb    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_B ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("nb    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2b   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTB", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_B   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ab    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_B  ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("HATL_T", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt2   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("t     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a+b_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a+b_B ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1BKV", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt2BKV", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_K ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Rf_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Rf_B ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Hibák ", MyF.Oszlopnév(i++) + sor);
                sor++;
                foreach (Adat_KerékMérő rekord in Adatok)
                {
                    i = 1;
                    MyX.Kiir(rekord.Pályaszám.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Km.ToString(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Tengely.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.AGY_J.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.AGY_B.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("yyyy.MM.dd"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("HH:mm:ss"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_h.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_a.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_hb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_nb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2b.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ab.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_HATL_T.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_t.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_apb_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_apb_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt2BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_K.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Rf_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Rf_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Hiba.Trim(), MyF.Oszlopnév(i++) + sor);
                    sor++;
                    Holtart.Lép();
                }
                // Másik lap elkészítése

                MyX.Munkalap_Új("Szűkített");
                //Fejléc

                sor = 1;
                i = 1;
                MyX.Kiir("Pályaszám", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Km", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Tengely", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("AGY_J", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("AGY_B", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Dátum", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Idő", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTJ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_J  ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_B ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTB", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_B  ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_J-ATM_B", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("HATL_T", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1BKV", MyF.Oszlopnév(i++) + sor);

                MyX.Kiir("Vt2   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt2BKV", MyF.Oszlopnév(i++) + sor);

                MyX.Kiir("t     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_J   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_B   ", MyF.Oszlopnév(i++) + sor);

                MyX.Kiir("n     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("nb    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2b   ", MyF.Oszlopnév(i++) + sor);

                MyX.Kiir("h     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("hb    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ab    ", MyF.Oszlopnév(i++) + sor);

                MyX.Kiir("Rf_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Rf_B ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Hibák ", MyF.Oszlopnév(i++) + sor);
                sor++;
                foreach (Adat_KerékMérő rekord in Adatok)
                {
                    i = 1;
                    MyX.Kiir(rekord.Pályaszám.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Km.ToString(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Tengely.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.AGY_J.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.AGY_B.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("yyyy.MM.dd"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("HH:mm:ss"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir((rekord.A_ATM_J - rekord.A_ATM_B).ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_HATL_T.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);

                    MyX.Kiir(rekord.A_Vt2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt2BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);

                    MyX.Kiir(rekord.A_t.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);

                    MyX.Kiir(rekord.A_n.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_nb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2b.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);

                    MyX.Kiir(rekord.A_h.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_hb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_a.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ab.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);


                    MyX.Kiir(rekord.A_Rf_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Rf_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Hiba.Trim(), MyF.Oszlopnév(i++) + sor);
                    sor++;
                    Holtart.Lép();
                }
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                MyF.Megnyitás(fájlexc);
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

        private void Excel_Kimenet()
        {
            try
            {
                if (Adatok == null) return;
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Kerékmérési eredmények konvertálása",
                    FileName = $"Kerékmérés{Program.PostásNév.ToStrTrim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.ExcelLétrehozás("Részletes");


                int sor = 1;
                int i = 1;
                //fejléc
                MyX.Kiir("Pályaszám", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Tengely", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Dátum", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Idő", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("h     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTJ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_J   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVJ ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_J  ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("hb    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_B ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("nb    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2b   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTB", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_B   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ab    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVB ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_B  ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("HATL_T", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt2   ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("t     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a+b_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a+b_B ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1BKV", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt2BKV", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_K ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Rf_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Rf_J ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Hibák ", MyF.Oszlopnév(i++) + sor);
                sor++;
                foreach (Adat_KerékMérő rekord in Adatok)
                {
                    i = 1;
                    MyX.Kiir(rekord.Pályaszám.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Tengely.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("yyyy.MM.dd"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("HH:mm:ss"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_h.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_a.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_hb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_nb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2b.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ab.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_HATL_T.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_t.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_apb_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_apb_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt2BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_K.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Rf_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Rf_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Hiba.Trim(), MyF.Oszlopnév(i++) + sor);
                    sor++;
                    Holtart.Lép();
                }
                // Másik lap elkészítése
                MyX.Munkalap_Új("Szűkített");
                //Fejléc

                sor = 1;
                i = 1;
                MyX.Kiir("Pályaszám  ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Tengely    ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Dátum      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Idő        ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPJ      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPJ      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_J      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMJ      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTJ     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVJ      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_J       ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAJ      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BKOPB      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KKOPB      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_B      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKMB      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("KIFUTB     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("NYKVB      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("QR_B       ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("BETAB      ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("HATL_T     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1        ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt1BKV     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt2        ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("Vt2BKV     ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("t          ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_J        ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("V_B        ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n          ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("nb         ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2         ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("n2b        ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("h          ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("hb         ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("a          ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ab         ", MyF.Oszlopnév(i++) + sor);
                MyX.Kiir("ATM_J-ATM_B", MyF.Oszlopnév(i++) + sor);
                sor++;
                foreach (Adat_KerékMérő rekord in Adatok)
                {
                    i = 1;
                    MyX.Kiir(rekord.Pályaszám.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.Tengely.Trim(), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("yyyy.MM.dd"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.DátumIdő.ToString("HH:mm:ss"), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAJ.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KKOPB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ATM_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKMB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_KIFUTB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_NYKVB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_QR_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_BETAB.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_HATL_T.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt1BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_Vt2BKV.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_t.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_J.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_V_B.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_nb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_n2b.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_h.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_hb.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_a.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir(rekord.A_ab.ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    MyX.Kiir((rekord.A_ATM_J - rekord.A_ATM_B).ToString().Replace(',', '.'), MyF.Oszlopnév(i++) + sor);
                    sor++;
                    Holtart.Lép();
                }
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                MyF.Megnyitás(fájlexc);
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

        private void MértÉrtékek(List<string> Lista)
        {
            try
            {
                Adat_KerékMérő Elem = new Adat_KerékMérő("", "", new DateTime(1900, 1, 1, 0, 0, 0), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                string előző = "";
                string előzőDátum = "";
                string előzőIdő = "";
                string hiba = "";
                foreach (string Sor in Lista)
                {
                    Holtart.Lép();
                    string[] darab = Sor.Split(';');
                    if (darab[0].Trim().Contains("Tengely") && darab.Length > 2)
                    {
                        if (előző.Trim() == "") előző = darab[0].Trim();
                        if (előző.Trim() != darab[0].Trim() && előző.Contains("Tengely"))
                        {
                            var szűr = from név in Nevek where név.Név.Trim() == előző.Trim() select név.SAP;
                            foreach (var Tengely in szűr)
                            {
                                Elem.Tengely = Tengely;
                            }
                            Elem.Pályaszám = pályaszám;
                            Elem.DátumIdő = DatumKonvert(előzőDátum, előzőIdő);
                            Elem.Hiba = hiba;
                            Adatok.Add(Elem);
                            Elem = new Adat_KerékMérő("", "", new DateTime(1900, 1, 1, 0, 0, 0), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");
                            előző = darab[0].Trim();
                            hiba = "";
                        }

                        if (darab.Length > 2 && darab[0].Contains("Tengely"))
                        {
                            if (!double.TryParse(darab[3], out double érték))
                            {
                                érték = 0;
                                if (hiba.Trim() == "") hiba = "Hibás adat(ok):";
                                hiba += darab[2].Trim() + ",";
                            }
                            előzőDátum = darab[7];
                            előzőIdő = darab[8];
                            switch (darab[2].Trim())
                            {
                                case "BKOPJ": Elem.A_BKOPJ = érték; break;
                                case "KKOPJ": Elem.A_KKOPJ = érték; break;
                                case "h": Elem.A_h = érték; break;
                                case "ATM_J": Elem.A_ATM_J = érték; break;
                                case "BETAJ": Elem.A_BETAJ = érték; break;
                                case "NYKMJ": Elem.A_NYKMJ = érték; break;
                                case "n": Elem.A_n = érték; break;
                                case "n2": Elem.A_n2 = érték; break;
                                case "KIFUTJ": Elem.A_KIFUTJ = érték; break;
                                case "V_J": Elem.A_V_J = érték; break;
                                case "a": Elem.A_a = érték; break;
                                case "NYKVJ": Elem.A_NYKVJ = érték; break;
                                case "QR_J": Elem.A_QR_J = érték; break;
                                case "BKOPB": Elem.A_BKOPB = érték; break;
                                case "KKOPB": Elem.A_KKOPB = érték; break;
                                case "hb": Elem.A_hb = érték; break;
                                case "ATM_B": Elem.A_ATM_B = érték; break;
                                case "BETAB": Elem.A_BETAB = érték; break;
                                case "NYKMB": Elem.A_NYKMB = érték; break;
                                case "nb": Elem.A_nb = érték; break;
                                case "n2b": Elem.A_n2b = érték; break;
                                case "KIFUTB": Elem.A_KIFUTB = érték; break;
                                case "V_B": Elem.A_V_B = érték; break;
                                case "ab": Elem.A_ab = érték; break;
                                case "NYKVB": Elem.A_NYKVB = érték; break;
                                case "QR_B": Elem.A_QR_B = érték; break;
                                case "HATL_T": Elem.A_HATL_T = érték; break;
                                case "Vt1": Elem.A_Vt1 = érték; break;
                                case "Vt2": Elem.A_Vt2 = érték; break;
                                case "t": Elem.A_t = érték; break;
                                case "a+b_J": Elem.A_apb_J = érték; break;
                                case "a+b_B": Elem.A_apb_B = érték; break;
                                case "Vt1BKV": Elem.A_Vt1BKV = érték; break;
                                case "Vt2BKV": Elem.A_Vt2BKV = érték; break;
                                case "ATM_K": Elem.A_ATM_K = érték; break;
                                case "Rf_J": Elem.A_Rf_J = érték; break;
                                case "Rf_B": Elem.A_Rf_B = érték; break;
                            }
                        }
                    }
                }
                var szűrt = from név in Nevek where név.Név.Trim() == előző.Trim() select név.SAP;
                foreach (var Tengely in szűrt)
                {
                    Elem.Tengely = Tengely;
                }
                Elem.Pályaszám = pályaszám;
                Elem.Hiba = hiba;
                Elem.DátumIdő = DatumKonvert(előzőDátum, előzőIdő);

                Adatok.Add(Elem);
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

        private void MértÉrtékekCAF(List<string> Lista)
        {
            try
            {
                Adat_KerékMérő Elem = new Adat_KerékMérő("", "", new DateTime(1900, 1, 1, 0, 0, 0), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "");

                string előző = "";
                string előzőDátum = "";
                string előzőIdő = "";
                string hiba = "";
                string tengelynév = "";
                string jobb = "Jobb";
                string bal = "Bal";

                foreach (string Sor in Lista)
                {
                    Holtart.Lép();
                    string[] darab = Sor.Split(';');
                    if (darab[0].Trim().Contains("Tengely") && darab.Length > 2)
                    {
                        if (előző.Trim() == "")
                        {
                            előző = darab[0].Trim();
                            string[] Bdarab = darab[0].Split('_');
                            if (Bdarab.Length >= 2) tengelynév = Bdarab[1].Trim();
                        }
                        if (előző.Trim() != darab[0].Trim() && előző.Contains("Tengely"))
                        {
                            Elem.Tengely = tengelynév;
                            Elem.Pályaszám = pályaszám;
                            Elem.AGY_J = jobb;
                            Elem.AGY_B = bal;
                            Elem.DátumIdő = DatumKonvert(előzőDátum, előzőIdő);
                            Elem.Hiba = hiba;
                            Elem.Km = km;
                            Adatok.Add(Elem);
                            Elem = new Adat_KerékMérő("", "", new DateTime(1900, 1, 1, 0, 0, 0), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "");
                            előző = darab[0].Trim();
                            string[] Bdarab = darab[0].Split('_');
                            if (Bdarab.Length >= 2) tengelynév = Bdarab[1].Trim();
                            hiba = "";
                        }

                        if (darab.Length > 2 && darab[1].Contains("Profil"))
                        {
                            string[] ProfilDarab = darab[1].Split('_');
                            if (ProfilDarab.Length == 2 && ProfilDarab[1].Contains("R"))
                            {
                                Adat_KerékmérőTengely ElemTengely = Nevek.Where(a => a.Név.Contains(ProfilDarab[1])).FirstOrDefault();
                                if (ElemTengely != null)
                                {
                                    jobb = ElemTengely.SAP;
                                    km = ElemTengely.Km;
                                }

                            }
                            if (ProfilDarab.Length == 2 && ProfilDarab[1].Contains("L"))
                            {
                                Adat_KerékmérőTengely ElemTengely = Nevek.Where(a => a.Név.Contains(ProfilDarab[1])).FirstOrDefault();
                                if (ElemTengely != null)
                                {
                                    bal = ElemTengely.SAP;
                                    km = ElemTengely.Km;
                                }
                            }
                        }

                        if (darab.Length > 2 && darab[0].Contains("Tengely"))
                        {

                            if (!double.TryParse(darab[3], out double érték))
                            {
                                érték = 0;
                                if (hiba.Trim() == "") hiba = "Hibás adat(ok):";
                                hiba += darab[2].Trim() + ",";
                            }
                            előzőDátum = darab[7];
                            előzőIdő = darab[8];
                            switch (darab[2].Trim())
                            {
                                case "BKOPJ": Elem.A_BKOPJ = érték; break;
                                case "KKOPJ": Elem.A_KKOPJ = érték; break;
                                case "h": Elem.A_h = érték; break;
                                case "ATM_J": Elem.A_ATM_J = érték; break;
                                case "BETAJ": Elem.A_BETAJ = érték; break;
                                case "NYKMJ": Elem.A_NYKMJ = érték; break;
                                case "n": Elem.A_n = érték; break;
                                case "n2": Elem.A_n2 = érték; break;
                                case "KIFUTJ": Elem.A_KIFUTJ = érték; break;
                                case "V_J": Elem.A_V_J = érték; break;
                                case "a": Elem.A_a = érték; break;
                                case "NYKVJ": Elem.A_NYKVJ = érték; break;
                                case "QR_J": Elem.A_QR_J = érték; break;
                                case "BKOPB": Elem.A_BKOPB = érték; break;
                                case "KKOPB": Elem.A_KKOPB = érték; break;
                                case "hb": Elem.A_hb = érték; break;
                                case "ATM_B": Elem.A_ATM_B = érték; break;
                                case "BETAB": Elem.A_BETAB = érték; break;
                                case "NYKMB": Elem.A_NYKMB = érték; break;
                                case "nb": Elem.A_nb = érték; break;
                                case "n2b": Elem.A_n2b = érték; break;
                                case "KIFUTB": Elem.A_KIFUTB = érték; break;
                                case "V_B": Elem.A_V_B = érték; break;
                                case "ab": Elem.A_ab = érték; break;
                                case "NYKVB": Elem.A_NYKVB = érték; break;
                                case "QR_B": Elem.A_QR_B = érték; break;
                                case "HATL_T": Elem.A_HATL_T = érték; break;
                                case "Vt1": Elem.A_Vt1 = érték; break;
                                case "Vt2": Elem.A_Vt2 = érték; break;
                                case "t": Elem.A_t = érték; break;
                                case "a+b_J": Elem.A_apb_J = érték; break;
                                case "a+b_B": Elem.A_apb_B = érték; break;
                                case "Vt1BKV": Elem.A_Vt1BKV = érték; break;
                                case "Vt2BKV": Elem.A_Vt2BKV = érték; break;
                                case "ATM_K": Elem.A_ATM_K = érték; break;
                                case "Rf_J": Elem.A_Rf_J = érték; break;
                                case "Rf_B": Elem.A_Rf_B = érték; break;

                            }
                        }
                    }
                }

                Elem.Tengely = tengelynév;
                Elem.Pályaszám = pályaszám;
                Elem.AGY_J = jobb;
                Elem.AGY_B = bal;
                Elem.Hiba = hiba;
                Elem.DátumIdő = DatumKonvert(előzőDátum, előzőIdő);
                Elem.Km = km;
                Adatok.Add(Elem);
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

        private DateTime DatumKonvert(string előzőDátum, string előzőIdő)
        {
            int év = int.TryParse(előzőDátum.Substring(0, 4), out év) ? év : 1900;
            int hó = int.TryParse(előzőDátum.Substring(4, 2), out hó) ? hó : 1;
            int nap = int.TryParse(előzőDátum.Substring(6, 2), out nap) ? nap : 1;
            int óra = int.TryParse(előzőIdő.Substring(0, 2), out óra) ? óra : 0;
            int perc = int.TryParse(előzőIdő.Substring(2, 2), out perc) ? perc : 0;
            int másodperc = int.TryParse(előzőIdő.Substring(4, 2), out másodperc) ? másodperc : 0;
            return new DateTime(év, hó, nap, óra, perc, másodperc);
        }

        private void TengelyAzonosítók(List<string> Adatok)
        {
            try
            {
                Nevek = new List<Adat_KerékmérőTengely>();
                Adat_KerékmérőTengely Elem;
                string[] darab = Adatok[0].Split(';');
                típus = darab[1].Trim();
                darab = Adatok[1].Split(';');
                pályaszám = darab[1].Trim();


                for (int i = 2; i < Adatok.Count; i++)
                {
                    darab = Adatok[i].Split(';');
                    //   if (darab[0].Trim() == "Mero"|| darab[0].Trim() == "MeasObject.Name") return;
                    if (darab[0].Trim() == "MeasObject.Name") return;
                    if (darab[0].Trim() == "psz") pályaszám = darab[1].Trim();
                    if (darab[0].Trim() == "MeasPlan.Name") típus = darab[1].Trim();
                    Elem = new Adat_KerékmérőTengely(darab[0].Trim(), darab[1].Trim());
                    Nevek.Add(Elem);
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

        private void TengelyAzonosítókCAF(List<string> Adatok)
        {
            try
            {
                Nevek = new List<Adat_KerékmérőTengely>();
                Adat_KerékmérőTengely Elem;
                string[] darab = Adatok[0].Split(';');
                típus = darab[1].Trim();
                darab = Adatok[1].Split(';');
                pályaszám = darab[1].Trim();
                km = 0;


                for (int i = 2; i < Adatok.Count; i++)
                {
                    darab = Adatok[i].Split(';');
                    //   if (darab[0].Trim() == "Mero"|| darab[0].Trim() == "MeasObject.Name") return;
                    if (darab[0].Trim() == "MeasObject.Name") return;
                    if (darab[0].Trim() == "psz") pályaszám = darab[1].Trim();
                    if (darab[0].Trim() == "MeasPlan.Name") típus = darab[1].Trim();
                    if (darab[0].Trim() == "km") km = darab[1].ToÉrt_Long();
                    Elem = new Adat_KerékmérőTengely(darab[0].Trim(), darab[1].Trim(), km);
                    Nevek.Add(Elem);
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

        List<string> Beolvas_CSV(string hely)
        {
            List<string> válasz = new List<string>();
            using (StreamReader sr = new StreamReader(hely))
            {
                while (!sr.EndOfStream)
                {
                    string EgySor = sr.ReadLine();
                    válasz.Add(EgySor);
                    Holtart.Lép();
                }
            }
            return válasz;
        }
    }
}
